using Godot;
using System;
using System.Collections.Generic;

public partial class FormPhase5 : AForm
{
    public override string Name => "Phase5";

    public override string FullName => "";

    public override string Description1 => "Uses magical Wind. Weak to Dark & Ice.";

    public override string Description2 => "";

    public override int SortOrder => 4;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAEWindBlast() };

    public override StatsMod StatsMod => new StatsMod(1, 1.4f, 1.4f, 1.6f, 1.6f, 2.5f,
        new KeyValuePair<Element, float>(Element.Dark, 3),
        new KeyValuePair<Element, float>(Element.Wind, 0.3f));
}
