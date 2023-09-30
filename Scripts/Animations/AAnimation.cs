using Godot;
using System;

public abstract class AAnimation
{
    public bool Done { get; private set; }
}

public abstract class AAnimation<T> : AAnimation where T : AAnimationArgs
{
    protected Unit unit;
    protected Interpolator interpolator;
    protected T args;

    protected abstract void Animate();

    public AAnimation Begin(Unit unit, Interpolator interpolator, T args)
    {
        this.args = args;
        this.unit = unit;
        this.interpolator = interpolator;
        Animate();
        return this;
    }
}
