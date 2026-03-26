using AuthorizationService.Application.Commands;
using AuthorizationService.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController: ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)=> _mediator = mediator;

    [HttpPost("sign_up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand command)
    {
        Guid id = await _mediator.Send(command);
        return Ok(id);
    }
    
    [HttpPost("verify_email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPost("sign_in_as_patient")]
    public async Task<IActionResult> SignInAsPatient([FromBody] SignInAsPatientCommand command)
    {
        TokenResponse tokens = await _mediator.Send(command);
        return Ok(tokens);
    }
    
    [HttpPost("refresh_token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        TokenResponse tokens = await _mediator.Send(command);
        return Ok(tokens);
    }

    [HttpPost("test_token")]
    [Authorize]
    public async Task<IActionResult> TestToken()
    {
        return Ok("Пользователь авторизован");
    }
}