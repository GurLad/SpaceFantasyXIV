using Godot;
using System;

public partial class VFXController : Node
{
    public static VFXController Current { get; private set; }

    public override void _Ready()
    {
        base._Ready();
        Current = this;
    }

    public VFX GetVFX(string name)
    {
        return (VFX)GetNode<VFX>(name).Duplicate();
    }
}
