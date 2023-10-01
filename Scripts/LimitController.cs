using Godot;
using System;

public partial class LimitController : Node
{
    public static LimitController Current { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public LimitSprite GetLimit(string name)
    {
        return (LimitSprite)GetNode<LimitSprite>(name).Duplicate();
    }
}
