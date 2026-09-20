using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class AstralHilt
    {
        public static void Add()
        {
            UnitTypePercMod unitTypePercMod = new UnitTypePercMod();
            unitTypePercMod.percentageToModify = 100;
            unitTypePercMod.doesIncrease = true;
            unitTypePercMod.unitType = "Zoincaillan";

            DamageDealtPercentageModifierByUnitType_Item astralHilt = new DamageDealtPercentageModifierByUnitType_Item("ST_AstralHilt_ID")
            {
                Item_ID = "AstralHilt_TW",
                Name = "Astral Hilt",
                Flavour = "\"Meet Potential Blade!\"",
                Description = "This party member deals 20% more damage.\nThis party member instead deals 100% more damage if their target hails from the realm of twin suns.",
                IsShopItem = true,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_astralhilt"),
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                OnUnlockUsesTHE = true,
                DefaultDoesIncreaseDamage = true,
                DefaultPercentageToModify = 20,
                UnitTypeData = new UnitTypePercMod[] { unitTypePercMod },
            };

            //unlock this
            string achievementID = "SorasToybox_Whhvay_Witness_ACH";
            string unlockID = "SorasToybox_Whhvay_Witness_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(astralHilt.item, new ItemModdedUnlockInfo(astralHilt.Item_ID, ResourceLoader.LoadSprite("item_astralhilt_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, astralHilt.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [astralHilt.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(astralHilt.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Osman_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Astral Hilt.");
            }

        }
    }
}
