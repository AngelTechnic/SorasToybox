using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class Dartboard
    {
        public static void Add()
        {
            SpawnEnemyAnywhereEffect gimmeInfinityTasbeht = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            gimmeInfinityTasbeht.enemy = LoadedAssetsHandler.GetEnemy("InfinityTasbeht_EN");
            gimmeInfinityTasbeht._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            gimmeInfinityTasbeht.givesExperience = false;

            UnitTypePercMod unitTypePercMod = new UnitTypePercMod();
            unitTypePercMod.percentageToModify = 100;
            unitTypePercMod.doesIncrease = true;
            unitTypePercMod.unitType = "Friendly";

            PerformEffectPassiveAbility dartboardDeployment = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            dartboardDeployment.name = "ST_DeploymentDartboard_PA";
            dartboardDeployment.m_PassiveID = "DeploymentDartboard";
            dartboardDeployment._passiveName = "Deployment";
            dartboardDeployment.passiveIcon = ResourceLoader.LoadSprite("IconDeployment");
            dartboardDeployment._characterDescription = "On receiving direct damage, summon an Infinity Tasbeht.";
            dartboardDeployment._enemyDescription = "On receiving direct damage, summon an Infinity Tasbeht.";
            dartboardDeployment.doesPassiveTriggerInformationPanel = true;
            dartboardDeployment._triggerOn = [TriggerCalls.OnDirectDamaged];
            dartboardDeployment.effects =
            [
                Effects.GenerateEffect(gimmeInfinityTasbeht, 1),
            ];
            Passives.AddCustomPassiveToPool("ST_DeploymentDartboard_PA", "Deployment", dartboardDeployment);

            ExtraPassiveAbility_Wearable_SMS dartboardDeploymentWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            dartboardDeploymentWearable._extraPassiveAbility = dartboardDeployment;


            DamageDealtPercentageModifierByUnitType_Item dartboard = new("ST_Dartboard_ID")
            {
                Item_ID = "ST_Dartboard_TW",
                Icon = ResourceLoader.LoadSprite("item_dartboard"),
                IsShopItem = false,
                Name = "Dartboard",
                Flavour = "\"Of many faces; usually the ones you hate most.\"",
                Description = "This party member deals 100% more damage to Friendly targets.\nOn receiving damage, summon a friendly Infinity Tasbeht.",
                ShopPrice = 8,
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                DoesPopUpInfo = false,
                DefaultPercentageToModify = 0,
                EquippedModifiers = [dartboardDeploymentWearable],
                DefaultDoesIncreaseDamage = false,
                UnitTypeData = new UnitTypePercMod[] { unitTypePercMod },
            };

            //unlock this
            string achievementID = "SorasToybox_Senek_Antagonist_ACH";
            string unlockID = "SorasToybox_Senek_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(dartboard.item, new ItemModdedUnlockInfo(dartboard.Item_ID, ResourceLoader.LoadSprite("item_dartboard_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, dartboard.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [dartboard.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Senek_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(dartboard.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Senek", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Dartboard.");
            }

        }
    }
}
