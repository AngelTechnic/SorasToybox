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
    public class FortyProof
    {
        public static void Add()
        {
            CasterTransformationEffect gopnikTF = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            gopnikTF._fullyHeal = false;
            gopnikTF._maintainMaxHealth = true;
            gopnikTF._currentToMaxHealth = false;
            gopnikTF._enemyTransformation = LoadedAssetsHandler.GetEnemy("GigglingUsurper_EN");

            //sound events maybe?
            PlayCustomSoundEffect cykaBlyat = ScriptableObject.CreateInstance<PlayCustomSoundEffect>();
            cykaBlyat._Sound = "event:/SorasSFX/Enemies/Misc/CykaBlyat";

            StatusEffect_Apply_Effect getTired = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getTired._Status = StatusField.GetCustomStatusEffect("Tired_ID");

            SpecificEnemiesTargeting giggles = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            giggles._enemies = ["GigglingMinister_EN"];
            giggles.slotOffsets = [0];
            giggles.targetUnitAllySlots = true;
            giggles.getAllUnitSelfSlots = true;

            AnimationVisualsEffect getDrunkVisuals =  ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            getDrunkVisuals._visuals = Visuals.Relapse;
            getDrunkVisuals._animationTarget = Targeting.Slot_SelfSlot;

            RandomTargetPerformEffectViaSubaction birdGetsDrunkAndGetsIntoBarFight = ScriptableObject.CreateInstance<RandomTargetPerformEffectViaSubaction>();
            birdGetsDrunkAndGetsIntoBarFight.effects =
                [
                    Effects.GenerateEffect(getDrunkVisuals),
                    Effects.GenerateEffect(gopnikTF),
                    Effects.GenerateEffect(cykaBlyat),
                ];

            AnimationVisualsEffect barFight = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            barFight._visuals = Visuals.BarFight;
            barFight._animationTarget = Targeting.Slot_Front;


            ConsumeItemEffect theyDrankTheWholeThing = ScriptableObject.CreateInstance<ConsumeItemEffect>();


            ExtraAbility_Wearable_SMS bottleWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();

            CasterAddOrRemoveExtraAbilityEffect bottleAdd = ScriptableObject.CreateInstance<CasterAddOrRemoveExtraAbilityEffect>();
            bottleAdd._extraAbility = bottleWearable;
            bottleAdd._removeExtraAbility = false;

            CasterAddOrRemoveExtraAbilityEffect bottleRemove = ScriptableObject.CreateInstance<CasterAddOrRemoveExtraAbilityEffect>();
            bottleRemove._extraAbility = bottleWearable;
            bottleRemove._removeExtraAbility = true;

            Ability bottleToss = new Ability("Bottle Toss", "ST_BottleToss_A")
            {
                Description = "Inflicts 2 Tired on the Opposing enemy.",
                Cost = [Pigments.Purple, Pigments.Purple],
                AbilitySprite = ResourceLoader.LoadSprite("fortyproof_bottletoss"),
                Effects =
                [
                    Effects.GenerateEffect(birdGetsDrunkAndGetsIntoBarFight, 1, giggles),
                    Effects.GenerateEffect(bottleRemove, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(barFight, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(getTired, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 3)),
                    
                ],
            };
            bottleToss.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Tired"]);

            bottleWearable._extraAbility = bottleToss.GenerateCharacterAbility(true);

            PerformEffect_Item fortyProof = new PerformEffect_Item("FortyProof_ID", null, false)
            {
                Item_ID = "FortyProof_SW",
                Name = "Forty-Proof",
                Flavour = "\"I *really* need that 40.\"",
                Description = "Adds \"Bottle Toss\" to this party member, allowing them to make an opponent get wasted.",
                IsShopItem = true, 
                DoesPopUpInfo = false,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_fortyproof"),
                ShopPrice = 10,
                TriggerOn = TriggerCalls.OnCombatStart,
                OnUnlockUsesTHE = false,
                Effects =
                [
                    Effects.GenerateEffect(bottleAdd, 1, Targeting.Slot_SelfSlot),
                ],
            };

            fortyProof.item._ItemTypeIDs =
            [
                "FoodID",
                "Drink",
            ];


            ItemUtils.AddItemToShopStatsCategoryAndGamePool(fortyProof.item, new ItemModdedUnlockInfo(fortyProof.Item_ID, ResourceLoader.LoadSprite("item_fortyproof_locked", null, 32, null), "SorasToybox_Mercurie_Abstraction_ACH"));

            //unlock this
            string achievementID = "SorasToybox_Mercurie_Abstraction_ACH";
            string unlockID = "SorasToybox_Mercurie_Abstraction_Unlock";



            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, fortyProof.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [fortyProof.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Forty-Proof", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Doula_Mercurie", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Forty-Proof.");
            }
        }
    }
}
