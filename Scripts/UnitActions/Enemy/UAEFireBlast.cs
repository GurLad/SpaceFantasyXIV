using Godot;
using System;

public partial class UAEFireBlast : AUAAttack<StatusBurn>
{
    public override bool Physical => true;

    public override float Power => 60;

    public override Element Element => Element.Fire;

    public override string Name => "Fire Blast";

    public override string Description => "Shoot people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "Boom";

    public override Func<Unit, StatusBurn> NewT => (a) => new StatusBurn(a, 3);
}
