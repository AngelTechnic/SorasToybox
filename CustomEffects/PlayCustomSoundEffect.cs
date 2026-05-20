using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class PlayCustomSoundEffect : EffectSO
    {
        public string _Sound = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = entryVariable;

            CombatManager.Instance.AddUIAction(new PlayStatusEffectSoundAndWaitUIAction(_Sound, 0f));

            return exitAmount > 0;
        }
    }
}
