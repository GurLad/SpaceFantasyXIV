using Godot;
using System;
using System.Collections.Generic;

public partial class StatusLoader : AGameDataLoader
{
    // Exports
    [Export]
    private Sprite2D icon;
    // Properties
    private StatsModifier statsModifier = new StatsModifier();
    private StatusData data = new StatusData();

    public override string DataFolder => "StatusEffects";

    protected override List<AGameDataPart> gameDatas => new List<AGameDataPart>()
    {
        new GameDataSerializablePart("StatsModifier", statsModifier),
        new GameDataSerializablePart("Data", data),
        new GameDataSpritePart("Icon", icon),
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
    }
}
