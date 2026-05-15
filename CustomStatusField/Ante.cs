using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomStatuses
{
    public class Ante : StatusEffect_SO
    {
        public override bool IsPositive => true;
        // Token: 0x0600000D RID: 13 RVA: 0x00002608 File Offset: 0x00000808
        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            //CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_01), TriggerCalls.OnBeingDamaged.ToString(), caller);
            CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_02), TriggerCalls.OnBeingDamaged.ToString(), caller);
            CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_03), TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_04), TriggerCalls.OnBeingHealed.ToString(), caller);
            CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_05), TriggerCalls.OnWillApplyHeal.ToString(), caller);
            //CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_03), TriggerCalls.OnBeingHealed.ToString(), caller);
            //CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_04), TriggerCalls.OnWillApplyHeal.ToString(), caller);
            //CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_05), TriggerCalls.OnRoundFinished.ToString(), caller);
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002690 File Offset: 0x00000890
        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            //CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_01), TriggerCalls.OnBeingDamaged.ToString(), caller);
            CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_02), TriggerCalls.OnBeingDamaged.ToString(), caller);
            CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_03), TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_04), TriggerCalls.OnBeingHealed.ToString(), caller);
            CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_05), TriggerCalls.OnWillApplyHeal.ToString(), caller);
            //CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_03), TriggerCalls.OnBeingHealed.ToString(), caller);
            //CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_04), TriggerCalls.OnWillApplyHeal.ToString(), caller);
            //CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_05), TriggerCalls.OnRoundFinished.ToString(), caller);
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002718 File Offset: 0x00000918
        /*
        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            DamageReceivedValueChangeException ex = args as DamageReceivedValueChangeException;
            ex.AddModifier(new PercentageValueModifier(false, holder.m_ContentMain * 20, true));
        }*/

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            DamageReceivedValueChangeException ex = args as DamageReceivedValueChangeException;
            ex.AddModifier(new AdditionValueModifier(false, holder.m_ContentMain));
        }

        public override void OnEventCall_03(StatusEffect_Holder holder, object sender, object args)
        {
            DamageDealtValueChangeException ex = args as DamageDealtValueChangeException;
            ex.AddModifier(new AdditionValueModifier(false, holder.m_ContentMain));
        }

        public override void OnEventCall_04(StatusEffect_Holder holder, object sender, object args)
        {
            HealingReceivedValueChangeException ex = args as HealingReceivedValueChangeException;
            ex.AddModifier(new AdditionValueModifier(false, holder.m_ContentMain));
        }

        public override void OnEventCall_05(StatusEffect_Holder holder, object sender, object args)
        {
            HealingDealtValueChangeException ex = args as HealingDealtValueChangeException;
            ex.AddModifier(new AdditionValueModifier(false, holder.m_ContentMain));
        }

        public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            bool flag = !base.CanReduceDuration;
            if (!flag)
            {
                int contentMain = holder.m_ContentMain;
                holder.m_ContentMain -= 1;
                bool flag2 = !this.TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain;
                if (flag2)
                {
                    effector.StatusEffectValuesChanged(this._StatusID, holder.m_ContentMain - contentMain, false);
                }
            }
        }

    }
}
