using Neo4j.Driver;
using System.Text.Json;

namespace nosql_neo4j.Service;

public class Neo4JService
{
    private readonly IDriver _neo4JDriver;

    public Neo4JService(IDriver neo4JDriver)
    {
        _neo4JDriver = neo4JDriver;
    }

    public async Task<string> ExecuteGenericReadQueryAsync(string query, object parameters)
    {
        await using var session = _neo4JDriver.AsyncSession();
        var resultList = new List<Dictionary<string, object>>();

        await session.ReadTransactionAsync(async tx =>
        {
            var cursor = await tx.RunAsync(query, parameters);
            var records = await cursor.ToListAsync();
            foreach (var record in records)
            {
                var recordDict = new Dictionary<string, object>();
                foreach (var key in record.Keys)
                {
                    var value = record[key];
                    recordDict.Add(key, value);
                }
                resultList.Add(recordDict);
            }
        });
        return JsonSerializer.Serialize(resultList, _options);
    }

    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    };
}
