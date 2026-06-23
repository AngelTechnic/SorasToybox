using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class ClockKing
    {
        public static void Add()
        {
            //Here comes the boyyyyyyyyyy
            SpawnEnemyAnywhereEffect summonClockGuy = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            summonClockGuy.enemy = LoadedAssetsHandler.GetEnemy("FriendlyGearYinimro_EN");
            summonClockGuy._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            summonClockGuy.givesExperience = false;

            PerformEffect_Item clockKing = new PerformEffect_Item("Clock King", null, false)
            {
                Item_ID = "ClockKing_TW",
                Name = "Clock King",
                Flavour = "\"*BONG*\"",
                Description = "On combat start, attempts to summon a hacked Yinimro to fight for your side.",
                IsShopItem = true,
                ShopPrice = 139,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_clockking"), //item goes here
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(summonClockGuy, 1, Targeting.Slot_Front),
                ]
            };

            //Unlock this
            string achievementID = "SorasToybox_Mercurie_Inevitable_ACH";
            string unlockID = "SorasToybox_Mercurie_Inevitable_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(clockKing.item, new ItemModdedUnlockInfo(clockKing.Item_ID, ResourceLoader.LoadSprite("item_clockking_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, clockKing.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [clockKing.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Clock King", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_March_Mercurie", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
