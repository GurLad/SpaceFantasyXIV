using Godot;
using System;

public partial class GameDataAudioStreamPart : GGE.Internal.AGameDataPart<GameDataAudioStreamPart.StreamWithPath, AudioStream>
{
    public GameDataAudioStreamPart(string name, AudioStreamPlayer player) :
        base(name, new StreamWithPath(player), ".ogg") { }

    public override void Clear()
    {
        SourceNode.Path = "";
        SourceNode.Player.Stream = null;
    }

    public override void Load(string folderPath)
    {
        SourceNode.Path = GetFullPath(folderPath);
        if (FileAccess.FileExists(SourceNode.Path))
        {
            SourceNode.Player.Stream = AudioStreamOggVorbis.LoadFromFile(SourceNode.Path);
        }
        else
        {
            SourceNode.Path = "";
        }
    }

    protected override void LoadFromRecordInternal(AudioStream record)
    {
        SourceNode.Player.Stream = record;
    }

    public override void Save(string folderPath)
    {
        if (string.IsNullOrEmpty(SourceNode.Path))
        {
            if (SourceNode.Player.Stream != null)
            {
                throw new Exception("Trying to save a stream without a defined path!");
            }
            else
            {
                return; // There's nothing to save
            }
        }
        if (SourceNode.Path != GetFullPath(folderPath))
        {
            DirAccess.CopyAbsolute(SourceNode.Path, GetFullPath(folderPath));
        }
    }

    protected override AudioStream SaveToRecordInternal()
    {
        return SourceNode.Player.Stream;
    }

    public record StreamWithPath(AudioStreamPlayer Player)
    {
        public string Path { get; set; } = "";
    }
}
