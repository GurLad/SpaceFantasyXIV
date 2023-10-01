using Godot;
using System;

public class UAEarthAttack2 : AUAAttack
{
    public override bool Physical => false;

    public override float Power => 5;

    public override Element Element => Element.Water;

    public override string Name => "Splash";

    public override string Description => "Punch people in the face!";

    public override int SortOrder => 3;

    public override string VFXName => "DarkSlash";
}
