using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoutePlanning.Application.Locations.Queries.SelectableLocationList;

namespace RoutePlanning.Client.Web.Api;

[Route("api/locations")]
[ApiController]
public sealed class LocationController : ControllerBase
{
    private readonly IMediator mediator;

    public LocationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<SelectableLocation>>> GetSelectableLocations(CancellationToken cancellationToken)
    {
        var query = new SelectableLocationListQuery();
        var locationDetails = await mediator.Send(query, cancellationToken);
        return Ok(locationDetails);
    }
}
