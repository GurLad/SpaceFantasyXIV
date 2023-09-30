using Godot;
using System;

public partial class StatusIconController : Node
{
    public static StatusIconController Current { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public Texture2D GetIcon(string name)
    {
        return GetNode<Sprite2D>(name).Texture;
    }
}
