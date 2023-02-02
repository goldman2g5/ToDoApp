global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ToDoApp.Client;
using Syncfusion.Blazor;
using MudBlazor.Services;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkVFpHaVxdX2NLfUN/T2dYdV9yZCQ7a15RRnVfQF1iSH5Sd0VqUX9fdA==;Mgo+DSMBPh8sVXJ0S0J+XE9AflRBQmBWfFN0RnNQdVpxflREcC0sT3RfQF5jS35Sd0ViWH9XeHVTTw==;ORg4AjUWIQA/Gnt2VVhkQlFacldJXnxKYVF2R2BJdlR0dV9HYkwgOX1dQl9gSXxTd0RjWH5eeXxWR2Q=;MTA0Njc1NkAzMjMwMmUzNDJlMzBZNFVSR3c4NjNrRFcwTjRxeUtDMEJWUldtQW5ZZnF1SHBheTl4MUNSNzZNPQ==;MTA0Njc1N0AzMjMwMmUzNDJlMzBHQzlkbnE4Z2lwKzJIMkV6MkNtYUc2RFZSQ2FmZmp1ejNFbXJkOVlncEg0PQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFtKVmBWfUx0RWFab1d6cFRMYV5BNQtUQF1hSn5RdkRiWX5fcH1dT2JY;MTA0Njc1OUAzMjMwMmUzNDJlMzBYYjNMSkNndlFNeWtlUmRmWmFmZDhJWGhCWk5tcTBEZE14WFNtNmFLMWtzPQ==;MTA0Njc2MEAzMjMwMmUzNDJlMzBuNDJaWjdWY05FNVdlM2oyMmQ4QU9GeFRhMEhRNUQzMVd4bVhiaXV3c0MwPQ==;Mgo+DSMBMAY9C3t2VVhkQlFacldJXnxKYVF2R2BJdlR0dV9HYkwgOX1dQl9gSXxTd0RjWH5eeX1QRWQ=;MTA0Njc2MkAzMjMwMmUzNDJlMzBjOFBlYVZyWVdZTlVQSDk3NWI4c0lrSVhIZ0pyV0hYTG1QQ3VvYXo5MXZNPQ==;MTA0Njc2M0AzMjMwMmUzNDJlMzBsNnA1Y3NBSDNrblQ1Y2NFUW9vQnlTN2toQldWazdhQ3Q1R1J3c2F1VTZjPQ==;MTA0Njc2NEAzMjMwMmUzNDJlMzBYYjNMSkNndlFNeWtlUmRmWmFmZDhJWGhCWk5tcTBEZE14WFNtNmFLMWtzPQ==");

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = false; });
builder.Services.AddMudServices();


await builder.Build().RunAsync();
