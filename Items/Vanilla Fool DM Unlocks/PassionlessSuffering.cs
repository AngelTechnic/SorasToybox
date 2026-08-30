using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class PassionlessSuffering
    {
        public static void Add()
        {
            RemoveOverflowManaEffect noverflow = ScriptableObject.CreateInstance<RemoveOverflowManaEffect>();
            noverflow._fullyDeplete = true;

            StatusEffect_Apply_Effect malfByPrev = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            malfByPrev._Status = StatusField.GetCustomStatusEffect("Malfunction_ID");
            malfByPrev._MultPreviousExitValueForEntry = true;

            ExtraPassiveAbility_Wearable_SMS getSuicidal = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getSuicidal._extraPassiveAbility = Passives.GetCustomPassive("ITA_Suicidal_PA");

            PerformEffect_Item passionless = new PerformEffect_Item("ST_Passionless_ID", null, false)
            {
                Item_ID = "PassionlessSuffering_TW",
                Name = "Passionless Suffering",
                Flavour = "\"More than I love any physical being.\"",
                Description = "This party member is now Suicidal.\nOn turn end, consume all Overflow and gain equal Malfunction.",
                Icon = ResourceLoader.LoadSprite("item_passionless"),
                TriggerOn = TriggerCalls.OnTurnFinished,
                Effects =
                [
                    Effects.GenerateEffect(noverflow, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(malfByPrev, 1, Targeting.Slot_SelfSlot),
                ],
                EquippedModifiers = [getSuicidal],
                IsShopItem = false,
                ShopPrice = 8,
                StartsLocked = true,
                OnUnlockUsesTHE = false,
            };

            //unlock this
            string achievementID = "SorasToybox_Cranes_Antagonist_ACH";
            string unlockID = "SorasToybox_Cranes_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(passionless.item, new ItemModdedUnlockInfo(passionless.Item_ID, ResourceLoader.LoadSprite("item_passionless_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, passionless.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [passionless.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Cranes", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Passionless Suffering", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Griffin", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Cranes_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Passionless Suffering.");
            }
        }
    }
}
