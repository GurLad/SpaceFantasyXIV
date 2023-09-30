using Godot;
using System;

public partial class VFXController : Node
{
    public static VFXController Current { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public AnimatedSprite2D GetVFX(string name)
    {
        return GetNode<AnimatedSprite2D>(name);
    }
}
