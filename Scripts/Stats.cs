using Godot;
using System;
using System.Collections.Generic;
using static ExtensionMethods;

public class Stats
{
    public int Health = 10000;
    public int PhysAttack = 100;
    public int PhysDefense = 100;
    public int MagAttack = 100;
    public int MagDefense = 100;
    public int Speed = 100;
    public Dictionary<Element, float> ElementMultipliers = new Dictionary<Element, float>();

    public Stats()
    {
        for (int i = 0; i < (int)Element.EndMarker; i++)
        {
            ElementMultipliers.Add((Element)i, 1);
        }
    }

    public int GetPhysDamage(Stats target, float attackPower, Element element)
    {
        return Mathf.RoundToInt(attackPower * target.ElementMultipliers[element] * RNG.NextFloat(0.95f, 1.05f) * Mathf.Pow(PhysAttack, 1.5f) / target.PhysDefense);
    }

    public int GetMagDamage(Stats target, float attackPower, Element element)
    {
        return Mathf.RoundToInt(attackPower * target.ElementMultipliers[element] * RNG.NextFloat(0.95f, 1.05f) * Mathf.Pow(MagAttack, 1.5f) / target.MagDefense);
    }

    public float GetATBIncrease(double delta)
    {
        return (float)(Speed * delta * 0.8f);
    }
}
