using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    //praise april
    public class ChangeHealthColorEffect : EffectSO
    {
        public ManaColorSO color;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].HasUnit && targets[i].Unit.ChangeHealthColor(color))
                {
                    exitAmount++;
                }
            }

            return exitAmount > 0;
        }

        public static ChangeHealthColorEffect Create(ManaColorSO color)
        {
            ChangeHealthColorEffect ret = ScriptableObject.CreateInstance<ChangeHealthColorEffect>();
            ret.color = color;
            return ret;
        }
    }
}
