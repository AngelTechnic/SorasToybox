using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using SorasToybox.Items.Vanilla_Fool_DM_Unlocks;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class SymbolOnFlesh
    {
        public static void Add()
        {

            FullHealEffect imsecretlyintothis = ScriptableObject.CreateInstance<FullHealEffect>();
            imsecretlyintothis._directHeal = true;

            ConsumeItemEffect ruined = ScriptableObject.CreateInstance<ConsumeItemEffect>();

            CurrentHealthEffectorCondition dead = ScriptableObject.CreateInstance<CurrentHealthEffectorCondition>();
            dead.healthUnderThreshold = true;
            dead.healthThreshold = 1;

            CountTargetSlotsEffect countempty = ScriptableObject.CreateInstance<CountTargetSlotsEffect>();
            countempty.m_CountOnlyEmptySlots = true;

            //spawning Shames
            SpawnEnemyAnywhereEffect shameFollows = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            shameFollows.enemy = LoadedAssetsHandler.GetEnemy("BurningShame_EN");
            shameFollows._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            shameFollows.givesExperience = false;

            PerformEffect_Item symbolOnFlesh = new PerformEffect_Item("ST_SymbolOnFlesh_ID")
            {
                Item_ID = "SymbolOnFlesh_TW",
                Name = "Symbol On Flesh",
                Flavour = "\"Your life could have been the most beautiful story ever told.\"",
                Description = "If this party member takes fatal damage, they will be given a second chance if there is room for Shame.",
                ShopPrice = 10,
                StartsLocked = true,
                IsShopItem = false,
                TriggerOn = TriggerCalls.OnDamaged,
                Conditions = [dead],
                ConsumeOnUse = true,
                Effects = 
                [
                    Effects.GenerateEffect(countempty, 1, Targeting.Slot_OpponentAllSlots),
                    Effects.GenerateEffect(imsecretlyintothis, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(shameFollows, 5, Targeting.Slot_OpponentAllSlots, Effects.CheckMultiplePreviousEffectsCondition([true, true], [1, 2])),
                ],
                OnUnlockUsesTHE = true,
                Icon = ResourceLoader.LoadSprite("item_birthmark"),
            };
            //unlock this
            string achievementID = "SorasToybox_Gospel_Antagonist_ACH";
            string unlockID = "SorasToybox_Gospel_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(symbolOnFlesh.item, new ItemModdedUnlockInfo(symbolOnFlesh.Item_ID, ResourceLoader.LoadSprite("item_birthmark_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, symbolOnFlesh.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [symbolOnFlesh.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Gospel", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(symbolOnFlesh.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Gospel", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Gospel_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Symbol On Flesh.");
            }
        }
    }
}
