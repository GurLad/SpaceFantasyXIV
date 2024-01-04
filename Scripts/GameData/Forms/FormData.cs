using Godot;
using System;
using System.Collections.Generic;

public class FormData : ISerializableData
{
    public string Name { get; set; } = "";
    public string FullName { get; set; } = "";
    public string Description1 { get; set; } = "";
    public string Description2 { get; set; } = "";
    public int SortOrder { get; set; } = 0;
    public List<string> Actions { get; set; } = new List<string>();

    public string Save()
    {
        return this.ToJson();
    }

    public void Load(string data)
    {
        FormData temp = data.JsonToObject<FormData>();
        Name = temp.Name;
        FullName = temp.FullName;
        Description1 = temp.Description1;
        Description2 = temp.Description2;
        SortOrder = temp.SortOrder;
        Actions = temp.Actions;
    }

    public void Clear()
    {
        Name = FullName = Description1 = Description2 = "";
        SortOrder = 0;
        Actions.Clear();
    }
}
