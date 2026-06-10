using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    //borrowed from salt enemies
    public class ExhaustMovementEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = entryVariable;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit && targetSlotInfo.IsTargetCharacterSlot)
                {
                    if (!(targetSlotInfo.Unit as CharacterCombat).CanSwap) continue;
                    (targetSlotInfo.Unit as CharacterCombat).CanSwap = false;
                    CombatManager.Instance.AddUIAction(new CharacterUpdateVolatilesUIAction((targetSlotInfo.Unit as CharacterCombat).ID, (targetSlotInfo.Unit as CharacterCombat).CanSwapNoTrigger, (targetSlotInfo.Unit as CharacterCombat).CanUseAbilitiesNoTrigger, shouldPopUp: true));
                    return true;
                }
            }
            return false;
        }
    }
}
