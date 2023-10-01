using Godot;
using System;

public partial class PortraitController : Node
{
    public static PortraitController Current { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public Texture2D GetPortrait(string character, string emotion = "Normal")
    {
        return GetNode<Sprite2D>(character + "/" + emotion).Texture;
    }
}
