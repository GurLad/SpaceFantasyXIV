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
    private ConversationPlayer conversationPlayer;
    [Export]
    private PlanetSelect planetSelectUI;
    [Export]
    private PackedScene statsDisplayScene;
    [Export]
    private Control statsDisplayHolder;
    // Properties
    private bool Idling = true;
    private bool Paused = false;
    private bool hardcodedPostFirst = true;

    public override void _Ready()
    {
        base._Ready();
        FormController.Reset();
        player.SetForm(FormController.PlayerForms[2]); // Hardcoding~
        enemy.SetForm(FormController.GetNextBossForm());
        player.Enemy = enemy;
        enemy.Enemy = player;
        player.BeganTurn += BeginPlayerTurn;
        enemy.BeganTurn += BeginEnemyTurn;
        conversationPlayer.FinishedConversation += PostConversation;
        planetSelectUI.MidSelectedPlanet += PostMidSelect;
        planetSelectUI.FinishedSelection += PostSelect;
        for (int i = 0; i < 2; i++)
        {
            Unit unit = i == 0 ? player : enemy;
            unit.FinishedTurn += () => Idling = true;
            unit.Died += () => UnitDeath(unit);
            StatsDisplay statsDisplay = statsDisplayScene.Instantiate<StatsDisplay>();
            statsDisplay.unit = unit;
            statsDisplayHolder.AddChild(statsDisplay);
            unit.StateChanged += () => statsDisplay.UpdateDisplay();
        }
        // Temp?
        player.ATB = enemy.ATB = 100;
        Paused = true;
        conversationPlayer.BeginConversation(ConversationController.Current.GetConversation("Init"));
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (Idling && !Paused)
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

    private void UnitDeath(Unit unit)
    {
        Paused = true;
        unit.QueueAnimation(new AnimDie(), new AnimDie.AnimDieArgs());
        // TBA: change phase
        if (unit.Forward) // Player
        {
            unit.QueueImmediateAction(() =>
            {
                planetSelectUI.Begin();
            });
        }
        else
        {
            unit.QueueImmediateAction(() => unit.SetForm(FormController.GetNextBossForm()));
            unit.QueueImmediateAction(() =>
                {
                    unit.RemoveDissolveFromSpriteAnimation();
                    unit.Health = 9999;
                    unit.ATB = 100;
                    unit.EmitSignal(Unit.SignalName.StateChanged);
                });
            unit.QueueAnimation(new AnimRecoverFromDamage(), new AnimRecoverFromDamage.AnimRecoverFromDamageArgs(unit.Forward));
            unit.QueueImmediateAction(() =>
            {
                conversationPlayer.BeginConversation(unit.Form.Name);
            });
        }
    }

    private void PostConversation()
    {
        Paused = false;
        if (hardcodedPostFirst)
        {
            Paused = true;
            planetSelectUI.Begin();
        }
    }

    private void PostMidSelect(string name)
    {
        int formID = FormController.PlayerForms.FindIndex(a => a.Name == name);
        FormController.LivingPlayerForms[formID] = false;
        player.SetForm(FormController.PlayerForms[formID]);
    }

    private void PostSelect()
    {
        player.QueueImmediateAction(() =>
        {
            player.RemoveDissolveFromSpriteAnimation();
            player.Health = 9999;
            player.ATB = 100;
            player.EmitSignal(Unit.SignalName.StateChanged);
        });
        if (!hardcodedPostFirst)
        {
            player.QueueAnimation(new AnimRecoverFromDamage(), new AnimRecoverFromDamage.AnimRecoverFromDamageArgs(player.Forward));
        }
        else
        {
            hardcodedPostFirst = false;
        }
        player.QueueImmediateAction(() =>
            conversationPlayer.BeginConversation(ConversationController.Current.GetConversation(player.Form.Name)));
    }
}
