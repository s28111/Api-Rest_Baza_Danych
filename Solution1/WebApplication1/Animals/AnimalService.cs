namespace WebApplication1.Animals;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(CreateAnimalDTOs dto);
    public bool PostAnimal(int idAnimal,CreateAnimalDTOs dto);
    public bool DeleteAnimal(int idAnimal);
}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _animalRepository.FetchAllAnimals(orderBy);
    }

    public bool AddAnimal(CreateAnimalDTOs dto)
    {
        return _animalRepository.CreateAnimal(dto.Name, dto.Description, dto.Category, dto.Area);
    }

    public bool PostAnimal(int idAnimal,CreateAnimalDTOs dto)
    {
        return _animalRepository.UpdateAnimal(idAnimal, dto.Name, dto.Description, dto.Category, dto.Area);
    }

    public bool DeleteAnimal(int idAnimal)
    {
        return _animalRepository.DeleteAnimal(idAnimal);
    }
}