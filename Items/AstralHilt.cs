using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Yarn.Analysis;

namespace SorasToybox.Items
{
    public class AstralHilt
    {
        public static void Add()
        {
            UnitTypePercMod unitTypePercMod = new UnitTypePercMod();
            unitTypePercMod.percentageToModify = 100;
            unitTypePercMod.doesIncrease = true;
            unitTypePercMod.unitType = "Zoincaillan";


            //copied directly from dune thresher
            SpawnEnemyAnywhereEffect suckMyClock = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            suckMyClock.enemy = LoadedAssetsHandler.GetEnemy("GearYinimroSummon_EN");
            suckMyClock._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            suckMyClock.givesExperience = false;






            PerformEffectPassiveAbility griyadinCall = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            griyadinCall.name = "GriyadinsCall_PA";
            griyadinCall.m_PassiveID = "GriyadinsCall";
            griyadinCall._passiveName = "Griyadin's Call";
            griyadinCall.passiveIcon = ResourceLoader.LoadSprite("passive_griyadin");
            griyadinCall._characterDescription = "When this party member deals damage, there is a 10% chance to summon a Gear Yinimro.\nIt will not be friendly.";
            griyadinCall.doesPassiveTriggerInformationPanel = false;
            griyadinCall._triggerOn = [TriggerCalls.OnDidApplyDamage];

            //Griyadin's Call popup
            PassivePopUpOnTargetEffect callPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            callPopup._name = griyadinCall._passiveName;
            callPopup._sprite = "passive_griyadin";
            callPopup._isUnitCharacter = true;

            //actually defining the effects now
            griyadinCall.effects =
            [
                Effects.GenerateEffect(callPopup, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(10)),
                Effects.GenerateEffect(suckMyClock, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
            ];
            Passives.AddCustomPassiveToPool(griyadinCall.name, griyadinCall._passiveName, griyadinCall);

            ExtraPassiveAbility_Wearable_SMS griyadinCallWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            griyadinCallWearable._extraPassiveAbility = griyadinCall;

            DamageDealtPercentageModifierByUnitType_Item astralHilt = new DamageDealtPercentageModifierByUnitType_Item("ST_AstralHilt_ID")
            {
                Item_ID = "AstralHilt_TW",
                Name = "Astral Hilt",
                Flavour = "\"Meet Potential Blade!\"",
                Description = "This party member deals 20% more damage.\nThis party member instead deals 100% more damage if their target hails from the realm of two suns.\nYou may need to use this bonus.",
                IsShopItem = true,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_astralhilt"),
                EquippedModifiers = [griyadinCallWearable],
                TriggerOn = TriggerCalls.OnWillApplyDamage,
                OnUnlockUsesTHE = true,
                DefaultDoesIncreaseDamage = true,
                DefaultPercentageToModify = 20,
                UnitTypeData = new UnitTypePercMod[] { unitTypePercMod },
            };
            astralHilt.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Knife.ToString()
            ];

            //unlock this
            string achievementID = "SorasToybox_Whhvay_Witness_ACH";
            string unlockID = "SorasToybox_Whhvay_Witness_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(astralHilt.item, new ItemModdedUnlockInfo(astralHilt.Item_ID, ResourceLoader.LoadSprite("item_astralhilt_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, astralHilt.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [astralHilt.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(astralHilt.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Osman_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Astral Hilt.");
            }

        }
    }
}
