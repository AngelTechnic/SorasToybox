using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class BugSpray
    {
        public static void Add()
        {
            SpawnRandomEnemyAnywhereEffect spawnRandomHumor = ScriptableObject.CreateInstance<SpawnRandomEnemyAnywhereEffect>();
            spawnRandomHumor._enemies = [
                LoadedAssetsHandler.GetEnemy("HumourCholeric_EN"),
                LoadedAssetsHandler.GetEnemy("HumourPhlegmatic_EN"),
                LoadedAssetsHandler.GetEnemy("HumourSanguine_EN"),
                LoadedAssetsHandler.GetEnemy("HumourMelancholy_EN"),
                ];
            spawnRandomHumor._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            spawnRandomHumor._givesExperience = false;

            CheckCasterOrTargetIsUnitTypeEffect isThisABedbuge = ScriptableObject.CreateInstance<CheckCasterOrTargetIsUnitTypeEffect>();
            isThisABedbuge._UnitTypeID = "Bug";


            GainPlayerCurrencyEffect gimmeMoney = ScriptableObject.CreateInstance<GainPlayerCurrencyEffect>();

            TargetPerformEffectViaSubaction bugSpraySubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            bugSpraySubAct.effects =
                [
                    Effects.GenerateEffect(isThisABedbuge, 1, Targeting.AllUnits),
                    Effects.GenerateEffect(gimmeMoney, 3, null, Effects.CheckPreviousEffectCondition(true, 1)),
                ];

            PerformEffect_Item bugSpray = new("ST_BugSpray_ID")
            {
                Item_ID = "ST_BugSpray_SW",
                Name = "Bug Spray",
                Icon = ResourceLoader.LoadSprite("item_bugspray"),
                OnUnlockUsesTHE = false,
                DoesPopUpInfo = true,
                Flavour = "\"Repels bugs, or your money back!*\"",
                Description = "At the start of each fight, attempt to spawn a random Humour, and then gain 3 coins if there is a bug.",
                TriggerOn = TriggerCalls.OnCombatStart,
                ShopPrice = 9,
                IsShopItem = true,
                Effects =
                [
                    Effects.GenerateEffect(spawnRandomHumor, 1),
                    Effects.GenerateEffect(bugSpraySubAct, 1, Targeting.Slot_SelfSlot),

                ],
            };

            //unlock this
            string achievementID = "SorasToybox_Senek_Dreamer_ACH";
            string unlockID = "SorasToybox_Senek_Dreamer_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(bugSpray.item, new ItemModdedUnlockInfo(bugSpray.Item_ID, ResourceLoader.LoadSprite("item_bugspray_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, bugSpray.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [bugSpray.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Senek_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(bugSpray.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_BlueSkies_Senek", null, 32, null), achievementID);
            unlockAchievement.IsSecret = true;
            unlockAchievement.SecretDescription = "Unlocked a new item.";
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added Bug Spray.");
            }
        }
    }
}
