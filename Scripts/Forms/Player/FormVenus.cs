using Godot;
using System;
using System.Collections.Generic;

public class FormVenus : AForm
{
    public override string Name => "Venus";

    public override string FullName => "The Venus Warrior";

    public override string Description1 => "Magic is everything! Can't take a punch.";

    public override string Description2 => "Uses magical Earth & Poison attacks. Applies Poison. Hates Ice, resists Wind.";

    public override int SortOrder => 2;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAVenusAttack1(), new UAVenusAttack2() };

    public override StatsMod StatsMod => new StatsMod(1, 0.5f, 0.4f, 2.0f, 1.8f, 1.0f,
        new KeyValuePair<Element, float>(Element.Wind, 0),
        new KeyValuePair<Element, float>(Element.Ice, 5));
}
