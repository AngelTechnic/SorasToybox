using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class CharacterHasItemEffectorCondition : EffectorConditionSO
    {
        public bool _passIfTrue;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (effector is CharacterCombat caster) 
            {
                return _passIfTrue == caster.HasUsableItem;
            }
            else { return false; }
        }
    }
}
