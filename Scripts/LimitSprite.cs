using Godot;
using System;

public partial class LimitSprite : Sprite2D
{
    [Export]
    private float rotSpeed = 20;
    [Export]
    private float movForwardSpeed = 200;
    [Export]
    private float scale = 3;

    public override void _Ready()
    {
        base._Ready();
        Scale *= scale;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        Position += movForwardSpeed * (float)delta * Vector2.Right;
        Rotate(rotSpeed * (float)delta);
    }
}
