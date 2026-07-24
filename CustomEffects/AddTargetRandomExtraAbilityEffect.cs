using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class AddTargetRandomExtraAbilityEffect : EffectSO
    {
        public List<ExtraAbilityInfo> _extraAbilities;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (var target in targets)
            {
                if (target.HasUnit && !target.IsTargetCharacterSlot && (target.Unit as EnemyCombat).AbilityCount < 10)
                {
                    List<ExtraAbilityInfo> extraAbilities = _extraAbilities;
                    var enemyAbilities = (target.Unit as EnemyCombat).Abilities;

                    extraAbilities.RemoveAll(extraAbilityInfo => enemyAbilities.Any(enemyAbility => enemyAbility.ability == extraAbilityInfo.ability));

                    if (extraAbilities.Count <= 0 )
                    {
                        return false;
                    }

                    target.Unit.AddExtraAbility(extraAbilities[UnityEngine.Random.Range(0, extraAbilities.Count)]);
                }
            }
            return true;
        }
    }
}
