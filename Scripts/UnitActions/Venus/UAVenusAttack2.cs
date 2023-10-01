using Godot;
using System;

public class UAVenusAttack2 : AUAAttack<StatusPoison>
{
    public override bool Physical => true;

    public override float Power => 35;

    public override Element Element => Element.Poison;

    public override string Name => "Thorn";

    public override string Description => "Punch people in the face!";

    public override int SortOrder => 3;

    public override string VFXName => "Needles";

    public override Func<Unit, StatusPoison> NewT => (a) => new StatusPoison(a, 3);
}
