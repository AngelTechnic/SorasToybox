using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Custom_Effects
{
    public class UnboundedHealEffect : EffectSO
    {
        public bool usePreviousExitValue;

        public bool entryAsPercentage;

        public bool _directHeal = true;

        public bool _onlyIfHasHealthOver0;

        public int _cycles = 1;

        public int _repeatChance = 50;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            int cycles = _cycles;
            if (usePreviousExitValue)
            {
                cycles *= PreviousExitValue;
            }
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                int ball = entryVariable;
                for (int i = 0; i < cycles; i++)
                {
                    ball += (int)Math.Ceiling(Math.Log(UnityEngine.Random.value) / Math.Log(_repeatChance / 100.0));
                }
                if (targetSlotInfo.HasUnit && (!_onlyIfHasHealthOver0 || targetSlotInfo.Unit.CurrentHealth > 0))
                {
                    if (entryAsPercentage)
                    {
                        ball = targetSlotInfo.Unit.CalculatePercentualAmount(ball);
                    }

                    if (_directHeal)
                    {
                        ball = caster.WillApplyHeal(ball, targetSlotInfo.Unit);
                    }

                    exitAmount += targetSlotInfo.Unit.Heal(ball, caster, _directHeal);
                }
            }

            return exitAmount > 0;
        }
    }
}
