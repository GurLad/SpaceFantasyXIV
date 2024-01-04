using Godot;
using System;

public partial class ActionSelector : OptionButton
{
    public override void _Ready()
    {
        base._Ready();
        GameDataPreloader.Current.GetAllNames("Actions").ForEach(a => AddItem(a));
        Text = GetItemText(0);
    }
}
