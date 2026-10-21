using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class ModernGiantCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.damagedUnit is IUnit damaged)
                {
                    damaged.ApplyStatusEffect(StatusField.GetCustomStatusEffect("Alacrity_ID"), 1);
                }
            }
            return false;
        }
    }
}
