using Godot;
using System;

public partial class GameDataPairPart<T, TNode, TRecord, S, SNode, SRecord> :
    GGE.Internal.AGameDataPart<GameDataPairPart<T, TNode, TRecord, S, SNode, SRecord>.GameDataPairPartNode, (TRecord, SRecord)>
    where T : GGE.Internal.AGameDataPart<TNode, TRecord> where S : GGE.Internal.AGameDataPart<SNode, SRecord>
{
    public GameDataPairPart(string name, Node parentNode, T item1, S item2) :
        this(name, parentNode, item1, item2, "Item1", "Item2")
    { }

    public GameDataPairPart(string name, Node parentNode, T item1, S item2, string item1Name, string item2Name) :
        base(name, null, "")
    {
        SourceNode = new GameDataPairPartNode(item1, item2);
        parentNode.AddChild(SourceNode);
        item1.Name = Name + SEPERATOR + item1Name;
        item2.Name = Name + SEPERATOR + item2Name;
        if (item1.SourceNode is Node node1 && !node1.IsInsideTree())
        {
            SourceNode.AddChild(node1);
        }
        if (item2.SourceNode is Node node2 && !node2.IsInsideTree())
        {
            SourceNode.AddChild(node2);
        }
    }

    public override void Clear()
    {
        SourceNode.Item1.Clear();
        SourceNode.Item2.Clear();
    }

    public override void Load(string folderPath)
    {
        SourceNode.Item1.Load(folderPath);
        SourceNode.Item2.Load(folderPath);
    }

    public override void Save(string folderPath)
    {
        SourceNode.Item1.Save(folderPath);
        SourceNode.Item2.Save(folderPath);
    }

    protected override void LoadFromRecordInternal((TRecord, SRecord) record)
    {
        SourceNode.Item1.LoadFromRecord(record.Item1);
        SourceNode.Item2.LoadFromRecord(record.Item2);
    }

    protected override (TRecord, SRecord) SaveToRecordInternal()
    {
        return ((TRecord)SourceNode.Item1.SaveToRecord(), (SRecord)SourceNode.Item2.SaveToRecord());
    }

    public partial class GameDataPairPartNode : Node
    {
        public T Item1 { get; init; }
        public S Item2 { get; init; }

        public GameDataPairPartNode(T item1, S item2)
        {
            Item1 = item1;
            Item2 = item2;
        }
    }
}
