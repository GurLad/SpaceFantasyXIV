using Godot;
using System;

public partial class UAEPoisonBlast : AUAAttack<StatusPoison>
{
    public override bool Physical => true;

    public override float Power => 60;

    public override Element Element => Element.Poison;

    public override string Name => "Poison Blast";

    public override string Description => "Shoot people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "Needles";

    public override Func<Unit, StatusPoison> NewT => (a) => new StatusPoison(a, 3);
}
