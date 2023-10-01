using Godot;
using System;

public partial class UAMercuryAttack1 : AUAAttack
{
    public override bool Physical => false;

    public override float Power => 90;

    public override Element Element => Element.Fire;

    public override string Name => "Rifle";

    public override string Description => "Shoot people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "Boom";
}
