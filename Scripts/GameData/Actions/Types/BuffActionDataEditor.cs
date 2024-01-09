using Godot;
using System;

public partial class BuffActionDataEditor : AActionInnerDataEditor<BuffActionData>
{
    [ExportCategory("Internal")]
    [Export]
    private DataSelector vfxNameEdit;
    [Export]
    private StatusWithLifespanEditor gainEdit;

    public override void _Ready()
    {
        vfxNameEdit.ItemSelected += (i) => { data.VFXName = vfxNameEdit.GetItemText((int)i); SetDirty(); };
        gainEdit.ValueChanged += () => { data.Gain = gainEdit.Data; SetDirty(); };
    }

    protected override void Refresh()
    {
        vfxNameEdit.Text = data.VFXName;
        gainEdit.Data = data.Gain;
    }
}
