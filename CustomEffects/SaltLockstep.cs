using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox
{
    public static class SaltLockstepCadence
    {
        public static void Example()
        {
            //just setting these as placeholder. set these to whatever values you actually use
            string storedValueName = "LockstepDir_SV";
            int goingLeft = 1;
            int goingRight = 2;

            MoveByCasterStoredValueEffect movement = ScriptableObject.CreateInstance<MoveByCasterStoredValueEffect>();
            movement.storedValue = storedValueName;
            movement.LeftValue = goingLeft;
            movement.RightValue = goingRight;

            SwapCasterStoredValueEffect swapdirection = ScriptableObject.CreateInstance<SwapCasterStoredValueEffect>();
            swapdirection.storedValue = storedValueName;
            swapdirection.firstValue = goingLeft;
            swapdirection.secondValue = goingRight;

            CasterSwapAnimationParametersByStoredValueEffect animator = ScriptableObject.CreateInstance<CasterSwapAnimationParametersByStoredValueEffect>();
            animator._parameterName = "Flip";//set this for the swap left right cusotm animator thing i think ur using that
            animator.storedValue = storedValueName;
            animator.LeftValue = goingLeft;
            animator.RightValue = goingRight;
            animator.setAsIfLeft = 0;//idk what ur setting these for if left or right for so i just kinda left it like this. change these 2 as needed.
            animator.setAsIfRight = 1;

            PreviousEffectCondition prev1 = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            prev1.previousAmount = 1;
            prev1.wasSuccessful = false;
            PreviousEffectCondition prev2 = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            prev2.previousAmount = 2;
            prev2.wasSuccessful = false;
            PreviousEffectCondition prev3 = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            prev3.previousAmount = 3;
            prev3.wasSuccessful = false;

            EffectInfo[] effects = new EffectInfo[8];
            effects[0] = Effects.GenerateEffect(movement, 1, Targeting.Slot_SelfSlot);
            effects[1] = Effects.GenerateEffect(swapdirection, 0, null, prev1);
            effects[2] = Effects.GenerateEffect(animator, 0, null, prev2);
            effects[3] = Effects.GenerateEffect(movement, 1, Targeting.Slot_SelfSlot, prev3);
            effects[4] = Effects.GenerateEffect(movement, 1, Targeting.Slot_SelfSlot);
            effects[5] = Effects.GenerateEffect(swapdirection, 0, null, prev1);
            effects[6] = Effects.GenerateEffect(animator, 0, null, prev2);
            effects[7] = Effects.GenerateEffect(movement, 1, Targeting.Slot_SelfSlot, prev3);
        }
    }

    public class MoveByCasterStoredValueEffect : SwapToOneSideEffect
    {
        public string storedValue;
        public int LeftValue;
        public int RightValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            int value = caster.SimpleGetStoredValue(storedValue);
            if (value == LeftValue) this._swapRight = false;
            else if (value == RightValue) this._swapRight = true;

            return base.PerformEffect(stats, caster, targets, areTargetSlots, entryVariable, out exitAmount);
        }
    }
    public class SwapCasterStoredValueEffect : EffectSO
    {
        public string storedValue;
        public int firstValue;
        public int secondValue;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            int num = caster.SimpleGetStoredValue(storedValue);

            if (num == firstValue) caster.SimpleSetStoredValue(storedValue, secondValue);
            else caster.SimpleSetStoredValue(storedValue, firstValue);

            exitAmount = caster.SimpleGetStoredValue(storedValue);

            return true;
        }
    }
    public class CasterSwapAnimationParametersByStoredValueEffect : SetCasterAnimationParameterEffect
    {
        public string storedValue;
        public int LeftValue;
        public int RightValue;

        public int setAsIfLeft;
        public int setAsIfRight;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            int num = caster.SimpleGetStoredValue(storedValue);
            if (num == LeftValue) this._parameterValue = setAsIfLeft;
            if (num == RightValue) this._parameterValue = setAsIfRight;

            return base.PerformEffect(stats, caster, targets, areTargetSlots, entryVariable, out exitAmount);
        }
    }
}
