using Godot;
using System;
using System.Collections.Generic;

public partial class FormPhase6 : AForm
{
    public override string Name => "Phase6";

    public override string FullName => "";

    public override string Description1 => "???";

    public override string Description2 => "";

    public override int SortOrder => 1;

    public override List<AUnitAction> Actions => new List<AUnitAction> { new UAEFightBlast() };

    public override StatsMod StatsMod => new StatsMod(1, 2f, 2f, 2f, 2f, 1.5f);
}
