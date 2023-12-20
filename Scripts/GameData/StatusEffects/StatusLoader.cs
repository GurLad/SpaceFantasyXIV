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

    protected override List<GGE.Internal.AGameDataPart> gameDatas => new List<GGE.Internal.AGameDataPart>()
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
            icon = new Sprite2D();
            AddChild(icon);
            icon.Visible = false;
        }
    }
}
