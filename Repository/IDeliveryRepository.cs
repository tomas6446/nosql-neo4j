namespace nosql_neo4j.Repository;

public interface IDeliveryRepository
{
    Task<string> FindOrderById(int orderId);

    Task<string> ListOrdersForCustomer(int customerId);

    Task<string> FindConnectedCustomersThroughOrders(int locationId);

    Task<string> CalculateOptimalDeliveryRoute(int orderId);

    Task<string> SummarizeTotalDistanceForCourier();
}
