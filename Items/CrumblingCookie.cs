using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class CrumblingCookie
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect overclockByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            overclockByPrevious._Status = StatusField.GetCustomStatusEffect("Overclock_ID");
            overclockByPrevious._MultPreviousExitValueForEntry = true;

            StatusEffect_Apply_Effect affixedByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            affixedByPrevious._Status = StatusField.GetCustomStatusEffect("Affixed_ID");
            affixedByPrevious._MultPreviousExitValueForEntry = true;

            CheckHasUnitEffect unitCounter = ScriptableObject.CreateInstance<CheckHasUnitEffect>();

            PerformEffect_Item cookie = new("ST_CrumblingCookie_ID")
            {
                Item_ID = "CrumblingCookie_TW",
                Name = "Crumbling Cookie",
                Flavour = "\"Let's get this over with.\"",
                Description = "At the start of your first turn, gain Overclock and Affixed equal to the amount of enemies present.",
                Icon = ResourceLoader.LoadSprite("item_cookie"),
                IsShopItem = false,
                ShopPrice = 1,
                StartsLocked = true,
                OnUnlockUsesTHE = false,
                TriggerOn = TriggerCalls.OnFirstTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(unitCounter, 1, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(overclockByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(unitCounter, 1, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(affixedByPrevious, 1, Targeting.Slot_SelfSlot),
                ],
            };
            cookie.item._ItemTypeIDs =
            [
                "FoodID",
            ];

            //Unlock this
            string achievementID = "SorasToybox_Senek_Inevitable_ACH";
            string unlockID = "SorasToybox_Senek_Inevitable_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(cookie.item, new ItemModdedUnlockInfo(cookie.Item_ID, ResourceLoader.LoadSprite("item_cookie_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, cookie.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [cookie.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Senek_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(cookie.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_March_Senek", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Crumbling Cookie.");
            }

        }
    }
}
