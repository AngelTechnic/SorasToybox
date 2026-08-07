using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorasToybox.CustomEffects
{
    //borrowed from HIF
    public class CheckCasterOrTargetIsUnitTypeEffect : EffectSO
    {
        public string _UnitTypeID;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit && targetSlotInfo.Unit.UnitTypes.Contains(_UnitTypeID))
                {
                    exitAmount++;
                }
            }
            if (caster.UnitTypes.Contains(_UnitTypeID))
            {
                exitAmount++;
            }
            return exitAmount > 0;
        }
    }
}
