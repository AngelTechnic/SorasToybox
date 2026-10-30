using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class WeatherBalloon
    {
        public static void Add()
        {
            FieldEffectCheckEffect iDoBelieveIAmOnFire = ScriptableObject.CreateInstance<FieldEffectCheckEffect>();
            iDoBelieveIAmOnFire._fields = [StatusField.OnFire];

            ContainsFieldEffectEffectorCondition fireCheckConditionIfItWorks = ScriptableObject.CreateInstance<ContainsFieldEffectEffectorCondition>();
            fireCheckConditionIfItWorks.fieldEffectCheck = StatusField.OnFire;
            
            ExtraPassiveAbility_Wearable_SMS infernoWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            infernoWearable._extraPassiveAbility = Passives.Inferno;

            StatusEffect_Apply_Effect gimmeCelerity = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            gimmeCelerity._Status = StatusField.GetCustomStatusEffect("Celerity_ID");

            AnimationVisualsEffect theBurning = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            theBurning._visuals = Visuals.Torched;
            theBurning._animationTarget = Targeting.Slot_SelfAll;

            PerformEffect_Item weatherBalloon = new("ST_WeatherBalloon_ID")
            {
                Item_ID = "WeatherBalloon_TW",
                Name = "Weather Balloon",
                Flavour = "\"Mission Control, do you read me? I'm coming home..!\"",
                Description = "Grant this party member Inferno as a passive.\nAt the end of your turn, if standing in fire, gain 2 Celerity.",
                ShopPrice = 22,
                IsShopItem = false,
                IsEffectImmediate = true,
                DoesPopUpInfo = true,
                EquippedModifiers = [infernoWearable],
                Conditions = [fireCheckConditionIfItWorks],
                TriggerOn = TriggerCalls.OnTurnFinished,
                Effects =
                [
                    Effects.GenerateEffect(theBurning, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(gimmeCelerity, 2, Targeting.Slot_SelfSlot),
                ],
                StartsLocked = true,
                OnUnlockUsesTHE = true,
                Icon = ResourceLoader.LoadSprite("item_weatherballoon"),
            };

            //Unlock this
            string achievementID = "SorasToybox_Wuz_Antagonist_ACH";
            string unlockID = "SorasToybox_Wuz_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(weatherBalloon.item, new ItemModdedUnlockInfo(weatherBalloon.Item_ID, ResourceLoader.LoadSprite("item_weatherballoon_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, weatherBalloon.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [weatherBalloon.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Wuz_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(weatherBalloon.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Wuz", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Wuz_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Weather Balloon.");
            }

        }

    }
}
