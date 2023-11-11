namespace nosql_neo4j.Repository;

public interface ICarRepository
{
    Task<Dictionary<string, object>> SearchCarByVin(string vinCode);

    Task<List<Dictionary<string, object>>> SearchCarsByOwner(string ownerName);

    Task<List<Dictionary<string, object>>> SearchAllSameMakeCarOwners(string make);

    Task<List<Dictionary<string, object>>> SearchAllOwnersOfCar(string vinCode);

    Task<int> CalculateAverageCarAge();
}
