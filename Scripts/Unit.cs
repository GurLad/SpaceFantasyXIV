using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Unit : Node
{
    // Exports
    [Export]
    private Godot.Collections.Dictionary<string, NodePath> pathSprites;
    [Export]
    private string initSprite;
    // Properties
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
    private Dictionary<string, UnitSprite> sprites = new Dictionary<string, UnitSprite>();
    private string currentSprite;
        
    private Interpolator interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
        pathSprites.Keys.ToList().ForEach(a => sprites.Add(a, GetNode<UnitSprite>(pathSprites[a])));
        SetSprite(currentSprite = initSprite);
    }

    public void SetSprite(string name)
    {
        sprites[currentSprite].Visible = false;
        sprites[currentSprite = name].Visible = true;
    }

    public void TakeDamage(Stats attackerStats, float amount, Element element, bool physical)
    {
        Health -= physical ? attackerStats.GetPhysDamage(finalStats, amount, element) : attackerStats.GetMagDamage(finalStats, amount, element);
        SetAnimation(UnitSprite.Animation.Hurt);
        interpolator.Interpolate(1, new Interpolator.InterpolateObject(
            SetAnimation,
            UnitSprite.Animation.Hurt,
            UnitSprite.Animation.Idle));
    }

    public void TakeDamage(Unit attacker, float amount, Element element, bool physical)
    {
        TakeDamage(attacker.finalStats, amount, element, physical);
    }

    public void AddStatus<T>(T newT) where T : AStatus
    {
        T current = (T)Statuses.Find(a => a is T);
        if (current != null)
        {
            current.Lifespan += newT.Lifespan;
        }
        else
        {
            Statuses.Add(newT);
        }
    }

    private void SetAnimation(UnitSprite.Animation animation)
    {
        sprites[currentSprite].SetAnimation(animation);
    }
}
