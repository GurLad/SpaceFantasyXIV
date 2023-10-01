using Godot;
using System;

public partial class UAMercuryLimit : AUALimitBreak
{
    public override bool Physical => true;

    public override float Power => 150;

    public override Element Element => Element.Fire;

    public override string Name => "LIMIT!";

    public override string Description => "KILLS SELF! Deals 150 physical Fire damage, and buffs Attack for 5 turns.";

    public override int SortOrder => 1;

    public override Func<Unit, AStatus> Inflict => null;

    public override Func<Unit, AStatus> Gain => (a) => new StatusAttackBuff(a, 5);
}
