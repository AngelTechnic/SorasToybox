using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using Tools;
using UnityEngine;

namespace SorasToybox.Items
{
    public class Milkshake
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect overclockMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            overclockMe._Status = StatusField.GetCustomStatusEffect("Overclock_ID");

            MilkshakeSpawnEffect bringAllTheBoysToTheYard = ScriptableObject.CreateInstance<MilkshakeSpawnEffect>();
            bringAllTheBoysToTheYard._permanentSpawn = false;
            bringAllTheBoysToTheYard._rank = 1;
            bringAllTheBoysToTheYard._nameAddition = NameAdditionLocID.NameAdditionNone;

            PerformEffect_Item milkshake = new PerformEffect_Item("ST_Milkshake_ID", null, false)
            {
                Item_ID = "GoldFlakeMilkshake_SW",
                Name = "Gold-Flake Milkshake",
                Flavour = "\"Who the hell puts GOLD in their milkshakes?\"",
                Description = "On killing an enemy, gain 2 Overclock.\nMight attract attention.",
                IsShopItem = true,
                ShopPrice = 10,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_goldmilkshake"),
                TriggerOn = TriggerCalls.OnKill,
                Effects = 
                [
                    Effects.GenerateEffect(overclockMe, 2, Targeting.Slot_SelfSlot), 
                    Effects.GenerateEffect(bringAllTheBoysToTheYard, 1, null, Effects.ChanceCondition(33))
                ], 
                   
            };

            milkshake.item._ItemTypeIDs =
            [
                "FoodID",
                "Drink",
            ];
            //unlock this
            string achievementID = "SorasToybox_Mercurie_Witness_ACH";
            string unlockID = "SorasToybox_Mercurie_Witness_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(milkshake.item, new ItemModdedUnlockInfo(milkshake.Item_ID, ResourceLoader.LoadSprite("item_goldmilkshake_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, milkshake.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [milkshake.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(milkshake.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Osman_Mercurie", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Gold-Flake Milkshake.");
            }
        }
    }
}
