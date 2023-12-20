using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract partial class AGameDataLoader : Node
{
    public abstract string DataFolder { get; }
    protected abstract List<GGE.Internal.AGameDataPart> gameDatas { get; }
    protected virtual string iconKey => "";

    private List<GGE.Internal.AGameDataPart> _gameDatas = null;
    private List<GGE.Internal.AGameDataPart> loadedGameDatas => _gameDatas ??= gameDatas;
    public bool Visible
    {
        set
        {
            GetChildren().ToList().ForEach(a =>
            {
                if (a.HasMethod("set_visible"))
                {
                    a.Call("set_visible", value);
                }
            });
        }
    }

    [Signal]
    public delegate void OnDirtyEventHandler();

    [Signal]
    public delegate void OnExternalChangeEventHandler();

    public void Load(string name, string folder)
    {
        var preloadedData = GameDataPreloader.Current?.GetRecord(DataFolder, name, folder);
        if (preloadedData != null)
        {
            LoadFromRecord(preloadedData);
            return;
        }
        string folderPath = this.GetFolderPath(name, folder, false);
        loadedGameDatas.ForEach(a => a.Load(folderPath));
        EmitSignal(SignalName.OnExternalChange);
    }

    public void Load(string name)
    {
        var preloadedData = GameDataPreloader.Current?.GetRecord(DataFolder, name);
        if (preloadedData != null)
        {
            LoadFromRecord(preloadedData);
        }
        else
        {
            throw new Exception(name + " not found!");
        }
    }

    private void LoadFromRecord(GameDataPreloader.GameDataRecord record)
    {
        loadedGameDatas.ForEach(a => a.LoadFromRecord(record.Records.Find(b => b.Item1 == a.Name)));
        EmitSignal(SignalName.OnExternalChange);
    }

    public void Save(string name, string folder)
    {
        string folderPath = this.GetFolderPath(name, folder, true);
        loadedGameDatas.ForEach(a => a.Save(folderPath));
    }

    public GameDataPreloader.GameDataRecord SaveToRecord()
    {
        GameDataPreloader.GameDataRecord record = new GameDataPreloader.GameDataRecord(new List<(string, object)>());
        loadedGameDatas.ForEach(a => record.Records.Add((a.Name, a.SaveToRecord())));
        return record;
    }

    public void New()
    {
        gameDatas.ForEach(a => a.Clear());
        EmitSignal(SignalName.OnExternalChange);
    }

    public Texture2D GetIcon(string name, string folder = "")
    {
        if (iconKey == "") return null;
        string folderPath = this.GetFolderPath(name, folder, true);
        if (FileAccess.FileExists(folderPath + FileSystem.SEPERATOR + iconKey + ".png"))
        {
            return ImageTexture.CreateFromImage(Image.LoadFromFile(folderPath + FileSystem.SEPERATOR + iconKey + ".png"));
        }
        else
        {
            return new Texture2D();
        }
    }

    public T GetData<T>(string key) where T : ISerializableData
    {
        GameDataSerializablePart part = (GameDataSerializablePart)gameDatas.Find(a => a is GameDataSerializablePart serializablePart && a.Name == key);
        return part != null ? (part.SourceNode is T result ? result :
            throw new Exception("Type mismatch! " + key + " isn't " + typeof(T))) :
            throw new Exception("No key! " + key);
    }

    public Sprite2D GetSprite(string key)
    {
        GameDataSpritePart part = (GameDataSpritePart)gameDatas.Find(a => a is GameDataSpritePart spritePart && a.Name == key);
        return part?.SourceNode ?? throw new Exception("No key! " + key);
    }
}
