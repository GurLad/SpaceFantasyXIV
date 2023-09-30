using Godot;
using System;

public partial class MainMenu : Control
{
    public void Start()
    {
        SceneController.Current.TransitionToScene("Game");
    }

    public void Quit()
    {
        GetTree().Quit();
    }
}
