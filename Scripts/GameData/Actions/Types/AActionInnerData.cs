using Godot;
using System;

public abstract class AActionInnerData
{
    protected AActionInnerData() { }
}

public abstract partial class AActionInnerDataEditor : Control
{
    public abstract AActionInnerData Data { get; set; }

    [Signal]
    public delegate void OnDirtyEventHandler();

    protected void SetDirty() => EmitSignal(SignalName.OnDirty);
}

public abstract partial class AActionInnerDataEditor<T> : AActionInnerDataEditor where T : AActionInnerData
{
    protected T data;

    public override AActionInnerData Data
    {
        get => data;
        set
        {
            if (value is T typedData)
            {
                data = typedData;
                Refresh();
            }
            else
            {
                throw new Exception("Inner data type mismatch!");
            }
        }
    }

    protected abstract void Refresh();
}
