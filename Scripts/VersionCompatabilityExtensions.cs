using BrutalAPI;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace SorasToybox
{
    public static class Help
    {
        public static void AnyAbilityHasFinished(IUnit unit, int caster, bool isChara, AbilitySO ability)
        {
            if (unit is CharacterCombat chara)
            {
                MethodInfo method = typeof(CharacterCombat).GetMethod(nameof(CharacterCombat.AnyAbilityHasFinished));
                if (method.GetParameters().Length == 0)
                    method.Invoke(unit, []);
                else if (method.GetParameters().Length == 1)
                {
                    try
                    {
                        Helper._subHelper_AnyAbilityHasFinished(unit, caster, isChara, ability);
                    }
                    catch
                    {
                        Debug.LogWarning("idk");
                    }
                }
            }
            if (unit is EnemyCombat enemy)
            {
                MethodInfo method = typeof(EnemyCombat).GetMethod(nameof(EnemyCombat.AnyAbilityHasFinished));
                if (method.GetParameters().Length == 0)
                    method.Invoke(unit, []);
                else if (method.GetParameters().Length == 1)
                {
                    try
                    {
                        Helper._subHelper_AnyAbilityHasFinished(unit, caster, isChara, ability);
                    }
                    catch
                    {
                        Debug.LogWarning("idk");
                    }
                }
            }
        }
    }
    public static class Helper
    {
        public static bool _subHelper_DirectDeath(IUnit target, IUnit killer, bool obliteration)
        {
            return target.DirectDeath(killer, obliteration, out int num);
        }
        public static void _subHelper_AnyAbilityHasFinished(IUnit unit, int caster, bool isChara, AbilitySO ability)
        {
            if (unit is CharacterCombat chara)
            {
                chara.AnyAbilityHasFinished(new AbilityUsageReference(caster, isChara, ability));
            }
            if (unit is EnemyCombat enemy)
            {
                enemy.AnyAbilityHasFinished(new AbilityUsageReference(caster, isChara, ability));
            }
        }
    }
}
