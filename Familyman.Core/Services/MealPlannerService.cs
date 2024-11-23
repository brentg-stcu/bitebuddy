namespace Familyman.Core.Services;

using Familyman.Core.HttpClients;
using Familyman.Core.Models;
using System.Text.Json;
using System.Threading.Tasks;

public interface IMealPlannerService
{
    Task<List<Meal>> GenerateMealPlanAsync(string preferences);
}

public class MealPlannerService : IMealPlannerService
{
    private readonly IOpenAIChatClient _chatClient;

    public MealPlannerService(IOpenAIChatClient openAIChatClient)
    {
        _chatClient = openAIChatClient;
    }

    public async Task<List<Meal>> GenerateMealPlanAsync(string preferences)
    {
        var messages = new List<ChatMessage>()
        {
            new()
            {
                Role = ChatMessageType.System,
                Content = "You are a culinary expert AI that generates meal plans tailored to user preferences, in a structured JSON format. " +
                            "The response should be an array of meal objects. Each object must contain the following properties: " +
                            "name (string), description (string), foodType (string), healthinessRating (number between 1-5), and expensivenessRating (number between 1-5)."
            },
            new()
            {
                Role = ChatMessageType.User,
                Content = $"Generate a meal plan with the following preferences: {preferences}. Ensure the response strictly follows the specified JSON structure."
            }
        };

        var completion = await _chatClient.SendMessageAsync(messages);
        string formattedJson = JsonSerializer.Serialize(completion.RootElement, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        Console.WriteLine(formattedJson);
        
        return JsonSerializer.Deserialize<List<Meal>>(completion) ?? [];


        //return completion?.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
    }
}

public class Meal
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FoodType { get; set; } = string.Empty;
    public int HealthinessRating { get; set; } // 1 (least healthy) to 5 (most healthy)
    public int ExpensivenessRating { get; set; } // 1 (cheapest) to 5 (most expensive)
}