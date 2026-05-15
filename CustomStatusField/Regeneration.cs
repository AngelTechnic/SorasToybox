using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;
using BrutalAPI;


namespace SorasToybox.CustomStatusField
{
    public class Regeneration : StatusEffect_SO
    {
        public override bool IsPositive => true;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_01), TriggerCalls.OnRoundFinished.ToString(), caller);
            //CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_02), TriggerCalls.OnTurnFinished.ToString(), caller);
            //CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_03), TriggerCalls.OnDirectDamaged.ToString(), caller);
        }


        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_01), TriggerCalls.OnRoundFinished.ToString(), caller);
            //CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_02), TriggerCalls.OnTurnFinished.ToString(), caller);
            //CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_03), TriggerCalls.OnDirectDamaged.ToString(), caller);
        }
    
        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            //(sender as IUnit).GenerateHealthMana(holder.m_ContentMain);
            if (sender != null)
            {
                int amount = (holder.m_ContentMain / 2) < 1 ? 1 : (holder.m_ContentMain / 2);
                (sender as IUnit).Heal(amount, sender as IUnit, true);
                this.ReduceDurationBy(holder, sender as IStatusEffector, amount);
                //(sender as IUnit).Damage((holder.m_ContentMain / 2), sender as IUnit, "", 0, false, false, true, "Atrophy_Damage");
            }
            
        }


        public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            bool flag = !base.CanReduceDuration && holder.m_ContentMain > 1;
            if (!flag)
            {
                int contentMain = holder.m_ContentMain;
                holder.m_ContentMain /= 2;
                bool flag2 = !this.TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain;
                if (flag2)
                {
                    effector.StatusEffectValuesChanged(this._StatusID, holder.m_ContentMain - contentMain, false);
                }
            }
        }


        public void ReduceDurationBy(StatusEffect_Holder holder, IStatusEffector effector, int amt)
        {
            bool flag = !base.CanReduceDuration && holder.m_ContentMain > 1;
            if (!flag)
            {
                int contentMain = holder.m_ContentMain;
                holder.m_ContentMain -= amt;
                bool flag2 = !this.TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain;
                if (flag2)
                {
                    effector.StatusEffectValuesChanged(this._StatusID, holder.m_ContentMain - contentMain, false);
                }
            }
        }



        public void ZeroDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            bool flag = !base.CanReduceDuration;
            if (!flag)
            {
                int contentMain = holder.m_ContentMain;
                holder.m_ContentMain = 0;
                bool flag2 = !this.TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain;
                if (flag2)
                {
                    effector.StatusEffectValuesChanged(this._StatusID, holder.m_ContentMain - contentMain, false);
                }
            }
        }
    }
}
