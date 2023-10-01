using Godot;
using System;

public partial class BossPhaseIcon : TextureButton
{
    [Export]
    private Label number;
    [Export]
    private Label x;

    public void Init(int num)
    {
        number.Text = (num + 1).ToString();
        x.Visible = FormController.BossPhase > num;
    }
}
