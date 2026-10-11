using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomPassives
{
    public class LocksmithPassiveAbility : BasePassiveAbilitySO
    {

        public override bool IsPassiveImmediate
        {
            get
            {
                return true;
            }
        }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x060000ED RID: 237 RVA: 0x000065D8 File Offset: 0x000047D8
        public override bool DoesPassiveTrigger
        {
            get
            {
                return true;
            }
        }

        // Token: 0x060000EE RID: 238 RVA: 0x000065EB File Offset: 0x000047EB
        public override void OnPassiveConnected(IUnit unit)
        {
        }


        public override void OnPassiveDisconnected(IUnit unit)
        {
        }


        public override void TriggerPassive(object sender, object args)
        {
            IUnit unit = sender as IUnit;
            DamageDealtValueChangeException ex = args as DamageDealtValueChangeException;
            bool flag = ex != null && (ex.damagedUnit.ContainsPassiveAbility(Passives.Constricting.m_PassiveID) || ex.casterUnit.ContainsFieldEffect(StatusField.Constricted.FieldID));
            if (flag)
            {
                ex.AddModifier(new PercentageValueModifier(true, 50, true));

            }
        }
    }
}
