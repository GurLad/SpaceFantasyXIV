using Godot;
using System;

public class StatusAttackBuff : AStatus
{
    public override Texture Icon => throw new NotImplementedException();

    public override string Name => "Powerful";

    public override string Description => "Power! Unlimited Power!";

    public override int SortOrder => -1;

    public override StatsMod StatsMod => new StatsMod(1, 3, 1, 3, 1, 1);
}
