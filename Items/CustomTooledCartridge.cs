using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class CustomTooledCartridge
    {
        public static void Add()
        {
            GainPlayerCurrencyEffect gimmeMoney = ScriptableObject.CreateInstance<GainPlayerCurrencyEffect>();
            
            LosePlayerCurrencyEffect spendMoney = ScriptableObject.CreateInstance<LosePlayerCurrencyEffect>();

            DoublePerformEffect_Item cartridge = new DoublePerformEffect_Item("ST_Cartridge_ID")
            {
                Item_ID = "CustomTooledCartridge_SW",
                OnUnlockUsesTHE = true,
                Name = "Custom-Tooled Cartridge",
                Flavour = "\"...for *twelve* seconds.\"",
                Description = "Gain 3 coins on killing something.\nLose 1 coin on performing an ability.",
                DoesPopUpInfo = true,
                TriggerOn = TriggerCalls.OnKill,
                Effects =
                [
                    Effects.GenerateEffect(gimmeMoney, 3),
                ],
                SecondaryDoesPopUpInfo = false,
                SecondaryTriggerOn = [TriggerCalls.OnAbilityUsed],
                SecondaryEffects = 
                [
                    Effects.GenerateEffect(spendMoney, 1),
                ],
                IsShopItem = true,
                ShopPrice = 10,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_cartridge"),
            };

            //unlock this
            string achievementID = "SorasToybox_Senek_Divine_ACH";
            string unlockID = "SorasToybox_Senek_Divine_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(cartridge.item, new ItemModdedUnlockInfo(cartridge.Item_ID, ResourceLoader.LoadSprite("item_cartridge_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, cartridge.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [cartridge.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Senek_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(cartridge.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Heaven_Senek", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Custom-Tooled Cartridge.");
            }
        }
    }
}
