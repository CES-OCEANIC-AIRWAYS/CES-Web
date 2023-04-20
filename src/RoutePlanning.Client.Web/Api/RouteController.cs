using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoutePlanning.Application.Locations.Commands.CreateTwoWayConnection;
using RoutePlanning.Application.Locations.Queries.Routes;
using RoutePlanning.Client.Web.Authorization;

namespace RoutePlanning.Client.Web.Api;

[Route("api/routes")]
[ApiController]
public sealed class RoutesController : ControllerBase
{
    private readonly IMediator mediator;

    public RoutesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("[action]")]
    public Task<string> HelloWorld()
    {
        return Task.FromResult("Hello World!");
    }

    [HttpPost("[action]")]
    public async Task AddTwoWayConnection(CreateTwoWayConnectionCommand command)
    {
        await mediator.Send(command);
    }

    [Route("find-routes")]
    [HttpPost]
    public async Task<ActionResult<List<RouteDetails>>> GetRouteList(RouteDetailsQuery query, CancellationToken cancellationToken)
    {
        var routeResults = await mediator.Send(query, cancellationToken);
        return Ok(routeResults);
    }
}
