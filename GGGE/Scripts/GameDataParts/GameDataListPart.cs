using Godot;
using System;
using System.Collections.Generic;

public partial class GameDataListPart<ItemType, ItemNodeType, ItemRecordType> : GGE.Internal.AGameDataPart<Node, List<ItemRecordType>>
    where ItemType : GGE.Internal.AGameDataPart<ItemNodeType, ItemRecordType> where ItemNodeType : Node
{
    private Func<ItemType> newItem;
    private List<ItemType> generatedItems = new List<ItemType>();

    public GameDataListPart(string name, Node sourceNode, PackedScene itemNodeScene, Func<ItemNodeType, ItemType> newItem, string fileExtension) :
        base(name, sourceNode, fileExtension)
    {
        this.newItem = () =>
        {
            ItemNodeType generatedNode = itemNodeScene.Instantiate<ItemNodeType>();
            SourceNode.AddChild(generatedNode);
            ItemType generatedItem = newItem(generatedNode);
            generatedItems.Add(generatedItem);
            return generatedItem;
        };
    }

    public override void Clear()
    {
        generatedItems.ForEach(a => a.SourceNode.QueueFree());
        generatedItems.Clear();
    }

    public override void Load(string folderPath)
    {
        Clear();
        // TBA
    }

    public override void Save(string folderPath)
    {
        // TBA
    }

    protected override void LoadFromRecordInternal(List<ItemRecordType> record)
    {
        Clear();
        record.ForEach(a =>
        {
            ItemType generatedItem = newItem();
            generatedItem.LoadFromRecord(a);
        });
    }

    protected override List<ItemRecordType> SaveToRecordInternal()
    {
        return generatedItems.ConvertAll(a => (ItemRecordType)a.SaveToRecord());
    }
}
