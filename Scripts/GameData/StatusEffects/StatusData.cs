using Godot;
using System;

public class StatusData : ISerializableData
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public int SortOrder { get; set; }
    public bool Stacks { get; set; }
    public bool Fades { get; set; }

    public StatusData() => Clear();

    public string Save()
    {
        return this.ToJson();
    }

    public void Load(string data)
    {
        StatusData temp = data.JsonToObject<StatusData>();
        Name = temp.Name;
        ShortName = temp.ShortName;
        Description = temp.Description;
        SortOrder = temp.SortOrder;
        Stacks = temp.Stacks;
        Fades = temp.Fades;
    }

    public void Clear()
    {
        Name = ShortName = Description = "";
        SortOrder = 0;
        Stacks = Fades = true;
    }
}
