using AuthorizationService.Application.Commands;
using MediatR;
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
}