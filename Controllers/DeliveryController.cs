using nosql_neo4j.Repository;

namespace nosql_neo4j.Controllers;

using ISession = Neo4j.Driver;

public class DeliveryController
{
    public static void MapDeliveryApi(RouteGroupBuilder app)
    {
        app.MapGet("/{rentId}", async (IDeliveryRepository repository, int rentId) =>
        {
            var result = await repository.FindOrderById(rentId);
            return Results.Content(result, "application/json");
        });

        app.MapGet("/customer/{customerId}", async (IDeliveryRepository repository, int customerId) =>
        {
            var result = await repository.ListOrdersForCustomer(customerId);
            return Results.Content(result, "application/json");
        });

        app.MapGet("/customers/{locationId}", async (IDeliveryRepository repository, int locationId) =>
        {
            var result = await repository.FindConnectedCustomersThroughOrders(locationId);
            return Results.Content(result, "application/json");
        });

        app.MapGet("/route/{orderId}", async (IDeliveryRepository repository, int orderId) =>
        {
            var result = await repository.CalculateOptimalDeliveryRoute(orderId);
            return Results.Content(result, "application/json");
        });

        app.MapGet("/totalDistance", async (IDeliveryRepository repository) =>
        {
            var result = await repository.SummarizeTotalDistanceForCourier();
            return Results.Content(result, "application/json");
        });
    }
}
