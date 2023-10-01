using Godot;
using System;

public class StatusBurn : AStatus
{
    public override Texture Icon => throw new NotImplementedException();

    public override string Name => "Burn";

    public override string Description => "Ahhh! I'm on fire and it hurts!!! This makes me weak for some reason...";

    public override int SortOrder => 3;

    public override bool Stacks => true;

    public override StatsMod StatsMod => new StatsMod(1, 0.5f, 1, 0.5f, 1, 1);

    public override void EndTurn()
    {
        base.EndTurn();
        thisUnit.TakeDamage(new Stats(), 20 * Lifespan, Element.Fire, true, "Boom");
    }

    public StatusBurn(Unit unit, int lifespan) : base(unit, lifespan) { }
}
