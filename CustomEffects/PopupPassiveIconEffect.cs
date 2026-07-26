using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomEffects
{
    public class PopupPassiveIconEffect : EffectSO
    {
        // Token: 0x060000CF RID: 207 RVA: 0x0000D83C File Offset: 0x0000BA3C
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            string text = this._passiveToPopup.GetPassiveLocData().text;
            Sprite passiveIcon = this._passiveToPopup.passiveIcon;
            exitAmount = 1;
            CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(caster.ID, caster.IsUnitCharacter, text, passiveIcon));
            return exitAmount > 0;
        }

        // Token: 0x04000046 RID: 70
        public BasePassiveAbilitySO _passiveToPopup;
    }

    public class PopupPassiveIconWithSetTextEffect : EffectSO
    {
        // Token: 0x060000CF RID: 207 RVA: 0x0000D83C File Offset: 0x0000BA3C
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            string text = text2use;
            Sprite passiveIcon = this._passiveToPopup.passiveIcon;
            exitAmount = 1;
            CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(caster.ID, caster.IsUnitCharacter, text, passiveIcon));
            return exitAmount > 0;
        }

        // Token: 0x04000046 RID: 70
        public BasePassiveAbilitySO _passiveToPopup;
        public string text2use = "";
    }
}
