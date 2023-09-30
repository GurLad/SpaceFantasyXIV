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
                1));
        interpolator.OnFinish = () =>
        {
            Done = true;
            unit.EmitSignal(Unit.SignalName.Died);
        };
    }

    public class AnimDieArgs : AAnimationArgs
    {
        public ShaderMaterial Dissolve;
        public float DissolveTime = 0.3f;

        public AnimDieArgs(float dissolveTime) => DissolveTime = dissolveTime;
    }
}
