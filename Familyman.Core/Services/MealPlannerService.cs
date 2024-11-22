namespace Familyman.Core.Services;

using Familyman.Core.HttpClients;
using Familyman.Core.Models;
using System.Threading.Tasks;

public interface IMealPlannerService
{
    Task<string> GenerateMealPlanAsync(string preferences);
}

public class MealPlannerService : IMealPlannerService
{
    private readonly IOpenAIChatClient _chatClient;

    public MealPlannerService(IOpenAIChatClient openAIChatClient)
    {
        _chatClient = openAIChatClient;
    }

    public async Task<string> GenerateMealPlanAsync(string preferences)
    {
        var messages = new List<ChatMessage>()
        {
            new()
            {
                Role = ChatMessageType.System,
                Content = "You are a culinary expert AI that generates diverse meal plans tailored to user preferences."
            },
            new()
            {
                Role = ChatMessageType.User,
                Content = $"Generate a meal plan with the following preferences: {preferences}"
            }
        };

        var completion = await _chatClient.SendMessageAsync(messages);
        return completion?.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
    }
}
