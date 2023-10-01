using Godot;
using System;
using System.Collections.Generic;

public class FormMercury : AForm
{
    public override string Name => "Mercury";

    public override string FullName => "The Mercury Mecha";

    public override string Description1 => "Strong and tough! But oh so slow.";

    public override string Description2 => "Uses magical Fire attacks. Can buff defense. Hates Fire, resists Poison.";

    public override int SortOrder => 0;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAMercuryLimit(), new UAMercuryAttack1(), new UAMercuryBuff1() };

    public override StatsMod StatsMod => new StatsMod(1, 1.2f, 1.5f, 1.2f, 1.5f, 0.6f,
        new KeyValuePair<Element, float>(Element.Poison, 0),
        new KeyValuePair<Element, float>(Element.Fire, 5));
}
