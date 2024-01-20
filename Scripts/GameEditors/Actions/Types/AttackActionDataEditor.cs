using Godot;
using System;

public partial class AttackActionDataEditor : AActionInnerDataEditor<AttackActionData>
{
    [ExportCategory("Internal")]
    [Export]
    private CheckBox physicalEdit;
    [Export]
    private Godot.Range powerEdit;
    [Export]
    private DataSelector elementEdit;
    [Export]
    private DataSelector vfxNameEdit;
    [Export]
    private StatusWithLifespanEditor inflictEdit;

    public override void _Ready()
    {
        base._Ready();
        physicalEdit.Toggled += (b) => { data.Physical = b; SetDirty(); };
        powerEdit.ValueChanged += (i) => { data.Power = (float)i; SetDirty(); };
        elementEdit.ItemSelected += (i) => { data.Element = elementEdit.GetItemText((int)i); SetDirty(); };
        vfxNameEdit.ItemSelected += (i) => { data.VFXName = vfxNameEdit.GetItemText((int)i); SetDirty(); };
        inflictEdit.ValueChanged += () => { data.Inflict = inflictEdit.Data; SetDirty(); };

    }

    protected override void Refresh()
    {
        physicalEdit.ButtonPressed = data.Physical;
        powerEdit.Value = data.Power;
        elementEdit.Text = data.Element;
        vfxNameEdit.Text = data.VFXName;
        inflictEdit.Data = data.Inflict;
    }
}