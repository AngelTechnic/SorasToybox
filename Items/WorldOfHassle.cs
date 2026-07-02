using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;


namespace SorasToybox.Items
{
    public class WorldOfHassle
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS getErasure = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getErasure._extraPassiveAbility = Passives.GetCustomPassive("Erasure_PA");

            RemoveStatusEffectEffect noSpotlight = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noSpotlight._status = StatusField.Spotlight;

            StatusEffect_Apply_Effect getSpotlight = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getSpotlight._Status = StatusField.Spotlight;

            StatusEffectCheckerEffect hasSpotlight = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasSpotlight._status = StatusField.Spotlight;

            StatusEffect_Apply_Effect getAnte = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getAnte._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            PerformEffect_Item mickeyMilan = new PerformEffect_Item("ST_WorldOfHassle_ID", null, false)
            {
                Item_ID = "WorldOfHassle_SW",
                Name = "World of Hassle",
                Flavour = "\"Step back into the limelight.\"",
                Description = "Gain Erasure as a passive.\nWhen this party member is damaged, gain 1 Ante.",
                TriggerOn = TriggerCalls.OnDamaged,
                Effects = 
                [
                    Effects.GenerateEffect(getSpotlight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getAnte, 1, Targeting.Slot_SelfSlot),
                ],
                EquippedModifiers = [getErasure],
                StartsLocked = true,
                ShopPrice = 9,
                Icon = ResourceLoader.LoadSprite("item_worldofhassle"),
                IsShopItem = true,
            };

            //Unlock this
            string achievementID = "SorasToybox_Mercurie_Forgotten_ACH";
            string unlockID = "SorasToybox_Mercurie_Forgotten_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(mickeyMilan.item, new ItemModdedUnlockInfo(mickeyMilan.Item_ID, ResourceLoader.LoadSprite("item_worldofhassle_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, mickeyMilan.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [mickeyMilan.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("World of Hassle", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Nobody_Mercurie", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
