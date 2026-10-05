using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomPassives
{
    public class DissolutePassiveAbility : BasePassiveAbilitySO
    {
        public override bool IsPassiveImmediate => true;
        public override bool DoesPassiveTrigger => true;

        public override void OnPassiveConnected(IUnit unit)
        {

        }

        public override void OnPassiveDisconnected(IUnit unit)
        {

        }

        public override void TriggerPassive(object sender, object args)
        {
            LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Malfunction_ID", out StatusEffect_SO Malfunction);

            if (args is not DamageReceivedValueChangeException ex || ex.damageTypeID == "Malfunction_Damage")
            {
                return;
            }

            if (sender is not IUnit unit)
            {
                return;
            }

            ex.AddModifier(new MalfunctionIntValueModifier(unit, Malfunction));
            ex.ShouldIgnoreUI = true;
        }

        public class MalfunctionIntValueModifier(IUnit attackedUnit, StatusEffect_SO malfunctionStatus) : IntValueModifier(101)
        {
            public override int Modify(int value)
            {
                return value > 0 && attackedUnit != null && malfunctionStatus != null && attackedUnit.ApplyStatusEffect(malfunctionStatus, value, 0) ? 0 : value;
            }
        }
    }
}