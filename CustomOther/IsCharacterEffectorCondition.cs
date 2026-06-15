using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class IsCharacterEffectorCondition : EffectorConditionSO
    {
        public bool _passIfTrue;

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return _passIfTrue == effector.IsUnitCharacter;
        }
    }
}
