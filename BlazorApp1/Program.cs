using BlazingBooks.Shared.Interfaces;
using BlazingBooks.Web.Services;
using BlazorApp1.Clients;
using BlazorApp1.Components;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;
using Keycloak.AuthServices.Authentication;
using GamesBlazor.Shared.States;
//using Microsoft.Maui.Networking;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
var ApiUrl = builder.Configuration["ADDApiUrl"];
builder.Services.AddHttpClient("https", client => client.BaseAddress = new Uri(ApiUrl!));
builder.Services.AddSingleton<GamesClient>();
builder.Services.AddSingleton<GenresClient>();
builder.Services.AddSingleton<ICommonService, CommonService>();
builder.Services.AddSingleton<ActivityIndicatorState>();
//builder.Services.AddSingleton<IConnectivity, BlazorConnectivity>();
//builder.Services.AddScoped<ILoginService, LoginService>();
//builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
//builder.Services.AddKeycloakAuthentication(builder.Configuration);
//builder.Services.AddAuthorization();


builder.Services.AddBlazoredSessionStorage();
builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddAdditionalAssemblies(typeof(GamesRazorClasslibrary._Imports).Assembly)
    .AddInteractiveServerRenderMode();
app.Run();
//public class BlazorConnectivity : IConnectivity
//{
//    public IEnumerable<ConnectionProfile> ConnectionProfiles => null;

//    public NetworkAccess NetworkAccess => NetworkAccess.Internet;

//    public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged;
//}
