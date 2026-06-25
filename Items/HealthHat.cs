using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class HealthHat
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS getMammal = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getMammal._extraPassiveAbility = Passives.GetCustomPassive("Mammal_PA");

            FullHealEffect imsecretlyintothis = ScriptableObject.CreateInstance<FullHealEffect>();
            imsecretlyintothis._directHeal = true;

            ConsumeItemEffect ruined = ScriptableObject.CreateInstance<ConsumeItemEffect>();

            CurrentHealthEffectorCondition dead = ScriptableObject.CreateInstance<CurrentHealthEffectorCondition>();
            dead.healthUnderThreshold = true;
            dead.healthThreshold = 1;

            PerformEffect_Item dkyHat = new PerformEffect_Item("ST_DKYHat_ID", null, false)
            {
                Item_ID = "HealthyHeadwear_SW",
                Name = "Healthy Headwear",
                Flavour = "\"Don't kill yourself.\"",
                Description = "Gain Mammal as a passive. If this party member dies, destroy this item and fully heal them.",
                TriggerOn = TriggerCalls.OnDamaged,
                Conditions = [dead],
                ShopPrice = 15,
                IsShopItem = true,
                StartsLocked = true,
                EquippedModifiers = [getMammal],
                Icon = ResourceLoader.LoadSprite("item_healthhat"),
                Effects =
                [
                    Effects.GenerateEffect(imsecretlyintothis, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ruined, 1, Targeting.Slot_SelfSlot),
                ],

            };

            //unlock this
            string achievementID = "SorasToybox_Karma_Divine_ACH";
            string unlockID = "SorasToybox_Karma_Divine_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(dkyHat.item, new ItemModdedUnlockInfo(dkyHat.Item_ID, ResourceLoader.LoadSprite("item_healthhat_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, dkyHat.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [dkyHat.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Karma_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Healthy Headwear", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Heaven_Karma", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);

        }
    }
}
