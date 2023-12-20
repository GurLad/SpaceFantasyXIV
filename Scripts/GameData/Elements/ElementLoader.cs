using Godot;
using System;
using System.Collections.Generic;

public partial class ElementLoader : AGameDataLoader
{
    private static ElementLoader ICON_LOADER { get; } = new ElementLoader();

    [Export]
    private Sprite2D icon;

    public override string DataFolder => "Elements";

    protected override List<GGE.Internal.AGameDataPart> gameDatas => new List<GGE.Internal.AGameDataPart>()
    {
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

    public static Texture2D GetElementIcon(string elementName)
    {
        return ICON_LOADER.GetIcon(elementName);
    }
}
