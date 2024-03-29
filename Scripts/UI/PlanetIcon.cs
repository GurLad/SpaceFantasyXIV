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
        MouseEntered += OnMouseEnter;
        MouseExited += OnMouseLeave;
    }

    public void SetPlanet(string name)
    {
        planetIcon = GetNode<TextureRect>(PlanetName = name);
        planetIcon.Visible = true;
    }

    public void OnMouseEnter()
    {
        cursorIcon.Texture = Selected ? cursorSelect : cursorHover;
    }

    public void OnMouseLeave()
    {
        cursorIcon.Texture = Selected ? cursorSelect : cursorEmpty;
    }

    public void Select()
    {
        Selected = true;
        cursorIcon.Texture = cursorSelect;
    }

    public void Unselect()
    {
        Selected = false;
        cursorIcon.Texture = cursorEmpty;
    }
}
