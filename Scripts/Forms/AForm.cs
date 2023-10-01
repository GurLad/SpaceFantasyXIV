using Godot;
using System;
using System.Collections.Generic;

public abstract class AForm
{
    public abstract string Name { get; }
    public abstract string FullName { get; }
    public abstract string Description1 { get; }
    public abstract string Description2 { get; }
    public abstract int SortOrder { get; }
    public abstract List<AUnitAction> Actions { get; }
    public abstract StatsMod StatsMod { get; }
}
