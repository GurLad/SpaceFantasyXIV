using Godot;
using System;

public partial class FormulaEditor : Control
{
    // Exports
    [ExportCategory("Internal")]
    [Export]
    private TextureRect iconHolder;
    [Export]
    private Label nameLabel;
    [Export]
    private LineEdit formulaEditor;
    // Properties
    public bool Changed => formula.Source != "1";
    private Formula formula;

    public void Init(Texture2D icon, string name, Formula formula, Action setDirty)
    {
        iconHolder.Texture = icon;
        nameLabel.Text = name;
        this.formula = formula;
        formulaEditor.TextChanged += (s) => { formula.Source = s != "" ? s : "1"; setDirty?.Invoke(); };
        formulaEditor.TextSubmitted += (s) => UpdateText(s);
        Refresh();
    }

    public void Refresh()
    {
        UpdateText(formula.Source);
    }

    private void UpdateText(string newValue)
    {
        //formulaEditor.AddThemeColorOverride("font_color", formulaEditor.Text == "1" ? Color.FromHtml("#666666") : new Color(1, 1, 1));
        formulaEditor.Text = newValue == "1" ? "" : newValue;
    }
}
