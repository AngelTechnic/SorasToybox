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
    public class GearYinimro
    {
        public static void Add()
        {
            TimelineLengthDamageEffect ballisticGearDmg = ScriptableObject.CreateInstance<TimelineLengthDamageEffect>();


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

            Enemy gearYinimro = new Enemy("Gear Yinimro", "GearYinimro_EN")
            {
                Health = 36,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineGearYinimro", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DeadGearYinimro", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineGearYinimro", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = LoadedAssetsHandler.GetCharacter("Doll_CH").deathSound,
                UnitTypes = ["Zoincaillan", "Robot"],
            };

            gearYinimro.PrepareEnemyPrefab("Assets/ToyboxEnemies/Yinimro/Gear Yinimro Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());

            CasterStoreValueSetterEffect fuck = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            fuck.m_unitStoredDataID = "LockstepDir_SV";
            CasterStoreValueSetterEffect initialize = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            initialize.m_unitStoredDataID = "LockstepAmount_SV";
            initialize._ignoreIfContains = true;
            gearYinimro.CombatEnterEffects = [
                Effects.GenerateEffect(fuck, 1, Targeting.Slot_SelfSlot, null),
                Effects.GenerateEffect(initialize, 1,Targeting.Slot_SelfSlot),
            ];


            StatusEffect_Apply_Effect overclockMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            overclockMe._Status = StatusField.GetCustomStatusEffect("Overclock_ID");

            Ability escapement = new Ability("Escapement", "ST_Escapement_A")
            {
                Description = "40% chance to gain 2 Overclock.",
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(tick, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(50)),
                    Effects.GenerateEffect(tock, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(overclockMe, 2, Targeting.Slot_SelfSlot, Effects.ChanceCondition(40)),
                ],
            };
            escapement.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock"]);

            Ability evilBallisticGear = new Ability("Ballistic Gear", "ST_BallisticGear_A")
            {
                Description = "Deals damage to the Opposing party member, boosted by the length of the timeline.",
                Rarity = Rarity.Impossible,
                AnimationTarget = Targeting.Slot_Front,
                Visuals = ballisticGearVis._visuals,
                Effects =
                [
                    Effects.GenerateEffect(ballisticGearDmg, 1, Targeting.Slot_Front),
                ]
            };
            evilBallisticGear.AddIntentsToTarget(Targeting.Slot_Front, ["Damage_Timeline"]);

            QueueTimelineAbilityByNameEffect shootTheShit = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            shootTheShit._abilityName = "Ballistic Gear";

            //Setting up patient here
            PerformEffectPassiveAbility gearYinimroPatient = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            gearYinimroPatient.name = "ST_PatientGearYinimro_PA";
            gearYinimroPatient._passiveName = "Ballistic Gear";
            gearYinimroPatient.m_PassiveID = "Patient";
            gearYinimroPatient.passiveIcon = ResourceLoader.LoadSprite("IconPatient");
            gearYinimroPatient._characterDescription = "party members can't queue into the timeline, ya know - maybe it could work like windup, though...";
            gearYinimroPatient._enemyDescription = "When the player turn ends, this enemy queues the ability \"Ballistic Gear\".";
            gearYinimroPatient._triggerOn = [TriggerCalls.OnPlayerTurnEnd_ForEnemy];
            gearYinimroPatient.effects = [
                Effects.GenerateEffect(shootTheShit, 1, Targeting.Slot_SelfSlot),
            ];


            gearYinimro.AddEnemyAbilities(
                [
                    escapement,
                    evilBallisticGear,
                ]);

            gearYinimro.AddPassives([Passives.GetCustomPassive("Houdini_PA"), Passives.GetCustomPassive("RedBlooded_1_PA"), Passives.Masochism1, CustomPassives.CustomPassive.SaltLockstepGenerator(1), gearYinimroPatient]);
            gearYinimro.AddEnemy(true, true, false);
        }
    }
}
