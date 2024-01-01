using Godot;
using System;

public partial class AttackActionDataEditor : AActionInnerDataEditor<AttackActionData>
{
    [ExportCategory("Internal")]
    [Export]
    private CheckBox physicalEdit;
    public bool Physical { get => physicalEdit.ButtonPressed; set => physicalEdit.ButtonPressed = value; }
    [Export]
    private Godot.Range powerEdit;
    public float Power { get => (float)powerEdit.Value; set => powerEdit.Value = value; }
    [Export]
    private LineEdit elementEdit;
    public string Element { get => elementEdit.Text; set => elementEdit.Text = value; }
    [Export]
    private LineEdit vfxNameEdit;
    public string VFXName { get => vfxNameEdit.Text; set => vfxNameEdit.Text = value; }
    [Export]
    private StatusWithLifespanEditor inflictEdit;
    public StatusWithLifespan Inflict { get => inflictEdit.Data; set => inflictEdit.Data = value; }

    public override AttackActionData Data
    {
        get => new AttackActionData(Physical, Power, Element, VFXName, Inflict);
        set
        {
            Physical = value.Physical;
            Power = value.Power;
            Element = value.Element;
            VFXName = value.VFXName;
            Inflict = value.Inflict;
        }
    }
}
