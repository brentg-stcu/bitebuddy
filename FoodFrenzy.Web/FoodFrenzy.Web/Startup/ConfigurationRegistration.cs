using System.Reflection;

namespace FoodFrenzy.Web.Startup;

public static class ConfigurationRegistration
{
    public static ConfigurationManager AddConfiguration(
        this ConfigurationManager configuration,
        WebApplicationBuilder builder)
    {
        var applicationName = builder.Configuration.GetValue<string>("ApplicationSettings:Name");
        var environmentName = builder.Configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT");

        configuration
            .AddEnvironmentVariables()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
            .Build();

        return configuration;
    }
}