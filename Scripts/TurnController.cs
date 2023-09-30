using Godot;
using System;

public partial class TurnController : Node
{
    // Exports
    [Export]
    private Unit Player;
    [Export]
    private Unit Enemy;

    public override void _Ready()
    {
        base._Ready();
        Player.Enemy = Enemy;
        Enemy.Enemy = Player;
        Player.FinishedTurn += () => Enemy.BeginTurn();
        Enemy.FinishedTurn += () => Player.BeginTurn();
        // TEMP
        Player.BeginTurn();
    }
}
