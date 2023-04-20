using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services;

public interface ICheapestPathService : IDomainService
{
    int CalculateCheapestPath(Location source, Location target);
}
