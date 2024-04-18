namespace WebApplication1.Database;

using WebApplication1.Models;
using WebApplication1.Models.DTOs;

public interface IAnimalDatabase
{
    IEnumerable<Animal> GetAnimals(string? orderBY);
    void AddAnimal(AddAnimal animal);
    int ChangeAnimal(int idAnimal, AddAnimal animal);
    int DeleteAnimal(int idAnimal);
}