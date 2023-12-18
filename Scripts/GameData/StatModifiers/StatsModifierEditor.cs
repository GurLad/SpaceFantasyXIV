using Godot;
using System;
using System.Collections.Generic;

public partial class StatsModifierEditor : ASerializableDataEditor<StatsModifier>
{
    // Exports
    [ExportCategory("Internal")]
    [Export]
    private PackedScene sceneElementLoader;
    [Export]
    private PackedScene sceneFormulaEditor;
    [Export]
    private Container statsContainer;
    [Export]
    private Container elementsContainer;
    [Export]
    private string[] statNames;
    // Properties
    private List<FormulaEditor> editors = new List<FormulaEditor>();

    public override void _Ready()
    {
        base._Ready();
        data.Clear();
        for (Stat i = 0; i < Stat.EndMarker; i++)
        {
            editors.Add(CreateEditor(statsContainer, null, statNames[(int)i], data.GetStatFormula(i.ToString())));
        }
        foreach (string element in GameDataPreloader.Current.GetAllNames("Elements"))
        {
            editors.Add(CreateEditor(elementsContainer, ElementLoader.GetElementIcon(element), element, data.GetElementFormula(element)));
        }

    }

    protected override void Refresh()
    {
        editors.ForEach(a => a.Refresh());
    }

    private FormulaEditor CreateEditor(Container container, Texture2D icon, string name, Formula formula)
    {
        FormulaEditor editor = sceneFormulaEditor.Instantiate<FormulaEditor>();
        editor.Init(icon, name, formula, SetDirty);
        container.AddChild(editor);
        return editor;
    }
}
