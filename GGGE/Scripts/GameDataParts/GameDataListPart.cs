using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameDataListPart<ItemType, ItemNodeType, ItemRecordType> :
    GGE.Internal.AGameDataPart<GameDataListPart<ItemType, ItemNodeType, ItemRecordType>.GameDataListPartNode, List<ItemRecordType>>
    where ItemType : GGE.Internal.AGameDataPart<ItemNodeType, ItemRecordType> where ItemNodeType : Node
{
    protected override string DATA_FILE => "ListData.data";

    public GameDataListPart(string name, Node parentNode, PackedScene itemNodeScene, Func<string, ItemNodeType, ItemType> newItemFunc) :
        base(name, null, "")
    {
        SourceNode = new GameDataListPartNode(itemNodeScene, newItemFunc);
        parentNode.AddChild(SourceNode);
    }

    public override void Clear()
    {
        SourceNode.ClearItems();
    }

    public override void Load(string folderPath)
    {
        Clear();
        string basePath = GetFullPath(folderPath, false);
        ListData data = FileSystem.LoadTextFile(basePath + SEPERATOR + DATA_FILE, "").JsonToObject<ListData>();
        for (int i = 0; i < data.Amount; i++)
        {
            SourceNode.CreateItem().Load(folderPath);
        }
    }

    public override void Save(string folderPath)
    {
        for (int i = 0; i < SourceNode.Items.Count; i++)
        {
            SourceNode.Items[i].Name = Name + SEPERATOR + i;
            SourceNode.Items[i].Save(folderPath);
        }
        string basePath = GetFullPath(folderPath, false);
        using var dataFile = FileAccess.Open(basePath + SEPERATOR + DATA_FILE, FileAccess.ModeFlags.Write);
        if (dataFile == null)
        {
            throw new Exception("Error creating file " + (basePath + SEPERATOR + DATA_FILE) + "!");
        }
        dataFile.StoreString(new ListData(SourceNode.Items.Count).ToJson());
    }

    protected override void LoadFromRecordInternal(List<ItemRecordType> record)
    {
        Clear();
        record.ForEach(a =>
        {
            ItemType generatedItem = SourceNode.CreateItem();
            generatedItem.LoadFromRecord(a);
        });
    }

    protected override List<ItemRecordType> SaveToRecordInternal()
    {
        return SourceNode.Items.ConvertAll(a => (ItemRecordType)a.SaveToRecord());
    }

    private record ListData(int Amount);

    public partial class GameDataListPartNode : Node
    {
        public List<ItemType> Items => generatedItems.ToList(); // Clone them to prevent shenanigans
        private List<ItemType> generatedItems { get; } = new List<ItemType>();
        private PackedScene itemNodeScene { get; init; }
        private Func<string, ItemNodeType, ItemType> newItemFunc { get; init; }

        public GameDataListPartNode(PackedScene itemNodeScene, Func<string, ItemNodeType, ItemType> newItemFunc)
        {
            this.itemNodeScene = itemNodeScene;
            this.newItemFunc = newItemFunc;
        }

        public ItemType CreateItem()
        {
            ItemNodeType generatedNode = itemNodeScene.Instantiate<ItemNodeType>();
            AddChild(generatedNode);
            ItemType generatedItem = newItemFunc(Name + SEPERATOR + generatedItems.Count, generatedNode);
            generatedItems.Add(generatedItem);
            return generatedItem;
        }

        public void ClearItems()
        {
            generatedItems.ForEach(a => a.SourceNode.QueueFree());
            generatedItems.Clear();
        }
    }
}
