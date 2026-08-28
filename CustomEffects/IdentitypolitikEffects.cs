using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomEffects
{
    public class IdentitypolitikCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is IntegerReference reference)
            {
                StatusEffect_Apply_Effect whiplashMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
                whiplashMe._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");

                CombatManager.Instance.AddSubAction(new EffectAction(new EffectInfo[]
                {
                    Effects.GenerateEffect(whiplashMe, reference.value, Targeting.Slot_SelfSlot),
                }, effector as IUnit));
            }
            return false;
        }
    }
}
