using Godot;
using System;

public partial class TurnController : Node
{
    // Exports
    [Export]
    private Unit player;
    [Export]
    private Unit enemy;
    [Export]
    private AttackUI attackUI;

    public override void _Ready()
    {
        base._Ready();
        player.Enemy = enemy;
        enemy.Enemy = player;
        player.BeganTurn += BeginPlayerTurn;
        enemy.BeganTurn += BeginEnemyTurn;
        player.FinishedTurn += () => enemy.BeginTurn();
        enemy.FinishedTurn += () => player.BeginTurn();
        // TEMP
        player.BeginTurn();
    }

    private void BeginPlayerTurn()
    {
        attackUI.ShowUI(player);
    }

    private void BeginEnemyTurn()
    {
        // TEMP
        enemy.UseAction(enemy.Actions[ExtensionMethods.RNG.Next(0, enemy.Actions.Count)]);
    }
}
