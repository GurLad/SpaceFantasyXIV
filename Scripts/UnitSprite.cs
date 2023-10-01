using Godot;
using System;

public partial class UnitSprite : Node2D
{
    public enum Animation { Idle, Attack, Hurt }

    // Exports
    [Export]
    private AnimatedSprite2D idleSprite;
    [Export]
    private AnimatedSprite2D attackSprite;
    [Export]
    private AnimatedSprite2D hurtSprite;
    // Properties
    private AnimatedSprite2D currentAnimation;

    public override void _Ready()
    {
        base._Ready();
        currentAnimation = idleSprite;
        SetAnimation(Animation.Idle);
    }

    public void SetAnimation(Animation animation)
    {
        currentAnimation.Visible = false;
        switch (animation)
        {
            case Animation.Idle:
                (currentAnimation = idleSprite).Visible = true;
                idleSprite.Play();
                break;
            case Animation.Attack:
                (currentAnimation = attackSprite).Visible = true;
                break;
            case Animation.Hurt:
                (currentAnimation = hurtSprite).Visible = true;
                break;
            default:
                break;
        }
    }

    public ShaderMaterial SetMaterial(ShaderMaterial material)
    {
        return (ShaderMaterial)(currentAnimation.Material = material);
    }

    public void RemoveMaterial()
    {
        currentAnimation.Material = null;
    }
}
