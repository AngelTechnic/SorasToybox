using BrutalAPI.Items;
using SorasToybox.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class WaterBalloons
    {
        public static void Add()
        {
            SpawnEnemyAnywhereEffect spawnSeaMine = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            spawnSeaMine.enemy = LoadedAssetsHandler.GetEnemy("ST_SeaMine_EN");

            PerformEffect_Item waterBalloons = new("ST_WaterBallons_ID")
            {
                Item_ID = "WaterBalloons_TW",
                Name = "\"Water Balloons\"",
                Flavour = "\"Don't pop them.\"",
                Description = "At the start of each fight, attempt to spawn up to 2 friendly Sea Mines.",
                ShopPrice = 12,
                IsShopItem = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_waterballoons"),
                TriggerOn = TriggerCalls.OnCombatStart,
                DoesPopUpInfo = true,
                Effects =
                [
                    Effects.GenerateEffect(spawnSeaMine, 2),
                ],
            };

            //Unlock this
            string achievementID = "SorasToybox_SheWhoSwims_Antagonist_ACH";
            string unlockID = "SorasToybox_SheWhoSwims_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(waterBalloons.item, new ItemModdedUnlockInfo(waterBalloons.Item_ID, ResourceLoader.LoadSprite("item_waterballoons_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, waterBalloons.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [waterBalloons.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("SheWhoSwims_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(waterBalloons.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_SWSID", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("SheWhoSwims_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the \"Water Balloons\".");
            }
        }
    }
}
