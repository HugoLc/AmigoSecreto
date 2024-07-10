using AmigoSecreto.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace AmigoSecreto.Api.Controllers;

[ApiController]
[Route("/api/")]
public class AmigoSecretoController : ControllerBase
{
    public AmigoSecretoController()
    {
    }

    [HttpPost("v1/create-user")]
    public IActionResult CreateUser([FromBody] UserRequest request)
    {
        //chamar mediator para registrar o command
        return Ok(request);
    }
}
