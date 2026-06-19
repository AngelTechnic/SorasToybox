using BrutalAPI;
using SorasToybox;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


namespace SorasToybox.Enemies
{
    public class Deathmatch
    {
        public class DeathmatchPinchMusicEffect : EffectSO
        {
            public static int Amount = 0;
            public static void Reset() => Amount = 0;
            public bool Add = true;
            public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
            {
                exitAmount = 0;
                if (CombatManager.Instance._stats.audioController.MusicCombatEvent.getParameterByName("DeathmatchPinch", out float num) == FMOD.RESULT.OK)
                {
                    CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("DeathmatchPinch", Add ? num + entryVariable : (entryVariable > num ? 0 : num - entryVariable));
                }
                return true;
            }
        }
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

            CasterInOneEdgeCheckEffect CheckLeft = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
            CheckLeft._right = false;

            CasterInOneEdgeCheckEffect CheckRight = ScriptableObject.CreateInstance<CasterInOneEdgeCheckEffect>();
            CheckRight._right = true;


            PreviousEffectCondition PreviousTrue = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousTrue.wasSuccessful = true;

            PreviousEffectCondition PreviousFalse = ScriptableObject.CreateInstance<PreviousEffectCondition>();
            PreviousFalse.wasSuccessful = false;

            //Instinct passive and whatnot 
            StatusEffect_Apply_Effect anteUp = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteUp._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            IntentInfoBasic InstinctIntent = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("Instinct.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Instinct", InstinctIntent);

            DeathmatchPinchMusicEffect add = ScriptableObject.CreateInstance<DeathmatchPinchMusicEffect>();
            add.Add = true;
            DeathmatchAltMusicCondition amIWeakEnough = ScriptableObject.CreateInstance<DeathmatchAltMusicCondition>();



            TargetPerformEffectViaSubaction instinctEffects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            instinctEffects.effects = 
            [
                Effects.GenerateEffect(add, 1, Targeting.Slot_SelfSlot, amIWeakEnough),
                Effects.GenerateEffect(youAndMeBabyAintNothinButMammals, 1, Targeting.Unit_AllOpponents), 
                Effects.GenerateEffect(anteUp, 1, Targeting.Unit_AllOpponents)
            ];


            PerformEffectPassiveAbility deathmatchInstinct = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            deathmatchInstinct.name = "ST_InstinctDeathmatch_PA";
            deathmatchInstinct._passiveName = "Instinct";
            deathmatchInstinct.m_PassiveID = "DMInstinct";
            deathmatchInstinct._characterDescription = "The fact that you have this means that I hate you.";
            deathmatchInstinct._enemyDescription = "On entering combat, apply 1 Ante, and Mammal as a passive, to all party members.";
            deathmatchInstinct.passiveIcon = ResourceLoader.LoadSprite("Instinct.png");
            deathmatchInstinct._triggerOn = [TriggerCalls.OnCombatStart];
            deathmatchInstinct.effects = 
                [
                    Effects.GenerateEffect(instinctEffects, 1, Targeting.Slot_SelfSlot),
                ];
            Passives.AddCustomPassiveToPool("ST_InstinctDeathmatch_PA", "Instinct", deathmatchInstinct);

            PassivePopUpOnTargetEffect instinctPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            instinctPopup._sprite = "Instinct";
            instinctPopup._name = "Instinct";

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
                Health = 333,
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
                Description = "Makes the Opposing party member deal a little damage to their left party member, then gain Ante equal to the result. Moves Left, assuming the grid wraps around.",
                Effects =
                [
                    Effects.GenerateEffect(judgementEffect, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                ],
                Rarity = Rarity.Uncommon,
                Visuals = Visuals.Bellow,
                AnimationTarget = Targeting.Slot_Front,
            };


            Ability prosecution = new Ability("Prosecution", "ST_DMProsecution_A")
            {
                Description = "Makes the Opposing party member deal a little damage to their right party member, then gain Ante equal to the result. Moves Right, assuming the grid wraps around.",
                Effects =
                [
                    Effects.GenerateEffect(prosecutionEffect, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                ],
                Rarity = Rarity.Uncommon,
                Visuals = Visuals.Bellow,
                AnimationTarget = Targeting.Slot_Front,
            };


            Ability trial = new Ability("Trial", "ST_DMTrial_A")
            {
                Description = "Moves the Left party member to the Right, then makes the Opposing party member deal almost no damage to themselves, gaining Ante equal to the result.\nMoves Right, assuming the grid wraps around.",
                Rarity = Rarity.VeryRare,
                Effects =
                [
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(trialerrorEffect, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CheckRight, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                ],
            };

            Ability error = new Ability("Error", "ST_DMError_A")
            {
                Description = "Moves the Right party member to the Left, then makes the Opposing party member deal almost no damage to themselves, gaining Ante equal to the result.\nMoves Left, assuming the grid wraps around.",
                Rarity = Rarity.VeryRare,
                Effects =
                [
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(trialerrorEffect, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(CheckLeft, 1, Targeting.Slot_SelfSlot, PreviousFalse),
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([true, false], [1, 2])),
                ],
            };


            //THE FOLLOWING ABILITY USED TO BE NAMED HANGING IM SORRYYYYYYY
            AttackVisualsSO hangingVisuals = ScriptableObject.CreateInstance<AttackVisualsSO>();
            hangingVisuals = Visuals.Headshot;


            AnimationVisualsEffect hangingVisEffect = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            hangingVisEffect._visuals = hangingVisuals;


            RemoveStatusEffectEffect noAnte = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noAnte._status = StatusField.GetCustomStatusEffect("Ante_ID");

            TargetPerformEffectViaSubaction hangingEffects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            hangingEffects.effects =
            [
                    Effects.GenerateEffect(hasAnte, 15, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(noAnte, 1, Targeting.Unit_OtherAllies, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(hangman, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
            ];

            AttackVisualsSO dooranim2;
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                dooranim2 = LoadedAssetsHandler.GetEnemyAbility("Die4U_A").visuals;
            }
            else
            {
                dooranim2 = Visuals.Clobber_Left;
            }


            Ability hangingJudge = new Ability("Hanging Judge", "ST_DMHangingJudge_A")
            {
                Description = "Repeats Instinct.",
                Rarity = Rarity.ImpossibleNoReroll,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Visuals = dooranim2,
                Effects =
                [
                    Effects.GenerateEffect(instinctPopup, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(instinctEffects, 1, Targeting.Slot_SelfSlot),

                ]
            };
            hangingJudge.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Passive_Instinct"]);


            UseSpecificAbilityByEntryEffect queueHangingJudge = ScriptableObject.CreateInstance<UseSpecificAbilityByEntryEffect>();
            queueHangingJudge.usePrev = false;

            ExtraAbilityInfo executionQueue = new()
            {
                ability = hangingJudge.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };
            queueHangingJudge._parentalAbility = executionQueue;

            Ability execution = new Ability("Death Row", "ST_DMExecution_A")
            {
                Description = "If any party members have 15 or more Ante, the first against the wall gets what they deserve.\nQueues the ability \"Hanging Judge\".",
                Rarity = Rarity.Impossible,
                Effects =
                [
                    Effects.GenerateEffect(hasAnte, 15, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(hangingVisEffect, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(hangingEffects, 1, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(queueHangingJudge, 1, Targeting.Slot_SelfSlot),
                ],
            };
            execution.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_Death)]);
            execution.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);

            //adding tragedy achievement.
            TryUnlockAchievementEffect deathmatchTragedyUnlock = ScriptableObject.CreateInstance<TryUnlockAchievementEffect>();
            deathmatchTragedyUnlock._unlockID = "SorasToybox_Deathmatch_Tragedy_Unlock";

            PerformEffectPassiveAbility deathmatchDismal = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            deathmatchDismal.name = "ST_DismalDeathmatch_PA";
            deathmatchDismal.m_PassiveID = "DismalDeathmatch";
            deathmatchDismal._passiveName = "Dismal";
            deathmatchDismal.passiveIcon = ResourceLoader.LoadSprite("passive_dismal.png");
            deathmatchDismal._enemyDescription = "This enemy is looking for a reason to hurt you.";
            deathmatchDismal._characterDescription = "This party member is looking for a reason to hurt you.";
            deathmatchDismal._triggerOn = [TriggerCalls.OnDeath];
            deathmatchDismal.doesPassiveTriggerInformationPanel = false;
            deathmatchDismal.effects = [Effects.GenerateEffect(deathmatchTragedyUnlock, 1, Targeting.Slot_SelfSlot)];

            Passives.AddCustomPassiveToPool("ST_DismalDeathmatch_PA", "Dismal", deathmatchDismal);

            RemovePassiveEffect noDismalForYou = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            noDismalForYou.m_PassiveID = "DismalDeathmatch";

            //EXCESS
            Ability admission = new Ability("Admission", "ST_DMExcessAbility_A")
            {
                Description = "Removes Ante from all party members, then applies the amount removed to the weakest party member.",
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_Weakest,
                Visuals = Visuals.Misery,
                Rarity = Rarity.ImpossibleNoReroll, 
                Effects =
                [
                    Effects.GenerateEffect(noAnte, 1, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(anteByPrevious, 1, Targeting.Spec_Unit_AllOpponents_Weakest),
                    Effects.GenerateEffect(noDismalForYou, 1, Targeting.Slot_SelfSlot),
                ]
            };
            admission.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Rem_Status_Ante"]);
            admission.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_Weakest, ["Status_Ante"]);



            UseSpecificAbilityByEntryEffect queueAdmission = ScriptableObject.CreateInstance<UseSpecificAbilityByEntryEffect>();
            queueAdmission.usePrev = false;

            ExtraAbilityInfo extraExcessAdmission = new()
            {
                ability = admission.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };
            queueAdmission._parentalAbility = extraExcessAdmission;

            PerformEffectPassiveAbility dmExcess = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            dmExcess.name = "DMExcess_PA";
            dmExcess._passiveName = "Excess (Admission)";
            dmExcess.m_PassiveID = "Excess_PA";
            dmExcess.passiveIcon = ResourceLoader.LoadSprite("passive_excess.png");
            dmExcess._enemyDescription = "Whenever overflow is triggered, this enemy will queue the ability \"Admission\".";
            dmExcess._characterDescription = "nah";
            dmExcess._triggerOn = [ExcessNotificationHook.OnExcessTriggered];
            dmExcess.effects = [
                Effects.GenerateEffect(queueAdmission,1,Targeting.Slot_SelfSlot),
                ];
            dmExcess.doesPassiveTriggerInformationPanel = true;



            Passives.AddCustomPassiveToPool("DMExcess_PA", "Excess (Admission)", dmExcess);




            ExtraAbilityInfo deathmatchExtra = new()
            {
                ability = execution.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };





            deathmatchEnemy.AddPassives([Passives.GetCustomPassive("Illegible_PA"), Passives.GetCustomPassive("BrokenBlooded_1_PA"), deathmatchInstinct, deathmatchDeployment, dmExcess, Passives.BonusAttackGenerator(deathmatchExtra), deathmatchDismal]);

            deathmatchEnemy.AddEnemyAbilities([
                judgement,
                prosecution,
                trial,
                error,
                hangingJudge,
                admission,
                ]);

            deathmatchEnemy.AddEnemy(true, false, false);


            //achievements
            string achievementID = "DeathmatchBoss_ACH";
            string unlockID = "Deathmatch_BOSS";
            string itemID = "SentientArcanite_TW";

            BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, itemID);
            UnlockableModData unlockData = new UnlockableModData(unlockID);
            unlockData.hasModdedAchievementUnlock = true;
            unlockData.moddedAchievementID = achievementID;
            unlockData.hasItemUnlock = true;
            unlockData.items = [itemID];
                
            ListedUnlockCheck unlockCheck = ScriptableObject.CreateInstance<ListedUnlockCheck>();
            unlockCheck.unlockID = unlockID;
            unlockCheck.unlockData = unlockData;
            Unlocks.AddUnlock_BeatBoss(unlockCheck);

            ModdedAchievements bossAchievement = new ModdedAchievements("The Antagonist", "Extinguish the Deathmatch.", ResourceLoader.LoadSprite("Ach_Boss_Deathmatch", null, 32, null), achievementID);
            bossAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.BossesTitleLabel);
            string[] deathmatchTips =
            [
                "We should just give up. We're not cut out for this... what? What am I saying?",
                "Wow, you REALLY need to make better choices in friends.",
                "Was that us? That can't have been us. I'm sorry."
            ];
            BrutalAPI.Dialogues.AddCustom_GameOver_BossLines("Deathmatch_BOSS", deathmatchTips);
        }
    }
}
