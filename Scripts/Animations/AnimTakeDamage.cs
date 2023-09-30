using Godot;
using System;

public partial class AnimTakeDamage : AAnimation<AnimTakeDamage.AnimTakeDamageArgs>
{
    protected override void Animate()
    {
        unit.SetSpriteAnimation(UnitSprite.Animation.Hurt);
        unit.AddChild(args.VFX);
        args.VFX.Reparent(unit.GetParent());
        args.VFX.Play();
        interpolator.Interpolate(args.MoveBackTime,
            new Interpolator.InterpolateObject(
                a => unit.Position = a,
                unit.Position,
                unit.Position - Vector2.Right * args.Sign * args.MoveBackTime,
                Easing.EaseOutElastic));
        interpolator.OnFinish = () =>
        {
            interpolator.Delay(args.MoveDelayTime);
            interpolator.OnFinish = () =>
            {
                args.VFX.QueueFree();
                interpolator.OnFinish = () => Done = true;
            };
        };
    }

    public class AnimTakeDamageArgs : AAnimationArgs
    {
        public AnimatedSprite2D VFX;
        private bool forward;
        public int Sign => forward ? 1 : -1;
        public float MoveBackDistance = 20;
        public float MoveDelayTime = 0.3f;
        public float MoveBackTime = 0.3f;

        public AnimTakeDamageArgs(string vfx, bool forward)
        {
            VFX = VFXController.Current.GetVFX(vfx);
            this.forward = forward;
        }
    }
}
