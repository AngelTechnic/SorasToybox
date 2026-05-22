using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    //I adapted this from the Inequity shhh.
    public class GoofyAssLitanyCoerceTargeting : EffectSO
    {
        public BasePassiveAbilitySO _passiveToAdd;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<TargetSlotInfo> list = [];
            int num = -1;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                //hopefully this should target the highest health enemy without infantile?
                if (targetSlotInfo.HasUnit && targetSlotInfo.Unit.IsAlive && !targetSlotInfo.Unit.ContainsPassiveAbility(Passives.Infantile.m_PassiveID))
                {
                    if (num < 0)
                    {
                        list.Add(targetSlotInfo);
                        num = targetSlotInfo.Unit.CurrentHealth;
                    }
                    else if (targetSlotInfo.Unit.CurrentHealth > num)
                    {
                        list.Clear();
                        list.Add(targetSlotInfo);
                        num = targetSlotInfo.Unit.CurrentHealth;
                    }
                    else if (targetSlotInfo.Unit.CurrentHealth == num)
                    {
                        list.Add(targetSlotInfo);
                    }
                }
            }

            foreach (TargetSlotInfo item in list)
            {
                item.Unit.AddPassiveAbility(_passiveToAdd);
            }

            return exitAmount > 0;
        }
    }
}
