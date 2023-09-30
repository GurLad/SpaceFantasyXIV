using Godot;
using System;

public abstract class AUAAttack<T> : AUnitAction where T : AStatus
{
    public abstract bool Physical { get; }
    public abstract float Power { get; }
    public abstract Element Element { get; }
    public abstract Func<T> NewT { get; }
    public abstract string VFXName { get; }

    public override void ActivateEffect(Unit enemy)
    {
        if (Power > 0)
        {
            thisUnit.QueueAnimation(new AnimAttack(), new AnimAttack.AnimAttackArgs(() => enemy.TakeDamage(thisUnit, Power, Element, Physical, VFXName), thisUnit.Forward));
        }
        if (NewT != null)
        {
            enemy.AddStatus(NewT());
        }
    }
}

public abstract class AUAAttack : AUAAttack<AStatus>
{
    public override Func<AStatus> NewT { get; } = null;
}
