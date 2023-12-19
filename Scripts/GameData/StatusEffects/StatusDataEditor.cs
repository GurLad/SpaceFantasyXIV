using Godot;
using System;

public partial class StatusDataEditor : ASerializableDataEditor<StatusData>
{
    [ExportCategory("Internal")]
    [Export]
    private LineEdit nameEdit;
    [Export]
    private LineEdit shortNameEdit;
    [Export]
    private TextEdit descriptionEdit;
    [Export]
    private Godot.Range sortOrderEdit;
    [Export]
    private CheckBox stacksEdit;
    [Export]
    private CheckBox fadesEdit;
    [Export]
    private CodeEdit beginTurnEdit;
    [Export]
    private CodeEdit endTurnEdit;

    public override void _Ready()
    {
        base._Ready();
        nameEdit.TextChanged += (s) => { data.Name = s; SetDirty(); };
        shortNameEdit.TextChanged += (s) => { data.ShortName = s; SetDirty(); };
        descriptionEdit.TextChanged += () => { data.Description = descriptionEdit.Text; SetDirty(); };
        sortOrderEdit.ValueChanged += (i) => { data.SortOrder = (int)i; SetDirty(); };
        stacksEdit.Toggled += (b) => { data.Stacks = b; SetDirty(); };
        fadesEdit.Toggled += (b) => { data.Fades = b; SetDirty(); };
        beginTurnEdit.TextChanged += () => { data.BeginTurn.Source = beginTurnEdit.Text; SetDirty(); };
        endTurnEdit.TextChanged += () => { data.EndTurn.Source = endTurnEdit.Text; SetDirty(); };
    }

    protected override void Refresh()
    {
        nameEdit.Text = data.Name;
        shortNameEdit.Text = data.ShortName;
        descriptionEdit.Text = data.Description;
        sortOrderEdit.Value = data.SortOrder;
        stacksEdit.ButtonPressed = data.Stacks;
        fadesEdit.ButtonPressed = data.Fades;
        beginTurnEdit.Text = data.BeginTurn.Source;
        endTurnEdit.Text = data.EndTurn.Source;
    }
}