using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomEffects
{
    public class StatusEffect_ApplyRestrictor_Effect : EffectSO
    {
        [Header("Status")]
        public StatusEffect_SO _Status;

        [Header("Data")]
        public bool _ApplyToFirstUnit;

        public bool _JustOneRandomTarget;

        public bool _RandomBetweenPrevious;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_Status == null)
            {
                return false;
            }

            if (_ApplyToFirstUnit || _JustOneRandomTarget)
            {
                List<TargetSlotInfo> list = new List<TargetSlotInfo>();
                foreach (TargetSlotInfo targetSlotInfo in targets)
                {
                    if (targetSlotInfo.HasUnit)
                    {
                        list.Add(targetSlotInfo);
                        if (_ApplyToFirstUnit)
                        {
                            break;
                        }
                    }
                }

                if (list.Count > 0)
                {
                    int index = UnityEngine.Random.Range(0, list.Count);
                    exitAmount += ApplyStatusEffect(list[index].Unit, entryVariable);
                }
            }
            else
            {
                for (int j = 0; j < targets.Length; j++)
                {
                    if (targets[j].HasUnit)
                    {
                        exitAmount += ApplyStatusEffect(targets[j].Unit, entryVariable);
                    }
                }
            }

            return exitAmount > 0;
        }

        public int ApplyStatusEffect(IUnit unit, int entryVariable)
        {
            int num = (_RandomBetweenPrevious ? UnityEngine.Random.Range(base.PreviousExitValue, entryVariable + 1) : entryVariable);
            if (num < _Status.MinimumRequiredToApply)
            {
                return 0;
            }

            if (!unit.ApplyStatusEffect(_Status, 0, num))
            {
                return 0;
            }

            return Mathf.Max(1, num);
        }
    }
}
