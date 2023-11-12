using Neo4j.Driver;
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
    public async Task<Dictionary<string, object>> FindOrderById(int orderId)
    {
        var query = "MATCH (o:Order {id: $id}) RETURN o";
        var parameters = new { id = orderId };
        return await _neo4JService.ExecuteReadQueryAsync(query, parameters);
    }

    /// <summary>
    ///     Lists all orders that are associated with a particular customer
    /// </summary>
    public async Task<List<Dictionary<string, object>>> ListOrdersForCustomer(int customerId)
    {
        var query = @"
            MATCH (cu:Customer {id: $id})-[:ORDERED]->(o:Order)
            RETURN o
        ";
        var parameters = new { id = customerId };
        return await _neo4JService.ListExecuteReadQueryAsync(query, parameters);
    }

    /// <summary>
    ///    Lists customers connected through ordering from the same location
    /// </summary>
    public async Task<List<Dictionary<string, object>>> FindConnectedCustomersThroughOrders(int locationId)
    {
        var query = @"
            MATCH (c1:Customer)-[:ORDERED]->(o1:Order)-[:HAS_LOCATION]->(l:Location)<-[:HAS_LOCATION]-(o2:Order)<-[:ORDERED]-(c2:Customer)
            WHERE c1 <> c2 AND l.id = $id
            RETURN l.name AS Location, collect(DISTINCT c1.name) AS Customer1, collect(DISTINCT c2.name) AS Customer2
        ";
        var parameters = new { id = locationId };
        return await _neo4JService.ListExecuteReadQueryAsync(query, parameters);
    }

    /// <summary>
    ///    Find the Shortest Path Considering Weights
    /// </summary>
    public async Task<List<Dictionary<string, object>>> CalculateOptimalDeliveryRoute(int orderId)
    {
        var query = @"
            MATCH (o:Order {id: $id})-[:HAS_LOCATION]->(start:Location),
                  (o)-[:ORDERED_BY]->(c:Customer)-[:HAS_LOCATION]->(end:Location)
            RETURN start.name AS StartLocation, end.name AS EndLocation, 
                   gds.shortestPath.dijkstra.stream({
                      nodeProjection: 'Location',
                      relationshipProjection: {
                        ROAD: {
                          type: 'ROAD',
                          properties: 'distance',
                          orientation: 'UNDIRECTED'
                        }
                      },
                      startNode: start,
                      endNode: end,
                      relationshipWeightProperty: 'distance'
                   }) AS Path
        ";
        var parameters = new { id = orderId };
        var result = await _neo4JService.ListExecuteReadQueryAsync(query, parameters);
        return result;
    }

    /// <summary>
    ///     Aggregates the total distance that a courier would travel to deliver all assigned orders
    /// </summary>
    public async Task<int> SummarizeTotalDistanceForCourier()
    {
        throw new NotImplementedException();
    }
}
