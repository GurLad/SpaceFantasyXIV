using Godot;
using System;

public partial class LimitActionDataEditor : AActionInnerDataEditor<LimitActionData>
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
    private StatusWithLifespanEditor inflictEdit;
    public StatusWithLifespan Inflict { get => inflictEdit.Data; set => inflictEdit.Data = value; }
    [Export]
    private StatusWithLifespanEditor gainEdit;
    public StatusWithLifespan Gain { get => gainEdit.Data; set => gainEdit.Data = value; }

    public override LimitActionData Data
    {
        get => new LimitActionData(Physical, Power, Element, Inflict, Gain);
        set
        {
            Physical = value.Physical;
            Power = value.Power;
            Element = value.Element;
            Inflict = value.Inflict;
            Gain = value.Gain;
        }
    }
}
