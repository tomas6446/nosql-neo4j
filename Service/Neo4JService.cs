using Neo4j.Driver;

namespace nosql_neo4j.Service;

public class Neo4JService
{
    private readonly IDriver _neo4JDriver;

    public Neo4JService(IDriver neo4JDriver)
    {
        _neo4JDriver = neo4JDriver;
    }

    public async Task<Dictionary<string, object>> ExecuteReadQueryAsync(string query, object parameters)
    {
        await using var session = _neo4JDriver.AsyncSession();
        var result = await session.ReadTransactionAsync(async tx =>
        {
            var cursor = await tx.RunAsync(query, parameters);
            var record = await cursor.SingleAsync();
            return record.Values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        });
        return result;
    }

    public async Task<List<Dictionary<string, object>>> ListExecuteReadQueryAsync(string query, object parameters)
    {
        await using var session = _neo4JDriver.AsyncSession();
        var result = await session.ReadTransactionAsync(async tx =>
        {
            var cursor = await tx.RunAsync(query, parameters);
            var records = await cursor.ToListAsync();
            return records.Select(record =>
                record.Values.Values
                    .OfType<INode>()
                    .Single()
                    .Properties
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value)
            ).ToList();
        });
        return result;
    }
}
