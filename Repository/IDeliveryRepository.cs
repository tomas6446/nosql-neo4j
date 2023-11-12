namespace nosql_neo4j.Repository;

public interface IDeliveryRepository
{
    Task<Dictionary<string, object>> FindOrderById(int id);

    Task<List<Dictionary<string, object>>> ListOrdersForCustomer(string name);

    Task<List<Dictionary<string, object>>> FindConnectedCustomersThroughOrders(string country);

    Task<List<Dictionary<string, object>>> CalculateOptimalDeliveryRoute(string id);

    Task<int> SummarizeTotalDistanceForCourier();
}
