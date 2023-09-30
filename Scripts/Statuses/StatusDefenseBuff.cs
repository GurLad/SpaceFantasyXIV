using Godot;
using System;

public partial class StatusDefenseBuff : AStatus
{
    public override Texture Icon => throw new NotImplementedException();

    public override string Name => "Sturdy";

    public override string Description => "Will definitly live forever.";

    public override int SortOrder => -1;

    public override StatsMod StatsMod => new StatsMod(1, 1, 3, 1, 3, 1);

    public StatusDefenseBuff(Unit unit, int lifespan) : base(unit, lifespan) { }
}
