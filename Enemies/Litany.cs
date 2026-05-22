using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;


//hey if yuou're seeing this make sure you change what's in the "using" list i'm gonna explodeeeeeee
namespace SorasToybox.Enemies
{
    public class Litany
    {
        public static void Add()
        {
            Enemy litany = new Enemy("Litany", "Litany_EN")
            {
                Health = 60,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineLitany.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineLitany.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                DamageSound = LoadedAssetsHandler.GetEnemy("Kookoo_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Kookoo_EN").deathSound,
            };

            //PropX damage teehee
            ProportionalCurHealthDamageEffect die = ScriptableObject.CreateInstance<ProportionalCurHealthDamageEffect>();
            die._returnKillAsSuccess = true;

            //infantile add
            GoofyAssLitanyCoerceTargeting cliqueAdopter = ScriptableObject.CreateInstance<GoofyAssLitanyCoerceTargeting>();
            cliqueAdopter._passiveToAdd = Passives.Infantile;

            //infantile popup. might need to revisit this idk.
            PassivePopUpOnTargetEffect imABaby = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            imABaby._name = "Infantile";
            imABaby._sprite = "Passive_Infantile";

            //Divine Protection
            StatusEffect_Apply_Effect iKindaFeelAwkwardAroundYouNGL = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            iKindaFeelAwkwardAroundYouNGL._Status = StatusField.DivineProtection;




            //Stored Values! dies.
            UnitStoreData_ModIntSO otheringCounter = ScriptableObject.CreateInstance<UnitStoreData_ModIntSO>();
            otheringCounter.m_Text = "Othering: {0}";
            otheringCounter._UnitStoreDataID = "OtheringStoredValue";

            CasterStoredValueChangeEffect otheringGoUp = ScriptableObject.CreateInstance<CasterStoredValueChangeEffect>();
            otheringGoUp.m_unitStoredDataID = "OtheringStoredValue";
            otheringGoUp._minimumValue = 0;
            otheringGoUp._increase = true;

            CasterStoredValueSetEffect otheringReset = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            otheringReset._valueName = "OtheringStoredValue";

            PreviousComparatorCheckEffect ThreePlus = ScriptableObject.CreateInstance<PreviousComparatorCheckEffect>();
            ThreePlus._atOrAbove = true;
            ThreePlus._entryIsComparator = false;
            ThreePlus._fixedComparator = 3;

            ExtraVariableForNext_SVEffect otheringGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            otheringGet.m_unitStoredDataID = "OtheringStoredValue";

            Ability litanyCoerceAbility = new Ability("Coerce", "LitanyCoerce_A")
            {
                Description = "Applies Infantile as a passive to the highest health enemy or enemies without it.\n(Once I figure this out) Resets \"Othering\" counter to 0.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Providence,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(cliqueAdopter, 1, Targeting.Unit_OtherAllies),
                    Effects.GenerateEffect(otheringReset, 0, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.VeryFast,
            };
            litanyCoerceAbility.AddIntentsToTarget(Targeting.Unit_OtherAllies, ["Passive_Infantile"]);
            litanyCoerceAbility.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            AttackVisualsSO headhuntervisuals;
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                headhuntervisuals = LoadedAssetsHandler.GetEnemyAbility("SlaughterOne_A").visuals;
            }
            else
            {
                headhuntervisuals = Visuals.SliceAndDice;
            }
            //Litany Headhunter ability
            Ability litanyHeadhunterAbility = new Ability("Headhunter", "LitanyHeadhunter_A")
            {
                Description = "Deals damage to the opposing party member equal to their current health.\n\"why did you listen\"",
                Cost = [Pigments.Purple],
                Visuals = headhuntervisuals,
                AnimationTarget = Targeting.Slot_Front,
                Rarity = Rarity.Impossible,
                Effects =
                [
                    Effects.GenerateEffect(die, 100, Targeting.Slot_Front),
                ],
            };
            litanyHeadhunterAbility.AddIntentsToTarget(Targeting.Slot_Front, ["Damage_PropEx"]);

            UseSpecificAbilityByEntryEffect imComingForYourHead = ScriptableObject.CreateInstance<UseSpecificAbilityByEntryEffect>();
            imComingForYourHead.usePrev = false;




            Ability litanyOtheringAbility = new Ability("Othering", "LitanyOthering_A")
            {
                Description = "Applies 1 Divine Protection to the Opposing party member.\nIf there isn't an Opposing party member, moves Left or Right\nQueues up \"Headhunter\" because I don't know how to do stored values yet.",
                Cost = [Pigments.Yellow],
                Rarity = Rarity.Impossible,
                Priority = Priority.VerySlow,
                Effects =
                [
                    Effects.GenerateEffect(iKindaFeelAwkwardAroundYouNGL, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(imComingForYourHead, 1, Targeting.Slot_SelfSlot)

                ],
            };
            litanyOtheringAbility.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_DivineProtection)]);
            litanyOtheringAbility.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            ExtraAbilityInfo headhunterQueue = new()
            {
                ability = litanyHeadhunterAbility.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };
            imComingForYourHead._parentalAbility = headhunterQueue;

            ExtraAbilityInfo otheringParental = new()
            {
                ability = litanyOtheringAbility.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };

            litany.AddPassives(
                [
                    Passives.ParentalGenerator(otheringParental), Passives.OverexertGenerator(9),
                ]);

            litany.AddEnemyAbilities(
                [
                    litanyCoerceAbility, litanyHeadhunterAbility,
                ]);
            litany.AddEnemy(true, false, false);
            Debug.Log("Litany kinda added? It's very incomplete tho");
            LoadedAssetsHandler.GetEnemy("Litany_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Kookoo_EN").enemyTemplate;

        }
    }
}
