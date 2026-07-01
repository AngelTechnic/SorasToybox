using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class CharybdisItem
    {
        public static void Add()
        {
            FieldEffect_Apply_Effect absolutionByPrevious = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            absolutionByPrevious._Field = StatusField.GetCustomFieldEffect("Absolution_ID");
            absolutionByPrevious._UsePreviousExitValueAsMultiplier = true;

            DamageEffect reaper = ScriptableObject.CreateInstance<DamageEffect>();
            reaper._ignoreShield = true;

            Ability spiritReaper = new Ability("Spirit Reaper", "ST_SpiritReaper_A")
            {
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red, Pigments.Purple],
                Description = "Deal 5 shield-piercing damage to the Left, Opposing, and Right enemies. Apply Absolution equal to the damage dealt to the Far Left and Far Right enemy spaces, assuming the grid wraps around.",
                AbilitySprite = ResourceLoader.LoadSprite("charybdis_spiritreaper"),
                AnimationTarget = Targeting.Slot_FrontAndSides,
                Visuals = Visuals.Slash,
                Effects =
                [
                    Effects.GenerateEffect(reaper, 5, Targeting.Slot_FrontAndSides),
                    Effects.GenerateEffect(absolutionByPrevious, 1, Targeting.GenerateSlotTarget([-3, -2, 2, 3], false)),
                ]
            };
            spiritReaper.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
            spiritReaper.AddIntentsToTarget(Targeting.GenerateSlotTarget([-3, -2, 2, 3], false), ["Field_Absolution"]);

            ExtraAbility_Wearable_SMS charybdisWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            charybdisWearable._extraAbility = spiritReaper.GenerateCharacterAbility();

            PerformEffect_Item charlie = new PerformEffect_Item("ST_Charybdis_ID", null, false)
            {
                Item_ID = "Charybdis_TW",
                Name = "Charybdis",
                Flavour = "\"SQUAAAWK!\"",
                Description = "Adds the ability \"Spirit Reaper\" to this party member, a wide-hitting area attack with space-warping capability.",
                TriggerOn = TriggerCalls.Count,
                EquippedModifiers = [charybdisWearable],
                StartsLocked = true,
                ShopPrice = 316,
                IsShopItem = false,
                Icon = ResourceLoader.LoadSprite("item_charybdis"),
            };

            charlie.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Magic.ToString(),
            ];

            //Construct shenanigans i think
            Connection_PerformEffectPassiveAbility connection_PerformEffectPassiveAbility = LoadedAssetsHandler.GetCharacter("Doll_CH").passiveAbilities[0] as Connection_PerformEffectPassiveAbility;
            CasterAddRandomExtraAbilityEffect casterAddRandomExtraAbilityEffect = connection_PerformEffectPassiveAbility.connectionEffects[1].effect as CasterAddRandomExtraAbilityEffect;
            casterAddRandomExtraAbilityEffect._extraData = [.. casterAddRandomExtraAbilityEffect._extraData, charybdisWearable];

            //Unlock this
            string achievementID = "SorasToybox_Karma_Forgotten_ACH";
            string unlockID = "SorasToybox_Karma_Forgotten_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(charlie.item, new ItemModdedUnlockInfo(charlie.Item_ID, ResourceLoader.LoadSprite("item_charybdis_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, charlie.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [charlie.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Karma_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Charybdis", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Nobody_Karma", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
