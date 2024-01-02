using Godot;
using System;

public partial class BuffActionDataEditor : AActionInnerDataEditor<BuffActionData>
{
    [ExportCategory("Internal")]
    [Export]
    private LineEdit vfxNameEdit;
    [Export]
    private StatusWithLifespanEditor gainEdit;

    public override void _Ready()
    {
        vfxNameEdit.TextChanged += (s) => { data.VFXName = s; SetDirty(); };
        gainEdit.ValueChanged += () => { data.Gain = gainEdit.Data; SetDirty(); };
    }

    protected override void Refresh()
    {
        vfxNameEdit.Text = data.VFXName;
        gainEdit.Data = data.Gain;
    }
}
