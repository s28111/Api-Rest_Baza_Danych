using System.Data.SqlClient;


namespace WebApplication1.Animals;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);
    public bool CreateAnimal(string name, string description, string category, string area);
    public bool UpdateAnimal(int idAnimal, string name, string description, string category, string area);
    public bool DeleteAnimal(int idAnimal);
}

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        var safeOrderBy = new string[] {"IdAnimal", "Name", "Description", "CATEGORY", "AREA"}.Contains(orderBy) ? orderBy : "IdAnimal";
        using var command = new SqlCommand($"SELECT IdAnimal, Name, Description, CATEGORY, AREA From Animal ORDER BY {safeOrderBy}", connection);
        using var reader = command.ExecuteReader();

        var animals = new List<Animal>();
        while (reader.Read())
        {
            var animal = new Animal()
            {
                IdAnimal = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString(),
                Description = reader["Description"].ToString(),
                CATEGORY = reader["CATEGORY"].ToString(),
                AREA = reader["AREA"].ToString()
            };
            animals.Add(animal);
        }

        return animals;
    }

    public bool CreateAnimal(string name, string description, string category, string area)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        using var command = new SqlCommand("INSERT INTO Animal (Name, Description, CATEGORY, AREA) VALUES (@name, @description, @category, @area)",connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@area", area);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }

    public bool UpdateAnimal(int idAnimal, string name, string description, string category, string area)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
    
        using var command = new SqlCommand("UPDATE Animal SET Name = @name, Description = @description, CATEGORY = @category, AREA = @area WHERE IdAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@area", area);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);
    
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }

    public bool DeleteAnimal(int idAnimal)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command = new SqlCommand("DELETE FROM Animal where IdAnimal = @idAnimal", connection);
        command.Parameters.AddWithValue("@idAnimal", idAnimal);

        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }
}