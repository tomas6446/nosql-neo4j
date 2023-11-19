namespace nosql_neo4j.Repository;

public interface IDeliveryRepository
{
    Task<string> FindOrderById(int orderId);

    Task<string> ListOrdersForCustomer(int customerId);

    Task<string> FindAllPathsFromLocations(int startLocationId, int endLocationId);

    Task<string> CalculateOptimalDeliveryRouteWarshall(int orderId);

    Task<string> CalculateOptimalDeliveryRouteDijkstra(int orderId);

    Task<string> CalculateOrderCountForCourier(int courierId);
}
