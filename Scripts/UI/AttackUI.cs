using Godot;
using System;
using System.Collections.Generic;

public partial class AttackUI : Control
{
    [Export]
    private PackedScene attackButtonScene;
    [Export]
    private Node buttonsHolder;
    [Export]
    private float showHideTime;
    private Interpolator interpolator;
    private float shownHeight;
    private float hiddenHeight;
    private List<AttackButton> generatedButtons = new List<AttackButton>();

    public override void _Ready()
    {
        base._Ready();
        interpolator = new Interpolator();
        AddChild(interpolator);
        shownHeight = Position.Y;
        hiddenHeight = Position.Y + Size.Y;
        Position = new Vector2(Position.X, hiddenHeight);
    }

    public void ShowUI(Unit unit)
    {
        foreach (var item in unit.Actions)
        {
            AttackButton attackButton = attackButtonScene.Instantiate<AttackButton>();
            buttonsHolder.AddChild(attackButton);
            attackButton.Action = item;
            attackButton.Text = item.Name;
            attackButton.Disabled = true;
            attackButton.ButtonUp += () =>
            {
                unit.UseAction(attackButton.Action);
                HideUI();
            };
            generatedButtons.Add(attackButton);
        }
        interpolator.Interpolate(showHideTime,
            new Interpolator.InterpolateObject(
                a => Position = new Vector2(Position.X, a),
                hiddenHeight,
                shownHeight,
                Easing.EaseOutQuad));
        interpolator.OnFinish = () => generatedButtons.ForEach(a => a.Disabled = false);
    }

    private void HideUI()
    {
        generatedButtons.ForEach(a => a.Disabled = true);
        interpolator.Interpolate(showHideTime,
            new Interpolator.InterpolateObject(
                a => Position = new Vector2(Position.X, a),
                shownHeight,
                hiddenHeight,
                Easing.EaseInQuad));
        interpolator.OnFinish = () =>
        {
            generatedButtons.ForEach(a => a.QueueFree());
            generatedButtons.Clear();
        };
    }
}
