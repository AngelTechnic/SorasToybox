using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class HollowedFang
    {
        public static void Add()
        {
            
            CheckIsThereOverflowEffectorCondition isThereNoOverflow = ScriptableObject.CreateInstance<CheckIsThereOverflowEffectorCondition>();
            isThereNoOverflow._isOver = false;
            isThereNoOverflow._overflowAmount = 1;

            //BEHOLD THE WORLD'S MOST UNNECESSARILY LONG EFFECT NAME
            ShatterBrokenPigmentEffect youMightThinkHeLovesYouForYourMoneyButIKnowWhatHeReallyLovesYouForItsYourBrandNewLeopardSkinPillboxHat = ScriptableObject.CreateInstance<ShatterBrokenPigmentEffect>();

            StatusEffect_Apply_Effect anteByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteByPrevious._Status = StatusField.GetCustomStatusEffect("Ante_ID");
            anteByPrevious._MultPreviousExitValueForEntry = true;

            AnimationVisualsEffect doTheKnifeThing = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            doTheKnifeThing._visuals = Visuals.Quills;
            doTheKnifeThing._animationTarget = Targeting.Slot_Front;

            AddPassiveEffect getBrokenBlooded = ScriptableObject.CreateInstance<AddPassiveEffect>();
            getBrokenBlooded._passiveToAdd = Passives.GetCustomPassive("BrokenBlooded_1_PA");

            PassivePopUpOnTargetEffect brokenBloodedPopUp = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            brokenBloodedPopUp._sprite = "IconStonebloodBroken";
            brokenBloodedPopUp._name = "Broken-Blooded (1)";

            TargetPerformEffectViaSubaction hollowedFangSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            hollowedFangSubAct.effects =
                [
                    Effects.GenerateEffect(getBrokenBlooded, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(brokenBloodedPopUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ];


            DoublePerformEffect_Item hollowedFang = new("ST_HollowedFang_ID")
            {
                Item_ID = "HollowedFang_TW",
                Icon = ResourceLoader.LoadSprite("item_hollowedfang"),
                Name = "Hollowed Fang",
                Flavour = "\"A little bit of bloodplay never hurt anyone!\"",
                Description = "Give all enemies Broken-Blooded as an extra passive at the start of the fight.\nAt the end of your turn, if there is NO overflow, shatter all Broken pigment and gain equal Ante.",
                IsShopItem = false,
                StartsLocked = true,
                DoesPopUpInfo = true,
                OnUnlockUsesTHE = true,
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(hollowedFangSubAct, 1, Targeting.Unit_AllOpponents)
                ],
                SecondaryConditions = [isThereNoOverflow],
                SecondaryTriggerOn = [TriggerCalls.OnTurnFinished],
                SecondaryEffects =
                [
                    Effects.GenerateEffect(doTheKnifeThing, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(youMightThinkHeLovesYouForYourMoneyButIKnowWhatHeReallyLovesYouForItsYourBrandNewLeopardSkinPillboxHat, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteByPrevious, 1, Targeting.Slot_SelfSlot),
                    ]
            };

            hollowedFang.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Knife.ToString(),
            ];


            //Unlock this
            string achievementID = "SorasToybox_Xichlasa_Antagonist_ACH";
            string unlockID = "SorasToybox_Xichlasa_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(hollowedFang.item, new ItemModdedUnlockInfo(hollowedFang.Item_ID, ResourceLoader.LoadSprite("item_hollowedfang_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, hollowedFang.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [hollowedFang.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Xichlasa_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(hollowedFang.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Xichlasa", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Xichlasa_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Hollowed Fang.");
            }
        }
    }
}
