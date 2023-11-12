namespace nosql_neo4j.Repository;

public interface IDeliveryRepository
{
    Task<Dictionary<string, object>> FindOrderById(int orderId);

    Task<List<Dictionary<string, object>>> ListOrdersForCustomer(int customerId);

    Task<List<Dictionary<string, object>>> FindConnectedCustomersThroughOrders(int locationId);

    Task<List<Dictionary<string, object>>> CalculateOptimalDeliveryRoute(int orderId);

    Task<int> SummarizeTotalDistanceForCourier();
}
