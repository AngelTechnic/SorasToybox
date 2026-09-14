using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class MemoryOfSyndomera
    {
        public static void Add()
        {
            StatusEffectCheckerEffect hasIrradiated = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasIrradiated._status = StatusField.GetCustomStatusEffect("Irradiated_ID");

            StatusEffect_Apply_Effect getIrradiated = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getIrradiated._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");

            TargetPerformEffectViaSubaction memsyndSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            memsyndSubAct.effects =
                [
                    Effects.GenerateEffect(hasIrradiated, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getIrradiated, 10, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                ];


            PerformEffect_Item memoryOfSyndomera = new PerformEffect_Item("ST_Diagnopsis_ID", null, false)
            {
                Item_ID = "MemoryOfSyndomera_TW",
                Name = "Memory of Syndomera",
                Flavour = "\"I hate what they did to this place.\"",
                Description = "At the start of each turn, inflict 10 Irradiated to all enemies that aren't already Irradiated.",
                TriggerOn = TriggerCalls.OnTurnStart,
                IsShopItem = false,
                ShopPrice = 7,
                OnUnlockUsesTHE = true,
                Icon = ResourceLoader.LoadSprite("item_memoryofsyndomera", null, 32, null),
                DoesPopUpInfo = true,
                Effects =
                [
                    Effects.GenerateEffect(memsyndSubAct, 1, Targeting.Unit_AllOpponents),
                ],
            };

            //unlock this
            string achievementID = "SorasToybox_Whhvay_Antagonist_ACH";
            string unlockID = "SorasToybox_Whhvay_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(memoryOfSyndomera.item, new ItemModdedUnlockInfo(memoryOfSyndomera.Item_ID, ResourceLoader.LoadSprite("item_memoryofsyndomera_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, memoryOfSyndomera.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [memoryOfSyndomera.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Memory of Syndomera", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Memory of Syndomera.");
            }
        }
    }
}
