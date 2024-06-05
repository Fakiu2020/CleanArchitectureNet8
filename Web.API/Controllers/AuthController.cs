using Application.Command.Auth;
using Application.Command.Order.Create;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Common;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginCommandResponse>>> Login(LoginCommand request)
        {
            return await Mediator.Send(request);
        }
    }
}
