using Netcompany.Net.DomainDrivenDesign.Services;

namespace RoutePlanning.Domain.Locations.Services;

public interface IRecommendedPathService : IDomainService
{
    int CalculateRecommendedPath(Location source, Location target);
}
