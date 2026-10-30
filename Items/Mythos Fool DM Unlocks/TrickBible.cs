using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class TrickBible
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS seepingWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            seepingWearable._extraPassiveAbility = Passives.GetCustomPassive("Seeping_PA");

            AddPassiveEffect gimmeSeeping = ScriptableObject.CreateInstance<AddPassiveEffect>();
            gimmeSeeping._passiveToAdd = Passives.GetCustomPassive("Seeping_PA");

            RemoveStatusEffectEffect noMadness = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noMadness._status = StatusField.GetCustomStatusEffect("Madness_ID");

            StatusEffect_Apply_Effect regenByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            regenByPrevious._Status = StatusField.GetCustomStatusEffect("Regen_ID");
            regenByPrevious._MultPreviousExitValueForEntry = true;

            StatusEffect_Apply_Effect doMadness = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            regenByPrevious._Status = StatusField.GetCustomStatusEffect("Madness_ID");


            StatusEffect_ApplyByHalfMissingHealth_Effect madnessByMissingHealth = ScriptableObject.CreateInstance<StatusEffect_ApplyByHalfMissingHealth_Effect>();
            madnessByMissingHealth._Status = StatusField.GetCustomStatusEffect("Madness_ID");


            TargetPerformEffectViaSubaction fingerTrickSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            fingerTrickSubAct.effects =
                [
                    Effects.GenerateEffect(noMadness, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(regenByPrevious, 2, Targeting.Slot_Front),
                ];
            TargetPerformEffectViaSubaction fingerTrickSecondSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            fingerTrickSecondSubAct.effects =
                [
                Effects.GenerateEffect(doMadness, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(madnessByMissingHealth, 1, Targeting.Slot_Front),
                ];

            Ability theFingerTrick = new("ST_TheFingerTrick_A")
            {
                Name = "The Finger Trick",
                Description = "Make all enemies with Madness exchange it all for twice as much Regeneration to their Opponents.\nLeave the enemy in awe at your brilliance.",
                Cost = [Pigments.BluePurple, Pigments.BluePurple, Pigments.BluePurple],
                AnimationTarget = Targeting.Unit_AllOpponents,
                Visuals = Visuals.FingerGuns,
                AbilitySprite = ResourceLoader.LoadSprite("trickbible_fingertrick"),
                Effects =
                [
                    Effects.GenerateEffect(gimmeSeeping, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(fingerTrickSubAct, 1, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(fingerTrickSecondSubAct, 1, Targeting.Slot_SelfSlot),

                ],
            };
            theFingerTrick.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Rem_Status_Madness", "Status_Regeneration"]);
            theFingerTrick.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Madness"]);

            ExtraAbility_Wearable_SMS fingerTrickWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            fingerTrickWearable._extraAbility = theFingerTrick.GenerateCharacterAbility();

            PerformEffect_Item trickBible = new("ST_TrickBible_ST")
            {
                Item_ID = "TrickBible_SW",
                IsShopItem = true,
                ShopPrice = 10,
                Icon = ResourceLoader.LoadSprite("item_trickbible"),
                Name = "Trick Bible",
                Flavour = "\"If you knew what I knew, you'd lose your frickin' mind.\"",
                Description = "Gain Seeping as a passive and \"The Finger Trick\" as an extra ability, allowing you to drive enemies mad in exchange for your own (mental) health.",
                EquippedModifiers = [seepingWearable, fingerTrickWearable],
                StartsLocked = true,
                TriggerOn = TriggerCalls.Count,
                OnUnlockUsesTHE = true,
            };

            //construct shit
            Connection_PerformEffectPassiveAbility connection_PerformEffectPassiveAbility = LoadedAssetsHandler.GetCharacter("Doll_CH").passiveAbilities[0] as Connection_PerformEffectPassiveAbility;
            CasterAddRandomExtraAbilityEffect casterAddRandomExtraAbilityEffect = connection_PerformEffectPassiveAbility.connectionEffects[1].effect as CasterAddRandomExtraAbilityEffect;
            casterAddRandomExtraAbilityEffect._extraData = [.. casterAddRandomExtraAbilityEffect._extraData, fingerTrickWearable];

            trickBible.item._ItemTypeIDs =
                [ItemType_GameIDs.Magic.ToString()];

            //Unlock this
            string achievementID = "SorasToybox_Isabelle_Antagonist_ACH";
            string unlockID = "SorasToybox_Isabelle_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(trickBible.item, new ItemModdedUnlockInfo(trickBible.Item_ID, ResourceLoader.LoadSprite("item_trickbible_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, trickBible.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [trickBible.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Isabelle_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(trickBible.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Isabelle", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Isabelle_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Trick Bible.");
            }
        }
    }
}
