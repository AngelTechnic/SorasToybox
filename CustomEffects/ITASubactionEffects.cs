using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    
    public class PerformEffectViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 1;
            CombatManager.Instance.AddSubAction(new EffectAction(this.effects, caster, 0));
            return exitAmount > 0;
        }

        public EffectInfo[] effects;
    }

    public class TargetPerformEffectViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                bool hasUnit = targetSlotInfo.HasUnit;
                if (hasUnit)
                {
                    CombatManager.Instance.AddSubAction(new EffectAction(this.effects, targetSlotInfo.Unit, 0));
                    exitAmount++;
                }
            }

            return exitAmount > 0;
        }

        public EffectInfo[] effects;
    }

    public class RandomTargetPerformEffectViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<TargetSlotInfo> victims = [];
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                bool hasUnit = targetSlotInfo.HasUnit;
                if (hasUnit && !victims.Contains(targetSlotInfo))
                {
                    victims.Add(targetSlotInfo);
                }
            }
            if (victims.Count > 0)
            {
                CombatManager.Instance.AddSubAction(new EffectAction(this.effects, victims[UnityEngine.Random.Range(0, victims.Count)].Unit, 0));
                exitAmount++;
            }


            return exitAmount > 0;
        }

        public EffectInfo[] effects;
    }

    public class PerformEffectXTimesViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 1;
            if (this.usePreviousExit)
            {
                entryVariable *= base.PreviousExitValue;
            }
            List<EffectInfo> loopedEffects = [];
            for (int i = 0; i < entryVariable; i++)
            {
                foreach (EffectInfo effect in this.effects)
                {
                    loopedEffects.Add(effect);
                }
            }
            CombatManager.Instance.AddSubAction(new EffectAction(loopedEffects.ToArray(), caster, 0));
            return exitAmount > 0;
        }

        public EffectInfo[] effects;
        public bool usePreviousExit;
    }

    public class PerformEffectXTimesStoredValueViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 1;
            if (this.usePreviousExit)
            {
                entryVariable *= base.PreviousExitValue;
            }
            List<EffectInfo> loopedEffects = [];
            int value = caster.SimpleGetStoredValue(m_unitStoredDataID);
            for (int i = 0; i < value; i++)
            {
                foreach (EffectInfo effect in this.effects)
                {
                    loopedEffects.Add(effect);
                }
            }
            CombatManager.Instance.AddSubAction(new EffectAction(loopedEffects.ToArray(), caster, 0));
            return exitAmount > 0;
        }

        public EffectInfo[] effects;
        public bool usePreviousExit;
        public string m_unitStoredDataID = "";
    }

    public class PerformRandomEffectViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 1;
            int index = UnityEngine.Random.Range(0, this.effects.Count);
            CombatManager.Instance.AddSubAction(new EffectAction(this.effects[index], caster, 0));
            return exitAmount > 0;
        }

        public List<EffectInfo[]> effects;
    }
    public class AltPerformEffectXTimesViaSubaction : EffectSO
    {// harvested from ITA - I wonder what the story behind this one is
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 1;
            bool flag = this.usePreviousExit;
            if (flag)
            {
                entryVariable *= base.PreviousExitValue;
            }
            for (int i = 0; i < entryVariable; i++)
            {
                CombatManager.Instance.AddSubAction(new EffectAction(this.effects, caster, 0));
            }
            return exitAmount > 0;
        }

        public EffectInfo[] effects;

        public bool usePreviousExit;
    }
}
