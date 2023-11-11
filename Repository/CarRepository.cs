using Neo4j.Driver;

namespace nosql_neo4j.Repository;

public class CarRepository : ICarRepository
{
    private readonly IDriver _neo4jDriver;

    public CarRepository(IDriver neo4JDriver)
    {
        _neo4jDriver = neo4JDriver;
    }

    /// <summary>
    ///     Find a car by its VIN
    /// </summary>
    public async Task<Dictionary<string, object>> SearchCarByVin(string vinCode)
    {
        await using var session = _neo4jDriver.AsyncSession();
        var result = await session.RunAsync("MATCH (c:Car {vin: $vinCode}) RETURN c LIMIT 1", new { vinCode });

        if (!await result.FetchAsync()) return null!;
        var record = result.Current["c"].As<INode>().Properties;
        return record.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>
    ///     List all cars owned by a specific person.
    /// </summary>
    public async Task<List<Dictionary<string, object>>> SearchCarsByOwner(string ownerName)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Find friends of friends who own a specific make of car.
    /// </summary>
    public async Task<List<Dictionary<string, object>>> SearchAllSameMakeCarOwners(string make)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Find all owners of the car, from first to current
    /// </summary>
    public async Task<List<Dictionary<string, object>>> SearchAllOwnersOfCar(string vinCode)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Calculate the average age of a car in the database
    /// </summary>
    public async Task<int> CalculateAverageCarAge()
    {
        throw new NotImplementedException();
    }
}
