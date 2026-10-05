using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using SorasToybox.Items.Vanilla_Fool_DM_Unlocks;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class Screwyoudriver
    {
        public static void Add()
        {
            PerformEffect_Item screwyoudriver = new PerformEffect_Item("ST_Screwyoudriver_ID", null, false)
            {
                Item_ID = "Screwyoudriver_TW",
                Name = "Screwyoudriver",
                Flavour = "\"Least efficient method of face removal. You're welcome.\"",
                Description = "Inflict Poisoned equal to your intended damage against targets of damage effects.\nThis ignores most modifiers.",
                IsShopItem = false,
                ShopPrice = 7,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_screwyoudriver"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<ScrewyoudriverCondition>()],

            };

            screwyoudriver.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Knife.ToString(),
            ];
            //unlock this
            string achievementID = "SorasToybox_Whhvay_Dreamer_ACH";
            string unlockID = "SorasToybox_Whhvay_Dreamer_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(screwyoudriver.item, new ItemModdedUnlockInfo(screwyoudriver.Item_ID, ResourceLoader.LoadSprite("item_screwyoudriver_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, screwyoudriver.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [screwyoudriver.Item_ID],
            };


            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(screwyoudriver.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_BlueSkies_Whhvay", null, 32, null), achievementID);
            unlockAchievement.IsSecret = true;
            unlockAchievement.SecretDescription = "Unlocked a new item.";
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Screwyoudriver.");
            }
        }
    }
}
