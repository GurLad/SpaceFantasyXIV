using Godot;
using System;
using System.Collections.Generic;

public partial class StatsModifier : ISerializableData
{
    private Dictionary<string, Formula> statMultiplierMods = new Dictionary<string, Formula>();
    private Dictionary<string, Formula> elementMultiplierMods = new Dictionary<string, Formula>();

    public string Save()
    {
        return this.ToJson();
    }

    public void Load(string data)
    {
        StatsModifier temp = data.JsonToObject<StatsModifier>();
        statMultiplierMods = temp.statMultiplierMods;
        elementMultiplierMods = temp.elementMultiplierMods;
    }

    public void Clear()
    {
        statMultiplierMods.Clear();
        for (Stat i = 0; i < Stat.EndMarker; i++)
        {
            statMultiplierMods.Add(i.ToString(), new Formula("1"));
        }
        elementMultiplierMods.Clear();
        foreach (string element in GameDataPreloader.Current.GetAllNames("Elements"))
        {
            elementMultiplierMods.Add(element, new Formula("1"));
        }
    }

    public Formula GetStatFormula(string key) => statMultiplierMods[key];

    public Formula GetElementFormula(string key) => elementMultiplierMods[key];
}
