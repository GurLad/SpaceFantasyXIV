using Godot;
using System;
using System.Linq;

public partial class ActionDataEditor : ASerializableDataEditor<ActionData>
{
    // Exports
    [ExportCategory("Internal")]
    [Export]
    private LineEdit nameEdit;
    [Export]
    private TextEdit descriptionEdit;
    [Export]
    private Godot.Range sortOrderEdit;
    [Export]
    private OptionButton actionTypeEdit;
    [Export]
    private AActionInnerDataEditor[] innerDataEditors;

    public override void _Ready()
    {
        base._Ready();
        for (int i = 0; i < (int)ActionData.Type.EndMarker; i++)
        {
            actionTypeEdit.AddItem(((ActionData.Type)i).ToString());
        }
        actionTypeEdit.ItemSelected += (i) => { int prev = (int)data.ActionType; data.UpdateType((ActionData.Type)i); UpdateActionType(prev, (int)i); };
        nameEdit.TextChanged += (s) => { data.Name = s; SetDirty(); };
        descriptionEdit.TextChanged += () => { data.Description = descriptionEdit.Text; SetDirty(); };
        sortOrderEdit.ValueChanged += (i) => { data.SortOrder = (int)i; SetDirty(); };
        innerDataEditors.ToList().ForEach(a => a.OnDirty += SetDirty);
    }

    protected override void Refresh()
    {
        nameEdit.Text = data.Name;
        descriptionEdit.Text = data.Description;
        sortOrderEdit.Value = data.SortOrder;
        UpdateActionType(actionTypeEdit.Selected, (int)data.ActionType);
    }

    private void UpdateActionType(int prev, int next, bool updateEdit = true)
    {
        if (prev >= 0)
        {
            innerDataEditors[prev].Visible = false;
        }
        if (updateEdit)
        {
            actionTypeEdit.Selected = next;
        }
        if (next >= 0)
        {
            innerDataEditors[next].Data = data.InnerData;
            innerDataEditors[next].Visible = true;
        }
    }
}
