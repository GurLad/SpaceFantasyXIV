using Godot;
using System;

public partial class PlanetIcon : TextureButton
{
    public string PlanetName;
    public bool Selected;
    [Export]
    private TextureRect cursorIcon;
    [Export]
    private Texture2D cursorEmpty;
    [Export]
    private Texture2D cursorHover;
    [Export]
    private Texture2D cursorSelect;
    private TextureRect planetIcon;

    public override void _Ready()
    {
        base._Ready();
    }

    public void SetPlanet(string name)
    {
        planetIcon = GetNode<TextureRect>(PlanetName = name);
    }

    public void OnMouseEnter()
    {
        planetIcon.Texture = Selected ? cursorSelect : cursorHover;
    }

    public void OnMouseLeave()
    {
        planetIcon.Texture = Selected ? cursorSelect : cursorEmpty;
    }

    public void Select()
    {
        planetIcon.Texture = cursorSelect;
        Selected = true;
    }
}
