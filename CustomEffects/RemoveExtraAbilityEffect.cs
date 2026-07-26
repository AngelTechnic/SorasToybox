using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class RemoveExtraAbilityEffect : EffectSO
    {
        public ExtraAbilityInfo _extraAbility;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (var slot in targets)
            {
                if (slot.HasUnit)
                {
                    slot.Unit.TryRemoveExtraAbility(_extraAbility);
                    exitAmount++;
                }
            }
            return exitAmount > 0;
        }
    }
}
