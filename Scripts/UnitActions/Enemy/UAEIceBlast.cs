using Godot;
using System;

public partial class UAEIceBlast : AUAAttack<StatusChill>
{
    public override bool Physical => false;

    public override float Power => 60;

    public override Element Element => Element.Ice;

    public override string Name => "Ice Blast";

    public override string Description => "Shoot people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "IcePound";

    public override Func<Unit, StatusChill> NewT => (a) => new StatusChill(a, 3);
}
