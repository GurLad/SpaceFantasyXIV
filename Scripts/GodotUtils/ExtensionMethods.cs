using Godot;
using System;
using System.Collections.Generic;

public enum Element { None, Wind, Earth, Fire, Water, Poison, Ice, Lightning, Light, Dark, Fighting, EndMarker }

public static class ExtensionMethods
{
    public static Random RNG { get; } = new Random();

    public static float Distance(this Vector2I origin, Vector2I target)
    {
        return Mathf.Sqrt(Mathf.Pow(origin.X - target.X, 2) + Mathf.Pow(origin.Y - target.Y, 2));
    }

    public static float NextFloat(this Random random, Vector2 range)
    {
        return random.NextFloat(range.X, range.Y);
    }

    public static float NextFloat(this Random random, float minValue, float maxValue)
    {
        return (float)(random.NextDouble() * (maxValue - minValue) + minValue);
    }

    public static float Percent(this Timer timer)
    {
        return (float)(1 - timer.TimeLeft / timer.WaitTime);
    }
}
