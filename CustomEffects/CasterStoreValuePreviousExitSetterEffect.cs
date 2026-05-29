using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class CasterStoreValuePreviousExitSetterEffect : EffectSO
    {
        public bool _ignoreIfContains;

        [UnitStoreValueNamesIDsEnumRef]
        public string m_unitStoredDataID = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            UnitStoreDataHolder holder;
            bool flag = caster.TryGetStoredData(m_unitStoredDataID, out holder);
            if (!_ignoreIfContains || !flag)
            {
                holder.m_MainData = base.PreviousExitValue;
                return true;
            }

            return false;
        }
    }
}
