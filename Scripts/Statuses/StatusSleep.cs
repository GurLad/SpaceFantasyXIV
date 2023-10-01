using Godot;
using System;

public class StatusSleep : AStatus
{
    public override Texture Icon => throw new NotImplementedException();

    public override string Name => "Sleep";

    public override string Description => "Brrrr! I'm soooo slooooow noooooow...";

    public override int SortOrder => 3;

    public override bool Stacks => true;

    public override StatsMod StatsMod => new StatsMod(1, 1, 1, 1, 1, 0.2f);

    public StatusSleep(Unit unit, int lifespan) : base(unit, lifespan) { }
}
