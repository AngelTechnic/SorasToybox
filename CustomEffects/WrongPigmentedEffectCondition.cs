using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class WrongPigmentedEffectCondition : EffectConditionSO
    {
        public bool trueIfWrongPig = true;

        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            return this.trueIfWrongPig == caster.LastCalculatedWrongMana > 0;
        }
    }
}
