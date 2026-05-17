using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.GraphicsBuffer;
using UnityEngine;

namespace SorasToybox.CustomEffects
{
    public class CasterMoveStatusToTargetEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (targets[0].HasUnit == false)
            {
                return false;
            }

            var casterStatus = new Dictionary<StatusEffect_SO, int>();
            if (caster is CharacterCombat casterCH)
            {
                foreach (IStatusEffect effect in casterCH.StatusEffects)
                {
                    Debug.Log($"Status Swapper | Caster has {effect.StatusID} - {effect.StatusContent}");
                    StatusEffect_SO applicableEffect;
                    LoadedDBsHandler._StatusFieldDB.TryGetStatusEffect(effect.StatusID, out applicableEffect);
                    casterStatus.Add(applicableEffect, effect.StatusContent);
                    exitAmount++;
                }
            }
            else if (caster is EnemyCombat casterEN)
            {
                foreach (IStatusEffect effect in casterEN.StatusEffects)
                {
                    Debug.Log($"Status Swapper | Caster has {effect.StatusID} - {effect.StatusContent}");
                    StatusEffect_SO applicableEffect;
                    LoadedDBsHandler._StatusFieldDB.TryGetStatusEffect(effect.StatusID, out applicableEffect);
                    casterStatus.Add(applicableEffect, effect.StatusContent);
                    exitAmount++;
                }
            }
            else
            {
                Debug.Log("Status Swapper | neither character nor enemy? how strange...");
                return false;
            }

            caster.TryRemoveAllStatusEffects();

            foreach (KeyValuePair<StatusEffect_SO, int> effect in casterStatus)
            {
                targets[0].Unit.ApplyStatusEffect(effect.Key, effect.Value);
            }

            return exitAmount > 0;
        }
    }
}
