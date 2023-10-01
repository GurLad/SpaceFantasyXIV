using Godot;
using System;

public partial class AnimLimit : AAnimation<AnimLimit.AnimLimitArgs>
{
    protected override void Animate()
    {
        unit.SetSpriteAnimation(UnitSprite.Animation.Idle);
        interpolator.Interpolate(args.MoveBackwardTime,
            new Interpolator.InterpolateObject(
                a => unit.Position = a,
                unit.Position,
                unit.Position - Vector2.Right * args.Sign * args.MoveBackwardDistance,
                Easing.EaseInOutQuad));
        interpolator.OnFinish = () =>
        {
            LimitSprite limit = LimitController.Current.GetLimit(unit.Form.Name);
            unit.AddChild(limit);
            limit.Position = Vector2.Zero;
            limit.Reparent(unit.GetParent());
            interpolator.Delay(args.RollTimePreFade);
            interpolator.OnFinish = () =>
            {
                args.TakeDamageAction();
                SceneController.Current.Transition(() => limit.QueueFree(), () =>
                {
                    unit.SetForm(FormController.PlayerForms[2]);
                    Done = true;
                    unit.QueueAnimation(new AnimRecoverFromDamage(),
                        new AnimRecoverFromDamage.AnimRecoverFromDamageArgs(unit.Forward, args.MoveBackwardDistance));
                    unit.QueueImmediateAction(() => { unit.EmitSignal(Unit.SignalName.NeedReplace); });
                });
            };
        };
    }

    public class AnimLimitArgs : AAnimationArgs
    {
        private bool forward;
        public Action TakeDamageAction;
        public int Sign => forward ? 1 : -1;
        public float MoveBackwardDistance = 120;
        public float MoveBackwardTime = 0.6f;
        public float RollTimePreFade = 1f;

        public AnimLimitArgs(Action takeDamageAction, bool forward)
        {
            TakeDamageAction = takeDamageAction;
            this.forward = forward;
        }
    }
}
