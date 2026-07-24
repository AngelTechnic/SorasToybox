using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using UnityEngine;
using static SorasToybox.CustomOther.CopyThatEffect;

namespace SorasToybox.CustomEffects
{
    public class AddEnemyAbilityFromListEffect : EffectSO
    {
        public List<Ability> _abilityList = new List<Ability>();
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is EnemyCombat enemy)
                    {
                        Debug.Log($"Ability Adder | target enemy found ({enemy.Name})");
                        if (enemy.Abilities.Count < entryVariable)
                        {
                            Debug.Log($"Ability Adder | choosing ability to add to {enemy.Name}...");
                            List<Ability> abilityListCopy = new List<Ability>();
                            abilityListCopy.AddRange(_abilityList);
                            while (abilityListCopy.Count > 1)
                            {
                                int randomIndex = UnityEngine.Random.Range(0, abilityListCopy.Count);
                                abilityListCopy.RemoveAt(randomIndex);
                            }
                            Debug.Log($"Ability Adder | adding ability {abilityListCopy[0].ability.name} to {enemy.Name}...");
                            enemy.Abilities.Add(new CombatAbility(abilityListCopy[0].GenerateEnemyAbility(false)));
                            Debug.Log($"Ability Adder | success!");
                            CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(enemy.ID));
                            exitAmount++;
                        }
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
