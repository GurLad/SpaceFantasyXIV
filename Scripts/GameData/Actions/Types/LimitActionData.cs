using Godot;
using System;

public partial class LimitActionData : AActionInnerData
{
    public bool Physical { get; set; }
    public float Power { get; set; }
    public string Element { get; set; }
    public StatusWithLifespan Inflict { get; set; } = null;
    public StatusWithLifespan Gain { get; set; } = null;
}
