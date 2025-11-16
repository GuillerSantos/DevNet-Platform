using DevNet.Application.Features.Auth.Commands.RefreshToken;
using DevNet.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        #region Public Methods

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { message = result.Error });
        }

        #endregion Public Methods
    }
}