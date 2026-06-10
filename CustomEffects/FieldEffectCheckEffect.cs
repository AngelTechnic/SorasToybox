using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class FieldEffectCheckEffect : EffectSO
    {
        public List<FieldEffect_SO> _fields = [];
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo target in targets)
            {
                if (!target.HasUnit)
                {
                    continue;
                }
                foreach (FieldEffect_SO fieldEffect in _fields)
                {
                    if (target.Unit.ContainsFieldEffect(fieldEffect._FieldID))
                    {
                        if (target.Unit is CharacterCombat ch)
                        {
                            IFieldSlotEffector fieldEffector = stats.combatSlots.CharacterSlots[ch.SlotID];
                            foreach (IFieldEffect field in fieldEffector.FieldEffects)
                            {
                                if (field.FieldID == fieldEffect.FieldID)
                                {
                                    exitAmount += field.FieldContent;
                                }
                            }
                        }
                        if (target.Unit is EnemyCombat en)
                        {
                            IFieldSlotEffector fieldEffector = stats.combatSlots.EnemySlots[en.SlotID];
                            foreach (IFieldEffect field in fieldEffector.FieldEffects)
                            {
                                if (field.FieldID == fieldEffect.FieldID)
                                {
                                    exitAmount += field.FieldContent;
                                }
                            }
                        }
                    }
                }
            }
            UnityEngine.Debug.Log(exitAmount > 0);
            return exitAmount > 0;
        }
    }
}
