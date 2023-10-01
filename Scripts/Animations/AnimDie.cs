using Godot;
using System;

public partial class AnimDie : AAnimation<AnimDie.AnimDieArgs>
{
    protected override void Animate()
    {
        unit.SetSpriteAnimation(UnitSprite.Animation.Hurt);
        args.Dissolve = unit.AddDissolveToSpriteAnimation();
        interpolator.Interpolate(args.DissolveTime,
            new Interpolator.InterpolateObject(
                a => args.Dissolve.SetShaderParameter("percent", a),
                0,
                1),
            new Interpolator.InterpolateObject(
                a => unit.Position = new Vector2(a, unit.Position.Y),
                unit.Position.X - args.MaxSideDistance,
                unit.Position.X + args.MaxSideDistance,
                (a) => Mathf.Sin(a * Mathf.Pi * 16)));
        interpolator.OnFinish = () =>
        {
            Done = true;
            //unit.EmitSignal(Unit.SignalName.Died);
        };
    }

    public class AnimDieArgs : AAnimationArgs
    {
        public ShaderMaterial Dissolve;
        public float DissolveTime = 1f;
        public float MaxSideDistance = 0.5f;

        public AnimDieArgs() { }

        public AnimDieArgs(float dissolveTime) => DissolveTime = dissolveTime;
    }
}
