using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class ShootingGallery
    {
        public static void Add()
        {
            SpawnEnemyAnywhereEffect giveMeALLTheTargets = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            giveMeALLTheTargets.enemy = LoadedAssetsHandler.GetEnemy("SenekTasbeht_EN");
            giveMeALLTheTargets._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            giveMeALLTheTargets.givesExperience = false;

            PerformEffect_Item shootingGallery = new PerformEffect_Item("ST_ShootingGallery_ID")
            {
                Item_ID = "ShootingGallery_SW",
                Icon = ResourceLoader.LoadSprite("item_shootinggallery"),
                Name = "Shooting Gallery",
                Flavour = "\"BREAK THE TARGETS!\"",
                Description = "At the start of combat, summon as many of Senek's Tasbehts as possible!",
                IsShopItem = true,
                ShopPrice = 8,
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(giveMeALLTheTargets, 5),
                ],
            };

            //unlock this
            string achievementID = "SorasToybox_Senek_Abstraction_ACH";
            string unlockID = "SorasToybox_Senek_Abstraction_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(shootingGallery.item, new ItemModdedUnlockInfo(shootingGallery.Item_ID, ResourceLoader.LoadSprite("item_shootinggallery_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, shootingGallery.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [shootingGallery.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Senek_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(shootingGallery.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Doula_Senek", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");
        }
    }
}
