using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Database;

public class AnimalDatabase:IAnimalDatabase

{
    private readonly IConfiguration _configuration;

    public AnimalDatabase(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimals(string? orderBY)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "SELECT * FROM Animal ORDER BY @name ASC";
        if (orderBY != null)
        {
            sqlCommand.Parameters.AddWithValue("@name",orderBY);
        }
        else
        {
            sqlCommand.Parameters.AddWithValue("@name","Name");
        }
        var reader = sqlCommand.ExecuteReader();
        var animals = new List<Animal>();
        int IdAnimalOrdinal = reader.GetOrdinal("IdAnimal");
        int NameOrdinal = reader.GetOrdinal("Name");
        int DercriptionOrdinal = reader.GetOrdinal("Description");
        int CategoryOrdinal = reader.GetOrdinal("Category");
        int AreaOrdinal = reader.GetOrdinal("Area");

        while (reader.HasRows)
        {
            var animal = new Animal()
            {
                IdAnimal = reader.GetInt32(IdAnimalOrdinal),
                Name = reader.GetString(NameOrdinal),
                Description = reader.GetString(DercriptionOrdinal),
                Category = reader.GetString(CategoryOrdinal),
                Area = reader.GetString(AreaOrdinal)
            };
        }

        return animals;
    }

    public void AddAnimal(AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "INSERT INTO Animal VALUES (@Id,@Name,@Description,@Category,@Area)";
        if (animal!= null)
        {
            sqlCommand.Parameters.AddWithValue("@Id",animal.IdAnimal);
            sqlCommand.Parameters.AddWithValue("@Name",animal.Name);
            sqlCommand.Parameters.AddWithValue("@Description",animal.Description);
            sqlCommand.Parameters.AddWithValue("@Category",animal.Category);
            sqlCommand.Parameters.AddWithValue("@Area",animal.Area);
        } 
        sqlCommand.ExecuteNonQuery();
    }

    public int ChangeAnimal(int idAnimal, AddAnimal animal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText =
            "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area Where IdAnimal = @idAnimal";
        if (animal!= null)
        {
            sqlCommand.Parameters.AddWithValue("@Name",animal.Name);
            sqlCommand.Parameters.AddWithValue("@Description",animal.Description);
            sqlCommand.Parameters.AddWithValue("@Category",animal.Category);
            sqlCommand.Parameters.AddWithValue("@Area",animal.Area);
            sqlCommand.Parameters.AddWithValue("@idAnimal", idAnimal);
        } 
        var result = sqlCommand.ExecuteNonQuery();
        return result;
    }

    public int DeleteAnimal(int idAnimal)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        connection.Open();
        using SqlCommand sqlCommand = new SqlCommand();
        sqlCommand.Connection = connection;
        sqlCommand.CommandText = "DELTE FROM ANIMAL WHERE IdAnimal = @IdAnimal";
        sqlCommand.Parameters.AddWithValue("@IdAnimal", idAnimal);
        var result = sqlCommand.ExecuteNonQuery();
        return result;
    }
}