using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    //borrowed from MEGALANIA from salt enemies
    public class ShiftCostsEffect : EffectSO
    {
        public static ManaColorSO[] RandomArray(ManaColorSO[] origin)
        {
            List<ManaColorSO> list = new List<ManaColorSO>();
            for (int i = 0; i < origin.Length; i++)
            {
                list.Add(RandomPig(origin[i]));
            }
            return list.ToArray();
        }
        public static ManaColorSO RandomPig(ManaColorSO source)
        {
            if (source.pigmentTypes.Count <= 0) return source;
            if (source.pigmentTypes.Count == 1) return Shift(source.pigmentTypes[0]);
            List<ManaColorSO> param = new List<ManaColorSO>();
            foreach (string id in source.pigmentTypes) param.Add(Shift(id));
            return Pigments.SplitPigment(param.ToArray());
        }
        public static ManaColorSO Shift(string id)
        {
            if (id == Pigments.Red.pigmentTypes[0]) return Pigments.Blue;
            if (id == Pigments.Blue.pigmentTypes[0]) return Pigments.Yellow;
            if (id == Pigments.Yellow.pigmentTypes[0]) return Pigments.Purple;
            if (id == Pigments.Purple.pigmentTypes[0]) return Pigments.Red;
            return Pigments.GetPigmentWithID(id);
        }
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (!targetSlotInfo.HasUnit || !(targetSlotInfo.Unit is CharacterCombat characterCombat))
                {
                    continue;
                }
                foreach (CombatAbility combatAbility in characterCombat.CombatAbilities)
                {
                    combatAbility.cost = RandomArray(combatAbility.cost);
                    exitAmount += combatAbility.cost.Length;
                }
                foreach (CharacterCombatUIInfo value in stats.combatUI._charactersInCombat.Values)
                {
                    if (value.SlotID != targetSlotInfo.Unit.SlotID)
                    {
                        continue;
                    }
                    CharacterCombatUIInfo characterCombatUIInfo = value;
                    List<CombatAbility> combatAbilities = (targetSlotInfo.Unit as CharacterCombat).CombatAbilities;
                    int num2 = 0;
                    CombatAbility[] array = new CombatAbility[combatAbilities.Count];
                    foreach (CombatAbility item in combatAbilities)
                    {
                        array[num2] = item;
                        num2++;
                    }
                    characterCombatUIInfo.UpdateAttacks(array);
                    break;
                }
                CombatManager instance = CombatManager.Instance;
                int iD = (targetSlotInfo.Unit as CharacterCombat).ID;
                List<CombatAbility> combatAbilities2 = (targetSlotInfo.Unit as CharacterCombat).CombatAbilities;
                int num3 = 0;
                CombatAbility[] array2 = new CombatAbility[combatAbilities2.Count];
                foreach (CombatAbility item2 in combatAbilities2)
                {
                    array2[num3] = item2;
                    num3++;
                }
                instance.AddUIAction(new CharacterUpdateAllAttacksUIAction(iD, array2));
            }
            return exitAmount > 0;
        }
    }
}
