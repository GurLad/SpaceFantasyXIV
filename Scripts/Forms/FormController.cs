using Godot;
using System;
using System.Collections.Generic;

public static class FormController
{
    public static List<AForm> PlayerForms { get; } = new List<AForm> {
        new FormMercury(),
        new FormVenus(),
        new FormEarth(),
        new FormMars(),
        new FormSaturn() };

    public static List<AForm> EnemyForms { get; } = new List<AForm> {
        new FormPhase1() };

    public static bool[] LivingPlayerForms { get; } = new bool[] { true, true, true, true, true };

    public static int BossPhase { get; private set; } = 0;

    public static void Reset()
    {
        for (int i = 0; i < LivingPlayerForms.Length; i++)
        {
            LivingPlayerForms[i] = true;
        }
        BossPhase = 0;
    }

    public static AForm GetNextBossForm()
    {
        return EnemyForms[BossPhase++];
    }
}
