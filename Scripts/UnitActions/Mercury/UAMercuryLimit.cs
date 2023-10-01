using Godot;
using System;

public partial class UAMercuryLimit : AUALimitBreak
{
    public override bool Physical => true;

    public override float Power => 150;

    public override Element Element => Element.Fire;

    public override string Name => "LIMIT!";

    public override string Description => "KILLS SELF! 150 Fire pow. Gain 5 Attack Buff.";

    public override int SortOrder => 1;

    public override Func<Unit, AStatus> Inflict => null;

    public override Func<Unit, AStatus> Gain => (a) => new StatusAttackBuff(a, 5);
}
