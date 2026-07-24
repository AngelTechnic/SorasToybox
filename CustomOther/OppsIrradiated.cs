using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class OppsIrradiated : BaseCombatTargettingSO
    {
        public override bool AreTargetAllies => false;
        public override bool AreTargetSlots => true;
        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            List<TargetSlotInfo> list = [];
            if (!isCasterCharacter)
            {
                foreach (CharacterCombat characterCombat in CombatManager._instance._stats.CharactersOnField.Values)
                {
                    bool flag = characterCombat.ContainsStatusEffect("Irradiated_ID");
                    if (flag)
                    {
                        list.Add(slots.GetAllySlotTarget(characterCombat.SlotID, 0, true));
                    }
                }
            }
            else
            {
                //List<TargetSlotInfo> list2 = [];
                foreach (EnemyCombat characterCombat2 in CombatManager._instance._stats.EnemiesOnField.Values)
                {
                    bool flag = characterCombat2.ContainsStatusEffect("Irradiated_ID");
                    if (flag)
                    {
                        list.Add(slots.GetOpponentSlotTarget(characterCombat2.SlotID, 0, false));
                    }
                }
            }



            return [.. list];
        }
    }


    public class OppsNotIrradiated : BaseCombatTargettingSO
    {
        public override bool AreTargetAllies => false;
        public override bool AreTargetSlots => true;
        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            List<TargetSlotInfo> list = [];
            if (!isCasterCharacter)
            {
                foreach (CharacterCombat characterCombat in CombatManager._instance._stats.CharactersOnField.Values)
                {
                    bool flag = characterCombat.ContainsStatusEffect("Irradiated_ID");
                    if (!flag)
                    {
                        list.Add(slots.GetAllySlotTarget(characterCombat.SlotID, 0, true));
                    }
                }
            }
            else
            {
                //List<TargetSlotInfo> list2 = [];
                foreach (EnemyCombat characterCombat2 in CombatManager._instance._stats.EnemiesOnField.Values)
                {
                    bool flag = characterCombat2.ContainsStatusEffect("Irradiated_ID");
                    if (!flag)
                    {
                        list.Add(slots.GetOpponentSlotTarget(characterCombat2.SlotID, 0, false));
                    }
                }
            }



            return [.. list];
        }
    }
}
