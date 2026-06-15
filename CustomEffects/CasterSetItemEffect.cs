using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class CasterSetItemEffect : EffectSO
    {
        public BaseWearableSO _item;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_item == null)
            {
                return false;
            }

            bool num = caster.TrySetUpNewItem(_item);
            if (num)
            {
                exitAmount = 1;
            }

            return num;
        }
    }
}
