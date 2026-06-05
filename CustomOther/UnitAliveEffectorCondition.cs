using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class UnitAliveEffectorCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return effector.CurrentHealth > 0;
        }
    }
}
