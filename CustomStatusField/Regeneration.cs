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
        // Token: 0x0600000D RID: 13 RVA: 0x00002608 File Offset: 0x00000808
        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_01), TriggerCalls.OnRoundFinished.ToString(), caller);
            CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_02), TriggerCalls.OnCombatEnd.ToString(), caller);
            //CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_02), TriggerCalls.OnTurnFinished.ToString(), caller);
            //CombatManager.Instance.AddObserver(new Action<object, object>(holder.OnEventTriggered_03), TriggerCalls.OnDirectDamaged.ToString(), caller);
        }

        // Token: 0x0600000E RID: 14 RVA: 0x00002690 File Offset: 0x00000890
        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_01), TriggerCalls.OnRoundFinished.ToString(), caller);
            CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_02), TriggerCalls.OnCombatEnd.ToString(), caller);
            //CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_02), TriggerCalls.OnTurnFinished.ToString(), caller);
            //CombatManager.Instance.RemoveObserver(new Action<object, object>(holder.OnEventTriggered_03), TriggerCalls.OnDirectDamaged.ToString(), caller);
        }

        // Token: 0x0600000F RID: 15 RVA: 0x00002718 File Offset: 0x00000918
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

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            //(sender as IUnit).GenerateHealthMana(holder.m_ContentMain);
            if (sender != null)
            {
                int amount = holder.m_ContentMain;
                (sender as IUnit).Heal(amount, sender as IUnit, true);
                this.ReduceDurationBy(holder, sender as IStatusEffector, amount);
                //(sender as IUnit).Damage((holder.m_ContentMain / 2), sender as IUnit, "", 0, false, false, true, "Atrophy_Damage");
            }

        }

        // Token: 0x06000010 RID: 16 RVA: 0x00002753 File Offset: 0x00000953

        // Token: 0x06000011 RID: 17 RVA: 0x00002764 File Offset: 0x00000964

        // Token: 0x06000012 RID: 18 RVA: 0x00002778 File Offset: 0x00000978
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

        // Token: 0x06000013 RID: 19 RVA: 0x000027E0 File Offset: 0x000009E0

    }
}
