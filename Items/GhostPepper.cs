using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements.Experimental;

namespace SorasToybox.Items
{
    public class GhostPepper
    {
        public static void Add()
        {
            AddPassiveEffect vegansAreBanned = ScriptableObject.CreateInstance<AddPassiveEffect>();
            vegansAreBanned._passiveToAdd = Passives.GetCustomPassive("Carnivorous_PA");

            DamageEffect eatMe = ScriptableObject.CreateInstance<DamageEffect>();
            
            AnimationVisualsEffect devourVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            devourVis._animationTarget = Targeting.Slot_SelfSlot;
            devourVis._visuals = Visuals.Devour;

            TargetPerformEffectViaSubaction leftBite = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            leftBite.effects =
                [
                    Effects.GenerateEffect(eatMe, 2, Targeting.Slot_AllyRight)
                ];

            TargetPerformEffectViaSubaction rightBite = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            rightBite.effects =
                [
                    Effects.GenerateEffect(eatMe, 2, Targeting.Slot_AllyLeft)
                ];


            DoublePerformEffect_Item ghostPepper = new DoublePerformEffect_Item("ST_GhostPepper_ID", null, false)
            {
                Item_ID = "ST_GhostPepper_SW",
                Name = "Ghost Pepper",
                Flavour = "\"MAKE HIM REGRET BEING BORN.\"",
                Description = "On combat start, apply Carnivorous as a passive to all party members. On round end, make any neighboring party members deal 2 damage to this party member.",
                Icon = ResourceLoader.LoadSprite("item_ghostpepper"),
                IsShopItem = true,
                ShopPrice = 5,
                DoesPopUpInfo = true,
                StartsLocked = true,
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(vegansAreBanned, 1, Targeting.Unit_AllAllies),
                ],
                SecondaryDoesPopUpInfo = true,
                SecondaryTriggerOn = [TriggerCalls.OnRoundFinished],
                SecondaryEffects =
                [
                    Effects.GenerateEffect(devourVis, 1, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(leftBite, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(rightBite, 1, Targeting.Slot_AllyRight),
                ],
                OnUnlockUsesTHE = true,
            };

            ghostPepper.item._ItemTypeIDs =
            [
                "FoodID",
            ];

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(ghostPepper.item, new ItemModdedUnlockInfo(ghostPepper.Item_ID, ResourceLoader.LoadSprite("item_ghostpepper_locked", null, 32, null), "SorasToybox_Karma_Inevitable_ACH"));
            //unlock this
            string achievementID = "SorasToybox_Karma_Inevitable_ACH";
            string unlockID = "SorasToybox_Karma_Inevitable_Unlock";



            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, ghostPepper.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [ghostPepper.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Karma_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Ghost Pepper", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_March_Karma", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");
        }
    }
}
