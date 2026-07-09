using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class LaughingGas
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect ecstasyByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            ecstasyByPrevious._MultPreviousExitValueForEntry = true;
            ecstasyByPrevious._Status = StatusField.GetCustomStatusEffect("Ecstasy_ID");

            StatusEffect_Apply_Effect scarMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            scarMe._Status = StatusField.Scars;

            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();
            damage._indirect = true;
            damage._returnKillAsSuccess = false;

            RefreshAbilityUseEffect refresh = ScriptableObject.CreateInstance<RefreshAbilityUseEffect>();

            Ability coughingFit = new Ability("Coughing Fit", "ST_CoughingFit_A")
            {
                Cost = [Pigments.Yellow],
                Description = "Gain 1 Scar.\nDeal 1 indirect damage to this party member.\nGain Ecstasy equal to the damage dealt.\nRefresh ability usage.",
                AbilitySprite = ResourceLoader.LoadSprite("n2ocan_coughingfit"),
                AnimationTarget = Targeting.Slot_SelfAll,
                Visuals = Visuals.Melt,
                Effects =
                [
                    Effects.GenerateEffect(scarMe, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ecstasyByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(refresh, 1, Targeting.Slot_SelfSlot),
                ],
            };
            coughingFit.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Damage_1_2), "Status_Ecstasy", nameof(IntentType_GameIDs.Other_Refresh)]);

            ExtraAbility_Wearable_SMS laughingGasWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            laughingGasWearable._extraAbility = coughingFit.GenerateCharacterAbility();

            //flavor rando attempt
            String flavorText = "";
            if (UnityEngine.Random.Range((float)0.0, (float)1.0) > 0.5)
            {
                if (UnityEngine.Random.Range((float)0.0, (float)1.0) > 0.5)
                {
                    flavorText = "\"Melt our tongues and become unglued.\"";
                }
                else
                {
                    flavorText = "\"All that haunts me is surely closure.\"";
                }
            }
            else
            {
                if (UnityEngine.Random.Range((float)0.0, (float)1.0) > 0.5)
                {
                    flavorText = "\"Carry me far into the tide.\"";
                }
                else
                {
                    flavorText = "\"Laugh as they go up in smoke.\"";
                }
            }


            PerformEffect_Item laughingGas = new PerformEffect_Item("ST_LaughingGas_ID", null, false)
            {
                Item_ID = "NitrousOxideCanister_SW",
                Name = "N2O Canister",
                Flavour = flavorText,
                Description = "Gain \"Coughing Fit\" as an extra ability, hurting yourself in exchange for immediate power.",
                Icon = ResourceLoader.LoadSprite("item_laughinggas"),
                TriggerOn = TriggerCalls.Count,
                EquippedModifiers = [laughingGasWearable],
                OnUnlockUsesTHE = true,
                IsShopItem = true,
                ShopPrice = 8,
                StartsLocked = true,
            };



            //Construct shenanigans i think
            Connection_PerformEffectPassiveAbility connection_PerformEffectPassiveAbility = LoadedAssetsHandler.GetCharacter("Doll_CH").passiveAbilities[0] as Connection_PerformEffectPassiveAbility;
            CasterAddRandomExtraAbilityEffect casterAddRandomExtraAbilityEffect = connection_PerformEffectPassiveAbility.connectionEffects[1].effect as CasterAddRandomExtraAbilityEffect;
            casterAddRandomExtraAbilityEffect._extraData = [.. casterAddRandomExtraAbilityEffect._extraData, laughingGasWearable];

            //unlock this
            string achievementID = "SorasToybox_Thype_Antagonist_ACH";
            string unlockID = "SorasToybox_Thype_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(laughingGas.item, new ItemModdedUnlockInfo(laughingGas.Item_ID, ResourceLoader.LoadSprite("item_laughinggas_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, laughingGas.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [laughingGas.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Thype", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("N2O Canister", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Thype", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Thype_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the N2O Canister.");
            }
        }
    }
}
