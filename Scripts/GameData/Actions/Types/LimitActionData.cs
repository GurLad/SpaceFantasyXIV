using Godot;
using System;

public partial class LimitActionData : AActionInnerData
{
    public bool Physical { get; set; }
    public float Power { get; set; }
    public string Element { get; set; }
    public StatusWithLifespan Inflict { get; set; } = null;
    public StatusWithLifespan Gain { get; set; } = null;

    public LimitActionData(bool physical, float power, string element, StatusWithLifespan inflict, StatusWithLifespan gain)
    {
        Physical = physical;
        Power = power;
        Element = element;
        Inflict = inflict;
        Gain = gain;
    }

    public LimitActionData() { }
}
