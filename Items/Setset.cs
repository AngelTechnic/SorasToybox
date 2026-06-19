using BrutalAPI;
using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements.Experimental;

namespace SorasToybox.Items
{
    public class Setset
    {
        public static void Add()
        {
            //Creating function that calls for Ante
            StatusEffect_Apply_Effect anteUp = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteUp._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            ExtraPassiveAbility_Wearable_SMS wearablePassive = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassive._extraPassiveAbility = Passives.GetCustomPassive("Karmic_PA");
            PerformEffect_Item sufferingBuildsCharacter = new PerformEffect_Item("Setset_ID", null, false)
            {
                Item_ID = "Setset_TW",
                Name = "Setset",
                Flavour = "\"I will outlive these bastards.\"",
                Description = "Gain Karmic as a passive.\nOn receiving any damage, gain 1 Ante.",
                IsShopItem = false,
                ShopPrice = 7,
                StartsLocked = true,

                Icon = ResourceLoader.LoadSprite("item_setset"),
                OnUnlockUsesTHE = false,
                //gives this passive to user
                EquippedModifiers = [wearablePassive],
                //Trigger on...?
                TriggerOn = TriggerCalls.OnBeingDamaged,
                Effects =
                [
                    //Apply Ante, 1 stack, to Self.
                    Effects.GenerateEffect(anteUp, 1, Targeting.Slot_SelfSlot),
                ]
            };
            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(sufferingBuildsCharacter.item, new ItemModdedUnlockInfo(sufferingBuildsCharacter.Item_ID, ResourceLoader.LoadSprite("item_setset_locked", null, 32, null), "SorasToybox_Karma_Antagonist_ACH"));
            //unlock this
            string achievementID = "SorasToybox_Karma_Antagonist_ACH";
            string unlockID = "SorasToybox_Karma_Antagonist_Unlock";



            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, sufferingBuildsCharacter.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [sufferingBuildsCharacter.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Karma_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Setset", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Karma", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");
        } 
    }
}
