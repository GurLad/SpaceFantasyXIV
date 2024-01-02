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
    private LineEdit descriptionEdit;
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
        actionTypeEdit.ItemSelected += (i) => data.UpdateType((ActionData.Type)i);
        nameEdit.TextChanged += (s) => { data.Name = s; SetDirty(); };
        descriptionEdit.TextChanged += (s) => { data.Description = s; SetDirty(); };
        sortOrderEdit.ValueChanged += (i) => { data.SortOrder = (int)i; SetDirty(); };
        innerDataEditors.ToList().ForEach(a => a.OnDirty += SetDirty);
    }

    protected override void Refresh()
    {
        nameEdit.Text = data.Name;
        descriptionEdit.Text = data.Description;
        sortOrderEdit.Value = data.SortOrder;
        if (actionTypeEdit.Selected != (int)data.ActionType)
        {
            if (actionTypeEdit.Selected >= 0)
            {
                innerDataEditors[actionTypeEdit.Selected].Visible = false;
            }
            actionTypeEdit.Selected = (int)data.ActionType;
            innerDataEditors[actionTypeEdit.Selected].Data = data.InnerData;
            innerDataEditors[actionTypeEdit.Selected].Visible = true;
        }
    }
}
