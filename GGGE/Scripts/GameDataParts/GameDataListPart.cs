using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameDataListPart<ItemType, ItemNodeType, ItemRecordType> : GGE.Internal.AGameDataPart<Node, List<ItemRecordType>>
    where ItemType : GGE.Internal.AGameDataPart<ItemNodeType, ItemRecordType> where ItemNodeType : Node
{
    protected override string DATA_FILE => "ListData.data";

    public Func<ItemType> CreateItem { get; init; }
    public List<ItemType> Items => generatedItems.ToList(); // Clone them to prevent shenanigans
    private List<ItemType> generatedItems { get; } = new List<ItemType>();

    public GameDataListPart(string name, Node sourceNode, PackedScene itemNodeScene, Func<string, ItemNodeType, ItemType> newItemFunc) :
        base(name, sourceNode, "")
    {
        CreateItem = () =>
        {
            ItemNodeType generatedNode = itemNodeScene.Instantiate<ItemNodeType>();
            SourceNode.AddChild(generatedNode);
            ItemType generatedItem = newItemFunc(Name + SEPERATOR + generatedItems.Count, generatedNode);
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
        string basePath = GetFullPath(folderPath, false);
        ListData data = FileSystem.LoadTextFile(basePath + SEPERATOR + DATA_FILE, "").JsonToObject<ListData>();
        for (int i = 0; i < data.Amount; i++)
        {
            CreateItem().Load(folderPath);
        }
    }

    public override void Save(string folderPath)
    {
        generatedItems.ForEach(a => a.Save(folderPath));
        string basePath = GetFullPath(folderPath, false);
        using var dataFile = FileAccess.Open(basePath + SEPERATOR + DATA_FILE, FileAccess.ModeFlags.Write);
        if (dataFile == null)
        {
            throw new Exception("Error creating file " + (basePath + SEPERATOR + DATA_FILE) + "!");
        }
        dataFile.StoreString(new ListData(generatedItems.Count).ToJson());
    }

    protected override void LoadFromRecordInternal(List<ItemRecordType> record)
    {
        Clear();
        record.ForEach(a =>
        {
            ItemType generatedItem = CreateItem();
            generatedItem.LoadFromRecord(a);
        });
    }

    protected override List<ItemRecordType> SaveToRecordInternal()
    {
        return generatedItems.ConvertAll(a => (ItemRecordType)a.SaveToRecord());
    }

    private record ListData(int Amount);
}
