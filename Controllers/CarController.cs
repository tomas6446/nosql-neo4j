using nosql_neo4j.Repository;

namespace nosql_neo4j.Controllers;

using ISession = Neo4j.Driver;

public class CarController
{
    public static void MapCarsApi(RouteGroupBuilder app)
    {
        app.MapGet("/api/cars/{vinCode}", async (ICarRepository carRepository, string vinCode) =>
        {
            var car = await carRepository.SearchCarByVin(vinCode);
            return Results.Json(car);
        });

        app.MapGet("/api/cars/owner/{ownerName}", async (ICarRepository carRepository, string ownerName) =>
        {
            var cars = await carRepository.SearchCarsByOwner(ownerName);
            return Results.Json(cars);
        });

        app.MapGet("/api/cars/make/{make}", async (ICarRepository carRepository, string make) =>
        {
            var cars = await carRepository.SearchAllSameMakeCarOwners(make);
            return Results.Json(cars);
        });

        app.MapGet("/api/cars/owners/{vinCode}", async (ICarRepository carRepository, string vinCode) =>
        {
            var cars = await carRepository.SearchAllOwnersOfCarByVinCode(vinCode);
            return Results.Json(cars);
        });

        app.MapGet("/api/cars/averageage", async (ICarRepository carRepository) =>
        {
            var cars = await carRepository.CalculateAverageCarAge();
            return Results.Json(cars);
        });
    }
}
