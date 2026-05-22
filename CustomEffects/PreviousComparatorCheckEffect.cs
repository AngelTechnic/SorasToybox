using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class PreviousComparatorCheckEffect : EffectSO
    {
        public bool _atOrAbove = true;

        public bool _entryIsComparator = true;

        public int _fixedComparator = 0;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_entryIsComparator)
            {
                return (_atOrAbove ? base.PreviousExitValue >= entryVariable : base.PreviousExitValue < entryVariable);
            }
            else if (!_entryIsComparator)
            {
                return (_atOrAbove ? base.PreviousExitValue >= _fixedComparator : base.PreviousExitValue < _fixedComparator);
            }
            return false;
        }
    }
}
