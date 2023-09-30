using Godot;
using System;

public class AnimRecoverFromDamage : AAnimation<AnimRecoverFromDamage.AnimRecoverFromDamageArgs>
{
    protected override void Animate()
    {
        unit.SetSpriteAnimation(UnitSprite.Animation.Idle);
        interpolator.Interpolate(args.MoveForwardTime,
            new Interpolator.InterpolateObject(
                a => unit.Position = a,
                unit.Position,
                unit.Position + Vector2.Right * args.Sign * args.MoveForwardDistance,
                Easing.EaseInOutQuad));
        interpolator.OnFinish = () => Done = true;
    }

    public class AnimRecoverFromDamageArgs : AAnimationArgs
    {
        private bool forward;
        public int Sign => forward ? 1 : -1;
        public float MoveForwardDistance = 20;
        public float MoveForwardTime = 0.3f;

        public AnimRecoverFromDamageArgs(bool forward)
        {
            this.forward = forward;
        }
    }
}
