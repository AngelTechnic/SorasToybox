using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;

namespace SorasToybox.CustomEffects
{
    public class DeathmatchAltMusicCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            return effector.CurrentHealth <= 222;
        }
    }
}
