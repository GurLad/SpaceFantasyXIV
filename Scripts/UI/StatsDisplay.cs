using Godot;
using System;
using System.Collections.Generic;

public partial class StatsDisplay : Control
{
    // Exports
    [ExportCategory("Bars")]
    [Export]
    private TextureRect hpBar;
    [Export]
    private TextureRect atbBar;
    [Export]
    private int hpMax;
    [Export]
    private float atbMax;
    //[Export]
    //private float lerpStrength;
    [Export]
    private float updateTime;
    [ExportCategory("Status")]
    [Export]
    private HBoxContainer statusContainer;
    [Export]
    private PackedScene statusIconScene;
    [ExportCategory("Both")]
    [Export]
    private Vector2 offset;
    // Properties
    public Unit unit;
    private float hp;
    private float atb;
    private float baseScale;
    private List<StatusUI> statusIcons = new List<StatusUI>();
    private Interpolator interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        baseScale = hpBar.Scale.X;
        Position = unit.Position + offset;
        hp = unit.Health;
        atb = unit.ATB;
        AddChild(interpolator);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        //hp = Lerp(hpBar, hp, unit.Health, hpMax, (float)delta);
        //Lerp(atbBar, atb, unit.ATB, atbMax, (float)delta);
        atb = unit.ATB;
        atbBar.Scale = new Vector2(baseScale * unit.ATB / atbMax, atbBar.Scale.Y);
    }

    private void UpdateHealth()
    {
        interpolator.Interpolate(updateTime,
            new Interpolator.InterpolateObject(
                a => hpBar.Scale = new Vector2(baseScale * (hp = a) / hpMax, hpBar.Scale.Y),
                hp,
                Mathf.Max(0, unit.Health)));
    }

    public void UpdateDisplay()
    {
        UpdateHealth();
        statusIcons.ForEach(a => a.QueueFree());
        statusIcons.Clear();
        unit.Statuses.ForEach(a =>
        {
            StatusUI newIcon = statusIconScene.Instantiate<StatusUI>();
            statusContainer.AddChild(newIcon);
            newIcon.Update(a.Name, a.Lifespan);
            statusIcons.Add(newIcon);
        });
    }

    //private float Lerp(TextureRect rect, float current, float target, float max, float delta)
    //{
    //    current = Mathf.Lerp(current, target, Mathf.Min(1, lerpStrength * delta));
    //    rect.Scale = new Vector2(baseScale * current / max, rect.Scale.Y);
    //    return current;
    //}
}
