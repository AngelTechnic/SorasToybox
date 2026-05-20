using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomPassives
{
    //borrowed from millie thanks millie
    public class SearchPartyPassiveAbility : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate
        {
            get
            {
                return true;
            }
        }
        public override bool DoesPassiveTrigger
        {
            get
            {
                return true;
            }
        }
        public override void TriggerPassive(object sender, object args)
        {
            if (args is IntegerReference integerReference)
            {
                IUnit unit = sender as IUnit;
                IntegerReference integerReference2 = new IntegerReference(0);
                CombatManager.Instance.ProcessImmediateAction(new CheckUnitsWithInfestationIAction("SearchParty_SV", integerReference), false);
                UnitStoreDataHolder unitStoreDataHolder;
                unit.TryGetStoredData("SearchParty_SV", out unitStoreDataHolder, true);
                int mainData = unitStoreDataHolder.m_MainData;
                int num = integerReference2.value - mainData;
                bool flag = num <= 0;
                if (!flag)
                {
                    integerReference.value += num;
                }
            }
            

        }

        // Token: 0x060000F0 RID: 240 RVA: 0x0000C92B File Offset: 0x0000AB2B
        public override void OnPassiveConnected(IUnit unit)
        {
            unit.SimpleSetStoredValue("SearchParty_SV", _modifyVal);
        }

        // Token: 0x060000F1 RID: 241 RVA: 0x0000C92E File Offset: 0x0000AB2E
        public override void OnPassiveDisconnected(IUnit unit)
        {
        }

        // Token: 0x0400004F RID: 79
        [Header("Value Data")]
        [SerializeField]
        [Min(0f)]
        public int _modifyVal = 1;
    }
}
