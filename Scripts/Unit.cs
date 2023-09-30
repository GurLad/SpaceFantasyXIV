using Godot;
using System;
using System.Collections.Generic;

public partial class Unit : Node
{
    public int Health;
    public Stats Stats;
    public List<StatsMod> Modifiers;
    public List<AStatus> Statuses;
    private Stats finalStats
    {
        get
        {
            Stats result = Stats;
            Modifiers.ForEach(a => result = a.Apply(result));
            Statuses.ForEach(a => result = a.StatsMod?.Apply(result) ?? result);
            return result;
        }
    }
        
    private Interpolator interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
    }

    public void TakeDamage(Stats attackerStats, float amount, Element element, bool physical)
    {
        Health -= physical ? attackerStats.GetPhysDamage(finalStats, amount, element) : attackerStats.GetMagDamage(finalStats, amount, element);
    }
}
