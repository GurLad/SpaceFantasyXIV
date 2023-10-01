using Godot;
using System;

public class UASaturnAttack1 : AUAAttack
{
    public override bool Physical => true;

    public override float Power => 55;

    public override Element Element => Element.None;

    public override string Name => "Pound";

    public override string Description => "Punch people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "Pound";
}
