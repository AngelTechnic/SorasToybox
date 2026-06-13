using System;
using System.Collections.Generic;
using System.Text;
using FMOD;

namespace SorasToybox.CustomEffects
{
    public class ConsumeCommonColorManaEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            List<ManaColorSO> manaList = new List<ManaColorSO>();
            foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
            {
                UnityEngine.Debug.Log($"color: {manaSlot?.ManaColor}");
                if (manaSlot.ManaColor != null)
                {
                    manaList.Add(manaSlot.ManaColor);
                }
            }

            ManaColorSO commonMana;
            if (manaList.Count == 0) {
                exitAmount = 0;
                return false; 
            }
            else if (manaList.Count == 1)
            {
                commonMana = manaList[0];
            }
            else
            {
                var freqs = GetFrequencies(manaList);
                ManaColorSO contender = manaList[0];
                int frequency = 0;
                foreach (var pair in freqs)
                {
                    if (pair.Value > frequency)
                    {
                        contender = pair.Key;
                        frequency = pair.Value;
                    }
                }
                commonMana = contender;
                UnityEngine.Debug.Log($"and the winner is... {commonMana}!");
            }

            JumpAnimationInformation jumpInfo = stats.GenerateUnitJumpInformation(caster.ID, caster.IsUnitCharacter);
            string manaConsumedSound = stats.audioController.manaConsumedSound;
            exitAmount = stats.MainManaBar.ConsumeAmountMana(commonMana, entryVariable, jumpInfo, manaConsumedSound);
            return exitAmount > 0;
        }

        static Dictionary<ManaColorSO, int> GetFrequencies(List<ManaColorSO> values)
        {
            var result = new Dictionary<ManaColorSO, int>();
            foreach (ManaColorSO value in values)
            {
                if (result.TryGetValue(value, out int count))
                {
                    result[value] = count + 1;
                }
                else
                {
                    result.Add(value, 1);
                }
            }
            return result;
        }
    }
}
