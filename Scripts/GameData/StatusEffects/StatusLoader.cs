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

    public override string DataFolder => "StatusEffects";

    protected override Dictionary<string, ISerializableData> gameDatas => new Dictionary<string, ISerializableData>()
        { { "StatsModifier", statsModifier } };

    protected override Dictionary<string, Sprite2D> sprites => new Dictionary<string, Sprite2D>() { { "Icon", icon } };

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
