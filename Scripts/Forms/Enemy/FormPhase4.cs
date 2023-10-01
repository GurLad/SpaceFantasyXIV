using Godot;
using System;
using System.Collections.Generic;

public partial class FormPhase4 : AForm
{
    public override string Name => "Phase4";

    public override string FullName => "";

    public override string Description1 => "Uses physical Poison, which Poisons. Weak to Ice.";

    public override string Description2 => "";

    public override int SortOrder => 1;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAEPoisonBlast() };

    public override StatsMod StatsMod => new StatsMod(1, 1.4f, 1.4f, 1.2f, 1.2f, 1,
        new KeyValuePair<Element, float>(Element.Ice, 3),
        new KeyValuePair<Element, float>(Element.Poison, 0.3f));
}
