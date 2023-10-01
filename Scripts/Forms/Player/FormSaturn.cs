using Godot;
using System;
using System.Collections.Generic;

public partial class FormSaturn : AForm
{
    public override string Name => "Saturn";

    public override string FullName => "The Saturn Hedgehog";

    public override string Description1 => "Gotta go fast! And hit like a wet noodle.";

    public override string Description2 => "Uses physical Ice & Normal attacks. Applies Chill. Hates Poison, resists Ice.";

    public override int SortOrder => 5;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UASaturnAttack1(), new UASaturnAttack2() };

    public override StatsMod StatsMod => new StatsMod(1, 0.7f, 0.8f, 0.7f, 0.8f, 1.8f,
        new KeyValuePair<Element, float>(Element.Ice, 0),
        new KeyValuePair<Element, float>(Element.Poison, 5));
}
