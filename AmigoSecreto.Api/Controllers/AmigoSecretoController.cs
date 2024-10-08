
using AmigoSecreto.Application.AmigoSecreto.Commands;
using AmigoSecreto.Application.AmigoSecreto.Queries;
using AmigoSecreto.Application.AmigoSecreto.Queries.Group;
using AmigoSecreto.Application.AmigoSecreto.Queries.User;
using AmigoSecreto.Contracts.Group;
using AmigoSecreto.Contracts.User;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public async Task<IActionResult> CreateUser([FromBody] UserRequest request)
    {

        //TODO: usar mapster
        var requestCommand = new CreateUserCommand(
            Name: request.Name,
            Password: request.Password,
            Phone: request.Phone,
            Gifts: request.Gifts
                .Select(g => new GiftCommand(g.Description, g.Link))
                .ToList(),
            GroupId: request.GroupId
        );
        //TODO: melhorar tratativa de erros
        try
        {
            var result = await _mediator.Send(requestCommand);
            return Ok(result);
        }
        catch (Exception ex)
        {
            var retorno = Content(ex.Message);
            retorno.StatusCode = 500;
            return retorno;
        }
    }

    [HttpGet("v1/get-user/{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        var requestQuery = new ReadUserByIdQuery(Guid.Parse(id));
        var result = await _mediator.Send(requestQuery);
        if (result == null)
        {
            return NoContent();
        }
        return Ok(result);
    }

    [HttpGet("v1/get-users")]
    public IActionResult GetUsers()
    {
        var requestQuery = new ReadUsersQuery();
        var result = _mediator.Send(requestQuery);
        if (result.Result == null || result.Result.Count == 0)
        {
            return NoContent();
        }
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

    [HttpGet("v1/get-group/{id}")]
    public IActionResult GetGroupById([FromRoute] string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest();
        }
        var requestQuery = new ReadGroupByIdQuery(Guid.Parse(id));
        var result = _mediator.Send(requestQuery);
        if (result.Result is null)
        {
            return NoContent();
        }
        return Ok(result.Result);
    }
    [HttpGet("v1/get-groups")]
    public IActionResult GetGroups()
    {

        var requestQuery = new ReadGroupsQuery();
        var result = _mediator.Send(requestQuery);

        if (result.Result == null || result.Result.Count == 0)
        {
            return NoContent();
        }
        return Ok(result.Result);
    }
    [HttpGet("v1/get-players-by-group/{id}")]
    public IActionResult GetPlayersByGroup([FromRoute] string id)
    {

        var requestQuery = new ReadUsersByGroupQuery(Guid.Parse(id));
        var result = _mediator.Send(requestQuery);

        if (result.Result == null || result.Result.Count == 0)
        {
            return NoContent();
        }
        return Ok(result.Result);
    }
    [HttpPost("v1/group/add-players")]
    public IActionResult AddPlayers([FromBody] AddPlayersRequest request)
    {
        var requestCommand = new AddPlayerToGroupCommand(
            Guid.Parse(request.GroupId),
            request.Players);
        var result = _mediator.Send(requestCommand);
        return Ok(result.Result);
    }
    [HttpPost("v1/group/draw-friends")]
    public async Task<IActionResult> DrawFriends([FromBody] DrawFriendsRequest request)
    {
        var requestCommand = new DrawFriendsCommand(request.GroupId);
        var result = await _mediator.Send(requestCommand);
        return Ok(result);
    }
    [HttpDelete("v1/user/delete/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string id)
    {
        var requestCommand = new DeleteUserCommand(id);
        await _mediator.Send(requestCommand);
        return NoContent();
    }
}
