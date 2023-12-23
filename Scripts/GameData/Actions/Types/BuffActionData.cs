using Godot;
using System;

public partial class BuffActionData : AActionInnerData
{
    public string VFXName { get; set; }
    public StatusWithLifespan Gain { get; set; } = null;
}
