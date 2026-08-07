using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrutalAPI;

namespace SorasToybox.CustomOther
{
    // Directly taken from the Hell Island Fell github repository
    public class SpecificAlliesByUnitTypeTargeting : BaseCombatTargettingSO
    {
        public string[] _unitTypes = [];
        public int[] slotOffsets;
        public bool targetUnitAllySlots;
        public bool getAllUnitSelfSlots;
        public bool blacklist = false;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            if (_unitTypes == null || slotOffsets == null)
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

                    var types = en.Enemy.unitTypes;
                    var matchesType = false;
                    foreach (string type in types)
                    {
                        if (_unitTypes.Contains(type))
                        {
                            matchesType = true;
                            break;
                        }
                    }
                    if (blacklist == false && matchesType == false)
                        continue;

                    if (blacklist == true && matchesType == true)
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

                    var types = ch.Character.unitTypes;
                    var matchesType = false;
                    foreach (string type in types)
                    {
                        if (_unitTypes.Contains(type))
                        {
                            matchesType = true;
                            break;
                        }
                    }
                    if (blacklist == false && matchesType == false)
                        continue;

                    if (blacklist == true && matchesType == true)
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
