using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class HasTurnsCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (effector is EnemyCombat enemy)
            {
                if (CombatManager.Instance._stats.IsPlayerTurn) return enemy.TurnsInTimeline > 0;

                CombatStats stats = CombatManager.Instance._stats;
                for (int i = stats.timeline.CurrentTurn + (stats.IsPlayerTurn ? 0 : 1); i < stats.timeline.Round.Count; i++)
                {
                    if (stats.timeline.Round[i].isPlayer) continue;

                    if (stats.timeline.Round[i].turnUnit == enemy)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
