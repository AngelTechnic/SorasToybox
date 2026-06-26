using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace SorasToybox.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class OpponentByStoredValueTargeting : BaseCombatTargettingSO
    {
        public string _storedValueID;
        public bool targetUnitAllySlots; // interpreted in reverse here, don't worry too much about it
        public bool getAllUnitSelfSlots;
        public bool reduceByOne = true;
        public int[] _modifiers = [0];

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_storedValueID == null)
                return [];

            var enemies = CombatManager.Instance._stats.EnemiesOnField;
            var chars = CombatManager.Instance._stats.CharactersOnField;
            IUnit caster = null;
            if (isCasterCharacter)
            {
                foreach (var ch in chars.Values)
                {
                    if (ch.SlotID == casterSlotID)
                    {
                        caster = ch;
                        break;
                    }
                }
            }
            else
            {
                foreach (var en in enemies.Values)
                {
                    if (en.SlotID == casterSlotID)
                    {
                        caster = en;
                        break;
                    }
                }
            }
            var res = new List<TargetSlotInfo>();
            var targetSlotID = 0;

            caster.TryGetStoredData(_storedValueID, out var targetSlotIDValue);
            if (reduceByOne)
            {
                targetSlotID = targetSlotIDValue.m_MainData - 1;
            }
            else
            {
                targetSlotID = targetSlotIDValue.m_MainData;
            }

            if (targetSlotID < 0 || targetSlotID > 4) { return []; }

            foreach (int modifier in _modifiers)
            {
                if (targetSlotID + modifier > 4 || targetSlotID + modifier < 0) { continue; }
                if (targetUnitAllySlots)
                {
                    res.Add(slots.GetAllySlotTarget(targetSlotID + modifier, 0, isCasterCharacter));
                }
                else
                {
                    res.Add(slots.GetOpponentSlotTarget(targetSlotID + modifier, 0, isCasterCharacter));
                }
            }
            return [.. res];
        }
    }
}
