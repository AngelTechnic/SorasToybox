using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class IsPlayerTurnEffectorCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return CombatManager.Instance._stats.IsPlayerTurn;
        }
    }
}
