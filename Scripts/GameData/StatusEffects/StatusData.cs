using Godot;
using System;

public class StatusData : ISerializableData
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public int SortOrder { get; set; }
    public bool Stacks { get; set; } = true;
    public bool Fades { get; set; } = true;
    public PhantasyScript BeginTurn { get; set; } = new PhantasyScript("");
    public PhantasyScript EndTurn { get; set; } = new PhantasyScript("");

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
        BeginTurn.Source = temp.BeginTurn.Source;
        EndTurn.Source = temp.EndTurn.Source;
    }

    public void Clear()
    {
        BeginTurn.Source = EndTurn.Source = Name = ShortName = Description = "";
        SortOrder = 0;
        Stacks = Fades = true;
    }
}
