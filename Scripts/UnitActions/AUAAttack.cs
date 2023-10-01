using Godot;
using System;

public abstract class AUAAttack<T> : AUAAttack where T : AStatus
{
    public abstract Func<Unit, T> NewT { get; }
    public override string StatusDesc
    {
        get
        {
            AStatus status = NewT?.Invoke(null) ?? null;
            return status != null ? "Apply " + status.Lifespan + " " + status.ShortName : "";
        }
    }

    public override void ActivateEffect(Unit enemy)
    {
        base.ActivateEffect(enemy);
        if (NewT != null)
        {
            enemy.AddStatus(NewT(enemy));
        }
    }
}

public abstract class AUAAttack : AUnitAction
{
    public abstract bool Physical { get; }
    public abstract float Power { get; }
    public abstract Element Element { get; }
    public abstract string VFXName { get; }
    public virtual string StatusDesc { get; } = "";

    public override void ActivateEffect(Unit enemy)
    {
        if (Power > 0)
        {
            thisUnit.QueueAnimation(new AnimAttack(), new AnimAttack.AnimAttackArgs(() => enemy.TakeDamage(thisUnit, Power, Element, Physical, VFXName), thisUnit.Forward));
        }
    }
}
