using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameDataAnimatedSpritePart : GGE.Internal.AGameDataPart<AnimatedSprite2D, SpriteFrames>
{
    private const char SEPERATOR = '-';
    private const string DATA_FILE = "AnimationData.data";

    private bool lockAnimations = true;
    private List<string> baseAnimations = new List<string>();
    private SpriteFrames spriteFrames => SourceNode.SpriteFrames;

    public GameDataAnimatedSpritePart(string name, AnimatedSprite2D sourceNode, string fileExtension = ".png") :
        base(name, sourceNode, fileExtension)
    {
        lockAnimations = false;
    }

    public GameDataAnimatedSpritePart(string name, AnimatedSprite2D sourceNode, string fileExtension, params string[] baseAnimations) :
        base(name, sourceNode, fileExtension)
    {
        lockAnimations = true;
        this.baseAnimations = baseAnimations.ToList();
    }

    public override void Clear()
    {
        spriteFrames.ClearAll();
        if (lockAnimations)
        {
            baseAnimations.FindAll(a => a != "default").ForEach(a => spriteFrames.AddAnimation(a));
        }
    }

    public override void Load(string folderPath)
    {
        spriteFrames.ClearAll();
        string basePath = GetFullPath(folderPath, false);
        string data = FileSystem.LoadTextFile(basePath + SEPERATOR + DATA_FILE, "");
        List<AnimationData> animationData = data.JsonToObject<List<AnimationData>>();
        List<string> animations = lockAnimations ? baseAnimations : animationData.ConvertAll(a => a.Name);
        for (int i = 0; i < animations.Count; i++)
        {
            string animation = animations[i];
            if (animation != "default")
            {
                spriteFrames.AddAnimation(animation);
            }
            if (i < animationData.Count)
            {
                List<Texture2D> frames = FileSystem.LoadAnimatedTextureFile(basePath + SEPERATOR + animation, animationData[i].NumFrames);
                frames.ForEach(a => spriteFrames.AddFrame(animation, a));
            }
        }
    }

    protected override void LoadFromRecordInternal(SpriteFrames record)
    {
        if (lockAnimations)
        {
            List<string> recordAnimations = record.GetAnimationNames().ToList();
            if (baseAnimations.Count != recordAnimations.Count ||
                baseAnimations.Find(a => !recordAnimations.Contains(a)) != null)
            {
                throw new Exception("Inconsistent record vs. source!" +
                    "\nSource: " + string.Join(", ", baseAnimations) +
                    "\nRecord: " + string.Join(", ", recordAnimations));
            }
        }
        SourceNode.SpriteFrames = record;
    }

    public override void Save(string folderPath)
    {
        string basePath = GetFullPath(folderPath, false);
        List<AnimationData> animationData = new List<AnimationData>();
        List<string> animations = lockAnimations ? baseAnimations : spriteFrames.GetAnimationNames().ToList();
        for (int i = 0; i < animations.Count; i++)
        {
            string animation = animations[i];
            Image template = spriteFrames.GetFrameTexture(animation, 0)?.GetImage();
            if (template != null)
            {
                int numFrames = spriteFrames.GetFrameCount(animation);
                animationData.Add(new AnimationData(animation, numFrames));
                Image result = Image.Create(template.GetWidth() * numFrames, template.GetHeight(), false, template.GetFormat());
                Rect2I sourceRect = new Rect2I(0, 0, template.GetWidth(), template.GetHeight());
                for (int j = 0; j < numFrames; j++)
                {
                    result.BlitRect(spriteFrames.GetFrameTexture(animation, j)?.GetImage(), sourceRect, new Vector2I(j * template.GetWidth(), 0));
                }
                result.SavePng(basePath + SEPERATOR + animation + fileExtension);
            }
        }
        using var dataFile = FileAccess.Open(basePath + SEPERATOR + DATA_FILE, FileAccess.ModeFlags.Write);
        if (dataFile == null)
        {
            throw new Exception("Error creating file " + (basePath + SEPERATOR + DATA_FILE) + "!");
        }
        dataFile.StoreString(animationData.ToJson());
    }

    protected override SpriteFrames SaveToRecordInternal()
    {
        return spriteFrames;
    }

    private record AnimationData(string Name, int NumFrames);
}
