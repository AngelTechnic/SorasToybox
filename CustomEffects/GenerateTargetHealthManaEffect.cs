using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class GenerateTargetHealthManaEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = entryVariable;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                bool hasUnit = targetSlotInfo.HasUnit;
                if (hasUnit)
                {
                    targetSlotInfo.Unit.GenerateHealthMana(entryVariable);
                }
            }
                    
            return exitAmount > 0;
        }
    }
}
