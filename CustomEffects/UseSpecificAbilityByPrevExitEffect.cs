using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class UseSpecificAbilityByEntryEffect : EffectSO
    {
        string note = "adapted from errants off of box of beasts";
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (usePrev)
            {
                entryVariable *= this.PreviousExitValue;
            }
            List<EnemyCombat> list = new List<EnemyCombat>();
            List<int> list2 = new List<int>();
            foreach (EnemyCombat enemyCombat in stats.EnemiesOnField.Values)
            {
                bool flag = enemyCombat.ID == caster.ID;
                if (flag)
                {
                    int lastAbilityIDFromName = enemyCombat.GetLastAbilityIDFromName(this._parentalAbility.ability.name);
                    bool flag2 = lastAbilityIDFromName > 0;
                    if (flag2)
                    {
                        for (int i = 0; i < entryVariable; i++) {
                            list.Add(enemyCombat);
                            list2.Add(lastAbilityIDFromName);
                        }
                    }
                }
            }
            stats.timeline.AddExtraEnemyTurns(list, list2);
            return exitAmount > 0;
        }

        // Token: 0x0400000F RID: 15
        public ExtraAbilityInfo _parentalAbility = new ExtraAbilityInfo();
        public bool usePrev = false;
    }
}
