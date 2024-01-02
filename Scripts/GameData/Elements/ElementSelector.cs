using Godot;
using System;

public partial class ElementSelector : OptionButton
{
    public override void _Ready()
    {
        base._Ready();
        GameDataPreloader.Current.GetAllNames("Elements").ForEach(a => AddIconItem(ElementLoader.GetElementIcon(a), a));
        Select(0);
    }
}
