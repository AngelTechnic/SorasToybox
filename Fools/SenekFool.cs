using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Fools
{
    public class SenekFool
    {
        public static void Add()
        {
            IntentInfoBasic targetInt = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("IconTargeter"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Targeter", targetInt);



            IntentInfoBasic targetIntRem = new IntentInfoBasic
            {
                _color = Color.grey,
                _sprite = ResourceLoader.LoadSprite("IconTargeter"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_Targeter", targetIntRem);


            CasterStoreValueSetterAdvancedEffect SetTargeter = ScriptableObject.CreateInstance<CasterStoreValueSetterAdvancedEffect>();
            SetTargeter.m_unitStoredDataID = "TargeterStoredValue";
            SetTargeter._ignoreIfContains = false;
            SetTargeter.usePreviousExitValue = true;

            OpponentByStoredValueTargeting TargeterTargeting = ScriptableObject.CreateInstance<OpponentByStoredValueTargeting>();
            TargeterTargeting._storedValueID = "TargeterStoredValue";
            TargeterTargeting.targetUnitAllySlots = false;
            TargeterTargeting.reduceByOne = true;

            PassivePopUpOnTargetEffect TargeterPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            TargeterPopup._name = "Targeter";
            TargeterPopup._sprite = "IconTargeter";
            TargeterPopup._isUnitCharacter = true;
            //getting visual for the lock on anim from SE
            AttackVisualsSO acquireAnim = null;
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                acquireAnim = LoadedAssetsHandler.GetEnemyAbility("Mercenary_A").visuals;
            }
            else
            {
                acquireAnim = Visuals.Resolve;
            }

            AnimationVisualsEffect TargetVisual = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            TargetVisual._visuals = acquireAnim;
            TargetVisual._animationTarget = TargeterTargeting;

            AttackVisualsSO railgunAnim;
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                railgunAnim = LoadedAssetsHandler.GetEnemyAbility("TheClassic_A").visuals;
            }
            else
            {
                railgunAnim = Visuals.FingerGuns;
            }


            PerformEffectPassiveAbility thresherEyeTargeterPassive = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            thresherEyeTargeterPassive.name = "ST_SenekTargeter_PA";
            thresherEyeTargeterPassive._passiveName = "Targeter";
            thresherEyeTargeterPassive.m_PassiveID = "Targeter";
            thresherEyeTargeterPassive.passiveIcon = ResourceLoader.LoadSprite("IconTargeter");
            thresherEyeTargeterPassive._characterDescription = "At the start of combat and the end of each turn, this party member will remember the position of the enemy with the highest current health.";
            thresherEyeTargeterPassive._enemyDescription = "hey, no, this one is for party members. no. bad. nuh uh. no.";
            thresherEyeTargeterPassive._triggerOn = [TriggerCalls.OnCombatStart, TriggerCalls.OnTurnFinished];
            thresherEyeTargeterPassive.doesPassiveTriggerInformationPanel = true;
            thresherEyeTargeterPassive.effects =
            [
                Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetGetSlotEffect>(), 1, Targeting.GenerateUnitTarget_Specific_Health(false, false, false, false)),
                Effects.GenerateEffect(SetTargeter, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(TargetVisual),
            ];
            Passives.AddCustomPassiveToPool("ST_SenekTargeter_PA", "Targeter", thresherEyeTargeterPassive);



            RefreshAbilityUseEffect refreshMe = ScriptableObject.CreateInstance<RefreshAbilityUseEffect>();


            Ability senekAcquire = new("ST_SenekAcquire_A")
            {
                Name = "Acquire",
                Description = "Sets the position of Senek's Targeter to the Opposing position.\nRefreshes this party member.",
                Cost = [Pigments.Yellow],
                AbilitySprite = ResourceLoader.LoadSprite("senek_acquire.png"),
                Visuals = acquireAnim,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<TargetGetSlotEffect>(), 1, Targeting.GenerateSlotTarget([0], false, false)),
                    Effects.GenerateEffect(SetTargeter, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(refreshMe, 1, Targeting.Slot_SelfSlot),
                ],
            };
            senekAcquire.AddIntentsToTarget(TargeterTargeting, ["Rem_Passive_Targeter"]);
            senekAcquire.AddIntentsToTarget(Targeting.GenerateSlotTarget([0], false, false), ["Passive_Targeter"]);
            senekAcquire.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Other_Refresh)]);

            Character senek = new("Senek", "Senek_CH")
            {
                BasicAbility = senekAcquire,
                HealthColor = Pigments.PurpleRed,
                UnitTypes = ["Zoincaillan", "Bug"],
                DamageSound = "event:/pBugHurt",
                DeathSound = "event:/pBugDie",
                DialogueSound = "event:/pBugRoar",
                FrontSprite = ResourceLoader.LoadSprite("senek_front.png", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("senek_back.png", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("senek_overworld.png", new Vector2(0.5f, 0f), 32),
                StartsLocked = true,
            };
            //setting up their unlock shit
            ModUnlockInfo senekMod = new ModUnlockInfo();

            //"senekMod" will be their achievement in the Fools section. This is the graphical stuff
            senekMod.m_ModSPrite = ResourceLoader.LoadSprite("Ach_Fool_Senek", null, 32, null);
            senekMod.m_ModTitle = "The Centaur";
            senekMod.m_ModDescription = "Unlocked Senek.";


            senek.GenerateMenuCharacter(ResourceLoader.LoadSprite("senek_menu.png"), ResourceLoader.LoadSprite("senek_menu_locked.png"));
            senek.MenuCharacterTrackData = ScriptableObject.CreateInstance<SenekUnlockTrackData>();
            senek.AddPassives([thresherEyeTargeterPassive, Passives.GetCustomPassive("Locksmith_PA")]);

            ModdedAchievements val2 = new ModdedAchievements("The Centaur", "Unlock Senek.", ResourceLoader.LoadSprite("Ach_Fool_Senek", null, 32, null), "Senek_Unlock_Achievement");
            //this will add her achievement to the Fools achievement category
            val2.AddNewAchievementToInGameCategory((AchievementCategoryIDs)2);

            //cheevo related to unlocking them
            UnlockableModData senekData = new UnlockableModData("Senek");
            senekData.hasCharacterUnlock = true;
            senekData.character = "Senek_CH";
            senekData.hasQuestCompletion = true;
            senekData.questID = "Senek";
            senekData.hasModdedAchievementUnlock = true;
            senekData.moddedAchievementID = "Senek_Unlock_Achievement";
            LoadedDBsHandler.UnlockablesDB.TryAddIDUnlock(senekData);


            DamageEffect railgunDamage = ScriptableObject.CreateInstance<DamageEffect>();
            railgunDamage._ignoreShield = true;

            FieldEffect_Apply_Effect getConstricted = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            getConstricted._Field = StatusField.Constricted;

            Ability senekRailgun1 = new Ability("Mk.1 Railgun", "ST_Railgun1_A")
            {

                Description = "Deal 8 damage to the Targeted position; this damage ignores Shield.\nGain 2 Constricted.",
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Red],
                AbilitySprite = ResourceLoader.LoadSprite("senek_railgun.png"),
                Visuals = railgunAnim,
                AnimationTarget = TargeterTargeting,
                Effects =
                [
                    Effects.GenerateEffect(railgunDamage, 8, TargeterTargeting),
                    Effects.GenerateEffect(getConstricted, 2, Targeting.Slot_SelfAll),
                ],
            };
            senekRailgun1.AddIntentsToTarget(TargeterTargeting, [nameof(IntentType_GameIDs.Damage_7_10)]);
            senekRailgun1.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Field_Constricted"]);

            Ability senekRailgun2 = new Ability(senekRailgun1.ability, "ST_SenekRailgun2_A",senekRailgun1.Cost)
            {
                Name = "Mk.2 Railgun",
                Description = "Deal 10 damage to the Targeted position; this damage ignores Shield.\nGain 2 Constricted.",
            };
            senekRailgun2.Effects[0].entryVariable = 10;

            Ability senekRailgun3 = new Ability(senekRailgun2.ability, "ST_SenekRailgun3_A", senekRailgun2.Cost)
            {
                Name = "Mk.3 Railgun",
                Description = "Deal 13 damage to the Targeted position; this damage ignores Shield.\nGain 2 Constricted.",
            };
            senekRailgun3.Effects[0].entryVariable = 13;
            senekRailgun3.EffectIntents[0].intents[0] = "Damage_11_15";

            Ability senekRailgun4 = new Ability(senekRailgun3.ability, "ST_SenekRailgun4_A", senekRailgun3.Cost)
            {
                Name = "Mk.X Railgun",
                Description = "Deal 15 damage to the Targeted position; this damage ignores Shield.\nGain 2 Constricted.",
            };
            senekRailgun4.Effects[0].entryVariable = 15;


            SpecificOpponentsByUnitTypeTargeting allFriendlyEnemies = ScriptableObject.CreateInstance<SpecificOpponentsByUnitTypeTargeting>();
            allFriendlyEnemies.targetUnitAllySlots = false;
            allFriendlyEnemies.slotOffsets = [0];
            allFriendlyEnemies.getAllUnitSelfSlots = true;
            allFriendlyEnemies._unitTypes = ["Friendly"];


            //Here comes the boyyyyyyyyyy
            //I copied the summon effect from Clock King for this, this is why it's called summonClockGuy even though it summons a bullseye now
            SpawnEnemyAnywhereEffect summonClockGuy = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            summonClockGuy.enemy = LoadedAssetsHandler.GetEnemy("SenekTasbeht_EN");
            summonClockGuy._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            summonClockGuy.givesExperience = false;


            AttackVisualsSO trickshotAnim;
            if (SorasToybox.CrossMod.IntoTheAbyss)
            {
                trickshotAnim = LoadedAssetsHandler.GetEnemyAbility("DeathRattle_A").visuals;
            }
            else
            {
                trickshotAnim = Visuals.Crush;
            }

            StatusEffect_Apply_Effect getDP = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getDP._Status = StatusField.DivineProtection;


            DamageEffect ricochetDamage = ScriptableObject.CreateInstance<DamageEffect>();

            AnimationVisualsEffect trickshotVisual = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            trickshotVisual._visuals = trickshotAnim;
            trickshotVisual._animationTarget = allFriendlyEnemies;


            Ability senekTrickshot1 = new Ability("Idiotic Trickshot", "ST_SenekTrickshot1_A")
            {
                Description = "Apply 1 Divine Protection to all Friendly enemies, then deal 5 damage to them.\nAttempt to summon a hacked Tasbeht to act as a target for this ability.",
                Cost = [Pigments.Purple, Pigments.Red, Pigments.Red],
                AbilitySprite = ResourceLoader.LoadSprite("senek_trickshot.png"),
                Effects =
                [
                    Effects.GenerateEffect(summonClockGuy, 1),
                    Effects.GenerateEffect(trickshotVisual, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getDP, 1, allFriendlyEnemies),
                    Effects.GenerateEffect(ricochetDamage, 5, allFriendlyEnemies),
                ]
            };
            senekTrickshot1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn)]);
            senekTrickshot1.AddIntentsToTarget(allFriendlyEnemies, [nameof(IntentType_GameIDs.Status_DivineProtection), nameof(IntentType_GameIDs.Damage_3_6)]);


            Ability senekTrickshot2 = new Ability(senekTrickshot1.ability, "ST_SenekTrickshot2_A", senekTrickshot1.Cost)
            {
                Name = "Foolish Trickshot",
                Description = "Apply 1 Divine Protection to all Friendly enemies, then deal 6 damage to them.\nAttempt to summon a hacked Tasbeht to act as a target for this ability."
            };
            senekTrickshot2.Effects[3].entryVariable = 6;


            Ability senekTrickshot3 = new Ability(senekTrickshot2.ability, "ST_SenekTrickshot3_A", senekTrickshot2.Cost)
            {
                Name = "Wicked Trickshot",
                Description = "Apply 1 Divine Protection to all Friendly enemies, then deal 7 damage to them.\nAttempt to summon a hacked Tasbeht to act as a target for this ability."
            };
            senekTrickshot3.Effects[3].entryVariable = 7;
            senekTrickshot3.EffectIntents[1].intents[1] = nameof(IntentType_GameIDs.Damage_7_10);

            Ability senekTrickshot4 = new Ability(senekTrickshot3.ability, "ST_SenekTrickshot4_A", senekTrickshot3.Cost)
            {
                Name = "Totally Calculated Trickshot",
                Description = "Apply 1 Divine Protection to all Friendly enemies, then deal 8 damage to them.\nAttempt to summon a hacked Tasbeht to act as a target for this ability."
            };
            senekTrickshot4.Effects[3].entryVariable = 8;


            DamageTargetRandomEffect randomBullshitGo = ScriptableObject.CreateInstance<DamageTargetRandomEffect>();
            

            
            Ability senekRicochet1 = new Ability("Focused Ricochet", "ST_SenekRicochet1_A")
            {
                Cost = [Pigments.Red, Pigments.Blue, Pigments.Red],
                Description = "Deal 5 damage to the Targeted position.\nDeal 4 damage to a random target.",
                AbilitySprite = ResourceLoader.LoadSprite("senek_ricochet.png"),
                Visuals = Visuals.Intrusion,
                AnimationTarget = TargeterTargeting,
                Effects =
                [
                    Effects.GenerateEffect(ricochetDamage, 5, TargeterTargeting),
                    Effects.GenerateEffect(randomBullshitGo, 4, Targeting.Unit_AllOpponents),
                ],
            };
            senekRicochet1.AddIntentsToTarget(TargeterTargeting, ["Damage_3_6"]);
            senekRicochet1.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Misc_Hidden", "Damage_3_6"]);

            Ability senekRicochet2 = new("ST_SenekRicochet2_A")
            {
                Name = "Reckless Ricochet",
                AbilitySprite = ResourceLoader.LoadSprite("senek_ricochet.png"),
                Visuals = Visuals.Intrusion,
                AnimationTarget = TargeterTargeting,
                Cost = [Pigments.Red, Pigments.Blue, Pigments.Red],
                Description = "Deal 7 damage to the Targeted position.\nDeal 5 damage to a random target.",
                Effects =
                [
                    Effects.GenerateEffect(ricochetDamage, 7, TargeterTargeting),
                    Effects.GenerateEffect(randomBullshitGo, 5, Targeting.Unit_AllOpponents),

                ]
            };
            senekRicochet2.AddIntentsToTarget(TargeterTargeting, ["Damage_7_10"]);
            senekRicochet2.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Misc_Hidden", "Damage_3_6"]);

            Ability senekRicochet3 = new("ST_SenekRicochet3_A")
            {
                Name = "Unhinged Ricochet",
                AbilitySprite = ResourceLoader.LoadSprite("senek_ricochet.png"),
                Visuals = Visuals.Intrusion,
                AnimationTarget = TargeterTargeting,
                Cost = [Pigments.Red, Pigments.Blue, Pigments.Red],
                Description = "Deal 9 damage to the Targeted position.\nDeal 5 damage to two random targets.",
                Effects =
                [
                    Effects.GenerateEffect(ricochetDamage, 9, TargeterTargeting),
                    Effects.GenerateEffect(randomBullshitGo, 5, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(randomBullshitGo, 5, Targeting.Unit_AllOpponents),
                ]
            };
            senekRicochet3.AddIntentsToTarget(TargeterTargeting, ["Swap_Left", "Damage_7_10", "Swap_Right"]);
            senekRicochet3.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Misc_Hidden", "Damage_3_6", "Damage_3_6"]);

            Ability senekRicochet4 = new("ST_SenekRicochet4_A")
            {
                Name = "Magdump Ricochet",
                AbilitySprite = ResourceLoader.LoadSprite("senek_ricochet.png"),
                Visuals = Visuals.Intrusion,
                AnimationTarget = TargeterTargeting,
                Cost = [Pigments.Red, Pigments.Blue, Pigments.Red],
                Description = "Deal 11 damage to the Targeted position.\nDeal 6 damage to two random targets.",
                Effects =
                [
                    Effects.GenerateEffect(ricochetDamage, 11, TargeterTargeting),
                    Effects.GenerateEffect(randomBullshitGo, 6, Targeting.Unit_AllOpponents),
                    Effects.GenerateEffect(randomBullshitGo, 6, Targeting.Unit_AllOpponents),
                ]
            };
            senekRicochet4.AddIntentsToTarget(TargeterTargeting, ["Swap_Left", "Damage_11_15", "Swap_Right"]);
            senekRicochet4.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Misc_Hidden", "Damage_3_6", "Damage_3_6"]);

            senek.AddLevelData(7, [senekRailgun1, senekTrickshot1, senekRicochet1]);
            senek.AddLevelData(9, [senekRailgun2, senekTrickshot2, senekRicochet2]);
            senek.AddLevelData(11, [senekRailgun3, senekTrickshot3, senekRicochet3]);
            senek.AddLevelData(13, [senekRailgun4, senekTrickshot4, senekRicochet4]);


            //cheevos
            senek.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "SorasToybox_Senek_Witness_ACH");
            senek.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "SorasToybox_Senek_Divine_ACH");
            if (SorasToybox.CrossMod.EnemyPack) { senek.AddFinalBossAchievementData("DoulaBoss", "SorasToybox_Senek_Abstraction_ACH"); }
            if (SorasToybox.CrossMod.GlitchsFreaks) { senek.AddFinalBossAchievementData("March_BOSS", "SorasToybox_Senek_Inevitable_ACH"); }
            if (SorasToybox.CrossMod.IntoTheAbyss) { senek.AddFinalBossAchievementData("Nobody_BOSS", "SorasToybox_Senek_Forgotten_ACH"); }
            if (SorasToybox.CrossMod.IntoTheAbyss) { senek.AddFinalBossAchievementData("Katalixi_BOSS", "SorasToybox_Senek_Boundary_ACH"); }
            if (SorasToybox.CrossMod.SaltEnemies) { senek.AddFinalBossAchievementData("BlueSky_BOSS", "SorasToybox_Senek_Dreamer_ACH"); }
            senek.AddFinalBossAchievementData("Deathmatch_BOSS", "SorasToybox_Senek_Antagonist_ACH");

            senek.AddCharacter(false, true);
            senek.SetMenuCharacterAsFullDPS();
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[FOOLS] Added the Centaur.");
            }

            //yapshenanigans
            SpeakerBundle speakerBundleSenek = new SpeakerBundle();
            speakerBundleSenek.bundleTextColor = new Color32(63, 66, 112, 255);
            speakerBundleSenek.dialogueSound = LoadedAssetsHandler.GetCharacter("Senek_CH").dxSound;
            speakerBundleSenek.portrait = ResourceLoader.LoadSprite("senek_talk", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleSenekPissed = new SpeakerBundle();
            speakerBundleSenekPissed.bundleTextColor = new Color32(63, 66, 112, 255);
            speakerBundleSenekPissed.dialogueSound = LoadedAssetsHandler.GetCharacter("Senek_CH").damageSound;
            speakerBundleSenekPissed.portrait = ResourceLoader.LoadSprite("senek_pissed", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleSenekSniping = new SpeakerBundle();
            speakerBundleSenekSniping.bundleTextColor = new Color32(63, 66, 112, 255);
            speakerBundleSenekSniping.dialogueSound = "event:/SorasSFX/Fools/SenekShootsYouWithAGun";
            speakerBundleSenekSniping.portrait = ResourceLoader.LoadSprite("senek_pissed", new Vector2(0.5f, 0f), 32);



            Dialogues.CreateAndAddCustom_SpeakerData("Senek", speakerBundleSenek, true, true, new SpeakerEmote[2]
            {
                new SpeakerEmote
                {
                    emotion = "Pissed",
                    bundle = speakerBundleSenekPissed,
                },
                new SpeakerEmote
                {
                    emotion = "Sniping",
                    bundle = speakerBundleSenekSniping,
                },
            });
        }
    }
}
