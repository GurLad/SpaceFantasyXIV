using Godot;
using System;

public partial class StatsDisplay : Control
{
    // Exports
    [Export]
    private TextureRect hpBar;
    [Export]
    private TextureRect atbBar;
    [Export]
    private int hpMax;
    [Export]
    private float atbMax;
    [Export]
    private float lerpStrength;
    [Export]
    private float updateTime;
    [Export]
    private Vector2 offset;
    // Properties
    public Unit unit;
    private float hp;
    private float atb;
    private float baseScale;
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

    public void UpdateHealth()
    {
        interpolator.Interpolate(updateTime,
            new Interpolator.InterpolateObject(
                a => hpBar.Scale = new Vector2(baseScale * (hp = a) / hpMax, hpBar.Scale.Y),
                hp,
                unit.Health));
    }

    private float Lerp(TextureRect rect, float current, float target, float max, float delta)
    {
        current = Mathf.Lerp(current, target, Mathf.Min(1, lerpStrength * delta));
        rect.Scale = new Vector2(baseScale * current / max, rect.Scale.Y);
        return current;
    }
}
