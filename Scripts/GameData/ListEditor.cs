using Godot;
using System;
using System.Collections.Generic;

public partial class ListEditor : Control
{
    [Export]
    private PackedScene sceneInnerEditor;
    [ExportCategory("Internal")]
    [Export]
    private Container listContainer;
    [Export]
    private Container itemContainerTemplate;
    [Export]
    private BaseButton newButton;
    [Export]
    private BaseButton deleteButtonTemplate;
    // Properties
    private List<Control> items { get; } = new List<Control>();
    // Functions
    private Func<Control, object> getDataFunc;
    private Action<Control, object> setDataFunc;
    private Action<Control> initEditor;

    [Signal]
    public delegate void OnItemAddedEventHandler();

    [Signal]
    public delegate void OnItemRemovedEventHandler();

    public override void _Ready()
    {
        base._Ready();
        newButton.Pressed += () => NewEditor();
    }

    public void Init<InnerEditorType, DataType>(
        Func<InnerEditorType, DataType> getDataFunc,
        Action<InnerEditorType, DataType> setDataFunc,
        Action<InnerEditorType> initEditor,
        Action dirtyAction) where InnerEditorType : Control
    {
        this.getDataFunc = (n) => getDataFunc((InnerEditorType)n);
        this.setDataFunc = (n, o) => setDataFunc((InnerEditorType)n, (DataType)o);
        this.initEditor = (n) => { if (n is InnerEditorType innerEditor) initEditor((InnerEditorType)n); else throw new Exception("Inner editor type mismatch!"); };
        OnItemAdded += () => dirtyAction();
        OnItemRemoved += () => dirtyAction();
    }

    public List<DataType> GetDatas<DataType>()
    {
        return items.ConvertAll(a => (DataType)getDataFunc(a));
    }

    public void SetDatas<DataType>(List<DataType> datas)
    {
        items.ForEach(a => DeleteEditor(a));
        items.Clear();
        datas.ForEach(a => setDataFunc(NewEditor(), a));
    }

    private Control NewEditor()
    {
        // Create editor
        Control newEditor = sceneInnerEditor.Instantiate<Control>();
        initEditor(newEditor);
        items.Add(newEditor);
        EmitSignal(SignalName.OnItemAdded);
        // Create editor container
        Container container = (Container)itemContainerTemplate.Duplicate();
        BaseButton newDeleteButton = (BaseButton)deleteButtonTemplate.Duplicate();
        container.AddChild(newDeleteButton);
        container.AddChild(newEditor);
        listContainer.AddChild(container);
        container.Visible = newDeleteButton.Visible = newEditor.Visible = true;
        newDeleteButton.Pressed += () => DeleteEditor(newEditor);
        return newEditor;
    }

    private void DeleteEditor(Control editor)
    {
        items.Remove(editor);
        editor.GetParent().QueueFree();
    }

    private class ListEditorFunctions<InnerEditorType, DataType> { }
}
