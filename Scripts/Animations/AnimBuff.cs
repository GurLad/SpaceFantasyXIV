using Godot;
using System;

public class AnimBuff : AAnimation<AnimBuff.AnimBuffArgs>
{
    protected override void Animate()
    {
        interpolator.Interpolate(args.MoveUpTime,
            new Interpolator.InterpolateObject(
                a => unit.Position = a,
                unit.Position,
                unit.Position + Vector2.Right * args.Sign * args.MoveUpDistance,
                Easing.EaseInQuart),
            new Interpolator.InterpolateObject(
                a => unit.SetSpriteAnimation(a),
                UnitSprite.Animation.Idle,
                UnitSprite.Animation.Attack,
                (a) => a > 0.5f ? 1 : 0));
        interpolator.OnFinish = () =>
        {
            args.BuffAction();
            unit.AddChild(args.VFX);
            args.VFX.Reparent(unit.GetParent());
            args.VFX.Play();
            interpolator.Delay(args.MoveDelayTime);
            interpolator.OnFinish = () =>
            {
                interpolator.Interpolate(args.MoveBackTime,
                    new Interpolator.InterpolateObject(
                        a => unit.Position = a,
                        unit.Position,
                        unit.Position - Vector2.Right * args.Sign * args.MoveUpDistance,
                        Easing.EaseOutQuart));
                interpolator.OnFinish = () => Done = true;
            };
        };
    }

    public class AnimBuffArgs : AAnimationArgs
    {
        public AnimatedSprite2D VFX;
        public Action BuffAction;
        private bool forward;
        public int Sign => forward ? 1 : -1;
        public float MoveUpTime = 0.5f;
        public float MoveUpDistance = 20;
        public float MoveDelayTime = 0.3f;
        public float MoveBackTime = 0.3f;

        public AnimBuffArgs(string vfx, Action buffAction, bool forward, float moveForwardTime, float moveForwardDistance, float moveDelayTime, float moveBackTime)
        {
            VFX = VFXController.Current.GetVFX(vfx);
            BuffAction = buffAction;
            this.forward = forward;
            MoveUpTime = moveForwardTime;
            MoveUpDistance = moveForwardDistance;
            MoveDelayTime = moveDelayTime;
            MoveBackTime = moveBackTime;
        }

        public AnimBuffArgs(string vfx, Action buffAction, bool forward)
        {
            VFX = VFXController.Current.GetVFX(vfx);
            BuffAction = buffAction;
            this.forward = forward;
        }
    }
}
