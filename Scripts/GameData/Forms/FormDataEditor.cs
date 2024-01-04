using Godot;
using System;

public partial class FormDataEditor : ASerializableDataEditor<FormData>
{
    // Exports
    [ExportCategory("Internal")]
    [Export]
    private LineEdit nameEdit;
    [Export]
    private LineEdit fullNameEdit;
    [Export]
    private LineEdit description1Edit;
    [Export]
    private LineEdit description2Edit;
    [Export]
    private Godot.Range sortOrderEdit;
    //[Export]
    //private TYPE actionsEdit;

    public override void _Ready()
    {
        nameEdit.TextChanged += (s) => { data.Name = s; SetDirty(); };
        fullNameEdit.TextChanged += (s) => { data.FullName = s; SetDirty(); };
        description1Edit.TextChanged += (s) => { data.Description1 = s; SetDirty(); };
        description2Edit.TextChanged += (s) => { data.Description2 = s; SetDirty(); };
        sortOrderEdit.ValueChanged += (i) => { data.SortOrder = (int)i; SetDirty(); };
        //actionsEdit.EVENT += () => { data.Actions = ; SetDirty(); };
    }

    protected override void Refresh()
    {
        nameEdit.Text = data.Name;
        fullNameEdit.Text = data.FullName;
        description1Edit.Text = data.Description1;
        description2Edit.Text = data.Description2;
        sortOrderEdit.Value = data.SortOrder;
        //actionsEdit.VAR = data.Actions;
    }
}
