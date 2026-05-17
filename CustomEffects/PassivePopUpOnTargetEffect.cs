using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace SorasToybox.CustomEffects
{
    public class PassivePopUpOnTargetEffect : EffectSO
    {
        public string _name;

        public string _sprite;

        public bool _isUnitCharacter;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(targetSlotInfo.Unit.ID, _isUnitCharacter, _name, ResourceLoader.LoadSprite(_sprite, null, 32, null)));
                    exitAmount++;
                }
            }

            return exitAmount > 0;
        }
    }
}
