using MediatR;
using Microsoft.AspNetCore.Components;
using RoutePlanning.Application.Users.Queries.AuthenticatedUser;
using RoutePlanning.Client.Web.Authentication;

namespace RoutePlanning.Client.Web.Pages;

public sealed partial class Login
{
    private string Username { get; set; } = string.Empty;
    private string Password { get; set; } = string.Empty;
    private AuthenticatedUser? User { get; set; }
    private bool ShowAuthError { get; set; }

    [Inject] private SimpleAuthenticationStateProvider AuthStateProvider { get; set; } = default!;

    [Inject] private IMediator Mediator { get; set; } = default!;

    public async Task Signin()
    {
        User = await Mediator.Send(new AuthenticatedUserQuery(Username, Password), CancellationToken.None);

        ShowAuthError = User is null;

        if (User is not null)
        {
            await AuthStateProvider.SetAuthenticationStateAsync(new UserSession(User.Username));
            StateHasChanged();
            NavigationManager.NavigateTo("/");
        }
    }
}
