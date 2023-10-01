using Godot;
using System;
using System.Collections.Generic;

public partial class FormPhase3 : AForm
{
    public override string Name => "Phase3";

    public override string FullName => "";

    public override string Description1 => "Uses magical Ice. Weak to Fire.";

    public override string Description2 => "";

    public override int SortOrder => 2;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAEIceBlast() };

    public override StatsMod StatsMod => new StatsMod(1, 1f, 1f, 1.2f, 1.2f, 1,
        new KeyValuePair<Element, float>(Element.Fire, 3),
        new KeyValuePair<Element, float>(Element.Ice, 0.3f));
}
