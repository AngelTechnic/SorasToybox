using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;

namespace SorasToybox.Items
{
    public class RottenKintsugi
    {
        public static void Add()
        {
            HealIncreasePercentCondition healMore = ScriptableObject.CreateInstance<HealIncreasePercentCondition>();
            healMore.percentage = 100;
            healMore.increase = true;

            DoublePerformEffect_Item rottenKintsugi = new DoublePerformEffect_Item("ST_RottenKintsugi_ID")
            {
                Item_ID = "RottenKintsugi_SW",
                Name = "Rotten Kintsugi",
                Flavour = "\"Time heals all- well, *most* wounds.\"",
                Description = "All healing dealt by this party member is doubled. Inflict 2 Petrified to healed targets.",
                TriggerOn = TriggerCalls.OnWillApplyHeal,
                DoesPopUpInfo = false,
                Conditions = [healMore],
                SecondaryTriggerOn = [TriggerCalls.OnWillApplyHeal, TriggerCalls.CanHeal],
                SecondaryConditions = [ScriptableObject.CreateInstance<RottenKintsugiCondition>()],
                SecondaryDoesPopUpInfo = false,
                StartsLocked = true,
                ShopPrice = 9,
                Icon = ResourceLoader.LoadSprite("item_rottenkintsugi"),
                IsShopItem = true,
                OnUnlockUsesTHE = false,
            };


            //Unlock this
            string achievementID = "SorasToybox_Lazarus_Antagonist_ACH";
            string unlockID = "SorasToybox_Lazarus_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(rottenKintsugi.item, new ItemModdedUnlockInfo(rottenKintsugi.Item_ID, ResourceLoader.LoadSprite("item_rottenkintsugi_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, rottenKintsugi.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [rottenKintsugi.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Lazarus_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(rottenKintsugi.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Lazarus", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Lazarus_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Rotten Kintsugi.");
            }
        }
    }
}
