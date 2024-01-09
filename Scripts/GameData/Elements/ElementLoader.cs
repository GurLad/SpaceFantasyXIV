using Godot;
using System;
using System.Collections.Generic;

public partial class ElementLoader : AGameDataLoader
{
    [Export]
    private Sprite2D icon;

    public override string DataFolder => "Elements";

    protected override List<AGameDataPart> gameDatas => new List<AGameDataPart>()
    {
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
