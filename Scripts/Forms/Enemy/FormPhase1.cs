using Godot;
using System;
using System.Collections.Generic;

public partial class FormPhase1 : AForm
{
    public override string Name => "Phase1";

    public override string FullName => "";

    public override string Description1 => "Uses Dark. Pretty weak.";

    public override string Description2 => "";

    public override int SortOrder => 0;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAEDarkBlast() };

    public override StatsMod StatsMod => new StatsMod(1, 0.8f, 0.8f, 0.8f, 0.8f, 1);
}
