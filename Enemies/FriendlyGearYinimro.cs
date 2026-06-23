using BepInEx;
using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomPassives;
using System.ComponentModel;

namespace SorasToybox.Enemies
{
    public class FriendlyGearYinimro
    {
        public static void Add()
        {
            MoveToRandomEmptyTileEffect escapistEffect = ScriptableObject.CreateInstance<MoveToRandomEmptyTileEffect>();

            TimelineLengthDamageEffect ballisticGearDmg = ScriptableObject.CreateInstance<TimelineLengthDamageEffect>();


            AnimationVisualsEffect inertiaBreakVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            if (LoadedAssetsHandler.GetEnemy("Kcolclock_EN") != null)
            {
                inertiaBreakVis._visuals = LoadedAssetsHandler.GetEnemyAbility("KcolclockTock_A").visuals;
            } else
            {
                inertiaBreakVis._visuals = Visuals.Wriggle;
            }


            AnimationVisualsEffect ballisticGearVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            if (LoadedAssetsHandler.GetCharacter("Pike_CH") != null)
            {
                ballisticGearVis._visuals = LoadedAssetsHandler.GetCharacterAbility("PikeProtocol1").visuals;
            }
            else
            {
                ballisticGearVis._visuals = Visuals.WrigglingWrath;
            }


            Enemy friendlyGearYinimro = new Enemy("Gear Yinimro", "FriendlyGearYinimro_EN")
            {
                Health = 36,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineFriendlyGearYinimro", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("TimelineFriendlyGearYinimro", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("DeadGearYinimro", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                DeathSound = LoadedAssetsHandler.GetCharacter("Doll_CH").deathSound,
                UnitTypes = ["Friendly", "Zoincaillan", "Robot"],
            };


            friendlyGearYinimro.PrepareEnemyPrefab("Assets/ToyboxEnemies/Yinimro/Gear Yinimro Friend.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());

            StatusEffect_Apply_Effect overclockMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            overclockMe._Status = StatusField.GetCustomStatusEffect("Overclock_ID");

            Ability friendlyInertiaBreak = new Ability("Inertia Break", "ST_FriendlyInertiaBreak_A")
            {
                Description = "Moves to a random unoccupied position. 50% chance to gain 3 Overclock.",
                Rarity = Rarity.Common,
                Visuals = inertiaBreakVis._visuals,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(overclockMe, 3, Targeting.Slot_SelfSlot, Effects.ChanceCondition(50)),
                    Effects.GenerateEffect(escapistEffect, 1, Targeting.Slot_SelfSlot),
                ]
            };
            friendlyInertiaBreak.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", nameof(IntentType_GameIDs.Swap_Mass)]);

            //Ringading ding baby
            Ability friendGearYinimroBallisticGear = new Ability("Ballistic Gear", "ST_FriendlyBallisticGear_A")
            {
                Description = "Deals damage to the highest health enemy (excluding self) boosted by the Length of the timeline.",
                Rarity = Rarity.Impossible,
                Visuals = ballisticGearVis._visuals,
                AnimationTarget = Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false),
                Effects =
                [
                    Effects.GenerateEffect(ballisticGearDmg, 1, Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false)),
                ],
            };
            friendGearYinimroBallisticGear.AddIntentsToTarget(Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false), ["Damage_Timeline"]);

            QueueTimelineAbilityByNameEffect shootTheShit = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            shootTheShit._abilityName = "Ballistic Gear";

            //Setting up patient here
            PerformEffectPassiveAbility friendYinimroPatient = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            friendYinimroPatient.name = "ST_PatientFriendGearYinimro_PA";
            friendYinimroPatient._passiveName = "Ballistic Gear";
            friendYinimroPatient.m_PassiveID = "Patient";
            friendYinimroPatient.passiveIcon = ResourceLoader.LoadSprite("IconPatient");
            friendYinimroPatient._characterDescription = "party members can't queue into the timeline, ya know - maybe it could work like windup, though...";
            friendYinimroPatient._enemyDescription = "When the player turn ends, this enemy queues the ability \"Ballistic Gear\".";
            friendYinimroPatient._triggerOn = [TriggerCalls.OnPlayerTurnEnd_ForEnemy];
            friendYinimroPatient.effects = [
                Effects.GenerateEffect(shootTheShit, 1, Targeting.Slot_SelfSlot),
            ];


            friendlyGearYinimro.AddEnemyAbilities(
                [
                    friendlyInertiaBreak,
                    friendGearYinimroBallisticGear,
                ]);
            friendlyGearYinimro.AddPassives([Passives.Masochism1, Passives.Withering, friendYinimroPatient]);
            friendlyGearYinimro.AddEnemy(true, true, false);
        }
    }
}
