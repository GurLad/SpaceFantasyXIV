using Godot;
using System;
using System.Collections.Generic;

public partial class ActionLoader : AGameDataLoader
{
    // Properties
    private ActionData data = new ActionData();

    public override string DataFolder => "Actions";

    protected override List<AGameDataPart> gameDatas => new List<AGameDataPart>()
    {
        new GameDataSerializablePart("Data", data),
    };
}
