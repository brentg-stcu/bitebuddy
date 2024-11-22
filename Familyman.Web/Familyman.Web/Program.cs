using Familyman.Core.Services;
using Familyman.Web.Components;
using Familyman.Web.Startup;

var builder = WebApplication.CreateBuilder(args);

// App registrations
builder.Configuration.AddConfiguration(builder);
builder.Services
    .AddOpenAIChatClient(builder.Configuration)
    .AddScoped<IMealPlannerService, MealPlannerService>();



// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Familyman.Web.Client._Imports).Assembly);

app.Run();
