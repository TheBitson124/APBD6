﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs;

public class AddAnimal
{
    [Required]
    [MinLength(5)]
    public string Name { get; set; }
    public string? Description { get; set; }
}