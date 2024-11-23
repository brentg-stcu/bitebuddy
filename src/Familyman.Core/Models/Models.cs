namespace Familyman.Core.Models;

public class ChatMessage
{
    public ChatMessageType Role { get; set; }
    public string Content { get; set; } = string.Empty;
}

public enum ChatMessageType
{
    System,
    User,
    Assistant
}

public class Meal
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string FoodType { get; set; } = string.Empty;
    public int HealthinessRating { get; set; }
    public int ExpensivenessRating { get; set; }
}

public class ShoppingListItem
{
    public string Name { get; set; } = string.Empty;
    public decimal AveragePrice { get; set; }
    public int Quantity { get; set; }
    public string FoodGroup { get; set; } = string.Empty;
}