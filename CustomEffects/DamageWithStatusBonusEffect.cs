using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class DamageWithStatusBonusEffect : EffectSO
    {
        [DeathTypeEnumRef]
        public string _DeathTypeID = "Basic";

        public bool _usePreviousExitValue;

        public bool _ignoreShield;

        public bool _indirect;

        public bool _returnKillAsSuccess;

        public int _bonusAmount = 1;

        public bool _bonusStacking;

        public StatusEffect_SO _status;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (_usePreviousExitValue)
            {
                entryVariable *= base.PreviousExitValue;
            }

            exitAmount = 0;
            bool flag = false;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    int targetSlotOffset = (areTargetSlots ? (targetSlotInfo.SlotID - targetSlotInfo.Unit.SlotID) : (-1));
                    int amount = entryVariable;
                    if (targetSlotInfo.Unit is EnemyCombat targetEN)
                    {
                        int bonus = 0;
                        foreach (IStatusEffect status in targetEN.StatusEffects)
                        {
                            if (status.StatusID == _status.StatusID)
                            {
                                if (_bonusStacking)
                                {
                                    bonus += _bonusAmount * status.StatusContent;
                                }
                                else
                                {
                                    bonus += _bonusAmount;
                                }
                            }
                        }
                        amount += bonus;
                    }
                    else if (targetSlotInfo.Unit is CharacterCombat targetCH)
                    {
                        int bonus = 0;
                        foreach (IStatusEffect status in targetCH.StatusEffects)
                        {
                            if (status.StatusID == _status.StatusID)
                            {
                                if (_bonusStacking)
                                {
                                    bonus += _bonusAmount * status.StatusContent;
                                }
                                else
                                {
                                    bonus += _bonusAmount;
                                }
                            }
                        }
                        amount += bonus;
                    }
                    DamageInfo damageInfo;
                    if (_indirect)
                    {
                        damageInfo = targetSlotInfo.Unit.Damage(amount, null, _DeathTypeID, targetSlotOffset, addHealthMana: false, directDamage: false, ignoresShield: true);
                    }
                    else
                    {
                        amount = caster.WillApplyDamage(amount, targetSlotInfo.Unit);
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
