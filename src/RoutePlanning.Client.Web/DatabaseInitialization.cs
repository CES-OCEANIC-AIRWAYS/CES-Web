using Microsoft.IdentityModel.Tokens;
using Netcompany.Net.UnitOfWork;
using RoutePlanning.Domain.Enums;
using RoutePlanning.Domain.Orders;
using RoutePlanning.Domain.Users;
using RoutePlanning.Infrastructure.Database;
using Location = RoutePlanning.Domain.Locations.Location;

namespace RoutePlanning.Client.Web;

public static class DatabaseInitialization
{
    public static async Task SeedDatabase(WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();

        var context = serviceScope.ServiceProvider.GetRequiredService<RoutePlanningDatabaseContext>();
        await context.Database.EnsureCreatedAsync();

        var unitOfWorkManager = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
        await using (var unitOfWork = unitOfWorkManager.Initiate())
        {
            await SeedUsers(context);
            await SeedLocationsAndRoutes(context);

            unitOfWork.Commit();
        }
    }

    private static async Task SeedLocationsAndRoutes(RoutePlanningDatabaseContext context)
    {
        if (context.Set<Location>().IsNullOrEmpty())
        {
            var berlin = new Location("Berlin");
            await context.AddAsync(berlin);

            var copenhagen = new Location("Copenhagen");
            await context.AddAsync(copenhagen);

            var paris = new Location("Paris");
            await context.AddAsync(paris);

            var warsaw = new Location("Warsaw");
            await context.AddAsync(warsaw);

            CreateTwoWayConnection(berlin, warsaw, ConnectionType.Air);
            CreateTwoWayConnection(berlin, copenhagen, ConnectionType.Air);
            CreateTwoWayConnection(berlin, paris, ConnectionType.Sea);
            CreateTwoWayConnection(copenhagen, paris, ConnectionType.Land);

            // seed an order
            var order = new Order("123", berlin, paris, 199, new DateTime(), 10, Domain.Enums.Status.Lost, "Telstar");
            await context.AddAsync(order);
        }
    }

    private static async Task SeedUsers(RoutePlanningDatabaseContext context)
    {
        if (context.Set<User>().IsNullOrEmpty())
        {
            var alice = new User("alice", User.ComputePasswordHash("alice123!"));
            await context.AddAsync(alice);

            var bob = new User("bob", User.ComputePasswordHash("!CapableStudentCries25"));
            await context.AddAsync(bob);
        }
    }

    private static void CreateTwoWayConnection(Location locationA, Location locationB, ConnectionType type)
    {
        locationA.AddConnection(locationB, type);
        locationB.AddConnection(locationA, type);
    }
}
