using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class ExtraVariableForNext_SVEffect : EffectSO
    {
        public UnitStoreData_BasicSO unitStoredData;

        [UnitStoreValueNamesIDsEnumRef]
        public string m_unitStoredDataID = "";
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            bool didit = caster.TryGetStoredData(m_unitStoredDataID, out var holder);
            exitAmount = holder.m_MainData;
            return didit;
        }
    }
}
