using Godot;
using System;

public partial class UAMarsLimit : AUALimitBreak
{
    public override bool Physical => true;

    public override float Power => 150;

    public override Element Element => Element.None;

    public override string Name => "LIMIT!";

    public override string Description => "KILLS SELF! 150 Normal pow. Apply 2 Sleep";

    public override int SortOrder => 1;

    public override Func<Unit, AStatus> Inflict => (a) => new StatusSleep(a, 2);

    public override Func<Unit, AStatus> Gain => null;
}
