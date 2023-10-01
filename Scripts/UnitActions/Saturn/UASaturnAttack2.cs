using Godot;
using System;

public class UASaturnAttack2 : AUAAttack<StatusChill>
{
    public override bool Physical => true;

    public override float Power => 30;

    public override Element Element => Element.Ice;

    public override string Name => "Cool";

    public override string Description => "Punch people in the face!";

    public override int SortOrder => 3;

    public override string VFXName => "IcePound";

    public override Func<Unit, StatusChill> NewT => (a) => new StatusChill(a, 5);
}
