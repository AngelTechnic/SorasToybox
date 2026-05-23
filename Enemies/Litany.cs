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
        //borrowing this whole method just for one fucking passive
        public static void CheckerPassivePoolAdd(string id, string name, BasePassiveAbilitySO passive)
        {
            if (!LoadedDBsHandler.PassiveDB._PassivesPool.Contains(id))
            {
                Debug.Log($"Passives | adding passive {name} ({id})");
                Passives.AddCustomPassiveToPool(id, name, passive);
            }
            else
            {
                Debug.Log($"Passives | passive with id {id} already registered! skipping...");
            }
        }
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
                DamageSound = "event:/SorasSFX/Enemies/Litany/LitanyHurt",
                DeathSound = "event:/SorasSFX/Enemies/Litany/LitanyDie",
            };

            litany.PrepareEnemyPrefab("Assets/ToyboxEnemies/Litany/Litany Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Litany/Litany_Gibs.prefab").GetComponent<ParticleSystem>());

            //irid blooded setup here
            GenerateColorManaEffect GiveIridPigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            GiveIridPigment.mana = LoadedDBsHandler.PigmentDB.GetPigment("Iridescent");

            PerformEffectPassiveAbility iridBlooded = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            iridBlooded.name = "IridBlooded_1_PA";
            iridBlooded._passiveName = "Iridescent-Blooded (1)";
            iridBlooded.m_PassiveID = "PigmentBlooded";
            iridBlooded.passiveIcon = ResourceLoader.LoadSprite("IconStonebloodIrid");
            iridBlooded._characterDescription = "Upon receiving direct damage this party member produces 1 additional Iridescent pigment, an esoteric pigment that diffracts into different colors on round start.";
            iridBlooded._enemyDescription = "Upon receiving direct damage this enemy produces 1 additional Iridescent pigment, an esoteric pigment that diffracts into different colors on round start.";
            iridBlooded._triggerOn = [TriggerCalls.OnDirectDamaged];
            iridBlooded.doesPassiveTriggerInformationPanel = true;
            iridBlooded.effects =
            [
                Effects.GenerateEffect(GiveIridPigment, 1, Targeting.Slot_SelfSlot),
            ];

            //dont forget to add it!
            CheckerPassivePoolAdd("IridBlooded_1_PA", "Iridescent-Blooded (1)", iridBlooded);

            //PropX damage teehee
            ProportionalCurHealthDamageEffect die = ScriptableObject.CreateInstance<ProportionalCurHealthDamageEffect>();
            die._returnKillAsSuccess = true;

            //infantile add
            GoofyAssLitanyCoerceTargeting cliqueAdopter = ScriptableObject.CreateInstance<GoofyAssLitanyCoerceTargeting>();
            cliqueAdopter._passiveToAdd = Passives.Infantile;

            //infantile popup. might need to revisit this idk.
            PassivePopUpOnTargetEffect imABaby = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            imABaby._name = "Infantile";
            imABaby._sprite = Passives.Infantile.passiveIcon.name;

            //Divine Protection
            StatusEffect_Apply_Effect iKindaFeelAwkwardAroundYouNGL = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            iKindaFeelAwkwardAroundYouNGL._Status = StatusField.DivineProtection;


            //Stored Values! dies.
            UnitStoreData_ModIntSO otheringTracker = ScriptableObject.CreateInstance<UnitStoreData_ModIntSO>();
            otheringTracker.m_Text = "Othering: {0}";
            otheringTracker._UnitStoreDataID = "OtheringStoredValue";
            otheringTracker.m_TextColor = new Color32(200, 200, 200, 255);
            otheringTracker.m_CompareDataToThis = -1;
            otheringTracker.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("OtheringStoredValue", otheringTracker);

            CasterStoredValueChangeEffect otheringGoUp = ScriptableObject.CreateInstance<CasterStoredValueChangeEffect>();
            otheringGoUp.m_unitStoredDataID = "OtheringStoredValue";
            otheringGoUp._minimumValue = 0;
            otheringGoUp._exitValueIsChange = false;
            otheringGoUp._increase = true;
            otheringGoUp._randomBetweenPrevious = false;
            otheringGoUp._usePreviousExitValue = false;
            otheringGoUp._exitValueIsChange = false;

            CasterStoredValueSetEffect otheringReset = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            otheringReset._valueName = "OtheringStoredValue";

            PreviousComparatorCheckEffect ThreePlus = ScriptableObject.CreateInstance<PreviousComparatorCheckEffect>();
            ThreePlus._atOrAbove = true;
            ThreePlus._entryIsComparator = false;
            ThreePlus._fixedComparator = 3;

            ExtraVariableForNext_SVEffect otheringGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            otheringGet.m_unitStoredDataID = "OtheringStoredValue";


            //Animation signals oh boy
            SetCasterAnimationParameterEffect otheringToZero = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            otheringToZero._parameterName = "Othering";
            otheringToZero._parameterValue = 0;
            otheringToZero._UsePrevious = false;

            SetCasterAnimationParameterEffect otheringSignalUp = ScriptableObject.CreateInstance<SetCasterAnimationParameterEffect>();
            otheringSignalUp._parameterName = "Othering";
            otheringSignalUp._parameterValue = 3;
            otheringSignalUp._UsePrevious = true;



            Ability litanyCoerceAbility = new Ability("Coerce", "LitanyCoerce_A")
            {
                Description = "Applies Infantile as a passive to the highest health enemy or enemies without it, excluding this enemy.\nGenerates 3 Iridescent pigment.\nResets \"Othering\" counter to 0.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Providence,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(cliqueAdopter, 1, Targeting.Unit_OtherAllies),
                    Effects.GenerateEffect(otheringReset, 0, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(otheringToZero, 0, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(GiveIridPigment, 3, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.ExtremelyCommon,
                Priority = Priority.VeryFast,
            };
            litanyCoerceAbility.AddIntentsToTarget(Targeting.Unit_OtherAllies, [Passives.Infantile.passiveIcon.name]);
            litanyCoerceAbility.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc)]);

            AttackVisualsSO headhuntervisuals;
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                headhuntervisuals = LoadedAssetsHandler.GetEnemyAbility("SlaughterOne_A").visuals;
            }
            else
            {
                headhuntervisuals = Visuals.Decimate;
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
                Description = "Applies 1 Divine Protection to the Opposing party member.\nIf there isn't an Opposing party member, moves Left or Right.\nIncreases \"Othering\" counter by 1; if this was used at least 3 times, queues up \"Headhunter\".",
                Cost = [Pigments.Yellow],
                Rarity = Rarity.Impossible,
                Priority = Priority.VerySlow,
                Effects =
                [
                    Effects.GenerateEffect(iKindaFeelAwkwardAroundYouNGL, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckHasUnitEffect>(), 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<SwapToSidesEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(otheringGoUp, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(otheringSignalUp, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(otheringGet, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(ThreePlus, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(imComingForYourHead, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),

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
                    Passives.GetCustomPassive("IridBlooded_1_PA"), 
                    Passives.ParentalGenerator(otheringParental), 
                    Passives.OverexertGenerator(9),
                    Passives.Withering,
                ]
            );

            litany.AddEnemyAbilities(
                [
                    litanyCoerceAbility, litanyHeadhunterAbility,
                ]);
            litany.AddEnemy(true, false, false);

        }
    }
}
