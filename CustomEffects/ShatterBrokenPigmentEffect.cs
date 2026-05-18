using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomEffects
{
    //huge thanks to WolfaCola for providing the broken pigment code!
    public class ShatterBrokenPigmentEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = stats.MainManaBar.ConsumeAllManaColor(LoadedDBsHandler.PigmentDB.GetPigment("Broken"), null, "event:/BrokenPigmentBreak");
            return exitAmount > 0;
        }
    }
}