using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class CheckIsBronzoThereEffect : EffectSO
    {
        // Token: 0x060000CD RID: 205 RVA: 0x0000D7FC File Offset: 0x0000B9FC
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<TargetSlotInfo> victims = [];
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                bool hasUnit = targetSlotInfo.HasUnit;
                if (hasUnit)
                {
                    if (!targetSlotInfo.Unit.IsUnitCharacter && ((targetSlotInfo.Unit as EnemyCombat).Enemy.name == "Bronzo5_EN"))
                    {
                        UnityEngine.Debug.Log("final phase bronzo fucking detected no stored value for chievo for you");
                        exitAmount++;
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
