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
    [Export]
    private ListEditor actionsEdit;

    public override void _Ready()
    {
        base._Ready();
        nameEdit.TextChanged += (s) => { data.Name = s; SetDirty(); };
        fullNameEdit.TextChanged += (s) => { data.FullName = s; SetDirty(); };
        description1Edit.TextChanged += (s) => { data.Description1 = s; SetDirty(); };
        description2Edit.TextChanged += (s) => { data.Description2 = s; SetDirty(); };
        sortOrderEdit.ValueChanged += (i) => { data.SortOrder = (int)i; SetDirty(); };
        actionsEdit.Init<DataSelector, string>(
            (selector) => selector.Text,
            (selector, s) => selector.Text = s,
            (selector) => { selector.DataFolder = "Actions"; selector.ItemSelected += (i) => { data.Actions = actionsEdit.GetDatas<string>(); SetDirty(); }; },
            () => { data.Actions = actionsEdit.GetDatas<string>(); SetDirty(); });
    }

    protected override void Refresh()
    {
        nameEdit.Text = data.Name;
        fullNameEdit.Text = data.FullName;
        description1Edit.Text = data.Description1;
        description2Edit.Text = data.Description2;
        sortOrderEdit.Value = data.SortOrder;
        actionsEdit.SetDatas(data.Actions);
    }
}