using Godot;
using System;
using System.Collections.Generic;

public partial class ElementLoader : AGameDataLoader
{
    private static ElementLoader ICON_LOADER { get; } = new ElementLoader();

    [Export]
    private Sprite2D icon;

    public override string DataFolder => "Elements";

    protected override Dictionary<string, ISerializableData> gameDatas => new Dictionary<string, ISerializableData>(); // No data

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

    public static Texture2D GetElementIcon(string elementName)
    {
        return ICON_LOADER.GetIcon(elementName);
    }
}
