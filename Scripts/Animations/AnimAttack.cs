using Godot;
using System;

public class AnimAttack : AAnimation<AnimAttack.AnimAttackArgs>
{
    protected override void Animate()
    {
        interpolator.Interpolate(args.MoveForwardTime,
            new Interpolator.InterpolateObject(
                a => unit.Position = a,
                unit.Position,
                unit.Position + Vector2.Right * args.Sign * args.MoveForwardDistance,
                Easing.EaseInQuart),
            new Interpolator.InterpolateObject(
                a => unit.SetSpriteAnimation(a),
                UnitSprite.Animation.Idle,
                UnitSprite.Animation.Attack,
                (a) => a > 0.5f ? 1 : 0));
        interpolator.OnFinish = () =>
        {
            args.TakeDamageAction();
            interpolator.Delay(args.MoveDelayTime);
            interpolator.OnFinish = () =>
            {
                unit.SetSpriteAnimation(UnitSprite.Animation.Idle);
                interpolator.Interpolate(args.MoveBackTime,
                    new Interpolator.InterpolateObject(
                        a => unit.Position = a,
                        unit.Position,
                        unit.Position - Vector2.Right * args.Sign * args.MoveForwardDistance,
                        Easing.EaseOutQuart));
                interpolator.OnFinish = () => Done = true;
            };
        };
    }

    public class AnimAttackArgs : AAnimationArgs
    {
        public Action TakeDamageAction;
        private bool forward;
        public int Sign => forward ? 1 : -1;
        public float MoveForwardTime = 0.5f;
        public float MoveForwardDistance = 20;
        public float MoveDelayTime = 0.3f;
        public float MoveBackTime = 0.3f;

        public AnimAttackArgs(Action takeDamageAction, bool forward, float moveForwardTime, float moveForwardDistance, float moveDelayTime, float moveBackTime)
        {
            TakeDamageAction = takeDamageAction;
            this.forward = forward;
            MoveForwardTime = moveForwardTime;
            MoveForwardDistance = moveForwardDistance;
            MoveDelayTime = moveDelayTime;
            MoveBackTime = moveBackTime;
        }

        public AnimAttackArgs(Action takeDamageAction, bool forward)
        {
            TakeDamageAction = takeDamageAction;
            this.forward = forward;
        }
    }
}
