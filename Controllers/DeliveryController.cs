using nosql_neo4j.Repository;

namespace nosql_neo4j.Controllers;

using ISession = Neo4j.Driver;

public class DeliveryController
{
    public static void MapDeliveryApi(RouteGroupBuilder app)
    {
        app.MapGet("/{id}", async (IDeliveryRepository repository, int id) =>
        {
            var result = await repository.FindOrderById(id);
            return Results.Ok(result);
        });

        app.MapGet("/customer/{name}", async (IDeliveryRepository repository, string name) =>
        {
            var result = await repository.ListOrdersForCustomer(name);
            return Results.Ok(result);
        });

        app.MapGet("/customers/{locationName}", async (IDeliveryRepository repository, string locationName) =>
        {
            var result = await repository.FindConnectedCustomersThroughOrders(locationName);
            return Results.Ok(result);
        });

        app.MapGet("/route/{orderId}", async (IDeliveryRepository repository, string orderId) =>
        {
            var result = await repository.CalculateOptimalDeliveryRoute(orderId);
            return Results.Ok(result);
        });

        app.MapGet("/totalDistance", async (IDeliveryRepository repository) =>
        {
            var result = await repository.SummarizeTotalDistanceForCourier();
            return Results.Ok(result);
        });
    }
}
