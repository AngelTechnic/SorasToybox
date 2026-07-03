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
    public class ProcessedSludge
    {
        public static void Add()
        {
            GenerateColorManaEffect genGrey = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            genGrey.mana = Pigments.Grey;

            StatusEffect_ApplyRestrictor_Effect permanentMisery = ScriptableObject.CreateInstance<StatusEffect_ApplyRestrictor_Effect>();
            permanentMisery._Status = StatusField.GetCustomStatusEffect("Misery_ID");

            PerformEffect_Item processedSludge = new("ST_ProcessedSludge_ID")
            {
                Item_ID = "ProcessedSludge_SW",
                Name = "Processed Sludge",
                Flavour = "\"\'Normal\' food...\"",
                Description = "On combat start, generate 5 Grey pigment, then gain 3 permanent Misery because this stuff tastes horrible.",
                Icon = ResourceLoader.LoadSprite("item_processedsludge"),
                TriggerOn = TriggerCalls.OnCombatStart,
                OnUnlockUsesTHE = false,
                IsShopItem = true,
                ShopPrice = 4,
                StartsLocked = true,
                Effects =
                [
                    Effects.GenerateEffect(genGrey, 5, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(permanentMisery, 3, Targeting.Slot_SelfSlot),
                ],
            };


            processedSludge.item._ItemTypeIDs =
            [
                "FoodID",
                "Meat",
            ];

            //unlock this
            string achievementID = "SorasToybox_Splig_Antagonist_ACH";
            string unlockID = "SorasToybox_Splig_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(processedSludge.item, new ItemModdedUnlockInfo(processedSludge.Item_ID, ResourceLoader.LoadSprite("item_processedsludge_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, processedSludge.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [processedSludge.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Splig", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Processed Sludge", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Splig", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Processed Sludge.");
            }
        }
    }
}
