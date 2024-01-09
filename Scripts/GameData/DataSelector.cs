using Godot;
using System;

public partial class DataSelector : OptionButton
{
    [Export]
    public string DataFolder;

    public override void _Ready()
    {
        base._Ready();
        GameDataPreloader.Current.GetAllNames(DataFolder).ForEach(a => AddIconItem(GameDataPreloader.Current.GetIcon(DataFolder, a), a));
        Text = GetItemText(0);
    }
}
