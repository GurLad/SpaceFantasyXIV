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
    private ElementSelector elementEdit;
    [Export]
    private LineEdit vfxNameEdit;
    [Export]
    private StatusWithLifespanEditor inflictEdit;

    public override void _Ready()
    {
        base._Ready();
        physicalEdit.Toggled += (b) => { data.Physical = b; SetDirty(); };
        powerEdit.ValueChanged += (i) => { data.Power = (float)i; SetDirty(); };
        elementEdit.ItemSelected += (i) => { data.Element = elementEdit.GetItemText((int)i); SetDirty(); };
        vfxNameEdit.TextChanged += (s) => { data.VFXName = s; SetDirty(); };
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
