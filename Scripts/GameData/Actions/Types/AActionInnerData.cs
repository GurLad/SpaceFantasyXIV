using Godot;
using System;

public abstract class AActionInnerData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int SortOrder { get; set; }
}

public record StatusWithLifespan(string Name, int Lifespan) { }
