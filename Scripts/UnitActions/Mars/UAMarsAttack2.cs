using Godot;
using System;

public class UAMarsAttack2 : AUAAttack
{
    public override bool Physical => true;

    public override float Power => 80;

    public override Element Element => Element.Dark;

    public override string Name => "ShdwClaw";

    public override string Description => "Punch people in the face!";

    public override int SortOrder => 3;

    public override string VFXName => "DarkSlash";
}
