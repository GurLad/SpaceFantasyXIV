using Godot;
using System;
using System.Collections.Generic;

public partial class FormPhase2 : AForm
{
    public override string Name => "Phase2";

    public override string FullName => "";

    public override string Description1 => "Uses physical Fire, which Burns. Weak to Earth.";

    public override string Description2 => "";

    public override int SortOrder => 1;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAEFireBlast() };

    public override StatsMod StatsMod => new StatsMod(1, 1f, 1f, 0.8f, 0.8f, 1,
        new KeyValuePair<Element, float>(Element.Earth, 3),
        new KeyValuePair<Element, float>(Element.Fire, 0.3f));
}
