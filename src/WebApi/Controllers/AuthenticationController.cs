using Application.Common.Interfaces;
using Application.UseCases.User.Queries.GetUser;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Errors;

namespace WebApi.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DoAuthentication([FromQuery] GetAuthUserQuery query)
        {
            // var user = await Mediator.Send(query);

            // if (user == null)
            //     return BadRequest(new BadRequestError() { Message = "Invalid Credentials" });

            // Only for testing
            var user = new UserAuthenticationDto
            {
                Id = 1,
                Name = "Test",
                Email = "test@test.com"
            };

            var token = await _authService.GenerateToken(user);

            return Ok(token);
        }
    }
}
