using Godot;
using System;

public partial class FormulaEditor : Node
{
    // Exports
    [Export]
    private TextureRect iconHolder;
    [Export]
    private Label nameLabel;
    [Export]
    private LineEdit formulaEditor;
    // Properties
    private Formula formula;

    public void Init(Texture2D icon, string name, Formula formula, Action setDirty)
    {
        iconHolder.Texture = icon;
        nameLabel.Text = name;
        this.formula = formula;
        formulaEditor.TextChanged += (s) => { formula.Source = s; setDirty?.Invoke(); };
        Refresh();
    }

    public void Refresh()
    {
        formulaEditor.Text = formula.Source;
    }
}
