using Microsoft.IdentityModel.Tokens;
using Netcompany.Net.UnitOfWork;
using RoutePlanning.Domain.Enums;
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

            var dekanarikooer = new Location("DEKANARISKEOEER");
            await context.AddAsync(dekanarikooer);
            var marrakesh = new Location("MARRAKESH");
            await context.AddAsync(marrakesh);
            var dakar = new Location("DAKAR");
            await context.AddAsync(dakar);
            var sierraleone = new Location("SIERRALEONE");
            await context.AddAsync(sierraleone);
            var guldkysten = new Location("GULDKYSTEN");
            await context.AddAsync(guldkysten);
            var timbuktu = new Location("TIMBUKTU");
            await context.AddAsync(timbuktu);
            var sahara = new Location("SAHARA");
            await context.AddAsync(sahara);
            var tunis = new Location("TUNIS");
            await context.AddAsync(tunis);
            var tanger = new Location("TANGER");
            await context.AddAsync(tanger);
            var tripoli = new Location("TRIPOLI");
            await context.AddAsync(tripoli);
            var wadai = new Location("WADAI");
            await context.AddAsync(wadai); 
            var slavekysten = new Location("SLAVEKYSTEN");
            await context.AddAsync(slavekysten);
            var darfur = new Location("DARFUR");
            await context.AddAsync(darfur);
            var omdurman = new Location("OMDURMAN");
            await context.AddAsync(omdurman);
            var cairo = new Location("CAIRO");
            await context.AddAsync(cairo);
            var suakin = new Location("SUAKIN");
            await context.AddAsync(suakin);
            var addisadeba = new Location("ADDISADEBA");
            await context.AddAsync(addisadeba);
            var kapguardafui = new Location("KAPGUARDAFUI");
            await context.AddAsync(kapguardafui);
            var bahrelghazal = new Location("BAHRELGHAZAL");
            await context.AddAsync(bahrelghazal);
            var congo = new Location("CONGO");
            await context.AddAsync(congo);
            var luanda = new Location("LUANDA");
            await context.AddAsync(luanda);
            var sthelena = new Location("STHELENA");
            await context.AddAsync(sthelena);
            var victoriasooen = new Location("VICTORIASOEEN");
            await context.AddAsync(victoriasooen);
            var kabalo = new Location("KABALO");
            await context.AddAsync(kabalo);
            var zanzibar = new Location("ZANZIBAR");
            await context.AddAsync(zanzibar);
            var mocambique = new Location("MOCAMBIQUE");
            await context.AddAsync(mocambique);
            var dragebjerget = new Location("DRAGEBJERGET");
            await context.AddAsync(dragebjerget);
            var victoriafaldene = new Location("VICTORIAFALDENE");
            await context.AddAsync(victoriafaldene);
            var hvalbugten = new Location("HVALBUGTEN");
            await context.AddAsync(hvalbugten);
            var kapstaden = new Location("KAPSTADEN");
            await context.AddAsync(kapstaden);
            var kapstmarie = new Location("KAPSTMARIE");
            await context.AddAsync(kapstmarie);
            var amatave = new Location("AMATAVE");
            await context.AddAsync(amatave);

            CreateTwoWayConnection(tanger, marrakesh, ConnectionType.Air );
            CreateTwoWayConnection(marrakesh, sierraleone, ConnectionType.Air);
            CreateTwoWayConnection(sierraleone, sthelena, ConnectionType.Air);
            CreateTwoWayConnection(marrakesh, guldkysten, ConnectionType.Air);
            CreateTwoWayConnection(guldkysten, tripoli, ConnectionType.Air);
            CreateTwoWayConnection(tanger, tripoli, ConnectionType.Air);
            CreateTwoWayConnection(guldkysten, luanda, ConnectionType.Air);
            CreateTwoWayConnection(guldkysten, hvalbugten, ConnectionType.Air);
            CreateTwoWayConnection(sthelena, kapstaden, ConnectionType.Air);
            CreateTwoWayConnection(hvalbugten, kapstaden, ConnectionType.Air);
            CreateTwoWayConnection(luanda, hvalbugten, ConnectionType.Air);
            CreateTwoWayConnection(tripoli, darfur, ConnectionType.Air);
            CreateTwoWayConnection(darfur , kabalo, ConnectionType.Air);
            CreateTwoWayConnection(kabalo, kapstaden, ConnectionType.Air);
            CreateTwoWayConnection(kapstaden,dragebjerget, ConnectionType.Air);
            CreateTwoWayConnection(dragebjerget, victoriasooen, ConnectionType.Air);
            CreateTwoWayConnection(kapstaden, amatave, ConnectionType.Air);
            CreateTwoWayConnection(kapstaden, kapstmarie, ConnectionType.Air);
            CreateTwoWayConnection(amatave, kapguardafui, ConnectionType.Air);
            CreateTwoWayConnection(kapguardafui, victoriasooen, ConnectionType.Air);
            CreateTwoWayConnection(victoriasooen, suakin, ConnectionType.Air);
            CreateTwoWayConnection(darfur, suakin, ConnectionType.Air);
            CreateTwoWayConnection(suakin, cairo, ConnectionType.Air);
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
