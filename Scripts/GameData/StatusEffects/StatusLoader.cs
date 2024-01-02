using Godot;
using System;
using System.Collections.Generic;

public partial class StatusLoader : AGameDataLoader
{
    private static StatusLoader ICON_LOADER { get; } = new StatusLoader();

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

    public static Texture2D GetStatusIcon(string statusName)
    {
        return ICON_LOADER.GetIcon(statusName);
    }
}
