using Godot;
using System;

public partial class StatusUI : Control
{
    // Exports
    [Export]
    private Label lifespanLabel;
    [Export]
    private TextureRect icon;

    public void Update(string status, int lifespan)
    {
        icon.Texture = StatusIconController.Current.GetIcon(status);
        lifespanLabel.Text = lifespan.ToString();
    }
}
