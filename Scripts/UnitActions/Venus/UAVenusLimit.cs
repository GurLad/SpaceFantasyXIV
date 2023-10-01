using Godot;
using System;

public partial class UAVenusLimit : AUALimitBreak
{
    public override bool Physical => false;

    public override float Power => 150;

    public override Element Element => Element.Earth;

    public override string Name => "LIMIT!";

    public override string Description => "KILLS SELF! 150 Earth pow. Apply 5 Poison";

    public override int SortOrder => 1;

    public override Func<Unit, AStatus> Inflict => (a) => new StatusPoison(a, 5);

    public override Func<Unit, AStatus> Gain => null;
}
