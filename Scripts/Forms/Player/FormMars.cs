using Godot;
using System;
using System.Collections.Generic;

public class FormMars : AForm
{
    public override string Name => "Mars";

    public override string FullName => "The Mars Commander";

    public override string Description1 => "Has a cat. Doesn't believe in magic.";

    public override string Description2 => "Uses physical Dark & Normal attacks. Hates Fighting, resists Dark.";

    public override int SortOrder => 4;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAMarsAttack1(), new UAMarsAttack2() };

    public override StatsMod StatsMod => new StatsMod(1, 2.0f, 1.8f, 0.5f, 0.4f, 1.0f,
        new KeyValuePair<Element, float>(Element.Dark, 0),
        new KeyValuePair<Element, float>(Element.Fighting, 5));
}
