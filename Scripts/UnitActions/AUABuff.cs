using Godot;
using System;

public abstract class AUABuff<T> : AUnitAction where T : AStatus
{
    public abstract Func<T> NewT { get; }
    public abstract string VFXName { get; }

    public override void ActivateEffect(Unit enemy)
    {
        thisUnit.QueueAnimation(new AnimBuff(), new AnimBuff.AnimBuffArgs(VFXName, () => thisUnit.AddStatus(NewT()), thisUnit.Forward));
    }
}