using Godot;
using System;

public record StatusWithLifespan(string Name, int Lifespan) { }

public partial class StatusWithLifespanEditor : Node
{
    [Export]
    private OptionButton statusSelect;
    public string DataName { get => statusSelect.Text; set => statusSelect.Text = value; }
    [Export]
    private Godot.Range lifespanEdit;
    public int Lifespan { get => Mathf.RoundToInt(lifespanEdit.Value); set => lifespanEdit.Value = value; }

    [Signal]
    public delegate void ValueChangedEventHandler();

    public StatusWithLifespan Data
    {
        get
        {
            return new StatusWithLifespan(DataName, Lifespan);
        }
        set
        {
            DataName = value.Name;
            Lifespan = value.Lifespan;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        GameDataPreloader.Current.GetAllNames("StatusEffects").ForEach(a => statusSelect.AddIconItem(StatusLoader.GetStatusIcon(a), a));
        statusSelect.ItemSelected += (i) => EmitSignal(SignalName.ValueChanged);
        lifespanEdit.ValueChanged += (i) => EmitSignal(SignalName.ValueChanged);
    }
}
