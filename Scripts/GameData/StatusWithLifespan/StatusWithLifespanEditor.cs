using Godot;
using System;

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
            return Lifespan > 0 ? new StatusWithLifespan(DataName, Lifespan) : null;
        }
        set
        {
            DataName = value?.Name ?? statusSelect.GetItemText(0);
            Lifespan = value?.Lifespan ?? 0;
            statusSelect.Disabled = Lifespan <= 0;
        }
    }

    public override void _Ready()
    {
        base._Ready();
        GameDataPreloader.Current.GetAllNames("StatusEffects").ForEach(a => statusSelect.AddIconItem(StatusLoader.GetStatusIcon(a), a));
        statusSelect.ItemSelected += (i) => EmitSignal(SignalName.ValueChanged);
        lifespanEdit.ValueChanged += (i) => { EmitSignal(SignalName.ValueChanged); statusSelect.Disabled = i <= 0; };
        statusSelect.Disabled = true;
    }
}
