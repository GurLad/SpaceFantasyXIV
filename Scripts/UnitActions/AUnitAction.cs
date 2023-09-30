using Godot;
using System;

public abstract class AUnitAction
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract int SortOrder { get; }
    protected Unit thisUnit;

    public void AttachToUnit(Unit unit)
    {
        thisUnit = unit;
    }

    protected abstract void ActivateEffect(Unit enemy);
}
