using Godot;
using System;
using System.Collections.Generic;

public partial class BattleBackgroundLoader : AGameDataLoader
{
    // Exports
    [Export]
    private AnimatedSprite2D renderer;
    // Properties
    public override string DataFolder => "BattleBackgrounds";

    protected override List<AGameDataPart> gameDatas => new List<AGameDataPart>()
    {
        new GameDataAnimatedSpritePart("Animation", renderer, ".png")
    };

}
