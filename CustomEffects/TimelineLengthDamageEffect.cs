using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class TimelineLengthDamageEffect : EffectSO
    {
        [DeathTypeEnumRef]
        public string _DeathTypeID = "Basic";

        public bool _usePreviousExitValue;

        public bool _ignoreShield;

        public bool _indirect;

        public bool _returnKillAsSuccess;

        public bool plusentry = false;

        //public float _proportion;

        //uses entry variable as a percentage
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {

            exitAmount = 0;

            if (_usePreviousExitValue)
            {
                entryVariable *= base.PreviousExitValue;
            }

            bool flag = false;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    int targetSlotOffset = (areTargetSlots ? (targetSlotInfo.SlotID - targetSlotInfo.Unit.SlotID) : (-1));
                    int amount = entryVariable;
                    int actualDamage = 0;
                    DamageInfo damageInfo;
                    if (_indirect)
                    {
                        if (plusentry)
                        {
                            actualDamage = entryVariable + stats.timeline.RoundTurnUIInfo.Length;
                        }
                        else
                        {
                            actualDamage = entryVariable * stats.timeline.RoundTurnUIInfo.Length;
                        }
                        
                        damageInfo = targetSlotInfo.Unit.Damage(actualDamage, null, _DeathTypeID, targetSlotOffset, addHealthMana: false, directDamage: false, ignoresShield: true);
                    }
                    else
                    {
                        if (plusentry)
                        {
                            actualDamage = entryVariable + stats.timeline.RoundTurnUIInfo.Length;
                        }
                        else
                        {
                            actualDamage = entryVariable * stats.timeline.RoundTurnUIInfo.Length;
                        }
                        amount = caster.WillApplyDamage(actualDamage, targetSlotInfo.Unit);
                        damageInfo = targetSlotInfo.Unit.Damage(amount, caster, _DeathTypeID, targetSlotOffset, addHealthMana: true, directDamage: true, _ignoreShield);
                    }

                    flag |= damageInfo.beenKilled;
                    exitAmount += damageInfo.damageAmount;
                }
            }

            if (!_indirect && exitAmount > 0)
            {
                caster.DidApplyDamage(exitAmount);
            }

            if (!_returnKillAsSuccess)
            {
                return exitAmount > 0;
            }

            return flag;
        }
    }
}
