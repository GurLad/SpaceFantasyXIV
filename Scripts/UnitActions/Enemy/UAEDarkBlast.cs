using Godot;
using System;

public partial class UAEDarkBlast : AUAAttack
{
    public override bool Physical => false;

    public override float Power => 60;

    public override Element Element => Element.Dark;

    public override string Name => "Dark Blast";

    public override string Description => "Shoot people in the face!";

    public override int SortOrder => 2;

    public override string VFXName => "DarkPound";
}
