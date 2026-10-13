using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class GenerateColorsByListManaEffect : EffectSO
    {
        public bool usePreviousExitValue;

        public ManaColorSO[] _manaColors = [];
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (usePreviousExitValue)
            {
                entryVariable *= base.PreviousExitValue;
            }

            int i = 0;
            exitAmount = entryVariable;
            while (i < exitAmount)
            {
                int randomIndex = UnityEngine.Random.Range(0, _manaColors.Length);
                CombatManager.Instance.ProcessImmediateAction(new AddManaToManaBarAction(_manaColors[randomIndex], 1, caster.IsUnitCharacter, caster.ID));
                i++;
            }
            return true;
        }
    }
}
