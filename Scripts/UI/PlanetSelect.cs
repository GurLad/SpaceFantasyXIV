using Godot;
using System;

public partial class PlanetSelect : Control
{
    [Export]
    private PackedScene bossPhaseIcon;
    [Export]
    private PackedScene planetIcon;
    [Export]
    private Label bossDescLabel;
    [Export]
    private Label planetNameLabel;
    [Export]
    private Label planetDesc1Label;
    [Export]
    private Label planetDesc2Label;
    [Export]
    private Button goButton;
    [Export]
    private Node BossIconHolder;
    [Export]
    private Node PlanetHolder;
    private PlanetIcon selected;

    public override void _Ready()
    {
        base._Ready();
        foreach (var item in FormController.EnemyForms)
        {
            BossPhaseIcon newIcon = bossPhaseIcon.Instantiate<BossPhaseIcon>();
            BossIconHolder.AddChild(newIcon);
            newIcon.Init(item.SortOrder);
            newIcon.Pressed += () => bossDescLabel.Text = item.Description1;
        }
        foreach (var item in FormController.PlayerForms)
        {
            PlanetIcon newIcon = planetIcon.Instantiate<PlanetIcon>();
            PlanetHolder.AddChild(newIcon);
            newIcon.SetPlanet(item.Name);
            newIcon.Pressed += () =>
            {
                selected?.Unselect();
                planetNameLabel.Text = item.FullName;
                planetDesc1Label.Text = item.Description1;
                planetDesc2Label.Text = item.Description2;
                selected = newIcon;
                selected.Select();
                goButton.Disabled = false;
            };
        }
    }
}
