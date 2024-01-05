using Godot;
using System;
using System.Collections.Generic;

public partial class FormLoader : AGameDataLoader
{
    // Exports
    [Export]
    private Sprite2D icon;
    [Export]
    private AnimatedSprite2D renderer;
    // Properties
    private StatsModifier statsModifier = new StatsModifier();
    private FormData data = new FormData();

    public override string DataFolder => "Forms";

    protected override List<AGameDataPart> gameDatas => new List<AGameDataPart>()
    {
        new GameDataSerializablePart("StatsModifier", statsModifier),
        new GameDataSerializablePart("Data", data),
        new GameDataSpritePart("Icon", icon),
        new GameDataAnimatedSpritePart("Animations", renderer, ".png", "Idle", "Attack", "Hurt"),
    };

    protected override string iconKey => "Icon";

    public override void _Ready()
    {
        base._Ready();
        if (icon == null)
        {
            AddChild(icon = new Sprite2D());
            icon.Visible = false;
        }
        if (renderer == null)
        {
            AddChild(renderer = new AnimatedSprite2D());
            renderer.SpriteFrames = new SpriteFrames();
            renderer.SpriteFrames.AddAnimation("Idle");
            renderer.SpriteFrames.AddAnimation("Attack");
            renderer.SpriteFrames.AddAnimation("Hurt");
            renderer.Visible = false;
        }
    }
}
