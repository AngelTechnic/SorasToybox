using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class LucifersHead
    {
        public static void Add()
        {
            GenerateColorManaEffect makeBrokenPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            makeBrokenPigment.mana = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

            StatusEffect_Apply_Effect getAnte = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getAnte._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            PerformEffect_Item lucifersHead = new PerformEffect_Item("ST_LucifersHead_ID", null, false)
            {
                Item_ID = "LucifersSeveredHead_TW",
                Name = "Lucifer's Severed Head",
                Flavour = "\"I am a sinner... All have sinned.\"",
                Description = "Apply 1 Ante to all party members and generate 1 Broken Pigment at the end of your turn.",
                Icon = ResourceLoader.LoadSprite("item_lucifershead"),
                TriggerOn = TriggerCalls.OnTurnFinished,
                OnUnlockUsesTHE = false,
                IsShopItem = false,
                ShopPrice = 666,
                StartsLocked = true,
                Effects =
                [
                    Effects.GenerateEffect(getAnte, 1, Targeting.Unit_AllAllies),
                    Effects.GenerateEffect(makeBrokenPigment, 1, Targeting.Slot_SelfSlot),
                ],
            };


            //unlock this
            string achievementID = "SorasToybox_Formosus_Antagonist_ACH";
            string unlockID = "SorasToybox_Formosus_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(lucifersHead.item, new ItemModdedUnlockInfo(lucifersHead.Item_ID, ResourceLoader.LoadSprite("item_lucifershead_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, lucifersHead.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [lucifersHead.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Formosus", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Lucifer's Severed Head", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Formosus", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Formosus_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Lucifer's Severed Head.");
            }
        }
    }
}
