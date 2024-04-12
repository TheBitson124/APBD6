using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Controllers;

[ApiController]
[Route ("api/[controller]")]
public class AnimalController:ControllerBase
{
        private readonly IConfiguration _configuration;

        public AnimalController(IConfiguration configuration)
        {
                _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAnimals()
        {
                using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                connection.Open();
                using SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "SELECT * FROM Animal";

                var reader = sqlCommand.ExecuteReader();
                var animals = new List<Animal>();
                int idAnimalOrdinal = reader.GetOrdinal("IdAnimal");
                int NameOrdinal = reader.GetOrdinal("Name");
                while (reader.Read())
                {
                        animals.Add(new Animal()
                        {
                                id = reader.GetInt32(idAnimalOrdinal),
                                name = reader.GetString(NameOrdinal)
                        });
                }

                var animals = _repository.GetAnimals();
                return Ok(animals);
        }

        [HttpPost]
        public IActionResult AddAnimal(AddAnimal animal)
        {
                using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
                connection.Open();
                using SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "INSERT INTO Animal VALUES(@animalName,'','','')";
                sqlCommand.Parameters.AddWithValue("@animalName", animal.Name);
                sqlCommand.ExecuteNonQuery();
                return Created("", null);
        }
}