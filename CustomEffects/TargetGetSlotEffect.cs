using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class TargetGetSlotEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<int> results = [];

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    results.Add(target.SlotID + 1);
                }
            }

            while (results.Count > 1)
            {
                int randomindex = UnityEngine.Random.Range(0, results.Count);
                results.RemoveAt(randomindex);
            }

            if (results.Count > 0)
            {
                exitAmount = results[0];
                Debug.Log("Targeter | chosen target in slot " + exitAmount);
            }
            return exitAmount > 0;
        }
    }
}
