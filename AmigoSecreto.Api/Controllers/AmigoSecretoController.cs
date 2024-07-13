
using AmigoSecreto.Application.AmigoSecreto.Commands;
using AmigoSecreto.Application.AmigoSecreto.Queries;
using AmigoSecreto.Contracts.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AmigoSecreto.Api.Controllers;

[ApiController]
[Route("/api/")]
public class AmigoSecretoController : ControllerBase
{
    private readonly ISender _mediator;
    public AmigoSecretoController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("v1/create-user")]
    public IActionResult CreateUser([FromBody] UserRequest request)
    {
        //TODO: usar mapster
        var requestCommand = new CreateUserCommand(
            Name: request.Name,
            Password: request.Password,
            Phone: request.Phone,
            Gifts: request.Gifts,
            GroupId: request.GroupId
        );
        var result = _mediator.Send(requestCommand);
        return Ok(result.Result);
    }

    [HttpGet("v1/get-user/{id}")]
    public IActionResult GetUserById([FromRoute] string id)
    {
        var requestQuery = new ReadUserByIdQuery(Guid.Parse(id));
        var result = _mediator.Send(requestQuery);
        return Ok(result.Result);
    }
}
