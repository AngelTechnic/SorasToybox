using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomPassives
{
    public class ClownPassiveAbility : PerformEffectPassiveAbility
    {
        public static TriggerCalls Trigger => (TriggerCalls)1914194;
        public static bool Set;
        public static void Setup()
        {
            if (Set) return;
            Set = true;
            SorasToybox.NotificationHook.AddAction(NotifCheck);
        }
        public static void NotifCheck(string notifname, object sender, object args)
        {
            if (notifname == TriggerCalls.OnDirectDamaged.ToString())
            {
                if (sender is IUnit unit && !CombatManager.Instance._stats.IsPassiveLocked(PassiveType_GameIDs.Infantile.ToString()))
                {
                    if (!unit.ContainsPassiveAbility(PassiveType_GameIDs.Infantile.ToString())) return;
                    CombatManager.Instance.AddRootAction(new ClownPassiveSubAction(unit));
                }
            }
        }
    }

    public class ClownPassiveSubAction : CombatAction
    {
        IUnit unit;
        public ClownPassiveSubAction(IUnit Unit)
        {
            unit = Unit;
        }
        public override IEnumerator Execute(CombatStats stats)
        {
            if (unit.IsUnitCharacter)
            {
                foreach (CharacterCombat chara in CombatManager.Instance._stats.CharactersOnField.Values)
                    CombatManager.Instance.PostNotification(ClownPassiveAbility.Trigger.ToString(), chara, unit);
            }
            else
            {
                foreach (EnemyCombat enemy in CombatManager.Instance._stats.EnemiesOnField.Values)
                    CombatManager.Instance.PostNotification(ClownPassiveAbility.Trigger.ToString(), enemy, unit);
            }
            yield return null;
        }
    }



}
