using FoodFrenzy.Core.HttpClients;
using System.Net.Http.Headers;

namespace FoodFrenzy.Web.Startup;

public static class OpenAIChatRegistration
{
    public static IServiceCollection AddOpenAIChatClient(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = Bind<OpenAISettings>(configuration);
        services
            .Configure<OpenAISettings>(configuration.GetSection(nameof(OpenAISettings)))
            .AddHttpClient<IOpenAIChatClient, OpenAIChatClient>(nameof(OpenAIChatClient), client =>
            {
                client.BaseAddress = new Uri(settings.BaseUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {settings.ApiKey}");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });


        return services;
    }

    private static T Bind<T>(IConfiguration configuration)
    {
        var settings = Activator.CreateInstance<T>();
        var typeName = settings?.GetType().Name ?? throw new ArgumentException();
        if (!configuration.GetSection(typeName).Exists())
        {
            throw new ArgumentException($"{nameof(T)} settings could not be found in app configuration.");
        }

        configuration.GetSection(typeName).Bind(settings);
        return settings;
    }

    private class OpenAISettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = string.Empty;
    }
}

