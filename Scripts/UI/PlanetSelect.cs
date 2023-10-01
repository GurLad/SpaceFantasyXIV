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

    public override void _Ready()
    {
        base._Ready();
        foreach (var item in FormController.EnemyForms)
        {

        }
    }
}
