using Godot;
using System;
using System.Collections.Generic;

public class StatsMod
{
    public float Health = 1;
    public float PhysAttack = 1;
    public float PhysDefense = 1;
    public float MagAttack = 1;
    public float MagDefense = 1;
    public float Speed = 1;
    public Dictionary<Element, float> ElementMultipliers = new Dictionary<Element, float>();

    public StatsMod(float health, float physAttack, float physDefense, float magAttack, float magDefense, float speed, params KeyValuePair<Element, float>[] elements)
    {
        Health = health;
        PhysAttack = physAttack;
        PhysDefense = physDefense;
        MagAttack = magAttack;
        MagDefense = magDefense;
        Speed = speed;
        for (int i = 0; i < (int)Element.EndMarker; i++)
        {
            ElementMultipliers.Add((Element)i, 1);
        }
        foreach (var pair in elements)
        {
            ElementMultipliers[pair.Key] = pair.Value;
        }
    }

    public Stats Apply(Stats target, bool includeMultipliers = true)
    {
        Stats stats = new Stats();
        stats.Health = Mathf.RoundToInt(target.Health * Health);
        stats.PhysAttack = Mathf.RoundToInt(target.PhysAttack * PhysAttack);
        stats.PhysDefense = Mathf.RoundToInt(target.PhysDefense * PhysDefense);
        stats.MagAttack = Mathf.RoundToInt(target.MagAttack * MagAttack);
        stats.MagDefense = Mathf.RoundToInt(target.MagDefense * MagDefense);
        stats.Speed = Mathf.RoundToInt(target.Speed * Speed);
        if (true)
        {
            for (int i = 0; i < (int)Element.EndMarker; i++)
            {
                stats.ElementMultipliers[(Element)i] = target.ElementMultipliers[(Element)i] * ElementMultipliers[(Element)i];
            }
        }
        return stats;
    }
}
