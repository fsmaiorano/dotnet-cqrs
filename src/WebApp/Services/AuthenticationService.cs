using Flurl;
using Flurl.Http;
using WebApp.Models;

namespace WebApp.Services
{
    public static class AuthenticationService
    {
        public static async Task<string> GetAuthenticationUrl(string email, string password)
        {
            try
            {
                var url = "https://localhost:7055/api/Authentication";

                var response = await url
                                .SetQueryParams(new
                                {
                                    email,
                                    PasswordHash = password
                                })
                                .AllowAnyHttpStatus()
                                .GetAsync();

                if (response.StatusCode < 300)
                {
                    var result = await response.GetJsonAsync<object>();
                    Console.WriteLine($"Success! {result}");
                }
                else if (response.StatusCode < 500)
                {
                    if (response.StatusCode == 400)
                        Console.WriteLine($"You did something wrong! {response.StatusCode} {response.ResponseMessage}");

                    var error = await response.GetJsonAsync<HttpErrorModel>();
                    Console.WriteLine($"You did something wrong! {error}");
                }
                else
                {
                    var error = await response.GetJsonAsync<HttpErrorModel>();
                    Console.WriteLine($"We did something wrong! {error}");
                }

                return "";
            }
            catch (FlurlHttpException ex)
            {
                var error = await ex.GetResponseJsonAsync<HttpErrorModel>();

                //logger.Write($"Error returned from {ex.Call.Request.Url}: {error.SomeDetails}");

                throw;
            }
        }
    }
}
