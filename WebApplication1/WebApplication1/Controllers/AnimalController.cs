using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication1.Database;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Controllers;

[ApiController]

public class AnimalController:ControllerBase
{
        private readonly IAnimalDatabase _database;

        public AnimalController(IAnimalDatabase database)
        {
                _database = database;
                
        }

        [HttpGet]
        [Route ("api/animals")]
        public IActionResult GetAnimals(string? orderBy)
        {
                var animals = _database.GetAnimals(orderBy);
                return Ok(animals);
        }

        [HttpPost]
        [Route ("api/animals")]
        public IActionResult AddAnimal(AddAnimal animal)
        {
               _database.AddAnimal(animal);
               return Created();
        }

        [HttpPut]
        [Route("api/animals/{IdAnimal:int}")]
        public IActionResult ChangeAnimal(int IdAnimal, AddAnimal animal)
        {
                var result = _database.ChangeAnimal(IdAnimal, animal);
                if (result == 1)
                {
                        return Ok("Changed Animal");
                }
                else
                {
                       return NotFound("Not Found");
                } 
        }
        [HttpDelete]
        [Route("api/animals/{IdAnimal:int}")]
        public IActionResult DeleteAnimal(int IdAnimal)
        {
                var result = _database.DeleteAnimal(IdAnimal);
                if (result == 1)
                {
                        return Ok("Deleted Animal");
                }
                else
                {
                        return NotFound("Not Found");
                } 
        }
}