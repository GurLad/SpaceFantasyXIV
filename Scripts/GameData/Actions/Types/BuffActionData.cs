using Godot;
using System;

public partial class BuffActionData : AActionInnerData
{
    public string VFXName { get; set; }
    public StatusWithLifespan Gain { get; set; } = null;

    public BuffActionData(string vfxName, StatusWithLifespan gain)
    {
        VFXName = vfxName;
        Gain = gain;
    }

    public BuffActionData() { }
}
