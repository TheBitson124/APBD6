using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

public class AddAnimal
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
    public object IdAnimal { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }


}