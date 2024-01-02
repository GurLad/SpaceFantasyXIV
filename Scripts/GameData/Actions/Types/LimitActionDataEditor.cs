using Godot;
using System;

public partial class LimitActionDataEditor : AActionInnerDataEditor<LimitActionData>
{
    // Exports
    [ExportCategory("Internal")]
    [Export]
    private CheckBox physicalEdit;
    [Export]
    private Godot.Range powerEdit;
    [Export]
    private ElementSelector elementEdit;
    [Export]
    private StatusWithLifespanEditor inflictEdit;
    [Export]
    private StatusWithLifespanEditor gainEdit;

    public override void _Ready()
    {
        physicalEdit.Toggled += (b) => { data.Physical = b; SetDirty(); };
        powerEdit.ValueChanged += (i) => { data.Power = (float)i; SetDirty(); };
        elementEdit.ItemSelected += (i) => { data.Element = elementEdit.GetItemText((int)i); SetDirty(); };
        inflictEdit.ValueChanged += () => { data.Inflict = inflictEdit.Data; SetDirty(); };
        gainEdit.ValueChanged += () => { data.Gain = gainEdit.Data; SetDirty(); };
    }

    protected override void Refresh()
    {
        physicalEdit.ButtonPressed = data.Physical;
        powerEdit.Value = data.Power;
        elementEdit.Text = data.Element;
        inflictEdit.Data = data.Inflict;
        gainEdit.Data = data.Gain;
    }
}
