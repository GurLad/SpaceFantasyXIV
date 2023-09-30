using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Unit : Node2D
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
    private AAnimation currentAnimation;
    private Queue<Action> actionQueue = new Queue<Action>();

    private Interpolator interpolator = new Interpolator();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
        pathSprites.Keys.ToList().ForEach(a => sprites.Add(a, GetNode<UnitSprite>(pathSprites[a])));
        SetSprite(currentSprite = initSprite);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (currentAnimation?.Done ?? actionQueue.Count > 0)
        {
            if (currentAnimation != null)
            {
                currentAnimation = null;
            }
            if (actionQueue.Count > 0)
            {
                actionQueue.Dequeue().Invoke();
            }
        }
    }

    public void SetSprite(string name)
    {
        sprites[currentSprite].Visible = false;
        sprites[currentSprite = name].Visible = true;
    }

    public void TakeDamage(Stats attackerStats, float amount, Element element, bool physical)
    {
        Health -= physical ? attackerStats.GetPhysDamage(finalStats, amount, element) : attackerStats.GetMagDamage(finalStats, amount, element);
        SetSpriteAnimation(UnitSprite.Animation.Hurt);
        interpolator.Interpolate(1, new Interpolator.InterpolateObject(
            SetSpriteAnimation,
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

    public void QueueAnimation<T, S>(T animation, S animationArgs) where S : AAnimationArgs where T : AAnimation<S>
    {
        actionQueue.Enqueue(() =>
        {
            currentAnimation = animation.Begin(this, interpolator, animationArgs);
        });
    }

    public void QueueImmediateAction(Action action)
    {
        actionQueue.Enqueue(action);
    }

    public void SetSpriteAnimation(UnitSprite.Animation animation)
    {
        sprites[currentSprite].SetAnimation(animation);
    }
}
