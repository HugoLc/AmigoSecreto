
using AmigoSecreto.Application.AmigoSecreto.Commands;
using AmigoSecreto.Application.AmigoSecreto.Queries;
using AmigoSecreto.Contracts.Group;
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
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        var requestQuery = new ReadUserByIdQuery(Guid.Parse(id));
        var result = _mediator.Send(requestQuery);
        if (result.Result is null)
        {
            return NoContent();
        }
        return Ok(result.Result);
    }

    [HttpGet("v1/get-users")]
    public IActionResult GetUsers()
    {
        var requestQuery = new ReadUsersQuery();
        var result = _mediator.Send(requestQuery);
        return Ok(result.Result);
    }
    [HttpPost("v1/create-group")]
    public IActionResult CreateGroup([FromBody] CreateGroupRequest request)
    {
        //TODO: usar mapster
        var requestCommand = new CreateGroupCommand(
            request.DrawDate,
            request.GiftsDate,
            request.Local,
            request.AdminId
        );
        var result = _mediator.Send(requestCommand);
        return Ok(result.Result);
    }
}
