using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    // from the Salt Enemies github repo (EvilDog.cs, ButterflyEffects.cs)
    public class QueueTimelineAbilityByNameEffect : EffectSO
    {
        public string _abilityName = "";
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (caster is EnemyCombat enemy)
            {
                stats.timeline.AddExtraEnemyTurns(new List<EnemyCombat>() { enemy }, new List<int>() { enemy.GetLastAbilityIDFromNameUsingAbilityName(_abilityName) });
            }
            return true;
        }
    }

    public static class AbilityByName
    {
        public static int GetLastAbilityIDFromNameUsingAbilityName(this EnemyCombat enemy, string abilityName)
        {
            for (int num = enemy.Abilities.Count - 1; num >= 0; num--)
            {
                if (enemy.Abilities[num].ability._abilityName == abilityName)
                {
                    return num;
                }
            }

            return -1;
        }
    }
}
