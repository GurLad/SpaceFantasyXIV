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
    [Export]
    private PackedScene statsDisplayScene;
    // Properties
    private bool Idling = true;

    public override void _Ready()
    {
        base._Ready();
        player.Enemy = enemy;
        enemy.Enemy = player;
            player.BeganTurn += BeginPlayerTurn;
            enemy.BeganTurn += BeginEnemyTurn;
        for (int i = 0; i < 2; i++)
        {
            Unit unit = i == 0 ? player : enemy;
            unit.FinishedTurn += () => Idling = true;
            StatsDisplay statsDisplay = statsDisplayScene.Instantiate<StatsDisplay>();
            statsDisplay.unit = unit;
            AddChild(statsDisplay);
            unit.TookDamage += () => statsDisplay.UpdateHealth();
        }
        // Temp?
        player.ATB = enemy.ATB = 100;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (Idling)
        {
            if (player.ATB >= 100)
            {
                player.BeginTurn();
                Idling = false;
                return;
            }
            if (enemy.ATB >= 100)
            {
                enemy.BeginTurn();
                Idling = false;
                return;
            }
            player.ATB += player.Stats.GetATBIncrease(delta);
            enemy.ATB += player.Stats.GetATBIncrease(delta);
        }
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
