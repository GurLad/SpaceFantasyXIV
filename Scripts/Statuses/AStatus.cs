using Godot;
using System;

public abstract class AStatus
{
    public abstract Texture Icon { get; }
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract int SortOrder { get; }
    public int Lifespan;
    public Unit Unit;

    public virtual StatsMod StatsMod => null;

    public virtual void BeginTurn() { }
    public virtual void EndTurn() { }
}