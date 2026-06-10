using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;

namespace SorasToybox.Items
{
    public class BastardNimbus
    {
        public static void Add()
        {
            //Getting Red Color
            string redID = ColorUtility.ToHtmlStringRGB(Color.red);

            //Burning (1) by retrieving from Firebird (salt enemies)
            ExtraPassiveAbility_Wearable_SMS iBleedFire = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            iBleedFire._extraPassiveAbility = Passives.GetCustomPassive("Blazing_PA"); 

            //Cold-Blooded
            ExtraPassiveAbility_Wearable_SMS butIllNeverBurn = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            butIllNeverBurn._extraPassiveAbility = Passives.GetCustomPassive("MadeOfFire_PA");

            PerformEffect_Item rrod = new PerformEffect_Item("BastardNimbus_ID", null, false)
            {
                Item_ID = "BastardNimbus_TW",
                Name = "Bastard Nimbus",
                Flavour = "<color=#" + redID + ">O</color>",
                Description = "Gain Blazing and Made Of Fire as passives.",
                IsShopItem = false,
                ShopPrice = 7,
                StartsLocked = true,
                EquippedModifiers = [iBleedFire, butIllNeverBurn],
                Icon = ResourceLoader.LoadSprite("item_bastard_nimbus"),
                OnUnlockUsesTHE = true,
            };
            rrod.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Magic.ToString(),
            ];

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(rrod.item, new ItemModdedUnlockInfo(rrod.Item_ID, ResourceLoader.LoadSprite("item_bastard_nimbus_locked", null, 32, null), "SorasToybox_Karma_Abstraction_ACH"));

            //unlock this
            string achievementID = "SorasToybox_Karma_Abstraction_ACH";
            string unlockID = "SorasToybox_Karma_Abstraction_Unlock";



            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, rrod.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [rrod.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Karma_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Bastard Nimbus", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Doula_Karma", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
