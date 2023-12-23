using Godot;
using System;

public class ActionData : ISerializableData
{
    public enum Type { Attack, Buff, Limit }

    public Type ActionType { get; set; }
    public AActionInnerData Data { get; set; }

    public string Save()
    {
        return (int)ActionType + "\n" + Data.ToJson();
    }

    public void Load(string data)
    {
        int seperator = data.IndexOf('\n');
        ActionType = (Type)int.Parse(data.Substring(0, seperator));
        string rest = data.Substring(seperator + 1);
        Data = ActionType switch
        {
            Type.Attack => rest.JsonToObject<AttackActionData>(),
            Type.Buff => rest.JsonToObject<BuffActionData>(),
            Type.Limit => rest.JsonToObject<LimitActionData>(),
            _ => throw new Exception("Invalid action type! " + ActionType)
        };
    }

    public void Clear()
    {
        ActionType = Type.Attack;
        Data = new AttackActionData();
    }
}
