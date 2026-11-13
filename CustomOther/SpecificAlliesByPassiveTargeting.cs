using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace SorasToybox.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class SpecificAlliesByPassiveTargeting : BaseCombatTargettingSO
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
            var chars = CombatManager.Instance._stats.CharactersOnField;
            var res = new List<TargetSlotInfo>();

            if (!isCasterCharacter)
            {
                foreach (var en in enemies.Values)
                {
                    if (en == null || en.Enemy == null)
                        continue;

                    var passives = en.PassiveAbilities;
                    if (passives.Count == 0 || Array.IndexOf(passives.ToArray(), _passive) < 0)
                        continue;

                    var chSID = en.SlotID;
                    var chIsCharacter = en.IsUnitCharacter;

                    foreach (var offs in slotOffsets)
                    {
                        if (offs == 0)
                        {
                            if (isCasterCharacter)
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                                    continue;
                                }
                                else if (getAllUnitSelfSlots)
                                {
                                    res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                                    continue;
                                }
                            }
                            else
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                                    continue;
                                }
                                else if (getAllUnitSelfSlots)
                                {
                                    res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                                    continue;
                                }
                            }
                        }

                        var slot = targetUnitAllySlots ? slots.GetAllySlotTarget(chSID, offs, chIsCharacter) : slots.GetOpponentSlotTarget(chSID, offs, chIsCharacter);

                        if (slot == null)
                            continue;

                        res.Add(slot);
                    }
                }
            }
            else
            {
                foreach (var ch in chars.Values)
                {
                    Debug.Log($"ch {ch} | ch.Character {ch.Character.name}");
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
                            if (isCasterCharacter)
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                                    continue;
                                }
                                else if (getAllUnitSelfSlots)
                                {
                                    res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                                    continue;
                                }
                            }
                            else
                            {
                                if (!targetUnitAllySlots)
                                {
                                    res.AddRange(slots.GetAllSelfSlots(chSID, chIsCharacter));
                                    continue;
                                }
                                else if (getAllUnitSelfSlots)
                                {
                                    res.AddRange(slots.GetFrontOpponentSlotTargets(chSID, chIsCharacter));
                                    continue;
                                }
                            }
                        }

                        var slot = targetUnitAllySlots ? slots.GetAllySlotTarget(chSID, offs, chIsCharacter) : slots.GetOpponentSlotTarget(chSID, offs, chIsCharacter);

                        if (slot == null)
                            continue;

                        res.Add(slot);
                    }
                }
            }
            return [.. res];
        }
    }
}
