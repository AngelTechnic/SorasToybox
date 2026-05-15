using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomPassives
{
    public class KarmicCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is IntegerReference reference)
            {
                StatusEffect_Apply_Effect regenUs = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
                regenUs._Status = StatusField.GetCustomStatusEffect("Regen_ID");

                CombatManager.Instance.AddSubAction(new EffectAction(new EffectInfo[]
                {
                    Effects.GenerateEffect(regenUs, reference.value, Targeting.Unit_AllAllies),
                }, effector as IUnit));
            }
            return false;
        }
    }
}
