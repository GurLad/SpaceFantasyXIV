using Godot;
using System;

public partial class MercuryBuff1 : AUABuff<StatusDefenseBuff>
{
    public override string Name => "Shields Up";

    public override string Description => "Massively buff defense for 5 turns.";

    public override int SortOrder => 3;

    public override Func<StatusDefenseBuff> NewT => () => new StatusDefenseBuff(thisUnit, 5);

    public override string VFXName => "BlueBuff";
}
