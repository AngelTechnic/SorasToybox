using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace SorasToybox.Enemies
{
    public class Sinful
    {
        public static void Add()
        {
            GenerateColorManaEffect gimmeBroken = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            gimmeBroken.mana = LoadedDBsHandler.PigmentDB.GetPigment("Broken");
            gimmeBroken.usePreviousExitValue = true;

            ConsumeRandomButCasterHealthManaEffect heyShitAss = ScriptableObject.CreateInstance<ConsumeRandomButCasterHealthManaEffect>();
            
            StatusEffect_Apply_Effect gimmeAnte = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            gimmeAnte._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            ChangeHealthColorEffect whyAmIRedAllOfASudden = ScriptableObject.CreateInstance<ChangeHealthColorEffect>();
            whyAmIRedAllOfASudden.color = Pigments.Red;

            //Move it!
            SwapToOneSideEffect swapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapLeft._swapRight = false;

            SwapToOneSideEffect swapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapRight._swapRight = true;


            //subact and stuff
            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            DamageOnDoubleCascadeEffect cascadeDamageTheFuck = ScriptableObject.CreateInstance<DamageOnDoubleCascadeEffect>();
            cascadeDamageTheFuck._consistentCascade = true;
            cascadeDamageTheFuck._cascadeIsIndirect = false;
            cascadeDamageTheFuck._decreaseAsPercentage = false;
            cascadeDamageTheFuck._cascadeDecrease = 2;


            AnimationVisualsEffect hatchetVisuals =ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            hatchetVisuals._animationTarget = Targeting.Slot_AllyRight;
            hatchetVisuals._visuals = Visuals.Extrusion;

            AnimationVisualsEffect bygonesVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            bygonesVisuals._animationTarget = Targeting.Slot_AllyLeft;
            bygonesVisuals._visuals = Visuals.Intrusion;


            TargetPerformEffectViaSubaction hatchetEffects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            hatchetEffects.effects =
                [
                    Effects.GenerateEffect(hatchetVisuals, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(cascadeDamageTheFuck, 6, Targeting.GenerateSlotTarget([1,2,3,4], true)),
                ];


            TargetPerformEffectViaSubaction bygonesEffect = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            bygonesEffect.effects =
                [
                    Effects.GenerateEffect(bygonesVisuals, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(cascadeDamageTheFuck, 6, Targeting.GenerateSlotTarget([-1,-2,-3,-4], true)),
                ];


            //Deployment passive
            ReturnValueComparatorEffectorCondition eightOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            eightOrMore._lessThan = false;
            eightOrMore._comparator = 11;

            //copied directly from dune thresher
            SpawnEnemyAnywhereEffect CallReinforcements = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            CallReinforcements.enemy = LoadedAssetsHandler.GetEnemy("BurningShame_EN");
            CallReinforcements._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            AnimationVisualsEffect deploymentVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            deploymentVis._visuals = Visuals.BodySnatcher;
            deploymentVis._animationTarget = Targeting.Slot_SelfAll;

            PerformEffectPassiveAbility sinfulDeployment = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            sinfulDeployment.name = "ST_DeploymentSinful_PA";
            sinfulDeployment.m_PassiveID = "DeploymentSinful";
            sinfulDeployment._passiveName = "Deployment (11)";
            sinfulDeployment.passiveIcon = ResourceLoader.LoadSprite("IconDeployment");
            sinfulDeployment._characterDescription = "HOW DID YOU GET THIS? THIS DOES NOT BELONG HERE. YOU DO NOT BELONG IN PURGATORY.";
            sinfulDeployment._enemyDescription = "On receiving 8 or more damage, a Burning Shame will crawl from the inflicted wound.";
            sinfulDeployment.doesPassiveTriggerInformationPanel = true;
            sinfulDeployment._triggerOn = [TriggerCalls.OnDirectDamaged];
            sinfulDeployment.conditions = [eightOrMore];
            sinfulDeployment.effects =
            [
                Effects.GenerateEffect(deploymentVis, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(CallReinforcements, 1, Targeting.Slot_SelfSlot),
            ];
            Passives.AddCustomPassiveToPool("ST_DeploymentSinful_PA", "Deployment (11)", sinfulDeployment);


            

            Enemy sinfulReminder = new("Sinful Reminder", "SinfulReminder_EN")
            {
                Health = 66,
                HealthColor = Pigments.Red,
                Size = 2,
                CombatSprite = ResourceLoader.LoadSprite("TimelineSinful", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DeadSinful", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineSinful", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/yBugHurt",
                DeathSound = "event:/yBugDie",
                UnitTypes = ["FemaleID", "Primal", "Bug"],
            };
            sinfulReminder.PrepareEnemyPrefab("Assets/ToyboxEnemies/Sinful/SinfulReminder Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Sea Mine/SeaMine GIbs.prefab").GetComponent<ParticleSystem>());


            Ability m1 = new("ST_SinfulM1_A")
            {
                Name = "You're Not Invited",
                Description = "Moves Right and deals a little damage to the Left enemy.\nMakes the original Left party member deal a Painful amount of friendly fire damage that cascades to their right.",
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Scream,
                AnimationTarget = Targeting.Slot_OpponentLeft,
                Effects =
                [
                    Effects.GenerateEffect(hatchetEffects, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 2, Targeting.Slot_AllyLeft),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelyFast,
            };
            m1.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Right"]);
            m1.AddIntentsToTarget(Targeting.Slot_AllyLeft, ["Damage_1_2"]);
            m1.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Misc), "Damage_3_6", "Swap_Right"]);

            Ability m2 = new("ST_SinfulM2_A")
            {
                Name = "You're Not Allowed",
                Description = "Moves Left and deals a little damage to the Right enemy.\nMakes the original Right party member deal a Painful amount of friendly fire damage that cascades to their left.",
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Scream,
                AnimationTarget = Targeting.Slot_OpponentRight,
                Effects =
                [
                    Effects.GenerateEffect(bygonesEffect, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 2, Targeting.Slot_AllyRight),

                ],
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelyFast,
            };
            m2.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Left"]);
            m2.AddIntentsToTarget(Targeting.Slot_AllyRight, ["Damage_1_2"]);
            m2.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Misc), "Damage_3_6", "Swap_Left"]);

            Ability m3 = new("ST_SinfulM3_A")
            {
                Name = "Fun Without You",
                Description = "Exchanges all pigment that doesn't match this enemy's health with Broken pigment.\nTurns Red.",
                Cost = [Pigments.Yellow],
                Visuals = Visuals.Lullaby,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(heyShitAss, 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(gimmeBroken, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(whyAmIRedAllOfASudden, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common, 
                Priority = Priority.ExtremelyFast,
            };
            m3.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Mana_Consume", "Mana_Generate", "Mana_Modify"]);

            //bonus moves and the stuff for them.
            TargetPerformEffectViaSubaction b1SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            b1SubAct.effects =
                [
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 1, Targeting.Slot_SelfSlot),
                ];


            Ability b1 = new("ST_SinfulB1_A")
            {
                Name = "What Point Is There In You",
                Description = "Makes the Left enemy move to the Left and deal an Agonizing amount of damage to their new Opposing party member.\nThey will take almost no damage from the backlash.",
                Visuals = Visuals.Contusion,
                AnimationTarget = Targeting.Slot_AllyLeft,
                Effects =
                [
                    Effects.GenerateEffect(b1SubAct, 1, Targeting.Slot_AllyLeft),
                ],
                Priority = Priority.ExtremelySlow,
            };
            b1.AddIntentsToTarget(Targeting.Slot_AllyLeft, ["Swap_Left", "Damage_1_2"]);
            b1.AddIntentsToTarget(Targeting.Slot_OpponentLeft, ["Damage_7_10"]);


            TargetPerformEffectViaSubaction b2SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            b2SubAct.effects =
                [
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 1, Targeting.Slot_SelfSlot),
                ];


            Ability b2 = new("ST_SinfulB2_A")
            {
                Name = "You Should Stop Being You",
                Description = "Makes the Right enemy move to the Right and deal an Agonizing amount of damage to their new Opposing party member.\nThey will take almost no damage from the backlash.",
                Visuals = Visuals.Contusion,
                AnimationTarget = Targeting.Slot_AllyRight,
                Effects =
                [
                    Effects.GenerateEffect(b2SubAct, 1, Targeting.Slot_AllyRight),
                ],
                Priority = Priority.ExtremelySlow,
            };
            b2.AddIntentsToTarget(Targeting.Slot_AllyRight, ["Swap_Right", "Damage_1_2"]);
            b2.AddIntentsToTarget(Targeting.Slot_OpponentRight, ["Damage_7_10"]);

            CheckEntryOrMoreEmptyManaSlotsEffect overflowCheck = ScriptableObject.CreateInstance<CheckEntryOrMoreEmptyManaSlotsEffect>();
            

            Ability b3 = new("ST_SinfulB3_A")
            {
                Name = "The Damage Is Done >)",
                Description = "If the pigment tray is full, inflict 1 Ante to all party members.\nOtherwise, deal 1 damage to all other enemies.",
                Visuals = Visuals.Flex,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(overflowCheck, 0),
                    Effects.GenerateEffect(gimmeAnte, 1, Targeting.Unit_AllOpponents, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(damage, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(false, 2)),
                ],
                Priority = Priority.ExtremelySlow,
            };
            b3.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Misc_Hidden"]);
            b3.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Ante"]);
            b3.AddIntentsToTarget(Targeting.Unit_OtherAllies, ["Damage_1_2"]);

            ExtraAbilityInfo sinfulb1Extra = new()
            {
                ability = b1.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo sinfulb2Extra = new()
            {
                ability = b2.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
                
            };

            ExtraAbilityInfo sinfulb3Extra = new()
            {
                ability = b3.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,

            };


            sinfulReminder.AddEnemyAbilities([
                m1, m2, m3,
                ]);

            sinfulReminder.AddPassives([Passives.GetCustomPassive("Shortwave_PA"), sinfulDeployment, CustomPassives.CustomPassive.BonusSuiteGenerator([sinfulb1Extra, sinfulb2Extra, sinfulb3Extra])]);


            sinfulReminder.AddEnemy(true, true, false);

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[ENEMIES] Added the Sinful Reminder.");
            }


        }
    }
}
