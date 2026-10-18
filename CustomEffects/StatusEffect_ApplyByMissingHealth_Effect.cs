using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using UnityEngine;

namespace SorasToybox.CustomEffects
{

    public class StatusEffect_ApplyByMissingHealth_Effect : EffectSO
    {
        [Header("Status")]
        public StatusEffect_SO _Status;

        [Header("Previous Random Option Data")]
        public bool _UseRandomBetweenPrevious;

        [Header("Previous Multiplier Option Data")]
        public bool _UsePreviousExitValueAsMultiplier;

        public int _PreviousExtraAddition;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            entryVariable = (caster.MaximumHealth - caster.CurrentHealth);
            if (entryVariable > 0)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    exitAmount += ApplyStatusEffect(targets[i].Unit, entryVariable);
                }
            }
            return exitAmount > 0;
        }

        public int ApplyStatusEffect(IUnit unit, int entryVariable)
        {
            int num = _UseRandomBetweenPrevious ? UnityEngine.Random.Range(PreviousExitValue, entryVariable + 1) : entryVariable;
            if (num < _Status.MinimumRequiredToApply)
            {
                return 0;
            }

            if (!unit.ApplyStatusEffect(_Status, num))
            {
                return 0;
            }

            return num;
        }
    }


}