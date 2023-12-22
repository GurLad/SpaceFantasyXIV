using Godot;
using System;
using System.Collections.Generic;

public partial class VFXLoader : AGameDataLoader
{
    // Exports
    [Export]
    private AnimatedSprite2D renderer;
    [Export]
    private AudioStreamPlayer player;
    // Properties
    public override string DataFolder => "VFX";

	protected override List<AGameDataPart> gameDatas => new List<AGameDataPart>()
	{
		new GameDataAnimatedSpritePart("Animation", renderer),
		new GameDataAudioStreamPart("SFX", player)
	};

    public override void _Ready()
    {
        base._Ready();
        if (renderer == null)
        {
            AddChild(renderer = new AnimatedSprite2D());
            renderer.SpriteFrames = new SpriteFrames();
            renderer.Visible = false;
        }
        if (player == null)
        {
            AddChild(player = new AudioStreamPlayer());
        }
    }
}
