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
            if (args is IntegerReference ex)
            {
                if (ex.value <= 0) { return false; }
                IUnit caster = effector as IUnit;
                caster.ApplyStatusEffect(StatusField.GetCustomStatusEffect("Whiplash_ID"), ex.value);

            }
            return false;
        }
    }
}
