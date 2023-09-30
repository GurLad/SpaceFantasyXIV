using Godot;
using System;

public partial class AnimDelay : AAnimation<AnimDelay.AnimDelayArgs>
{
    protected override void Animate()
    {
        interpolator.Delay(args.Time);
        interpolator.OnFinish = () => Done = true;
    }

    public class AnimDelayArgs : AAnimationArgs
    {
        public float Time = 0.3f;

        public AnimDelayArgs(float time) => Time = time;
    }
}
