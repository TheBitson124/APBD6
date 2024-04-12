using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers;

[ApiController]
[Route ("api/{controller}")]
public class AnimalController:ControllerBase
{
        [HttpGet]
        public IActionResult GetAnimals()
        {
                return Ok();
        }
}