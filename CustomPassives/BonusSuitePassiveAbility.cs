using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomPassives
{
    public class BonusSuitePassiveAbility : BasePassiveAbilitySO
    {
        [Header("ExtraAttack Data")]
        public List<ExtraAbilityInfo> _suiteAbilities;
        public override bool IsPassiveImmediate => true;

        public override bool DoesPassiveTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            if (args is List<string> list)
            {
                list.Add(_suiteAbilities[ChooseAbility()].ability?.name);
            }
        }

        public override void OnPassiveConnected(IUnit unit)
        {
            List<string> addedOnes = new List<string>();
            foreach (ExtraAbilityInfo ability in _suiteAbilities)
            {
                //Debug.Log("added ability " + ability.ability.name);
                if (!addedOnes.Contains(ability.ability._abilityName))
                {
                    unit.AddExtraAbility(ability);
                    addedOnes.Add(ability.ability._abilityName);
                }
            }
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            foreach (ExtraAbilityInfo ability in _suiteAbilities)
            {
                //Debug.Log(ability.ability.name);
                unit.TryRemoveExtraAbility(ability);
            }
        }

        public int ChooseAbility()
        {
            return UnityEngine.Random.Range(0, _suiteAbilities.Count);
        }
    }
}
