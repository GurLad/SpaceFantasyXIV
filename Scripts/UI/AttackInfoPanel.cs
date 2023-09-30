using Godot;
using System;

public partial class AttackInfoPanel : PanelContainer
{
    [Export]
    private Label nameLabel;
    [Export]
    private Label powerLabel;
    [Export]
    private Label physMagLabel;
    [Export]
    private Label typeLabel;
    [Export]
    private Label statusLabel;
    [Export]
    private Label descriptionLabel;
    [Export]
    private Control detailsHolder;

    public void Display(AUnitAction action)
    {
        nameLabel.Text = action.Name;
        if (action is AUAAttack attack)
        {
            powerLabel.Text = "Pow: " + attack.Power;
            physMagLabel.Text = attack.Physical ? "Physical" : "Magical";
            typeLabel.Text = "Type: " + attack.Element.ToString();
            AStatus status = attack.NewT?.Invoke() ?? null;
            statusLabel.Text = status != null ? "Apply " + status.Lifespan + " " + status.ShortName : "";
            descriptionLabel.Visible = false;
            detailsHolder.Visible = true;
        }
        else
        {
            descriptionLabel.Text = action.Description;
            descriptionLabel.Visible = true;
            detailsHolder.Visible = false;
        }
    }

    public void Hide()
    {
        nameLabel.Text = "Select attack";
        descriptionLabel.Visible = false;
        detailsHolder.Visible = false;
    }
}
