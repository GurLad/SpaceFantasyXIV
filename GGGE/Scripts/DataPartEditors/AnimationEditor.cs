using Godot;
using System;
using System.Collections.Generic;
using GGE.Internal;

public partial class AnimationEditor : Control
{
    // Exports
    [Export]
    private string animationName = "default";
    [Export]
    private string dataKey;
    [Export]
    private AGameDataLoader loader;
    [Export]
    private Vector2I targetResolution;
    [ExportCategory("Internal exports")]
    [Export]
    private TextureRect previewRect;
    [Export]
    private Button browseButton;
    [Export]
    private Button togglePreview;
    [Export]
    private FileDialog fileDialog;
    // Properties
    private AnimatedSprite2D data;
    private int currentFrame;
    private Timer previewTimer;
    private List<Texture2D> _frames = new List<Texture2D>();
    private List<Texture2D> frames
    {
        get => _frames;
        set
        {
            _frames = value;
            data.SpriteFrames.RemoveAnimation(animationName);
            data.SpriteFrames.AddAnimation(animationName);
            frames.ForEach(a => data.SpriteFrames.AddFrame(animationName, a));
        }
    }

    [Signal]
    public delegate void OnDirtyEventHandler();

    public override void _Ready()
    {
        base._Ready();
        previewRect.CustomMinimumSize = targetResolution;
        data = loader.GetAnimatedSprite(dataKey);
        OnDirty += () => loader.EmitSignal(AGameDataLoader.SignalName.OnDirty);
        loader.OnExternalChange += () =>
        {
            frames.Clear();
            for (int i = 0; i < data.SpriteFrames.GetFrameCount(animationName); i++)
            {
                frames.Add(data.SpriteFrames.GetFrameTexture(animationName, i));
            }
            UpdatePreview();
        };
        // Init preview timer
        AddChild(previewTimer = new Timer());
        previewTimer.OneShot = false;
        previewTimer.WaitTime = 0.1f; // TEMP - need to add a way to add custom animation speeds in the future
        previewTimer.Timeout += () =>
        {
            if (frames.Count > 0)
            {
                currentFrame = (currentFrame + 1) % frames.Count;
                UpdatePreview();
            }
        };
        togglePreview.Pressed += () =>
        {
            if (previewTimer.IsStopped())
            {
                previewTimer.Start();
                togglePreview.Text = "Stop";
            }
            else
            {
                previewTimer.Stop();
                togglePreview.Text = "Play";
            }
        };
        // Init file dialog
        if (fileDialog == null)
        {
            fileDialog = new FileDialog();
            fileDialog.Init();
        }
        fileDialog.FileSelected += (path) =>
        {
            Image source = Image.LoadFromFile(path);
            if (targetResolution.X > 0)
            {
                frames = source.SplitImage(source.GetWidth() / targetResolution.X);
                UpdatePreview();
                SetDirty();
            }
            else
            {
                // Not ideal, but it works
                Control parent = this;
                while (parent.GetParent() is Control control && control.Visible) { GD.Print(control.Name); parent = control; };
                InputBox.Show(parent, "Enter frame count:", (s) =>
                {
                    int frameCount;
                    if (int.TryParse(s, out frameCount) && frameCount > 0)
                    {
                        frames = source.SplitImage(frameCount);
                        UpdatePreview();
                        SetDirty();
                    }
                    else
                    {
                        MessageBox.ShowError(parent, "Invalid frame count!");
                    }
                });
            }
        };
        browseButton.Pressed += fileDialog.Show;
        AddChild(fileDialog);
    }

    private void UpdatePreview()
    {
        if (frames.Count > 0)
        {
            previewRect.Texture = frames[currentFrame % frames.Count];
        }
        else
        {
            previewRect.Texture = null;
        }
    }

    private void SetDirty() => EmitSignal(SignalName.OnDirty);
}
