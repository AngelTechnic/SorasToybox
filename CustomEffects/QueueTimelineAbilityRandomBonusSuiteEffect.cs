using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomEffects
{
    // from the Salt Enemies github repo (EvilDog.cs, ButterflyEffects.cs)
    public class QueueTimelineAbilityRandomBonusSuiteEffect : EffectSO
    {
        public bool _excludeIfMatchesPrevious = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (caster is EnemyCombat enemy)
            {
                int extraCount = enemy.ExtraAbilities.Count;
                int totalCount = enemy.Abilities.Count;
                Debug.Log("total count: " + totalCount + " | extra count: " + extraCount + " | result range: " + (totalCount - extraCount) + " to " + (totalCount - 1));
                int index = 0;
                index = totalCount - UnityEngine.Random.Range(0, extraCount) - 1;
                while (_excludeIfMatchesPrevious)
                {
                    if (index != base.PreviousExitValue) { break; }
                    index = totalCount - UnityEngine.Random.Range(0, extraCount) - 1;
                    Debug.Log("rerolled to " + index);
                }
                stats.timeline.AddExtraEnemyTurns(new List<EnemyCombat>() { enemy }, new List<int>() { index });
            }
            return true;
        }
    }
}
