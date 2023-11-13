using nosql_neo4j.Service;

namespace nosql_neo4j.Repository;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly Neo4JService _neo4JService;

    public DeliveryRepository(Neo4JService neo4JService)
    {
        _neo4JService = neo4JService;
    }

    /// <summary>
    ///     Retrieves a specific order
    /// </summary>
    public async Task<string> FindOrderById(int orderId)
    {
        var query = "MATCH (o:Order {id: $id}) RETURN o LIMIT 1";
        var parameters = new { id = orderId };
        return await _neo4JService.ExecuteGenericReadQueryAsync(query, parameters);
    }

    /// <summary>
    ///     Lists all orders that are associated with a particular customer
    /// </summary>
    public async Task<string> ListOrdersForCustomer(int customerId)
    {
        var query = @"
            MATCH (cu:Customer {id: $id})-[:ORDERED]->(o:Order)
            RETURN o.id, o.name, cu.name
        ";
        var parameters = new { id = customerId };
        return await _neo4JService.ExecuteGenericReadQueryAsync(query, parameters);
    }

    /// <summary>
    ///     Lists all paths from your location to all other locations
    /// </summary>
    public async Task<string> FindAllPathsFromLocations(int startLocationId, int endLocationId)
    {
        var query = @"
            MATCH (start:Location {id: $startId}), (end:Location {id: $endId}),
                  path = (start)-[:ROAD*]-(end)
            RETURN start.name AS startLocation, end.name AS endLocation, [n in nodes(path) | n.name] AS Path
            LIMIT 10;
        ";
        var parameters = new { startId = startLocationId, endId = endLocationId };
        return await _neo4JService.ExecuteGenericReadQueryAsync(query, parameters);
    }

    /// <summary>
    ///     Find the Shortest Path Considering Weights
    /// </summary>
    public async Task<string> CalculateOptimalDeliveryRoute(int orderId)
    {
        var query = @"
            MATCH (c:Customer)-[:ORDERED]->(o:Order)-[:HAS_LOCATION]->(start:Location), 
                  (c)-[:HAS_LOCATION]->(end:Location), 
                  path = (start)-[:ROAD*]-(end) 
            WHERE o.id = $id 
            WITH start, end, path, 
                 reduce(distance = 0, r in relationships(path) | distance + r.distance) AS totalDistance 
            RETURN start.name AS startLocation, end.name AS endLocation, totalDistance, [n in nodes(path) | n.name] AS Path 
            ORDER BY totalDistance 
            LIMIT 1;
        ";
        var parameters = new { id = orderId };
        var result = await _neo4JService.ExecuteGenericReadQueryAsync(query, parameters);
        return result;
    }

    /// <summary>
    ///     Aggregates the total count of orders for a specific courier
    /// </summary>
    /// <param name="courierId"></param>
    public async Task<string> CalculateOrderCountForCourier(int courierId)
    {
        var query = @"
            MATCH (c:Courier {id: $id})-[:ASSIGNED_TO_DELIVER]->(o:Order)
            WITH c, count(o) AS orderCount
            RETURN c.name AS courierName, orderCount
        ";
        var parameters = new { id = courierId };
        var result = await _neo4JService.ExecuteGenericReadQueryAsync(query, parameters);
        return result;
    }
}
