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
}
