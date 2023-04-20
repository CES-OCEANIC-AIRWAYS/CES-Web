using Netcompany.Net.Cqs.Queries;

namespace RoutePlanning.Application.Locations.Queries.Routes;
public record RouteDetailsQuery : IQuery<int>
{
    public long Weight { get; set; }

    public long Size { get; set; }

    public long Length { get; set; }

    public long Width { get; set; }

    public Guid SourceId { get; set; }

    public Guid DestinationId { get; set; }
}
