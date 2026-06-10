using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class MegaHammer
    {
        public static void Add()
        {
            DamageEffect hammerIndirectDamage = ScriptableObject.CreateInstance<DamageEffect>();
            hammerIndirectDamage._indirect = true;

            FieldEffectCheckEffect getShield = ScriptableObject.CreateInstance<FieldEffectCheckEffect>();
            getShield._fields = [StatusField.Shield];

            ExtraPassiveAbility_Wearable_SMS backlash = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            backlash._extraPassiveAbility = Passives.GetCustomPassive("Backlash_PA");

            AnimationVisualsEffect hammerVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            hammerVis._visuals = Visuals.Crush;
            hammerVis._animationTarget = Targeting.Slot_Front;

            ExhaustMovementEffect thisThingIsHeavyOKCutMeSomeSlack = ScriptableObject.CreateInstance<ExhaustMovementEffect>();


            DoublePerformEffect_Item mcHammer = new DoublePerformEffect_Item("ST_MegaHammer_ID", null, false)
            {
                Item_ID = "MegaHammer_TW",
                Name = "Mega Hammer",
                Icon = ResourceLoader.LoadSprite("item_megahammer"),
                Flavour = "\"The fact that you have this means I hate you.\"",
                ShopPrice = 674,
                Description = "Gain Backlash as a passive.\nOn turn start, exhaust movement.\nOn performing an ability, if this party member is standing in Shield, deal 10 bonus indirect damage to the Opposing position.",
                IsShopItem = false,
                DoesPopUpInfo = true,
                StartsLocked = true,
                EquippedModifiers = [backlash],
                TriggerOn = TriggerCalls.OnAbilityUsed,
                Effects =
                [
                    Effects.GenerateEffect(getShield, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(hammerVis, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getShield, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(hammerIndirectDamage, 10, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                ],

                SecondaryTriggerOn = [TriggerCalls.OnTurnStart],
                SecondaryEffects = [
                    Effects.GenerateEffect(thisThingIsHeavyOKCutMeSomeSlack, 1, Targeting.Slot_SelfSlot),
                    ],
                SecondaryDoesPopUpInfo = false,
                OnUnlockUsesTHE = true,

            };

            //unlock this
            string achievementID = "SorasToybox_Karma_Dreamer_ACH";
            string unlockID = "SorasToybox_Karma_Dreamer_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(mcHammer.item, new ItemModdedUnlockInfo(mcHammer.Item_ID, ResourceLoader.LoadSprite("item_megahammer_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, mcHammer.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [mcHammer.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("BlueSky_BOSS", ResourceLoader.LoadSprite("BlueSkyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Karma_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Mega Hammer", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_BlueSkies_Karma", null, 32, null), achievementID);
            unlockAchievement.IsSecret = true;
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BlueSky_BOSS", "The Dreamer");
        }
    }
}
