using Godot;
using System;

public partial class ConversationController : Node
{
    public static ConversationController Current { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public string GetConversation(string name)
    {
        return GetNode<Label>(name).Text;
    }
}
