using Godot;
using System;

public class AttackActionData : AActionInnerData
{
    public bool Physical { get; set; }
    public float Power { get; set; }
    public string Element { get; set; }
    public string VFXName { get; set; }
    public StatusWithLifespan Inflict { get; set; } = null;

    public AttackActionData(bool physical, float power, string element, string vFXName, StatusWithLifespan inflict)
    {
        Physical = physical;
        Power = power;
        Element = element;
        VFXName = vFXName;
        Inflict = inflict;
    }

    public AttackActionData() { }
}
