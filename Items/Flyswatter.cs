using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class Flyswatter
    {
        public static void Add()
        {
            UnitTypePercMod unitTypePercMod = new UnitTypePercMod();
            unitTypePercMod.percentageToModify = 125;
            unitTypePercMod.doesIncrease = true;
            unitTypePercMod.unitType = "Bug";

            DamageDealtPercentageModifierByUnitType_Item flyswatter = new DamageDealtPercentageModifierByUnitType_Item("ST_Flyswatter_ID")
            {
                Item_ID = "ST_Flyswatter_SW",
                Name = "Flyswatter",
                Flavour = "\"They're coming to get meeeee!\"",
                Description = "This party member deals 33% less damage.\nThis party member deals 125% more damage to bugs.",
                IsShopItem = true,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_flyswatter"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                OnUnlockUsesTHE = true,
                DefaultDoesIncreaseDamage = false,
                DefaultPercentageToModify = 33,
                UnitTypeData = new UnitTypePercMod[] { unitTypePercMod },
            };

            //unlock this
            string achievementID = "SorasToybox_Senek_Witness_ACH";
            string unlockID = "SorasToybox_Senek_Witness_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(flyswatter.item, new ItemModdedUnlockInfo(flyswatter.Item_ID, ResourceLoader.LoadSprite("item_flyswatter_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, flyswatter.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [flyswatter.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Senek_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(flyswatter.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Osman_Senek", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Flyswatter.");
            }
        }

    }
}
