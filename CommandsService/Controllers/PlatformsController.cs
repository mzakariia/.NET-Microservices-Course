using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/commands/platforms")]
public class PlatformsController : ControllerBase
{
    public PlatformsController()
    {
    }

    [HttpPost]
    public ActionResult TestConnection()
    {
        Console.WriteLine("Test connection to platforms controller");
        return Ok("Test connection successful!");
    }



}