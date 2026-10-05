using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class RottenKintsugiCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            StatusEffect_Apply_Effect getPetrified = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getPetrified._Status = StatusField.GetCustomStatusEffect("Petrified_ID");

            //CanHealReferenceDetectionEffectorCondition canIHeal = ScriptableObject.CreateInstance<CanHealReferenceDetectionEffectorCondition>();



            if (args is HealingDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.healingUnit is IUnit healed)
                {
                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(getPetrified, 2, Targeting.Slot_SelfSlot)], reference.healingUnit, 0));
                }
            }
            return false;
        }
    }
}