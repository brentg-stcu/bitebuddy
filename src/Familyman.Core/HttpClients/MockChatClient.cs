using Familyman.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Familyman.Core.HttpClients;

public class MockChatClient : IOpenAIChatClient
{
    private readonly HttpClient _httpClient;

    public MockChatClient(HttpClient httpClient, IFileLoggerService logToFileService)
    {
        _httpClient = httpClient;
    }

    public Task<JsonDocument> SendMessageAsync(IEnumerable<ChatMessage> messages, string model = "gpt-4", double temperature = 0.7) =>
        Task.Run(() => JsonDocument.Parse(CreateResponse()));

    private string CreateResponse() =>
        "{\r\n  \"id\": \"chatcmpl-AWnNueOGiP0zlOAQrIY3BHI6dlnFQ\",\r\n  \"object\": \"chat.completion\",\r\n  \"created\": 1732379694,\r\n  \"model\": \"gpt-4-0613\",\r\n  \"choices\": [\r\n    {\r\n      \"index\": 0,\r\n      \"message\": {\r\n        \"role\": \"assistant\",\r\n        \"content\": \"[\\n  {\\n    \\\"name\\\": \\\"Traditional Beef Tacos\\\",\\n    \\\"description\\\": \\\"Delicious tacos made with ground beef, shredded lettuce, diced tomatoes, shredded cheese, and served with salsa and sour cream.\\\",\\n    \\\"foodType\\\": \\\"Mexican\\\",\\n    \\\"healthinessRating\\\": 3,\\n    \\\"expensivenessRating\\\": 2\\n  },\\n  {\\n    \\\"name\\\": \\\"Fish Tacos with Cilantro Lime Sauce\\\",\\n    \\\"description\\\": \\\"Tacos filled with crispy fish, cabbage slaw, and topped with a homemade cilantro lime sauce.\\\",\\n    \\\"foodType\\\": \\\"Mexican\\\",\\n    \\\"healthinessRating\\\": 4,\\n    \\\"expensivenessRating\\\": 3\\n  },\\n  {\\n    \\\"name\\\": \\\"Vegan Tofu Tacos\\\",\\n    \\\"description\\\": \\\"Healthy and delicious tacos made with tofu, black beans, corn, and topped with avocado and salsa.\\\",\\n    \\\"foodType\\\": \\\"Mexican\\\",\\n    \\\"healthinessRating\\\": 5,\\n    \\\"expensivenessRating\\\": 2\\n  },\\n  {\\n    \\\"name\\\": \\\"Chicken Fajita Tacos\\\",\\n    \\\"description\\\": \\\"Tacos filled with marinated chicken, bell peppers, onions, and served with a side of guacamole.\\\",\\n    \\\"foodType\\\": \\\"Mexican\\\",\\n    \\\"healthinessRating\\\": 3,\\n    \\\"expensivenessRating\\\": 2\\n  },\\n  {\\n    \\\"name\\\": \\\"Shrimp Tacos with Mango Salsa\\\",\\n    \\\"description\\\": \\\"Tacos filled with grilled shrimp and topped with a homemade mango salsa.\\\",\\n    \\\"foodType\\\": \\\"Mexican\\\",\\n    \\\"healthinessRating\\\": 4,\\n    \\\"expensivenessRating\\\": 4\\n  }\\n]\",\r\n        \"refusal\": null\r\n      },\r\n      \"logprobs\": null,\r\n      \"finish_reason\": \"stop\"\r\n    }\r\n  ],\r\n  \"usage\": {\r\n    \"prompt_tokens\": 108,\r\n    \"completion_tokens\": 354,\r\n    \"total_tokens\": 462,\r\n    \"prompt_tokens_details\": {\r\n      \"cached_tokens\": 0,\r\n      \"audio_tokens\": 0\r\n    },\r\n    \"completion_tokens_details\": {\r\n      \"reasoning_tokens\": 0,\r\n      \"audio_tokens\": 0,\r\n      \"accepted_prediction_tokens\": 0,\r\n      \"rejected_prediction_tokens\": 0\r\n    }\r\n  },\r\n  \"system_fingerprint\": null\r\n}\r\n";
}
