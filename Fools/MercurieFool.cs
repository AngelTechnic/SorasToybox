using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomEffects;
using SorasToybox.CustomStatuses;
using Yarn;

namespace SorasToybox.Fools
{
    public class MercurieFool
    {
        public static void Add()
        {
            //Atrophy application
            StatusEffect_Apply_Effect atrophyMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            atrophyMe._Status = StatusField.GetCustomStatusEffect("Atrophy_ID");

            //Malfunction application
            StatusEffect_Apply_Effect malfunctionMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            malfunctionMe._Status = StatusField.GetCustomStatusEffect("Malfunction_ID");
            malfunctionMe._MultPreviousExitValueForEntry = true;

            //Overclock application
            StatusEffect_Apply_Effect overclockMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            overclockMe._Status = StatusField.GetCustomStatusEffect("Overclock_ID");

            StatusEffectCheckerEffect hasAtrophy = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasAtrophy._status = StatusField.GetCustomStatusEffect("Atrophy_ID");

            RemoveStatusEffectEffect noMalfunction = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noMalfunction._status = StatusField.GetCustomStatusEffect("Malfunction_ID");


            RemoveStatusEffectEffect noAtrophy = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noAtrophy._status = StatusField.GetCustomStatusEffect("Atrophy_ID");


            //These next two effects are for Accelerator
            StatusEffect_Apply_Effect atrophyAgain = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            atrophyAgain._Status = StatusField.GetCustomStatusEffect("Atrophy_ID");
            atrophyAgain._MultPreviousExitValueForEntry = true;

            DamageEffect damageBoostedByAtrophy = ScriptableObject.CreateInstance<DamageEffect>();
            damageBoostedByAtrophy._usePreviousExitValue = true;


            DamageEffect normalDamage = ScriptableObject.CreateInstance<DamageEffect>();


            DamageEffect damageDeadIsTrue = ScriptableObject.CreateInstance<DamageEffect>();
            damageDeadIsTrue._returnKillAsSuccess = true;

            //funny sound effect from mythos if it exists
            string deathSound = "";
            if (SorasToybox.CrossMod.MythosFriends)
            {
                deathSound = LoadedAssetsHandler.GetEnemy("DimensionalShambler_EN").deathSound;
            }
            else
            {
                deathSound = LoadedAssetsHandler.GetEnemy("Estelle_EN").deathSound;
            }


            Character mercurie = new Character("Mercurie", "Mercurie_CH")
            {
                HealthColor = Pigments.Yellow,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("mercurie_front.png", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("mercurie_back.png", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("mercurie_overworld.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/EstelleHurt",
                DeathSound = deathSound,
                DialogueSound = "event:/EstelleRoar",
                UnitTypes = ["FemaleID", "Zoincaillan", "Angel"],
            };
            mercurie.GenerateMenuCharacter(ResourceLoader.LoadSprite("mercurie_menu.png"), ResourceLoader.LoadSprite("mercurie_menu_locked.png"));
            mercurie.AddPassives([Passives.GetCustomPassive("Erasure_PA")]);
            mercurie.SetMenuCharacterAsFullDPS();

            //Waste/Decay/Rot/Entropy Accelerator
            Ability accelerator1 = new Ability("Waste Accelerator", "ST_MercurieAccelerator1_A")
            {
                Description = "Inflict 5 Atrophy to the Opposing enemy. Remove all Atrophy from them, re-add it, then deal damage equal to the amount re-applied.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_accelerator.png"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(atrophyMe, 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(noAtrophy, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(atrophyAgain, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damageBoostedByAtrophy, 1, Targeting.Slot_Front),
                ],
            };
            accelerator1.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Status_Atrophy", "Status_Atrophy", nameof(IntentType_GameIDs.Damage_3_6)]);

            Ability accelerator2 = new Ability("Decay Accelerator", "ST_MercurieAccelerator2_A")
            {
                Description = "Inflict 7 Atrophy to the Opposing enemy. Remove all Atrophy from them, re-add it, then deal damage equal to the amount re-applied.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_accelerator.png"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(atrophyMe, 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(noAtrophy, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(atrophyAgain, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damageBoostedByAtrophy, 1, Targeting.Slot_Front),
                ],
            };
            accelerator2.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Status_Atrophy", "Status_Atrophy", nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability accelerator3 = new Ability("Rot Accelerator", "ST_MercurieAccelerator3_A")
            {
                Description = "Inflict 9 Atrophy to the Opposing enemy. Remove all Atrophy from them, re-add it, then deal damage equal to the amount re-applied.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_accelerator.png"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(atrophyMe, 9, Targeting.Slot_Front),
                    Effects.GenerateEffect(noAtrophy, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(atrophyAgain, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damageBoostedByAtrophy, 1, Targeting.Slot_Front),
                ],
            };
            accelerator3.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Status_Atrophy", "Status_Atrophy", nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability accelerator4 = new Ability("Entropy Accelerator", "ST_MercurieAccelerator4_A")
            {
                Description = "Inflict 11 Atrophy to the Opposing enemy. Remove all Atrophy from them, re-add it, then deal damage equal to the amount re-applied.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_accelerator.png"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Yellow],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(atrophyMe, 11, Targeting.Slot_Front),
                    Effects.GenerateEffect(noAtrophy, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(atrophyAgain, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damageBoostedByAtrophy, 1, Targeting.Slot_Front),
                ],
            };
            accelerator4.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Status_Atrophy", "Status_Atrophy", nameof(IntentType_GameIDs.Damage_11_15)]);


            //The End of Days/An Age/An Era/Time
            Ability theEnd1 = new Ability("The End of Days", "ST_MercurieTheEnd1_A")
            {
                Description = "Deal 3 damage to the Left and Right enemies, then apply Atrophy to all enemies equal to the amount of damage dealt.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_theend.png"),
                Cost = [Pigments.Blue, Pigments.Yellow, Pigments.Yellow],
                Visuals = Visuals.FingerGuns,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(normalDamage, 3, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(atrophyAgain, 1, Targeting.Unit_AllOpponents),
                ],
            };
            theEnd1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
            theEnd1.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Atrophy"]);

            Ability theEnd2 = new Ability("The End of An Age", "ST_MercurieTheEnd2_A")
            {
                Description = "Deal 4 damage to the Left and Right enemies, then apply Atrophy to all enemies equal to the amount of damage dealt.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_theend.png"),
                Cost = [Pigments.Blue, Pigments.Yellow, Pigments.Yellow],
                Visuals = Visuals.FingerGuns,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(normalDamage, 4, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(atrophyAgain, 1, Targeting.Unit_AllOpponents),
                ],
            };
            theEnd2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6)]);
            theEnd2.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Atrophy"]);

            Ability theEnd3 = new Ability("The End of An Era", "ST_MercurieTheEnd3_A")
            {
                Description = "Deal 6 damage to the Left and Right enemies, then apply Atrophy to all enemies equal to the amount of damage dealt.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_theend.png"),
                Cost = [Pigments.Blue, Pigments.Yellow, Pigments.Yellow],
                Visuals = Visuals.FingerGuns,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(normalDamage, 6, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(atrophyAgain, 1, Targeting.Unit_AllOpponents),
                ],
            };
            theEnd3.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_7_10)]);
            theEnd3.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Atrophy"]);

            Ability theEnd4 = new Ability("The End of Time", "ST_MercurieTheEnd4_A")
            {
                Description = "Deal 7 damage to the Left and Right enemies, then apply Atrophy to all enemies equal to the amount of damage dealt.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_theend.png"),
                Cost = [Pigments.Blue, Pigments.Yellow, Pigments.Yellow],
                Visuals = Visuals.FingerGuns,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(normalDamage, 7, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(atrophyAgain, 1, Targeting.Unit_AllOpponents),
                ],
            };
            theEnd4.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_7_10)]);
            theEnd4.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Atrophy"]);


            AnimationVisualsEffect clockVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            clockVisuals._visuals = Visuals.Conductor;
            clockVisuals._animationTarget = Targeting.Slot_Front;

            //Clockmaker/watcher/keeper/stopper
            Ability clock1 = new Ability("Clockwatcher", "ST_MercurieClock1_A")
            {
                Description = "Convert all Atrophy on this party member into Malfunction.\nDeal 6 damage to the Opposing enemy.\nIf this kills, gain 2 Overclock and remove all Malfunction from this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_clock.png"),
                Cost = [Pigments.Red, Pigments.Yellow, Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(noAtrophy, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(malfunctionMe, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(clockVisuals, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageDeadIsTrue, 6, Targeting.Slot_Front),
                    Effects.GenerateEffect(overclockMe, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(noMalfunction, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
            };
            clock1.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Rem_Status_Atrophy", "Status_Malfunction"]);
            clock1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Misc_Hidden)]);
            clock1.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", "Rem_Status_Malfunction"]);

            Ability clock2 = new Ability("Clockmaker", "ST_MercurieClock2_A")
            {
                Description = "Convert all Atrophy on this party member into Malfunction.\nDeal 7 damage to the Opposing enemy.\nIf this kills, gain 2 Overclock and remove all Malfunction from this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_clock.png"),
                Cost = [Pigments.Red, Pigments.Yellow, Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(noAtrophy, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(malfunctionMe, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(clockVisuals, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageDeadIsTrue, 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(overclockMe, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(noMalfunction, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
            };
            clock2.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Rem_Status_Atrophy", "Status_Malfunction"]);
            clock2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10), nameof(IntentType_GameIDs.Misc_Hidden)]);
            clock2.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", "Rem_Status_Malfunction"]);

            Ability clock3 = new Ability("Clockkeeper", "ST_MercurieClock3_A")
            {
                Description = "Convert all Atrophy on this party member into Malfunction.\nDeal 8 damage to the Opposing enemy.\nIf this kills, gain 3 Overclock and remove all Malfunction from this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_clock.png"),
                Cost = [Pigments.Red, Pigments.Yellow, Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(noAtrophy, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(malfunctionMe, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(clockVisuals, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageDeadIsTrue, 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(overclockMe, 3, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(noMalfunction, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
            };
            clock3.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Rem_Status_Atrophy", "Status_Malfunction"]);
            clock3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10), nameof(IntentType_GameIDs.Misc_Hidden)]);
            clock3.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", "Rem_Status_Malfunction"]);

            Ability clock4 = new Ability("Clockstopper", "ST_MercurieClock4_A")
            {
                Description = "Convert all Atrophy on this party member into Malfunction.\nDeal 9 damage to the Opposing enemy.\nIf this kills, gain 3 Overclock and remove all Malfunction from this party member.",
                AbilitySprite = ResourceLoader.LoadSprite("mercurie_clock.png"),
                Cost = [Pigments.Red, Pigments.Yellow, Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(noAtrophy, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(malfunctionMe, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(clockVisuals, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageDeadIsTrue, 9, Targeting.Slot_Front),
                    Effects.GenerateEffect(overclockMe, 3, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(noMalfunction, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
            };
            clock4.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Rem_Status_Atrophy", "Status_Malfunction"]);
            clock4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10), nameof(IntentType_GameIDs.Misc_Hidden)]);
            clock4.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", "Rem_Status_Malfunction"]);


            mercurie.AddLevelData(8, [accelerator1, theEnd1, clock1]);
            mercurie.AddLevelData(10, [accelerator2, theEnd2, clock2]);
            mercurie.AddLevelData(13, [accelerator3, theEnd3, clock3]);
            mercurie.AddLevelData(18, [accelerator4, theEnd4, clock4]);

            //cheevos
            mercurie.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "SorasToybox_Mercurie_Witness_ACH");
            mercurie.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "SorasToybox_Mercurie_Divine_ACH");
            if (SorasToybox.CrossMod.EnemyPack) { mercurie.AddFinalBossAchievementData("DoulaBoss", "SorasToybox_Mercurie_Abstraction_ACH"); }
            if (SorasToybox.CrossMod.GlitchsFreaks) { mercurie.AddFinalBossAchievementData("March_BOSS", "SorasToybox_Mercurie_Inevitable_ACH"); }
            if (SorasToybox.CrossMod.IntoTheAbyss) { mercurie.AddFinalBossAchievementData("Nobody_BOSS", "SorasToybox_Mercurie_Forgotten_ACH"); }
            if (SorasToybox.CrossMod.IntoTheAbyss) { mercurie.AddFinalBossAchievementData("Katalixi_BOSS", "SorasToybox_Mercurie_Boundary_ACH"); }
            if (SorasToybox.CrossMod.SaltEnemies) { mercurie.AddFinalBossAchievementData("BlueSky_BOSS", "SorasToybox_Mercurie_Dreamer_ACH"); }
            mercurie.AddFinalBossAchievementData("Deathmatch_BOSS", "SorasToybox_Mercurie_Antagonist_ACH");
            mercurie.AddCharacter(true, false);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Timewarped.");
            }

            //SpeakerBundle nonsense
            SpeakerBundle speakerBundleMercurie = new SpeakerBundle();
            speakerBundleMercurie.bundleTextColor = new Color32(96, 215, 181, 255);
            speakerBundleMercurie.dialogueSound = LoadedAssetsHandler.GetCharacter("Mercurie_CH").dxSound;
            speakerBundleMercurie.portrait = ResourceLoader.LoadSprite("mercurie_talk", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMercurieMad = new SpeakerBundle();
            speakerBundleMercurieMad.bundleTextColor = new Color32(96, 215, 181, 255);
            speakerBundleMercurieMad.dialogueSound = LoadedAssetsHandler.GetCharacter("Mercurie_CH").dxSound;
            speakerBundleMercurieMad.portrait = ResourceLoader.LoadSprite("mercurie_talkmad", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMercurieFront = new SpeakerBundle();
            speakerBundleMercurieFront.bundleTextColor = new Color32(96, 215, 181, 255);
            speakerBundleMercurieFront.dialogueSound = LoadedAssetsHandler.GetCharacter("Mercurie_CH").dxSound;
            speakerBundleMercurieFront.portrait = ResourceLoader.LoadSprite("mercurie_front", new Vector2(0.5f, 0f), 32);


            SpeakerBundle speakerBundleMercurieBack = new SpeakerBundle();
            speakerBundleMercurieBack.bundleTextColor = new Color32(96, 215, 181, 255);
            speakerBundleMercurieBack.dialogueSound = LoadedAssetsHandler.GetCharacter("Mercurie_CH").dxSound;
            speakerBundleMercurieBack.portrait = ResourceLoader.LoadSprite("mercurie_back", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMercurieWarded = new SpeakerBundle();
            speakerBundleMercurieWarded.bundleTextColor = new Color32(96, 215, 181, 255);
            speakerBundleMercurieWarded.dialogueSound = LoadedAssetsHandler.GetCharacter("Mercurie_CH").damageSound;
            speakerBundleMercurieWarded.portrait = ResourceLoader.LoadSprite("mercurie_frontwarded", new Vector2(0.5f, 0f), 32);

            Dialogues.CreateAndAddCustom_SpeakerData("Mercurie", speakerBundleMercurie, true, false, new SpeakerEmote[4]
            {
                new SpeakerEmote
                {
                    emotion = "Front",
                    bundle = speakerBundleMercurieFront,
                },
                new SpeakerEmote
                {
                    emotion = "Back",
                    bundle = speakerBundleMercurieBack,
                },
                new SpeakerEmote
                {
                    emotion = "Mad",
                    bundle = speakerBundleMercurieMad,
                },
                new SpeakerEmote
                {
                    emotion = "Warded",
                    bundle = speakerBundleMercurieWarded,
                }
            });

            SpeakerBundle speakerBundleMercurieMain = new SpeakerBundle();
            speakerBundleMercurieMain.bundleTextColor = speakerBundleMercurie.bundleTextColor;
            speakerBundleMercurieMain.dialogueSound = speakerBundleMercurie.dialogueSound;
            speakerBundleMercurieMain.portrait = ResourceLoader.LoadSprite("mercuriemain_talk", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMercurieMainMad = new SpeakerBundle();
            speakerBundleMercurieMainMad.bundleTextColor = speakerBundleMercurieMad.bundleTextColor;
            speakerBundleMercurieMainMad.dialogueSound = speakerBundleMercurieMad.dialogueSound;
            speakerBundleMercurieMainMad.portrait = ResourceLoader.LoadSprite("mercuriemain_talkmad", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMercurieMainFront = new SpeakerBundle();
            speakerBundleMercurieMainFront.bundleTextColor = speakerBundleMercurieFront.bundleTextColor;
            speakerBundleMercurieMainFront.dialogueSound = speakerBundleMercurieFront.dialogueSound;
            speakerBundleMercurieMainFront.portrait = ResourceLoader.LoadSprite("mercurie_front", new Vector2(0.5f, 0f), 32);


            SpeakerBundle speakerBundleMercurieMainBack = new SpeakerBundle();
            speakerBundleMercurieMainBack.bundleTextColor = speakerBundleMercurieBack.bundleTextColor;
            speakerBundleMercurieMainBack.dialogueSound = speakerBundleMercurieBack.dialogueSound;
            speakerBundleMercurieMainBack.portrait = ResourceLoader.LoadSprite("mercurie_back", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMercurieMainWarded = new SpeakerBundle();
            speakerBundleMercurieMainWarded.bundleTextColor = speakerBundleMercurieWarded.bundleTextColor;
            speakerBundleMercurieMainWarded.dialogueSound = speakerBundleMercurieWarded.dialogueSound;
            speakerBundleMercurieMainWarded.portrait = ResourceLoader.LoadSprite("mercurie_frontwarded", new Vector2(0.5f, 0f), 32);

            Dialogues.CreateAndAddCustom_SpeakerData("MercurieMain", speakerBundleMercurie, false, false, new SpeakerEmote[4]
            {
                new SpeakerEmote
                {
                    emotion = "Front",
                    bundle = speakerBundleMercurieMainFront,
                },
                new SpeakerEmote
                {
                    emotion = "Back",
                    bundle = speakerBundleMercurieMainBack,
                },
                new SpeakerEmote
                {
                    emotion = "Mad",
                    bundle = speakerBundleMercurieMainMad,
                },
                new SpeakerEmote
                {
                    emotion = "Warded",
                    bundle = speakerBundleMercurieMainWarded,
                },
            });
        }
    }
}
