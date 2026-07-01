using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class PrincessItem
    {
        public static void Add()
        {
            AnimationVisualsEffect chompVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            chompVisuals._visuals = Visuals.Chomp;
            chompVisuals._animationTarget = Targeting.Slot_Front;

            AnimationVisualsEffect bellowVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            bellowVisuals._visuals = Visuals.Bellow;
            bellowVisuals._animationTarget = Targeting.Slot_SelfSlot;



            CheckTargetAndCasterHealthEffect darwinism = ScriptableObject.CreateInstance<CheckTargetAndCasterHealthEffect>();
            darwinism._UseCasterCurrentHealth = true;
            darwinism._CheckUnderInstead = true;

            ProportionalCurHealthDamageEffect yummers = ScriptableObject.CreateInstance<ProportionalCurHealthDamageEffect>();


            StatusEffect_Apply_Effect atrophyByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            atrophyByPrevious._MultPreviousExitValueForEntry = true;
            atrophyByPrevious._Status = StatusField.GetCustomStatusEffect("Atrophy_ID");

            Ability feedingTime = new Ability("Feeding Time", "ST_FeedingTime_A")
            {
                Description = "The following only works if the Opposing enemy's health is equal to or lower than this party member's:\nDeal damage to the Opposing enemy equal to their current health. Inflict Atrophy equal to the damage dealt on this party member.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red, Pigments.Red, Pigments.Red],
                AbilitySprite = ResourceLoader.LoadSprite("princess_feedingtime"),
                Effects =
                [
                    Effects.GenerateEffect(darwinism, 100, Targeting.Slot_Front),//it calculates a percentage of the difference, so to get any value below your own you wanna input 100
                    Effects.GenerateEffect(chompVisuals, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(yummers, 100, Targeting.Slot_Front,  Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(atrophyByPrevious, 1, Targeting.Slot_SelfSlot,  Effects.CheckPreviousEffectCondition(true, 3)),
                    Effects.GenerateEffect(bellowVisuals, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 4)),
                ]
            };
            feedingTime.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), "Damage_PropEx"]);
            feedingTime.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Atrophy"]);

            ExtraAbility_Wearable_SMS princessWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            princessWearable._extraAbility = feedingTime.GenerateCharacterAbility();

            PerformEffect_Item princess = new PerformEffect_Item("ST_PrincessItem_ID")
            {
                Item_ID = "Princess_TW",
                Name = "Princess",
                Flavour = "\"Appetizer. MWAHAHAHAHAH!\"",
                Description = "Gain \"Feeding Time\" as an extra ability, allowing you to devour the Opposing enemy. It hurts, though!",
                EquippedModifiers = [princessWearable],
                StartsLocked = true,
                IsShopItem = false,
                TriggerOn = TriggerCalls.Count,
                ShopPrice = 316,
                Icon = ResourceLoader.LoadSprite("item_princess"),
            };

            princess.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Magic.ToString(),
            ];

            //Construct shenanigans i think
            Connection_PerformEffectPassiveAbility connection_PerformEffectPassiveAbility = LoadedAssetsHandler.GetCharacter("Doll_CH").passiveAbilities[0] as Connection_PerformEffectPassiveAbility;
            CasterAddRandomExtraAbilityEffect casterAddRandomExtraAbilityEffect = connection_PerformEffectPassiveAbility.connectionEffects[1].effect as CasterAddRandomExtraAbilityEffect;
            casterAddRandomExtraAbilityEffect._extraData = [.. casterAddRandomExtraAbilityEffect._extraData, princessWearable];

            //Unlock this
            string achievementID = "SorasToybox_Karma_Boundary_ACH";
            string unlockID = "SorasToybox_Karma_Boundary_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(princess.item, new ItemModdedUnlockInfo(princess.Item_ID, ResourceLoader.LoadSprite("item_princess_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, princess.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [princess.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Katalixi_BOSS", ResourceLoader.LoadSprite("KatalixiPearl", null, 32, null));
            unlockCheck.AddUnlockData("Karma_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Princess", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Katalixi_Karma", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BoundaryTitleLabel", "The Boundary");
        }
    }
}
