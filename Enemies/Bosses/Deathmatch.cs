using System;
using System.Reflection;
using BrutalAPI;
using SorasToybox;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using UnityEngine;


namespace SorasToybox.Enemies
{
    public class Deathmatch
    {
        public static void Add()
        {
            AddPassiveEffect youAndMeBabyAintNothinButMammals = ScriptableObject.CreateInstance<AddPassiveEffect>();
            youAndMeBabyAintNothinButMammals._passiveToAdd = Passives.GetCustomPassive("Mammal_PA");

            StatusEffectCheckerEffect hasAnte = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasAnte._status = StatusField.GetCustomStatusEffect("Ante_ID");
            hasAnte._allTargetsHaveStatusEffect = false;


            StatusEffect_Apply_Effect anteByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteByPrevious._MultPreviousExitValueForEntry = true;
            anteByPrevious._Status = StatusField.GetCustomStatusEffect("Ante_ID");


            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            DirectDeathEffect hangman = ScriptableObject.CreateInstance<DirectDeathEffect>();

            //subactions for deathmatch
            AnimationVisualsEffect judgementVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            judgementVis._visuals = Visuals.Decimate;
            judgementVis._animationTarget = Targeting.Slot_AllyLeft;


            TargetPerformEffectViaSubaction judgementEffect = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            judgementEffect.effects =
            [
                Effects.GenerateEffect(judgementVis, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 2, Targeting.Slot_AllyLeft),
                Effects.GenerateEffect(anteByPrevious, 1, Targeting.Slot_SelfSlot)
            ];

            AnimationVisualsEffect prosecutionVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            prosecutionVis._visuals = Visuals.Decimate;
            prosecutionVis._animationTarget = Targeting.Slot_AllyRight;

            TargetPerformEffectViaSubaction prosecutionEffect = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            prosecutionEffect.effects =
            [
                Effects.GenerateEffect(prosecutionVis, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 2, Targeting.Slot_AllyRight),
                Effects.GenerateEffect(anteByPrevious, 1, Targeting.Slot_SelfSlot)
            ];

            AnimationVisualsEffect trialErrorVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            trialErrorVis._visuals = Visuals.RendRight;
            trialErrorVis._animationTarget = Targeting.Slot_SelfSlot;

            TargetPerformEffectViaSubaction trialerrorEffect = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            trialerrorEffect.effects =
            [
                Effects.GenerateEffect(trialErrorVis, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(anteByPrevious, 1, Targeting.Slot_SelfSlot),
            ];


            //Move it!
            SwapToOneSideEffect swapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapLeft._swapRight = false;

            SwapToOneSideEffect swapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapRight._swapRight = true;


            //Instinct passive and whatnot
            PerformEffectPassiveAbility deathmatchInstinct = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            deathmatchInstinct.name = "ST_InstinctDeathmatch_PA";
            deathmatchInstinct._passiveName = "Instinct";
            deathmatchInstinct.m_PassiveID = "DMInstinct";
            deathmatchInstinct._characterDescription = "The fact that you have this means that I hate you.";
            deathmatchInstinct._enemyDescription = "On entering combat, apply Mammal as a passive to all party members.";
            deathmatchInstinct.passiveIcon = ResourceLoader.LoadSprite("Instinct.png");
            deathmatchInstinct._triggerOn = [TriggerCalls.OnCombatStart];
            deathmatchInstinct.effects = [Effects.GenerateEffect(youAndMeBabyAintNothinButMammals, 1, Targeting.Unit_AllOpponents)];
            Passives.AddCustomPassiveToPool("ST_InstinctDeathmatch_PA", "Instinct", deathmatchInstinct);

            //Deployment passive
            ReturnValueComparatorEffectorCondition fifteenOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            fifteenOrMore._lessThan = false;
            fifteenOrMore._comparator = 15;

            //copied directly from dune thresher
            SpawnEnemyAnywhereEffect CallReinforcements = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            CallReinforcements.enemy = LoadedAssetsHandler.GetEnemy("BurningShame_EN");
            CallReinforcements._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            AnimationVisualsEffect deploymentVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            deploymentVis._visuals = Visuals.BodySnatcher;
            deploymentVis._animationTarget = Targeting.Slot_SelfSlot;

            PerformEffectPassiveAbility deathmatchDeployment = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            deathmatchDeployment.name = "ST_DeploymentDeathmatch_PA";
            deathmatchDeployment.m_PassiveID = "DeploymentDeathmatch";
            deathmatchDeployment._passiveName = "Deployment (15)";
            deathmatchDeployment.passiveIcon = ResourceLoader.LoadSprite("IconDeployment");
            deathmatchDeployment._characterDescription = "HOW DID YOU GET THIS? THIS DOES NOT BELONG HERE. YOU DO NOT BELONG IN PURGATORY.";
            deathmatchDeployment._enemyDescription = "On receiving 15 or more damage, a Burning Shame will crawl from the inflicted wound.";
            deathmatchDeployment.doesPassiveTriggerInformationPanel = true;
            deathmatchDeployment._triggerOn = [TriggerCalls.OnDirectDamaged];
            deathmatchDeployment.conditions = [fifteenOrMore];
            deathmatchDeployment.effects =
            [
                Effects.GenerateEffect(deploymentVis, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(CallReinforcements, 1, Targeting.Slot_SelfSlot),
            ];
            Passives.AddCustomPassiveToPool("ST_DeploymentDeathmatch_PA", "Deployment (15)", deathmatchDeployment);


            //Basic unit setup
            Enemy deathmatchEnemy = new Enemy("Deathmatch", "Deathmatch_BOSS")
            {
                Health = 1024,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineDeathmatchBoss", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineDeathmatchBoss", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Lilith_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Lilith_CH").deathSound,
                UnitTypes = ["FemaleID"],
            };

            //making deathmatch end the game
            SpecialSceneEndingSetUpEffect specialSceneEndingSetUpEffect = ScriptableObject.CreateInstance<SpecialSceneEndingSetUpEffect>();
            specialSceneEndingSetUpEffect._shouldCombatEnd = false;
            specialSceneEndingSetUpEffect._specialScene = SpecialSceneType.HardEnding;
            deathmatchEnemy.CombatEnterEffects =
            [
                Effects.GenerateEffect(specialSceneEndingSetUpEffect, 1, Targeting.Slot_SelfSlot, null),
            ];

            //deathmatch has no gibs rn
            deathmatchEnemy.PrepareEnemyPrefab("Assets/ToyboxEnemies/Deathmatch/Deathmatch Boss.prefab", SorasToybox.assetbundle, null);







            Ability judgement = new Ability("Judgement", "ST_DMJudgement_A")
            {
                Description = "Makes the Opposing party member deal a little damage to their left party member, then gain Ante equal to the result. Moves Left.",
                Effects =
                [
                    Effects.GenerateEffect(judgementEffect, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,

            };


            Ability prosecution = new Ability("Prosecution", "ST_DMProsecution_A")
            {
                Description = "Makes the Opposing party member deal a little damage to their right party member, then gain Ante equal to the result. Moves Right.",
                Effects =
                [
                    Effects.GenerateEffect(prosecutionEffect, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
            };


            Ability trial = new Ability("Trial", "ST_DMTrial_A")
            {
                Description = "Moves the Left party member to the Right, then makes the Opposing party member deal almost no damage to themselves, gaining Ante equal to the result.\r\n",
                Rarity = Rarity.VeryRare,
                Effects =
                [
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(trialerrorEffect, 1, Targeting.Slot_Front),
                ],
            };

            Ability error = new Ability("Error", "ST_DMError_A")
            {
                Description = "Moves the Right party member to the Left, then makes the Opposing party member deal almost no damage to themselves, gaining Ante equal to the result.\r\n",
                Rarity = Rarity.VeryRare,
                Effects =
                [
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(trialerrorEffect, 1, Targeting.Slot_Front),
                ],
            };

            AttackVisualsSO hangingVisuals = ScriptableObject.CreateInstance<AttackVisualsSO>();
            hangingVisuals = Visuals.Headshot;


            AnimationVisualsEffect hangingVisEffect = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            hangingVisEffect._visuals = hangingVisuals;


            TargetPerformEffectViaSubaction hangingEffects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            hangingEffects.effects =
            [
                    Effects.GenerateEffect(hasAnte, 15, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(hangman, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
            ];

            

            Ability hanging = new Ability("Hanging", "ST_DMHanging_A")
            {
                Description = "If any party members have 15 or more Ante, give them what they deserve.\nRepeats Instinct.",
                Rarity = Rarity.Impossible,
                Effects =
                [
                    Effects.GenerateEffect(hasAnte, 15, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(hangingVisEffect, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(hangingEffects, 1, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(youAndMeBabyAintNothinButMammals, 1, Targeting.Unit_AllOpponents),
                ],
            };
            hanging.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_Death)]);

            ExtraAbilityInfo deathmatchExtra = new()
            {
                ability = hanging.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };

            deathmatchEnemy.AddPassives([Passives.GetCustomPassive("Illegible_PA"), Passives.GetCustomPassive("BrokenBlooded_1_PA"), deathmatchInstinct, deathmatchDeployment, Passives.BonusAttackGenerator(deathmatchExtra)]);

            deathmatchEnemy.AddEnemyAbilities([
                judgement,
                prosecution,
                trial,
                error,
                ]);

            deathmatchEnemy.AddEnemy(true, false, false);
        }
    }
}
