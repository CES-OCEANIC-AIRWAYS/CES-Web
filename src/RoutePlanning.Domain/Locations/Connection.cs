using System.Diagnostics;
using Netcompany.Net.DomainDrivenDesign.Models;
using RoutePlanning.Domain.Enums;

namespace RoutePlanning.Domain.Locations;

[DebuggerDisplay("{Source} --{Distance}--> {Destination}")]
public sealed class Connection : Entity<Connection>
{
    public Connection(Location source, Location destination, ConnectionType type)
    {
        Source = source;
        Destination = destination;
        Type = type;
    }

    private Connection()
    {
        Source = null!;
        Destination = null!;
    }

    public Location Source { get; private set; }

    public Location Destination { get; private set; }

    public ConnectionType Type { get; private set; }
}
