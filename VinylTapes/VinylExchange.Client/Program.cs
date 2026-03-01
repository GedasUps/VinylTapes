using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VinylExchange.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7022/")
});

// Add simple auth state service so components can react to login/logout without a full authentication provider
builder.Services.AddSingleton<VinylExchange.Client.Services.AuthStateService>();

await builder.Build().RunAsync();
