using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;


namespace SorasToybox.Items
{
    public class TempusBoostHarness
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS becomeIrresolute = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            becomeIrresolute._extraPassiveAbility = Passives.GetCustomPassive("Irresolute_PA");

            ExtraPassiveAbility_Wearable_SMS thisThingIsReallyUncomfortableOops = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            thisThingIsReallyUncomfortableOops._extraPassiveAbility = Passives.Skittish;



            PerformEffect_Item evilBirdSuit = new PerformEffect_Item("ST_TempusHarness_ID", null, false)
            {
                Item_ID = "TempusBoostHarness_TW",
                Name = "TEMPUS Boost Harness",
                Flavour = "\"Alternate timelines make my head hurt.\"",
                Description = "Gain Irresolute and Skittish as passives.",
                IsShopItem = false,
                ShopPrice = 7,
                StartsLocked = true,
                EquippedModifiers = [becomeIrresolute, thisThingIsReallyUncomfortableOops],
                OnUnlockUsesTHE = true,

            };

            //unlock this
            string achievementID = "SorasToybox_Mercurie_Divine_ACH";
            string unlockID = "SorasToybox_Mercurie_Divine_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(evilBirdSuit.item, new ItemModdedUnlockInfo(evilBirdSuit.Item_ID, ResourceLoader.LoadSprite("item_goldmilkshake_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, evilBirdSuit.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [evilBirdSuit.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("TEMPUS Boost Harness", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Heaven_Mercurie", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);
        }
    }
}
