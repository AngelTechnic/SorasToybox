using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorasToybox.CustomOther
{
    // Directly taken from the Hell Island Fell github repository, then modified
    public class SpecificUnitsByPassiveTargeting : BaseCombatTargettingSO
    {
        public BasePassiveAbilitySO _passive;
        public int[] slotOffsets;
        public bool targetUnitAllySlots;
        public bool getAllUnitSelfSlots;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_passive == null || slotOffsets == null)
                return [];

            var enemies = CombatManager.Instance._stats.EnemiesOnField;
            var characters = CombatManager.Instance._stats.CharactersOnField;
            var res = new List<TargetSlotInfo>();

            foreach (var en in enemies.Values)
            {
                if (en == null || en.Enemy == null)
                    continue;

                var passives = en.PassiveAbilities;
                if (passives.Count == 0 || Array.IndexOf(passives.ToArray(), _passive) < 0)
                    continue;

                var enSID = en.SlotID;
                var enIsCharacter = en.IsUnitCharacter;

                foreach (var offs in slotOffsets)
                {
                    if (offs == 0)
                    {
                        if (!targetUnitAllySlots)
                        {
                            res.AddRange(slots.GetFrontOpponentSlotTargets(enSID, enIsCharacter));
                            continue;
                        }
                        else if (getAllUnitSelfSlots)
                        {
                            res.AddRange(slots.GetAllSelfSlots(enSID, enIsCharacter));
                            continue;
                        }
                    }

                    var slot = targetUnitAllySlots ? slots.GetAllySlotTarget(enSID, offs, enIsCharacter) : slots.GetOpponentSlotTarget(enSID, offs, enIsCharacter);

                    if (slot == null)
                        continue;

                    res.Add(slot);
                }
            }

            foreach (var ch in characters.Values)
            {
                if (ch == null || ch.Character == null)
                    continue;

                var passives = ch.PassiveAbilities;
                if (passives.Count == 0 || Array.IndexOf(passives.ToArray(), _passive) < 0)
                    continue;

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
