﻿namespace Familyman.Core.Models;

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