using BepInEx;
using BrutalAPI;
using FMOD;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomPassives;

namespace SorasToybox.Enemies
{
    public class Dozer
    {
        //thanks april
        public class DozerDynamicMusicEffect : EffectSO
        {
            public static int Amount = 0;
            public static void Reset() => Amount = 0;
            public bool Add = true;
            public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
            {
                exitAmount = 0;
                bool GOING = Amount > 0;
                if (Add) Amount++;
                else Amount--;
                if ((Amount > 0) == GOING) return Amount > 0;
                if (Amount > 0)
                {
                    if (changeMusic != null)
                    {
                        try { changeMusic.Abort(); } catch { UnityEngine.Debug.LogWarning("black star thread failed to shut down."); }
                    }
                    changeMusic = new System.Threading.Thread(GO);
                    changeMusic.Start();
                }
                else
                {
                    if (changeMusic != null)
                    {
                        try { changeMusic.Abort(); } catch { UnityEngine.Debug.LogWarning("black star thread failed to shut down."); }
                    }
                    changeMusic = new System.Threading.Thread(STOP);
                    changeMusic.Start();
                }
                return Amount > 0;
            }

            public static System.Threading.Thread changeMusic;
            public static void GO()
            {
                int start = 0;
                if (CombatManager.Instance._stats.audioController.MusicCombatEvent.getParameterByName("DozerGoesWoke", out float num) == RESULT.OK) start = (int)num;
                //UnityEngine.Debug.Log("going: " + start);
                for (int i = start; i <= 100 && Amount > 0; i++)
                {
                    CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("DozerGoesWoke", i);
                    System.Threading.Thread.Sleep(20);
                    //if (i > 95) UnityEngine.Debug.Log("we;re getting there properly");
                }
                //UnityEngine.Debug.Log("done");
            }
            public static void STOP()
            {
                int start = 0;
                if (CombatManager.Instance._stats.audioController.MusicCombatEvent.getParameterByName("DozerGoesWoke", out float num) == RESULT.OK) start = (int)num;
                //UnityEngine.Debug.Log("going: " + start);
                for (int i = start; i >= 0 && Amount <= 0; i--)
                {
                    CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("DozerGoesWoke", i);
                    System.Threading.Thread.Sleep(20);
                    //if (i < 5) UnityEngine.Debug.Log("we;re getting there properly");
                }
                //UnityEngine.Debug.Log("done");
            }
        }
        public static void Add()
        {
            //Tracked passive
            StatusEffect_Apply_Effect trackMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            trackMe._Status = StatusField.GetCustomStatusEffect("Tracked_ID");

            //#Eeepy dozer
            Enemy dozerSleep = new Enemy("Dozer", "Dozer_EN")
            {
                Health = 48,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineDozer.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("deadDozer.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineDozer.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/Dozer/DozerHurt",
                DeathSound = "event:/SorasSFX/Enemies/Dozer/DozerDie",
            };
            dozerSleep.AddPassives([Passives.GetCustomPassive("YellowBlooded_1_PA"), CustomPassive.SaltLockstepGenerator(1), Passives.GetCustomPassive("Reflex_PA")]);
            dozerSleep.PrepareEnemyPrefab("Assets/ToyboxEnemies/Dozer/Dozer Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Dozer/DozerGibs.prefab").GetComponent<ParticleSystem>());


            // The absolute agony that is Lockstep
            CasterStoreValueSetterEffect fuck = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            fuck.m_unitStoredDataID = "LockstepDir_SV";
            CasterStoreValueSetterEffect initialize = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            initialize.m_unitStoredDataID = "LockstepAmount_SV";
            initialize._ignoreIfContains = true;
            dozerSleep.CombatEnterEffects = [
                Effects.GenerateEffect(fuck, 1, Targeting.Slot_SelfSlot, null),
                Effects.GenerateEffect(initialize, 1,Targeting.Slot_SelfSlot),
            ];

            //Dozer transformations here


            CasterTransformationEffect dozerGoesToBed = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            dozerGoesToBed._fullyHeal = false;
            dozerGoesToBed._maintainMaxHealth = true;
            dozerGoesToBed._currentToMaxHealth = false;
            dozerGoesToBed._enemyTransformation = dozerSleep.enemy;


            //Separating Sleepy Dozer from Awake Dozer here


            //#Woke Dozer
            Enemy dozerAwake = new Enemy("Dozer", "DozerAwake_EN")
            {
                Health = 48,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineDozerWoke.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("deadDozer.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineDozerWoke.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/Dozer/DozerAwakeHurt",
                DeathSound = "event:/SorasSFX/Enemies/Dozer/DozerDie",
            };

            dozerAwake.PrepareEnemyPrefab("Assets/ToyboxEnemies/Dozer/DozerAwake Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Dozer/DozerGibs.prefab").GetComponent<ParticleSystem>());

            //Dynamic music implementing, borrowed from Prioress code
            DozerDynamicMusicEffect add = ScriptableObject.CreateInstance<DozerDynamicMusicEffect>();
            add.Add = true;
            DozerDynamicMusicEffect remove = ScriptableObject.CreateInstance<DozerDynamicMusicEffect>();
            remove.Add = false;

            dozerAwake.CombatEnterEffects = new EffectInfo[]
            {
                Effects.GenerateEffect(add,1),
                //Effects.GenerateEffect(ScriptableObject.CreateInstance<AmalgaDropFishEffect>(), 3)
            };
            dozerAwake.CombatExitEffects = new EffectInfo[]
            {
                Effects.GenerateEffect(remove,1),
                //Effects.GenerateEffect(ScriptableObject.CreateInstance<AmalgaDropFishEffect>(), 3)
            };

            //If greasy fools is installed you get something funny!
            AttackVisualsSO dozerEyesClosedVisuals = ScriptableObject.CreateInstance<AttackVisualsSO>();
            if (LoadedAssetsHandler.GetCharacterAbility("sleep_4") != null)
            {
                dozerEyesClosedVisuals = LoadedAssetsHandler.GetCharacterAbility("sleep_4").visuals;
            } 
            else
            {
                dozerEyesClosedVisuals = Visuals.Flirt;
            }


            //Screaming because visual studiou keeps complaining
            CasterTransformationEffect dozerWakesUp = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            dozerWakesUp._fullyHeal = false;
            dozerWakesUp._maintainMaxHealth = true;
            dozerWakesUp._currentToMaxHealth = false;
            dozerWakesUp._enemyTransformation = dozerAwake.enemy;

            //Dozer goes back to sleep
            Ability dozerAwakeEyesClosed = new Ability("Eyes Closed", "ST_DozerEyesClosed_A")
            {
                Description = "Back to bed.",
                Rarity = Rarity.Impossible,
                Visuals = dozerEyesClosedVisuals,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(dozerGoesToBed, 1, Targeting.Slot_SelfSlot),
                ],
            };
            dozerAwakeEyesClosed.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Transform_Instument)]);

            QueueTimelineAbilityByNameEffect napTime = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            napTime._abilityName = "Eyes Closed";

            //Setting up patient here
            PerformEffectPassiveAbility patientPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            patientPassive.name = "ST_PatientDozer_PA";
            patientPassive._passiveName = "Eyes Closed";
            patientPassive.m_PassiveID = "Patient";
            patientPassive.passiveIcon = ResourceLoader.LoadSprite("IconPatient");
            patientPassive._characterDescription = "party members can't queue into the timeline, ya know - maybe it could work like windup, though...";
            patientPassive._enemyDescription = "When the player turn ends, this enemy queues the ability \"Eyes Closed\".";
            patientPassive._triggerOn = [TriggerCalls.OnPlayerTurnEnd_ForEnemy];
            patientPassive.effects = [
                Effects.GenerateEffect(napTime, 1, Targeting.Slot_SelfSlot),
            ];

            //Boom
            Ability dozerAwakeMorningAlarm = new Ability("Morning Alarm", "ST_DozerAwakeMorningAlarm_A")
            {
                Description = "Applies 12 Tracked to the Opposing party member.",
                Rarity = Rarity.Common,
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(trackMe, 12, Targeting.Slot_Front),
                ],
            };
            dozerAwakeMorningAlarm.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Tracked"]);

            //Ok abilities go here.
            Ability dozersleepZees = new Ability("ZZZ", "ST_DozerZZZ_A")
            {
                Description = "Applies 1 Tracked to the Opposing party member.",
                Rarity = Rarity.Uncommon,
                Visuals = null,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(trackMe, 1, Targeting.Slot_Front),
                ],
            };
            dozersleepZees.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Tracked"]);

            Ability dozerSleepEyesOpen = new Ability("Eyes Open", "ST_DozerEyesOpen_A")
            {
                Description = "This enemy wakes up.",
                Rarity = Rarity.VeryRare,
                Visuals = Visuals.Painting,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(dozerWakesUp, 1, Targeting.Slot_SelfSlot),
                ],

            };
            dozerSleepEyesOpen.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Transform_Instument)]);

            dozerSleep.AddEnemyAbilities(
                [
                dozersleepZees,
                dozerSleepEyesOpen,
                ]);
            dozerSleep.AddEnemy(true, true, false);
            dozerAwake.AddEnemyAbilities(
                [
                dozerAwakeMorningAlarm,
                dozerAwakeEyesClosed,
                ]);
            //moving the awake dozer passives down here so they get loaded after everything else is set up
            dozerAwake.AddPassives([Passives.GetCustomPassive("YellowBlooded_1_PA"), CustomPassive.SaltLockstepGenerator(1), Passives.GetCustomPassive("Itchy_PA"), patientPassive]);
            dozerAwake.AddEnemy(true, true, false);
        }
    }
}
