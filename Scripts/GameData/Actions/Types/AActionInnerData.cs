using Godot;
using System;

public abstract class AActionInnerData
{
    protected AActionInnerData() { }
}

public abstract class AActionInnerDataEditor<T> where T : AActionInnerData
{
    public abstract T Data { get; set; }
}
