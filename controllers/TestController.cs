using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/test")]
[ApiController]
public class TestController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [Route("GetAdmin")]
    [HttpGet]
    public IActionResult AdminEndpoint()
    {
        return Ok("This is a protected Admin endpoint.");
    }

    [Authorize(Roles = "User")]
    [Route("GetUser")]
    [HttpGet]
    public IActionResult UserEndpoint()
    {
        return Ok("This is a protected User endpoint.");
    }
}
