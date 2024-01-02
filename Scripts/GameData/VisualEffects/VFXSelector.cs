using Godot;
using System;

public partial class VFXSelector : OptionButton
{
    public override void _Ready()
    {
        base._Ready();
        GameDataPreloader.Current.GetAllNames("VFX").ForEach(a => AddItem(a));
        Select(0);
    }
}
