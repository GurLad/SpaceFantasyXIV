using Godot;
using System;

public class UAVenusAttack1 : AUAAttack
{
    public override bool Physical => true;

    public override float Power => 75;

    public override Element Element => Element.Earth;

    public override string Name => "Quake";

    public override string Description => "Punch people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "Quake";
}
