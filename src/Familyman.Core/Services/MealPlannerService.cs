namespace Familyman.Core.Services;

using Familyman.Core.HttpClients;
using Familyman.Core.Models;
using System.Text.Json;
using System.Threading.Tasks;

public interface IMealPlannerService
{
    Task<List<Meal>> GenerateMealPlanAsync(string preferences);
    Task<IEnumerable<ShoppingListItem>> GenerateShoppingList(IEnumerable<Meal> meals);
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
        return JsonSerializer.Deserialize<List<Meal>>(
            completion?.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
    }

    public async Task<IEnumerable<ShoppingListItem>> GenerateShoppingList(IEnumerable<Meal> meals)
    {
        var shoppingList = string.Join("\n - ", meals.Select(m => m.Name));
        var messages = new List<ChatMessage>()
        {
            new()
            {
                Role = ChatMessageType.System,
                Content = "You are a culinary assistant that creates shopping lists grouped into logical categories. You calculate approximate costs for each item based on general market prices. Your goal is to stick closely to the ingredients in the recipes and provide a practical shopping list. No long descriptions or irrelevant text. Use a structured JSON format. " +
                            "The response should be an array of ShoppingListItem objects. Each object must contain the following properties: " +
                            "name (string), averagePrice (decimal), quantity (number), and foodGroup (string)."
            },
            new()
            {
                Role = ChatMessageType.User,
                Content = $"Create a shopping list based on the following recipes:\n\n{shoppingList}\n\n" + 
                    "The shopping list should:\n- Group items into logical food groups (e.g., Vegetables, Grains, Spices, etc.).\n" + 
                    "- Include approximate costs for each item.\n" + 
                    "- Stick as closely as possible to the ingredients in the recipes.\n\n" +
                    "The recipes should:\n" +
                    "- Be highly rated and come from quality sources.\n" +
                    "- Provide only the essential details (bullet-point format).\n" +
                    "- Include a list of ingredients and concise preparation instructions.\n" +
                    "Ensure the response strictly follows the specified JSON structure."
            }
        };

        var completion = await _chatClient.SendMessageAsync(messages);
        return JsonSerializer.Deserialize<List<ShoppingListItem>>(
            completion?.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
    }
}