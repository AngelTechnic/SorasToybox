using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using UnityEngine;

namespace SorasToybox.CustomEffects
{

    public class FieldEffect_ApplyByMissingHealth_Effect : EffectSO
    {
        [Header("Field")]
        public FieldEffect_SO _Field;

        [Header("Previous Random Option Data")]
        public bool _UseRandomBetweenPrevious;

        [Header("Previous Multiplier Option Data")]
        public bool _UsePreviousExitValueAsMultiplier;

        public int _PreviousExtraAddition;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            entryVariable = caster.MaximumHealth - caster.CurrentHealth;
            for (int i = 0; i < targets.Length; i++)
            {
                exitAmount += ApplyFieldEffect(stats, targets[i], entryVariable);
            }

            return exitAmount > 0;
        }

        public int ApplyFieldEffect(CombatStats stats, TargetSlotInfo target, int entryVariable)
        {
            int num = _UseRandomBetweenPrevious ? UnityEngine.Random.Range(PreviousExitValue, entryVariable + 1) : entryVariable;
            if (num < _Field.MinimumRequiredToApply)
            {
                return 0;
            }

            if (!stats.combatSlots.ApplyFieldEffect(target.SlotID, target.IsTargetCharacterSlot, _Field, num))
            {
                return 0;
            }

            return num;
        }
    }


}
