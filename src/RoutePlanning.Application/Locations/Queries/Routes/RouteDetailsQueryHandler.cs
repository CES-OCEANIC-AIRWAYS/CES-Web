using Netcompany.Net.Cqs.Queries;
using RoutePlanning.Domain.Locations.Services;
using RoutePlanning.Domain.Locations;
using Microsoft.EntityFrameworkCore;

namespace RoutePlanning.Application.Locations.Queries.Routes;
public class RouteDetailsQueryHandler : IQueryHandler<RouteDetailsQuery, int>
{
    private readonly IQueryable<Location> _locations;
    private readonly IShortestDistanceService _shortestDistanceService;

    public RouteDetailsQueryHandler(IQueryable<Location> locations, IShortestDistanceService shortestDistanceService)
    {
        _locations = locations;
        _shortestDistanceService = shortestDistanceService;
    }

    public async Task<int> Handle(RouteDetailsQuery request, CancellationToken cancellationToken)
    {
        var source = await _locations.FirstAsync(l => l.Id == request.SourceId, cancellationToken);
        var destination = await _locations.FirstAsync(l => l.Id == request.DestinationId, cancellationToken);

        var distance = _shortestDistanceService.CalculateShortestDistance(source, destination);

        return distance;
    }
}
