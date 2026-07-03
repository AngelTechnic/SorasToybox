using BrutalAPI;
using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items.Vanilla_Fool_DM_Unlocks
{
    public class BadPublicity
    {
        public static void Add()
        {
            //spawning a Shame
            SpawnEnemyAnywhereEffect shameFollows = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            shameFollows.enemy = LoadedAssetsHandler.GetEnemy("BurningShame_EN");
            shameFollows._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            shameFollows.givesExperience = false;

            Check_CountPigmentEffect howMuchBroken = ScriptableObject.CreateInstance<Check_CountPigmentEffect>();
            howMuchBroken._colourPigment = LoadedDBsHandler.PigmentDB.GetPigment("Broken");

            StatusEffect_Apply_Effect strengthByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            strengthByPrevious._MultPreviousExitValueForEntry = true;
            strengthByPrevious._Status = StatusField.GetCustomStatusEffect("Strength_ID");

            DoublePerformEffect_Item badPublicity = new DoublePerformEffect_Item("ST_BadPublicity_ID", null, false)
            {
                Item_ID = "BadPublicity_TW",
                Name = "Bad Publicity",
                Flavour = "\"I miss the quiet.\"",
                Description = "On turn end, gain Strength equal to the amount of Broken Pigment in the tray.\nYou are being followed.",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = false, 
                Icon = ResourceLoader.LoadSprite("item_badpublicity"), //item goes here
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(shameFollows, 1, Targeting.Slot_Front),
                ],
                SecondaryTriggerOn = [TriggerCalls.OnTurnFinished],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects =
                [
                    Effects.GenerateEffect(howMuchBroken),
                    Effects.GenerateEffect(strengthByPrevious, 1, Targeting.Slot_SelfSlot),
                ],
                OnUnlockUsesTHE = false,
            };

            //unlock this
            string achievementID = "SorasToybox_Kleiver_Antagonist_ACH";
            string unlockID = "SorasToybox_Griffin_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(badPublicity.item, new ItemModdedUnlockInfo(badPublicity.Item_ID, ResourceLoader.LoadSprite("item_badpublicity_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, badPublicity.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [badPublicity.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Kleiver", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Bad Publicity", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Kleiver", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Bad Publicity.");
            }
        }
    }
}
