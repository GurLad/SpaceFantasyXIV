using Godot;
using System;

public partial class BuffActionDataEditor : AActionInnerDataEditor<BuffActionData>
{
    [ExportCategory("Internal")]
    [Export]
    private LineEdit vfxNameEdit;
    public string VFXName { get => vfxNameEdit.Text; set => vfxNameEdit.Text = value; }
    [Export]
    private StatusWithLifespanEditor gainEdit;
    public StatusWithLifespan Gain { get => gainEdit.Data; set => gainEdit.Data = value; }

    public override BuffActionData Data
    {
        get => new BuffActionData(VFXName, Gain);
        set
        {
            VFXName = value.VFXName;
            Gain = value.Gain;
        }
    }
}
