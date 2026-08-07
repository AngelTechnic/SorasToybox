using FMOD.Studio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class StretchMarksCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            StatusEffect_Apply_Effect getScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getScars._Status = StatusField.Scars;

            ChangeMaxHealthEffect reduceHealth = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            reduceHealth._increase = true;

            if (args is HealingDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.healingUnit is IUnit healed)
                {
                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(reduceHealth, 3, Targeting.Slot_SelfSlot)], reference.healingUnit, 0));
                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(getScars, 1, Targeting.Slot_SelfSlot)], reference.healingUnit, 0));
                }
            }
            return false;
        }
    }
}
