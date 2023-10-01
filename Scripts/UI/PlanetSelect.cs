using Godot;
using System;
using System.Collections.Generic;

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
    [Export]
    private Control holder;
    [Export]
    private float showHideTime = 0.3f;
    private PlanetIcon selected;
    private Interpolator interpolator = new Interpolator();
    private float shownHeight;
    private float hiddenHeight;
    private List<Control> generated = new List<Control>();

    [Signal]
    public delegate void MidSelectedPlanetEventHandler(string name);

    [Signal]
    public delegate void FinishedSelectionEventHandler();

    public override void _Ready()
    {
        base._Ready();
        AddChild(interpolator);
        shownHeight = holder.Position.Y;
        hiddenHeight = holder.Position.Y + holder.Size.Y;
        Position = new Vector2(holder.Position.X, hiddenHeight);
        goButton.Pressed += End;
    }

    private void Generate()
    {
        goButton.Disabled = true;
        selected = null;
        generated.Clear();
        foreach (var item in FormController.EnemyForms)
        {
            BossPhaseIcon newIcon = bossPhaseIcon.Instantiate<BossPhaseIcon>();
            BossIconHolder.AddChild(newIcon);
            newIcon.Init(item.SortOrder);
            newIcon.Pressed += () => bossDescLabel.Text = item.Description1;
            generated.Add(newIcon);
        }
        foreach (var item in FormController.PlayerForms)
        {
            if (!FormController.LivingPlayerForms[item.SortOrder])
            {
                continue;
            }
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
            generated.Add(newIcon);
        }
    }

    public void Begin()
    {
        Generate();
        interpolator.Interpolate(showHideTime,
            new Interpolator.InterpolateObject(
                a => holder.Position = new Vector2(holder.Position.X, a),
                hiddenHeight,
                shownHeight,
                Easing.EaseOutQuad));
    }

    public void End()
    {
        EmitSignal(SignalName.MidSelectedPlanet, selected.PlanetName);
        interpolator.Interpolate(showHideTime,
            new Interpolator.InterpolateObject(
                a => holder.Position = new Vector2(holder.Position.X, a),
                shownHeight,
                hiddenHeight,
                Easing.EaseInQuad));
        interpolator.OnFinish = () =>
        {
            generated.ForEach(a => a?.QueueFree());
            EmitSignal(SignalName.FinishedSelection);
        };
    }
}
