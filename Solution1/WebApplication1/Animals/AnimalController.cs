using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Animals;

[ApiController]
[Route("/api/animals")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _animalService;

    public AnimalController(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAnimals([FromQuery] string orderBy)
    {
        var students = _animalService.GetAllAnimals(orderBy);
        return Ok(students);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimal([FromRoute] int id)
    {
        return Ok(id);
    }

    [HttpPost]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTOs dto)
    {
        var success = _animalService.AddAnimal(dto);
        return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }

    [HttpPut()]
    public IActionResult UpdateAnimal([FromQuery] int id, CreateAnimalDTOs dto)
    {
        var success = _animalService.PostAnimal(id, dto);
        return success ? StatusCode(StatusCodes.Status201Created) : NotFound();
    }

    [HttpDelete()]
    public IActionResult DeleteAnimal([FromBody] int id)
    {
        var success = _animalService.DeleteAnimal(id);
        return success ? StatusCode(StatusCodes.Status204NoContent) : NotFound();
    }
}