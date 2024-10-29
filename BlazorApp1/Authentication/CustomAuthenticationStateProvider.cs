using BlazorApp1.Extensions;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorApp1.Authentication
{
    //public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    //{
    //    private readonly ISessionStorageService sessionStorage;
    //    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    //    public CustomAuthenticationStateProvider(ISessionStorageService sessionStorage)
    //    {
    //        this.sessionStorage = sessionStorage;
    //    }
    //    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //    {
    //        try
    //        {
    //            var userSession = await sessionStorage.ReadEncryptedItemAsync<UserSession>("UserSession");
    //            if (userSession == null)
    //            {
    //                return await Task.FromResult(new AuthenticationState(_anonymous));
    //            }
    //            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name, userSession.Name!),
    //        new Claim(ClaimTypes.Role , userSession.Role!)
    //    }, "JwtAuth"));
    //            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    //        }
    //        catch
    //        {
    //            return await Task.FromResult(new AuthenticationState(_anonymous));
    //        }
    //    }

    //    public async Task UpdateAuthenticationState(UserSession? userSession)
    //    {
    //        ClaimsPrincipal claimsPrincipal;
    //        if (userSession != null)
    //        {
    //            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name, userSession.Name!),
    //        new Claim(ClaimTypes.Role , userSession.Role!)
    //    }, "JwtAuth"));
    //            await sessionStorage.SaveItemEncryptedAsync("UserSession", userSession);
    //        }
    //        else
    //        {
    //            claimsPrincipal = _anonymous;
    //            await sessionStorage.RemoveItemAsync("UserSession");
    //        }

    //        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    //    }
    //}
}
