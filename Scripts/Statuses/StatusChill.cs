using Godot;
using System;

public class StatusChill : AStatus
{
    public override Texture Icon => throw new NotImplementedException();

    public override string Name => "Chill";

    public override string Description => "Brrrr! I'm soooo slooooow noooooow...";

    public override int SortOrder => 3;

    public override StatsMod StatsMod => new StatsMod(1, 1, 1, 1, 0.5f + (1f / (Lifespan + 1)), 1);
}
