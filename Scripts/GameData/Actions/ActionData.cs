using Godot;
using System;

public class ActionData : ISerializableData
{
    public enum Type { Attack, Buff, Limit, EndMarker }

    public string Name { get; set; }
    public string Description { get; set; }
    public int SortOrder { get; set; }
    public Type ActionType { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public AActionInnerData InnerData { get; set; }

    public string Save()
    {
        return this.ToJson(false) + "\n" + InnerData.ToJson();
    }

    public void Load(string data)
    {
        int seperator = data.IndexOf('\n');
        var actionData = data.Substring(0, seperator).JsonToObject<ActionData>();
        Name = actionData.Name;
        Description = actionData.Description;
        SortOrder = actionData.SortOrder;
        ActionType = actionData.ActionType;
        string rest = data.Substring(seperator + 1);
        InnerData = ActionType switch
        {
            Type.Attack => rest.JsonToObject<AttackActionData>(),
            Type.Buff => rest.JsonToObject<BuffActionData>(),
            Type.Limit => rest.JsonToObject<LimitActionData>(),
            _ => throw new Exception("Invalid action type! " + ActionType)
        };
    }

    public void Clear()
    {
        Name = "";
        Description = "";
        SortOrder = 0;
        ActionType = Type.Attack;
        InnerData = new AttackActionData();
    }

    public void UpdateType(Type newType)
    {
        if (ActionType != newType)
        {
            ActionType = newType;
            InnerData = ActionType switch
            {
                Type.Attack => new AttackActionData(),
                Type.Buff => new BuffActionData(),
                Type.Limit => new LimitActionData(),
                _ => throw new Exception("Invalid action type! " + ActionType)
            };
        }
    }
}
