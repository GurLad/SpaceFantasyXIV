using Godot;
using System;

public partial class VFX : AnimatedSprite2D
{
    [Export]
    public AudioStream SFX;

    public void PlayWithSound()
    {
        Play();
        SoundController.Current.PlaySFX(SFX);
    }
}
