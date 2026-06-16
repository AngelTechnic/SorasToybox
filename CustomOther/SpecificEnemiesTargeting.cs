using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class SpecificEnemiesTargeting : BaseCombatTargettingSO
    {
        public string[] _enemies;
        public int[] slotOffsets;
        public bool targetUnitAllySlots;
        public bool getAllUnitSelfSlots;
        public bool blacklist = false;
        public List<string> _passiveBlacklist = new List<string>();
        public bool _excludeCaster = false;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_enemies == null || slotOffsets == null)
                return [];

            var chars = CombatManager.Instance._stats.EnemiesOnField;
            var res = new List<TargetSlotInfo>();

            foreach (var ch in chars.Values)
            {
                if (ch == null || ch.Enemy == null) { continue; }

                if (ch.SlotID == casterSlotID && _excludeCaster == true) { continue; }

                var id = ch.Enemy.name;
                if (string.IsNullOrEmpty(id))
                    continue;
                if (blacklist == false && Array.IndexOf(_enemies, id) < 0)
                    continue;
                if (blacklist == true && Array.IndexOf(_enemies, id) >= 0)
                    continue;
                bool passivePass = true;
                if (_passiveBlacklist.Count > 0)
                {
                    foreach (BasePassiveAbilitySO passive in ch.PassiveAbilities)
                    {
                        if (_passiveBlacklist.Contains(passive.m_PassiveID))
                        {
                            passivePass = false;
                            break;
                        }
                    }
                }
                if (passivePass == false)
                {
                    continue;
                }

                var chSID = ch.SlotID;
                var chIsCharacter = ch.IsUnitCharacter;

                foreach (var offs in slotOffsets)
                {
                    if (offs == 0)
                    {
                        if (!targetUnitAllySlots)
                        {
                            res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                            continue;
                        }
                        else if (getAllUnitSelfSlots)
                        {
                            res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                            continue;
                        }
                    }

                    var slot = targetUnitAllySlots ? slots.GetAllySlotTarget(chSID, offs, chIsCharacter) : slots.GetOpponentSlotTarget(chSID, offs, chIsCharacter);

                    if (slot == null)
                        continue;

                    res.Add(slot);
                }
            }

            return [.. res];
        }
    }
}
