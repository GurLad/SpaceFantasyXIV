using Godot;
using System;
using System.Collections.Generic;

public class FormEarth : AForm
{
    public override string Name => "Earth";

    public override string FullName => "The Earth Human";

    public override string Description1 => "YOU WILL LOSE IF IT DIES!!!";

    public override string Description2 => "Uses physical normal attacks. Very average.";

    public override int SortOrder => 2;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAVenusAttack1(), new UAVenusAttack2() };

    public override StatsMod StatsMod => new StatsMod(1, 1, 1, 1, 1, 1);
}
