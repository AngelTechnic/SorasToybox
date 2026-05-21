using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class CheckEntryOrMoreEmptyManaSlotsEffect : EffectSO
    {
        public bool usePreviousExitValue;

        public ManaColorSO mana;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (usePreviousExitValue)
            {
                entryVariable *= base.PreviousExitValue;
            }
            int amtempty = 0;
            foreach (ManaBarSlot slot in stats.MainManaBar.ManaBarSlots)
            {
                if (slot.IsEmpty)
                {
                    //Debug.Log("added slot");
                    amtempty++;
                }
            }
            exitAmount = amtempty;
            return amtempty >= entryVariable;
        }
    }
}
