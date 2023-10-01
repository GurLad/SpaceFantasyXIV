using Godot;
using System;

public partial class UAEWindBlast : AUAAttack
{
    public override bool Physical => false;

    public override float Power => 60;

    public override Element Element => Element.Wind;

    public override string Name => "Wind Blast";

    public override string Description => "Shoot people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "Pound";
}
