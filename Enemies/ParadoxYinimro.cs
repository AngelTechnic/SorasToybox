using BepInEx;
using BrutalAPI;
using FMOD;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomPassives;
using SorasToybox.CustomOther;

namespace SorasToybox.Enemies
{
    public class ParadoxYinimro
    {
        public class ParadoxMusicEffect : EffectSO
        {
            public static int Amount = 0;
            public static void Reset() => Amount = 0;
            public bool Add = true;
            public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
            {
                exitAmount = 0;
                if (CombatManager.Instance._stats.audioController.MusicCombatEvent.getParameterByName("YinimroParadox", out float num) == FMOD.RESULT.OK)
                {
                    CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("YinimroParadox", Add ? num + entryVariable : (entryVariable > num ? 0 : num - entryVariable));
                }
                return true;
            }
        }
        public static void Add()
        {
            Enemy paradoxShade = new Enemy("Paradox Shade", "ParadoxShade_EN")
            {
                Health = 15,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineParadoxYinimro", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineParadoxYinimro", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound,
                UnitTypes = ["Zoincaillan"],
            };
            paradoxShade.PrepareEnemyPrefab("Assets/ToyboxEnemies/Yinimro/Paradox Shadow Enemy.prefab", SorasToybox.assetbundle, null);

            StatusEffect_Apply_Effect getDazed = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getDazed._Status = StatusField.GetCustomStatusEffect("Dazed_ID");

            Ability uncertainty = new Ability("ST_ShadeUncertainty_A")
            {
                Name = "Uncertainty Principle",
                Description = "Gain 2 Dazed.",
                Rarity = Rarity.ImpossibleNoReroll,
                Effects =
                [
                    Effects.GenerateEffect(getDazed, 2, Targeting.Slot_SelfSlot),
                ]
            };
            uncertainty.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Dazed"]);

            
            //setting up riposte
            QueueTimelineAbilityByNameEffect riposteQueue = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            riposteQueue._abilityName = "Uncertainty Principle";

            ReturnValueComparatorEffectorCondition eightOrMore = ScriptableObject.CreateInstance<ReturnValueComparatorEffectorCondition>();
            eightOrMore._lessThan = false;
            eightOrMore._comparator = 8;

            PerformEffectPassiveAbility shadeRiposte = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            shadeRiposte.name = "ST_RiposteShade_PA";
            shadeRiposte._passiveName = "Uncertainty Principle (8)";
            shadeRiposte.m_PassiveID = "Riposte";
            shadeRiposte._characterDescription = "I don't think having this passive is beneficial for you.";
            shadeRiposte._enemyDescription = "Whenever this enemy receives 8 or more direct damage, queue the ability \"Uncertainty Principle.\"";
            shadeRiposte.passiveIcon = ResourceLoader.LoadSprite("passive_riposte.png");
            shadeRiposte._triggerOn = [TriggerCalls.OnDirectDamaged];
            shadeRiposte.conditions = [eightOrMore];
            shadeRiposte.effects =
                [
                    Effects.GenerateEffect(riposteQueue, 1, Targeting.Slot_SelfSlot),
                ];



            paradoxShade.AddEnemyAbilities([uncertainty]);

            paradoxShade.AddPassives([Passives.Infestation1, Passives.Withering, Passives.Catalyst, shadeRiposte]);

            PlayCustomSoundEffect dunDun = ScriptableObject.CreateInstance<PlayCustomSoundEffect>();
            dunDun._Sound = LoadedAssetsHandler.GetEnemy("TaMaGoa_EN").damageSound;

            paradoxShade.CombatEnterEffects =
                [
                Effects.GenerateEffect(dunDun)
                ];

            paradoxShade.AddEnemy(false, false, false);


            //now for the main event
            Enemy paradoxYinimro = new Enemy("Paradox Yinimro", "ParadoxYinimro_EN")
            {
                Health = 90,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineParadoxYinimro", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DeadParadoxYinimro", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineParadoxYinimro", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("ClockTower_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("ClockTower_EN").deathSound,
                UnitTypes = ["Zoincaillan", "Robot", "FemaleID"],
            };
            paradoxYinimro.PrepareEnemyPrefab("Assets/ToyboxEnemies/Yinimro/Paradox Yinimro Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());


            //effects set up
            SpawnEnemyAnywhereEffect makeShade = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            makeShade.enemy = LoadedAssetsHandler.GetEnemy("ParadoxShade_EN");
            makeShade._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            makeShade.givesExperience = false;

            //music
            ParadoxMusicEffect add = ScriptableObject.CreateInstance<ParadoxMusicEffect>();
            add.Add = true;

            ParadoxMusicEffect remove = ScriptableObject.CreateInstance<ParadoxMusicEffect>();
            remove.Add = false;
            paradoxYinimro.CombatEnterEffects = new EffectInfo[]
{
                Effects.GenerateEffect(add,1),
};

            paradoxYinimro.CombatExitEffects = new EffectInfo[]
            {
                Effects.GenerateEffect(remove,1),
            };

            MassSwapZoneEffect momentumBreakEffect = ScriptableObject.CreateInstance<MassSwapZoneEffect>();

            MoveToRandomEmptyTileEffect inertiaBreakEffect = ScriptableObject.CreateInstance<MoveToRandomEmptyTileEffect>();

            AnimationVisualsEffect tick = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            if (LoadedAssetsHandler.GetEnemy("Kcolclock_EN") != null)
            {
                tick._visuals = LoadedAssetsHandler.GetEnemyAbility("KcolclockTick_A").visuals;
            }
            else
            {
                tick._visuals = Visuals.Wriggle;
            }
            tick._animationTarget = Targeting.Slot_SelfSlot;

            AnimationVisualsEffect tock = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            if (LoadedAssetsHandler.GetEnemy("Kcolclock_EN") != null)
            {
                tock._visuals = LoadedAssetsHandler.GetEnemyAbility("KcolclockTock_A").visuals;
            }
            else
            {
                tock._visuals = Visuals.Wriggle;
            }
            tock._animationTarget = Targeting.Slot_SelfSlot;

            AnimationVisualsEffect ballisticGearVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            if (LoadedAssetsHandler.GetEnemy("WanderFellow_EN") != null)
            {
                ballisticGearVis._visuals = LoadedAssetsHandler.GetEnemyAbility("WFNowhere_A").visuals;
            }
            else
            {
                ballisticGearVis._visuals = Visuals.WrigglingWrath;
            }




            Ability flatCircle = new Ability("ST_FlatCircle_A")
            {
                Name = "Flat Circle",
                Description = "Moves to a random unoccupied position.\nAttempts to spawn a Paradox Shade.",
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(tick, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(50)),
                    Effects.GenerateEffect(tock, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(inertiaBreakEffect, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(makeShade, 1, Targeting.Slot_SelfSlot),
                ],
            };
            flatCircle.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Mass), nameof(IntentType_GameIDs.Other_Spawn)]);

            StatusEffect_Apply_Effect getEntropy = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getEntropy._Status = StatusField.GetCustomStatusEffect("Salt_Entropy_ID");

            StatusEffectCheckerEffect hasDazed = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasDazed._status = StatusField.GetCustomStatusEffect("Dazed_ID");

            TargetPerformEffectViaSubaction paradoxGearEffects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            paradoxGearEffects.effects =
                [
                    Effects.GenerateEffect(hasDazed, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getEntropy, 10, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 1)),
                ];

            SpecificEnemiesTargeting allShades = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            allShades._enemies = ["ParadoxShade_EN"];
            allShades.targetUnitAllySlots = true;
            allShades.slotOffsets = [0];

            SpecificEnemiesTargeting opposingShades = ScriptableObject.CreateInstance<SpecificEnemiesTargeting>();
            opposingShades._enemies = ["ParadoxShade_EN"];
            opposingShades.targetUnitAllySlots = false;
            opposingShades.slotOffsets = [0];



            Ability momentumBreak = new Ability("ST_MomentumBreak_A")
            {
                Name = "Momentum Break",
                Description = "Randomly shuffles all enemy positions.",
                Rarity = Rarity.AbsurdlyRare,
                Effects = [
                    Effects.GenerateEffect(momentumBreakEffect, 1, Targeting.Unit_AllAllies),
                    ],
            };
            momentumBreak.AddIntentsToTarget(Targeting.Unit_AllAllies, [nameof(IntentType_GameIDs.Swap_Mass)]);



            QueueTimelineAbilityByNameEffect dance = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            dance._abilityName = "Momentum Break";

            Ability paradoxGear = new Ability("ST_ParadoxGear_A")
            {
                Name = "Paradox Gear",
                Description = "Queues \"Momentum Break.\"\nMakes all Paradox Shades that aren't Dazed apply 10 Entropy to their Opposing party members.",
                Rarity = Rarity.ImpossibleNoReroll,
                Visuals = ballisticGearVis._visuals,
                AnimationTarget = opposingShades,
                Effects =
                [
                    Effects.GenerateEffect(paradoxGearEffects, 1, allShades),
                    Effects.GenerateEffect(dance, 1, Targeting.Slot_SelfSlot),
                ]
            };
            paradoxGear.AddIntentsToTarget(opposingShades, [nameof(IntentType_GameIDs.Misc), "Status_Salt_Entropy"]);
            paradoxGear.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Additional)]);

            QueueTimelineAbilityByNameEffect shootTheShit = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            shootTheShit._abilityName = "Paradox Gear";

            //Setting up patient here
            PerformEffectPassiveAbility paradoxYinimroPatient = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            paradoxYinimroPatient.name = "ST_PatientGearYinimro_PA";
            paradoxYinimroPatient._passiveName = "Paradox Gear";
            paradoxYinimroPatient.m_PassiveID = "Patient";
            paradoxYinimroPatient.passiveIcon = ResourceLoader.LoadSprite("IconPatient");
            paradoxYinimroPatient._characterDescription = "party members can't queue into the timeline, ya know - maybe it could work like windup, though...";
            paradoxYinimroPatient._enemyDescription = "When the player turn ends, this enemy queues the ability \"Paradox Gear\".";
            paradoxYinimroPatient._triggerOn = [TriggerCalls.OnPlayerTurnEnd_ForEnemy];
            paradoxYinimroPatient.effects = [
                Effects.GenerateEffect(shootTheShit, 1, Targeting.Slot_SelfSlot),
            ];

            paradoxYinimro.AddEnemyAbilities(
                [
                    flatCircle,
                    momentumBreak,
                    paradoxGear,
                ]);

            paradoxYinimro.AddPassives([Passives.GetCustomPassive("ST_Deathbound_PA"), Passives.GetCustomPassive("Radio_PA"), paradoxYinimroPatient]);
            paradoxYinimro.AddEnemy(true, true, false);
        }
    }
}
