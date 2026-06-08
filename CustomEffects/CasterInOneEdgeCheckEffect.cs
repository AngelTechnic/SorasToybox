using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class CasterInOneEdgeCheckEffect : EffectSO
    {
        public bool _right = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_right)
            {
                if (caster.SlotID == 4)
                {
                    exitAmount++;
                }
            }
            else if (!_right)
            {
                if (caster.SlotID == 0)
                {
                    exitAmount++;
                }
            }

            return exitAmount > 0;
        }
    }
}
