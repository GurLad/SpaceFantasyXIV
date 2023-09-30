using Godot;
using System;

public partial class TurnController : Node2D
{
    // Exports
    [Export]
    private Unit Player;
    [Export]
    private Unit Enemy;

    public override void _Ready()
    {
        base._Ready();
        Player.FinishedTurn += () => Enemy.BeginTurn();
        Enemy.FinishedTurn += () => Player.BeginTurn();
        // TEMP
        Player.BeginTurn();
    }
}
