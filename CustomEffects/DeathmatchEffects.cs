using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;

namespace SorasToybox.CustomEffects
{
    public class DeathmatchAltMusicCondition : EffectConditionSO
    {
        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            return caster.CurrentHealth <= 175 && caster.EntityID == "Deathmatch_BOSS";
        }
    }
}
