using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.MiscPatches
{
    [HarmonyPatch]
    public static class PigmentOverflowPatch
    {
        public const string OnTurnFinishedAfterItems = "WolfaCola.ApolloMod_PigmentOverflow";

        [HarmonyPatch(typeof(DepleteOverflowManaUIAction), nameof(DepleteOverflowManaUIAction.Execute))]
        [HarmonyPostfix]
        public static void RemoveBrokenPigment(CombatStats stats)
        {
            stats.MainManaBar.ConsumeAllManaColor(LoadedDBsHandler.PigmentDB.GetPigment("Broken"), null, "event:/BrokenPigmentBreak");
        }
    }
}