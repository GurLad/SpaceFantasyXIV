using Godot;
using System;

public partial class GameEditor : Control
{
    [Export]
    private Vector2I editorResolution;
    [Export]
    private int editorScale;
    [Export]
    private Vector2I gameResolution;
    [Export]
    private int gameScale;

    public override void _Ready()
    {
        base._Ready();
        Enter();
    }

    public void Enter()
    {
        GetTree().Root.ContentScaleSize = editorResolution;
        GetTree().Root.ContentScaleFactor = editorScale;
    }

    public void Leave()
    {
        SceneController.Current.TransitionToScene("Menu", () =>
        {
            GetTree().Root.ContentScaleSize = gameResolution;
            GetTree().Root.ContentScaleFactor = gameScale;
        });
    }
}
