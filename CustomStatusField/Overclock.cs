using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomStatusField
{
    public class OverclockSE_SO : StatusEffect_SO
    {
        public override bool IsPositive => true;

        //When something gets this status, attach these triggers to the unit?
        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnAbilityUsed.ToString(), caller);
        }

        //On losing the status detach these triggers
        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnAbilityUsed.ToString(), caller);
        }

        //first event (OnWillApplyDamage) what do you do when you deal damage?
        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            DamageDealtValueChangeException ex = args as DamageDealtValueChangeException;
            IStatusEffector effector = sender as IStatusEffector;
            ex.AddModifier(new MultiplyIntValueModifier(true, 2));
        }
        //second event (OnAbilityUsed) what do you do when you perform an ability?
        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            //Decrements stack count of self. By default needs no extras as it does so by 1.
            ReduceDuration(holder, sender as IStatusEffector);
        }
    }
}
