using Godot;
using System;
using System.Collections.Generic;

public partial class StatsModifier : ISerializableData
{
    private Dictionary<string, Formula> statMultiplierMods { get; init; } = new Dictionary<string, Formula>();
    private Dictionary<string, Formula> elementMultiplierMods { get; init; } = new Dictionary<string, Formula>();

    public string Save()
    {
        return new JsonData(statMultiplierMods, elementMultiplierMods).ToJson();
    }

    public void Load(string data)
    {
        JsonData temp = data.JsonToObject<JsonData>();
        foreach (string key in statMultiplierMods.Keys)
        {
            if (temp.StatMultiplierMods?.ContainsKey(key) ?? false)
            {
                GetStatFormula(key).Source = temp.StatMultiplierMods[key].Source;
            }
        }
        foreach (string key in elementMultiplierMods.Keys)
        {
            if (temp.ElementMultiplierMods?.ContainsKey(key) ?? false)
            {
                GetElementFormula(key).Source = temp.ElementMultiplierMods[key].Source;
            }
        }
    }

    public void Clear()
    {
        for (Stat i = 0; i < Stat.EndMarker; i++)
        {
            if (statMultiplierMods.ContainsKey(i.ToString()))
            {
                GetStatFormula(i.ToString()).Source = "1";
            }
            else
            {
                statMultiplierMods.Add(i.ToString(), new Formula("1"));
            }
        }
        foreach (string element in GameDataPreloader.Current.GetAllNames("Elements"))
        {
            if (elementMultiplierMods.ContainsKey(element))
            {
                GetElementFormula(element).Source = "1";
            }
            else
            {
                elementMultiplierMods.Add(element, new Formula("1"));
            }
        }
    }

    public Formula GetStatFormula(string key) => statMultiplierMods[key];

    public Formula GetElementFormula(string key) => elementMultiplierMods[key];

    private record JsonData
    (
        Dictionary<string, Formula> StatMultiplierMods,
        Dictionary<string, Formula> ElementMultiplierMods
    );
}
