using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class UnlicensedTonic
    {
        public static void Add()
        {
            GenerateColorsByListManaEffect GiveRandomPigment = ScriptableObject.CreateInstance<GenerateColorsByListManaEffect>();
            GiveRandomPigment._manaColors = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue, Pigments.Yellow, Pigments.Yellow, Pigments.Purple];

            PerformEffectPassiveAbility randomBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            randomBlooded.name = "Random4Blooded_1_PA";
            randomBlooded._passiveName = "Random-Blooded (1)";
            randomBlooded.m_PassiveID = "PigmentBlooded";
            randomBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodPrimary");
            randomBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional pigment of the four basic colors.";
            randomBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional pigment of the four basic colors.";
            randomBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
            randomBlooded.doesPassiveTriggerInformationPanel = true;
            randomBlooded.effects =
            [
                Effects.GenerateEffect(GiveRandomPigment, 1, Targeting.Slot_SelfSlot),
            ];

            ExtraPassiveAbility_Wearable_SMS randomBloodedWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            randomBloodedWearable._extraPassiveAbility = randomBlooded;

            ExtraPassiveAbility_Wearable_SMS toyboxFormlessWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            toyboxFormlessWearable._extraPassiveAbility = Passives.GetCustomPassive("ST_Formless_PA");

            PerformEffect_Item unlicensedTonic = new("ST_Tonic_ID")
            {
                Item_ID = "UnlicensedTonic_SW",
                Name = "Unlicensed Tonic",
                Flavour = "\"I LIVE IN A LOW INCOME BARONY.\"",
                OnUnlockUsesTHE = true,
                Description = "This party member now has Random-Blooded (1) and... Formless??? as passives.",
                Icon = ResourceLoader.LoadSprite("item_tonic"),
                TriggerOn = TriggerCalls.Count,
                EquippedModifiers = [randomBloodedWearable, toyboxFormlessWearable],
                StartsLocked = true,
                ShopPrice = 5,
                IsShopItem = true,
            };


            //Unlock this
            string achievementID = "SorasToybox_Senek_Forgotten_ACH";
            string unlockID = "SorasToybox_Senek_Forgotten_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(unlicensedTonic.item, new ItemModdedUnlockInfo(unlicensedTonic.Item_ID, ResourceLoader.LoadSprite("item_tonic_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, unlicensedTonic.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [unlicensedTonic.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Senek_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(unlicensedTonic.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Nobody_Senek", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Unlicensed Tonic.");
            }
        }
    }
}
