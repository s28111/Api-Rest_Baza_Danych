using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Animals;

public class CreateAnimalDTOs
{
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    [StringLength(200)]
    public string Description { get; set; }

    [Required]
    [StringLength(200)]
    public string Category { get; set; }

    [Required]
    [StringLength(200)]
    public string Area { get; set; }
}