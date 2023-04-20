using Microsoft.EntityFrameworkCore;

namespace RoutePlanning.Domain.Locations.Services;

public sealed class RecommendedPathService : IRecommendedPathService
{
    private readonly IQueryable<Location> _locations;

    public RecommendedPathService(IQueryable<Location> locations)
    {
        _locations = locations;
    }

    public int CalculateRecommendedPath(Location source, Location target)
    {
        var locations = _locations.Include(l => l.Connections).ThenInclude(c => c.Destination);

        var path = CalculateShortestRecommendedPath(locations, source, target);

        return path.Sum(c => c.Distance);
    }

    /// <summary>
    /// An implementation of the Dijkstra's shortest path algorithm
    /// </summary>
    private static IEnumerable<Connection> CalculateShortestRecommendedPath(IEnumerable<Location> locations, Location start, Location end)
    {
        var shortestConnections = CalculateShortestRecommendedConnections(locations, start, end);

        var path = ConstructShortestRecommendedPath(start, end, shortestConnections);

        return path;
    }

    /// <summary>
    /// An implementation of the Dijkstra's algorithm that computes the shortest connections to all locations until the end location is reached
    /// </summary>
    private static Dictionary<Location, (Connection? SourceConnection, int Distance)> CalculateShortestRecommendedConnections(IEnumerable<Location> locations, Location start, Location end)
    {
        var shortestRecommendedConnections = new Dictionary<Location, (Connection? SourceConnection, int Distance)>();
        var unvisitedLocations = locations.ToHashSet();

        foreach (var location in unvisitedLocations)
        {
            shortestRecommendedConnections[location] = (SourceConnection: null, Distance: int.MaxValue);
        }

        shortestRecommendedConnections[start] = (SourceConnection: null, Distance: 0);

        while (unvisitedLocations.Count > 0)
        {
            var location = unvisitedLocations.OrderBy(location => shortestRecommendedConnections[location].Distance).First();

            if (location == end)
            {
                break;
            }

            foreach (var connection in location.Connections)
            {
                UpdateShortestRecommendedConnections(shortestRecommendedConnections, location, connection);
            }

            unvisitedLocations.Remove(location);
        }

        
        
        return shortestRecommendedConnections;
    }

    private static void UpdateShortestRecommendedConnections(Dictionary<Location, (Connection? SourceConnection, int Distance)> shortestRecommendedConnections, Location location, Connection connection)
    {
        var distance = shortestRecommendedConnections[location].Distance + connection.Distance;

        if (distance < shortestRecommendedConnections[connection.Destination].Distance)
        {
            shortestRecommendedConnections[connection.Destination] = (SourceConnection: connection, Distance: distance);
        }
    }

    /// <summary>
    /// The shortest path is constructed by backtracking the Dijkstra connection data from the end location
    /// </summary>
    private static IEnumerable<Connection> ConstructShortestRecommendedPath(Location start, Location end, Dictionary<Location, (Connection? SourceConnection, int Distance)> sourceConnections)
    {
        var path = new List<Connection>();
        var location = end;

        while (location != start)
        {
            var shortestRecommendedConnection = sourceConnections[location].SourceConnection!;
            path.Add(shortestRecommendedConnection);
            location = shortestRecommendedConnection.Source;
        }

        path.Reverse();
        for (var i = 0; i < path.Count(); i++)
        {
            Console.WriteLine(path[i].Source.Name + "->" + path[i].Destination.Name);
        }

        return path;
    }
}
