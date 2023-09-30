using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Unit : Node2D
{
    private enum State { Wait, Upkeep, Main, Endstep }

    // Exports
    [Export]
    private Godot.Collections.Dictionary<string, NodePath> pathSprites;
    [Export]
    private string initSprite;
    [Export]
    private PackedScene damageTextScene;
    [Export]
    public bool Forward;
    // Properties
    public int Health = 9999;
    public float ATB = 0;
    public Stats Stats = new Stats();
    public List<StatsMod> Modifiers = new List<StatsMod>();
    public List<AStatus> Statuses = new List<AStatus>();
    public List<AUnitAction> Actions { get; } = new List<AUnitAction>();
    public Unit Enemy;
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
    private State state = State.Wait;
    // Animations
    private Dictionary<string, UnitSprite> sprites = new Dictionary<string, UnitSprite>();
    private string currentSprite;
    private AAnimation currentAnimation;
    private Queue<Action> actionQueue = new Queue<Action>();

    private Interpolator interpolator = new Interpolator();

    [Signal]
    public delegate void FinishedTurnEventHandler();

    [Signal]
    public delegate void BeganTurnEventHandler();

    [Signal]
    public delegate void TookDamageEventHandler();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
        pathSprites.Keys.ToList().ForEach(a => sprites.Add(a, GetNode<UnitSprite>(pathSprites[a])));
        SetSprite(currentSprite = initSprite);
        // TEMP - Add UASaturnAttack1
        AttachAction(new UAMercuryAttack1());
        AttachAction(new UAMercuryBuff1());
        AttachAction(new UASaturnAttack1());
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
        else if (currentAnimation == null && actionQueue.Count <= 0)
        {
            switch (state)
            {
                case State.Wait:
                    // Not our turn - do nothing
                    break;
                case State.Upkeep:
                    // Use AI/show player UI
                    EmitSignal(SignalName.BeganTurn);
                    state = State.Wait;
                    break;
                case State.Main:
                    // Activate end turn statuses
                    Statuses.ForEach(a => a.EndTurn());
                    state = State.Endstep;
                    break;
                case State.Endstep:
                    // Finish turn
                    Statuses.ForEach(a => a.Lifespan--);
                    Statuses = Statuses.FindAll(a => a.Lifespan > 0);
                    ATB = 0;
                    EmitSignal(SignalName.FinishedTurn);
                    state = State.Wait;
                    break;
                default:
                    break;
            }
        }
    }

    public void SetSprite(string name)
    {
        sprites[currentSprite].Visible = false;
        sprites[currentSprite = name].Visible = true;
    }

    public void TakeDamage(Stats attackerStats, float amount, Element element, bool physical, string vfx)
    {
        int damageTaken = physical ? attackerStats.GetPhysDamage(finalStats, amount, element) : attackerStats.GetMagDamage(finalStats, amount, element);
        Health -= damageTaken;
        QueueAnimation(new AnimTakeDamage(), new AnimTakeDamage.AnimTakeDamageArgs(vfx, Forward));
        DamageText damageText = damageTextScene.Instantiate<DamageText>();
        AddChild(damageText);
        damageText.Display(damageTaken, Position);
        EmitSignal(SignalName.TookDamage);
        // TEMP
        QueueAnimation(new AnimRecoverFromDamage(), new AnimRecoverFromDamage.AnimRecoverFromDamageArgs(Forward));
    }

    public void TakeDamage(Unit attacker, float amount, Element element, bool physical, string vfx)
    {
        TakeDamage(attacker.finalStats, amount, element, physical, vfx);
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

    public void BeginTurn()
    {
        Statuses.ForEach(a => a.BeginTurn());
        state = State.Upkeep;
    }

    // Animations

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

    // Actions

    public void AttachAction(AUnitAction unitAction)
    {
        unitAction.AttachToUnit(this);
        Actions.Add(unitAction);
        Actions.Sort((a, b) => a.SortOrder > b.SortOrder ? 1 : (a.SortOrder < b.SortOrder ? -1 : 0));
    }

    public bool CanUseAction<T>() where T : AUnitAction
    {
        T target = (T)Actions.Find(a => a is T);
        return target != null;
    }

    public void RemoveAction<T>() where T : AUnitAction
    {
        T target = (T)Actions.Find(a => a is T);
        if (target != null)
        {
            Actions.Remove(target);
        }
    }

    public void UseAction<T>() where T : AUnitAction
    {
        T target = (T)Actions.Find(a => a is T);
        if (target != null)
        {
            target.ActivateEffect(Enemy);
            QueueImmediateAction(() => state = State.Main);
        }
        else
        {
            throw new Exception("Unit can't use " + nameof(T) + "!");
        }
    }

    public void UseAction(AUnitAction action)
    {
        action.ActivateEffect(Enemy);
        QueueImmediateAction(() => state = State.Main);
    }
}
