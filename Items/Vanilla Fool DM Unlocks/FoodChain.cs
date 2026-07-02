using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using UnityEngine;

namespace SorasToybox.Items
{
    public class FoodChain
    {
        public static void Add()
        {
            ProportionalCurHealthDamageEffect cringe = ScriptableObject.CreateInstance<ProportionalCurHealthDamageEffect>();

            ExtraPassiveAbility_Wearable_SMS getOvertuned = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getOvertuned._extraPassiveAbility = Passives.GetCustomPassive("ST_Overtuned_PA");

            PerformEffect_Item sosigLink = new PerformEffect_Item("ST_FoodChain_ID", null, false)
            {
                Item_ID = "ST_FoodChain_SW",
                Name = "Food Chain",
                Flavour = "\"Parasocial Darwinism.\"",
                Description = "Gain Overtuned as a passive. On combat start, kill the weakest party member, excluding self.",
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(cringe, 100, Targeting.GenerateUnitTarget_Specific_Health(true, true, false, true)),
                ],
                EquippedModifiers = [getOvertuned],
                StartsLocked = true,
                ShopPrice = 6,
                Icon = ResourceLoader.LoadSprite("item_foodchain"),
                IsShopItem = true,
            };

            //unlock this
            string achievementID = "SorasToybox_Pearl_Antagonist_ACH";
            string unlockID = "SorasToybox_Pearl_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(sosigLink.item, new ItemModdedUnlockInfo(sosigLink.Item_ID, ResourceLoader.LoadSprite("item_foodchain_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, sosigLink.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [sosigLink.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Pearl", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Food Chain", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Pearl", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");
        }
    }
}
