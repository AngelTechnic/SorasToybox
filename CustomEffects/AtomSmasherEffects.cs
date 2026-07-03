using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using SorasToybox;

namespace SorasToybox.CustomEffects
{
    public class AtomSmasherCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.damagedUnit is IUnit damaged)
                {
                    damaged.ApplyStatusEffect(StatusField.GetCustomStatusEffect("Collapse_ID"), reference.amount);
                }
            }
            return false;
        }
    }
}
