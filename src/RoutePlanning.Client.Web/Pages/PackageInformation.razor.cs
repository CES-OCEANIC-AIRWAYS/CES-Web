
using MediatR;
using Microsoft.AspNetCore.Components;
using RoutePlanning.Application.Locations.Queries.SelectableLocationList;
namespace RoutePlanning.Client.Web.Pages;

public partial class PackageInformation
{
    private List<SelectableLocation> locations = new List<SelectableLocation>();

    [Inject]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private IMediator mediator { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var query = new SelectableLocationListQuery();
        var data = await mediator.Send(query);
        locations = data.ToList();
    }
}
