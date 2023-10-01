using Godot;
using System;

public partial class UASaturnLimit : AUALimitBreak
{
    public override bool Physical => true;

    public override float Power => 150;

    public override Element Element => Element.Ice;

    public override string Name => "LIMIT!";

    public override string Description => "KILLS SELF! 150 Ice pow. Apply 5 Chill";

    public override int SortOrder => 1;

    public override Func<Unit, AStatus> Inflict => (a) => new StatusChill(a, 5);

    public override Func<Unit, AStatus> Gain => null;
}
