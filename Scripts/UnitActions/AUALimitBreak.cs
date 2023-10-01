using Godot;
using System;

public abstract class AUALimitBreak : AUnitAction
{ 
    public abstract Func<Unit, AStatus> Inflict { get; }
    public abstract Func<Unit, AStatus> Gain { get; }
    public abstract bool Physical { get; }
    public abstract float Power { get; }
    public abstract Element Element { get; }

    public override void ActivateEffect(Unit enemy)
    {
        if (Power > 0)
        {
            thisUnit.QueueAnimation(new AnimLimit(),
                new AnimLimit.AnimLimitArgs(() =>
                    {
                        enemy.TakeDamage(thisUnit, Power, Element, Physical, "Pound");
                        if (Inflict != null)
                        {
                            enemy.AddStatus(Inflict(enemy));
                        }
                        if (Gain != null)
                        {
                            thisUnit.AddStatus(Gain(thisUnit));
                        }
                    },
                    thisUnit.Forward));
        }
    }
}
