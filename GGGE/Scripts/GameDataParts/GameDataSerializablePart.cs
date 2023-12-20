using Godot;
using System;

public class GameDataSerializablePart : GGE.Internal.AGameDataPart<ISerializableData, string>
{
    public GameDataSerializablePart(string name, ISerializableData sourceNode, string fileExtension = ".json") :
        base(name, sourceNode, fileExtension) { }

    public override void Clear()
    {
        SourceNode.Clear();
    }

    public override void Load(string folderPath)
    {
        string file = FileSystem.LoadTextFile(folderPath + FileSystem.SEPERATOR + Name, fileExtension);
        if (file != null)
        {
            SourceNode.Load(file);
        }
        else
        {
            SourceNode.Clear();
        }
    }

    protected override void LoadFromRecord(string record)
    {
        SourceNode.Load(record);
    }

    public override void Save(string folderPath)
    {
        using var file = FileAccess.Open(folderPath + FileSystem.SEPERATOR + Name + fileExtension, FileAccess.ModeFlags.Write);
        if (file == null)
        {
            throw new Exception("Error creating file " + (folderPath + FileSystem.SEPERATOR + Name + fileExtension) + "!");
        }
        file.StoreString(SourceNode.Save());
    }

    public override string SaveToRecord()
    {
        return SourceNode.Save();
    }
}
