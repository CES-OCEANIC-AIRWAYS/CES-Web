using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;
using RoutePlanning.Domain.Enums;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Name}")]
public sealed class Location : AggregateRoot<Location>
{
    public Location(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    private readonly List<Connection> _connections = new();

    public IReadOnlyCollection<Connection> Connections => _connections.AsReadOnly();

    public Connection AddConnection(Location destination, ConnectionType type)
    {
        Connection connection = new(this, destination, type);

        _connections.Add(connection);

        return connection;
    }
}
