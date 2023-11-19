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

        app.MapGet("/routes/{startLocationId}/{endLocationId}",
            async (IDeliveryRepository repository, int startLocationId, int endLocationId) =>
            {
                var result = await repository.FindAllPathsFromLocations(startLocationId, endLocationId);
                return Results.Content(result, "application/json");
            });

        app.MapGet("/warshall/route/{orderId}", async (IDeliveryRepository repository, int orderId) =>
        {
            var result = await repository.CalculateOptimalDeliveryRouteWarshall(orderId);
            return Results.Content(result, "application/json");
        });
        
        app.MapGet("/dijkstra/route/{orderId}", async (IDeliveryRepository repository, int orderId) =>
        {
            var result = await repository.CalculateOptimalDeliveryRouteDijkstra(orderId);
            return Results.Content(result, "application/json");
        });

        app.MapGet("/courier/orderCount/{couriedId}", async (IDeliveryRepository repository, int couriedId) =>
        {
            var result = await repository.CalculateOrderCountForCourier(couriedId);
            return Results.Content(result, "application/json");
        });
    }
}
