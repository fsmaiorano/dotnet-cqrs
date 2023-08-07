﻿using Application.Common.Interfaces;
using Application.UseCases.User.Queries.GetUser;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> IndexAsync([FromQuery] GetAuthUserQuery query)
        {
            var user = await Mediator.Send(query);

            //var token = await _authService.HandleUserAuthentication(command);

            return Ok("");
        }
    }
}
