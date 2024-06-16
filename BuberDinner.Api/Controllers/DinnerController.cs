using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

public class DinnerController : ApiControllerBase
{
    public IActionResult GetDinner()
    {
        return Ok(Array.Empty<string>());
    }
}