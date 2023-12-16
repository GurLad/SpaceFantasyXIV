using Godot;
using System;

public partial class MainMenu : Control
{
    public void Start()
    {
        SceneController.Current.TransitionToScene("Game");
    }

    public void LaunchEditor()
    {
        SceneController.Current.TransitionToScene("Editor");
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}
