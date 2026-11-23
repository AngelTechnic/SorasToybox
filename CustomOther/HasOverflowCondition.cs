using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    //thanks Ent
    public class HasOverflowCondition : EffectConditionSO
    {
        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            CombatStats stats = CombatManager.Instance._stats;
            if (stats.overflowMana.OverflowManaAmount > 0)
            {
                return true;
            }
            return false;
        }
    }
}
