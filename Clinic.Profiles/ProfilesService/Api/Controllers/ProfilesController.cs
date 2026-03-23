using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProfilesService.Application.Commands;
using ProfilesService.Application.Queries;

namespace ProfilesService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("check-match")]
    public async Task<IActionResult> CheckMatch([FromBody] CheckPatientProfileMatchQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProfile([FromBody] CreatePatientProfileCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("link")]
    public async Task<IActionResult> LinkProfile([FromBody] LinkPatientProfileToExistingAccountCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}