using Godot;
using System;

public class UAEarthAttack1 : AUAAttack
{
    public override bool Physical => true;

    public override float Power => 80;

    public override Element Element => Element.None;

    public override string Name => "Stab";

    public override string Description => "Punch people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "Slash";
}
