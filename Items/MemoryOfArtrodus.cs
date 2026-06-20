using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class MemoryOfArtrodus
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect getDeterminedMaybe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getDeterminedMaybe._Status = StatusField.GetCustomStatusEffect("Determined_ID");

            PercentageEffectorCondition TheChance = ScriptableObject.CreateInstance<PercentageEffectorCondition>();
            TheChance.triggerPercentage = 70;


            PerformEffect_Item goodSpaceStation = new PerformEffect_Item("ST_GoodSpaceStation_ID", null, false)
            {
                Item_ID = "MemoryOfArtrodus_TW",
                Name = "Memory of ARTRODUS",
                Flavour = "\"I still feel fondly about this place.\"",
                Description = "On being healed, 70% chance to gain 2 Determined.",
                IsShopItem = false,
                ShopPrice = 7,
                StartsLocked = true,

                Icon = ResourceLoader.LoadSprite("item_memoryofartrodus"),
                TriggerOn = TriggerCalls.OnBeingHealed,
                Conditions = [TheChance],
                Effects =
                [
                    Effects.GenerateEffect(getDeterminedMaybe, 2, Targeting.Slot_SelfSlot),
                ],
            };
            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(goodSpaceStation.item, new ItemModdedUnlockInfo(goodSpaceStation.Item_ID, ResourceLoader.LoadSprite("item_memoryofartrodus_locked", null, 32, null), "SorasToybox_Mercurie_Dreamer_ACH"));

            //unlock this
            string achievementID = "SorasToybox_Mercurie_Dreamer_ACH";
            string unlockID = "SorasToybox_Mercurie_Dreamer_Unlock";



            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, goodSpaceStation.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [goodSpaceStation.Item_ID],
            };


            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Memory of ARTRODUS", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_BlueSkies_Mercurie", null, 32, null), achievementID);
            unlockAchievement.IsSecret = true;
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
        }
    }
}
