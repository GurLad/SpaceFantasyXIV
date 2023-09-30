using Godot;
using System;

public class StatusPoison : AStatus
{
    public override Texture Icon => throw new NotImplementedException();

    public override string Name => "Poison";

    public override string Description => "Cough cough! I keep taking damage...";

    public override int SortOrder => 1;

    public override void EndTurn()
    {
        base.EndTurn();
        Unit.TakeDamage(new Stats(), 40 * Lifespan, Element.Poison, true);
    }
}
