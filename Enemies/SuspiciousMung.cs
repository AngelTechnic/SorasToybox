using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace SorasToybox.Enemies
{
    public class SuspiciousMung
    {
        public static void Add()
        {


            AnimationVisualsEffect jumpscareHaha = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            jumpscareHaha._visuals = Visuals.DemonCore;
            jumpscareHaha._animationTarget = Targeting.Slot_SelfSlot;

            SpawnEnemyAnywhereEffect move = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            move.givesExperience = true;
            move.enemy = LoadedAssetsHandler.GetEnemy("Primus_EN");
            move._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            PerformEffectPassiveAbility susMungBecomesPrimusDecay = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            susMungBecomesPrimusDecay._passiveName = "Decay";
            susMungBecomesPrimusDecay.passiveIcon = Passives.Example_Decay_MudLung.passiveIcon;
            susMungBecomesPrimusDecay.m_PassiveID = Passives.Example_Decay_MudLung.m_PassiveID;
            susMungBecomesPrimusDecay._enemyDescription = "On death, this enemy dreams of the stars.";
            susMungBecomesPrimusDecay._characterDescription = "My God... it's full of stars...";
            susMungBecomesPrimusDecay._triggerOn = new TriggerCalls[] { TriggerCalls.OnDeath };
            susMungBecomesPrimusDecay.doesPassiveTriggerInformationPanel = true;
            susMungBecomesPrimusDecay.effects = new EffectInfo[]
            {
                Effects.GenerateEffect(jumpscareHaha, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(move, 1, Targeting.Slot_SelfSlot),
            };

            Enemy susMung = new Enemy("Mung", "SuspiciousMung_EN")
            {
                Health = 6,
                HealthColor = Pigments.Red,
                Size = 5,
                CombatSprite = ResourceLoader.LoadSprite("timelineSusMung.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineSusMung.png", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Mung_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Mung_EN").deathSound,
                UnitTypes = ["Fish"],
            };
            susMung.AddPassives(new BasePassiveAbilitySO[] { susMungBecomesPrimusDecay });


            susMung.enemy.abilities.Add(LoadedAssetsHandler.GetEnemy("Mung_EN").abilities[0]);
            susMung.enemy.abilities.Add(LoadedAssetsHandler.GetEnemy("Mung_EN").abilities[1]);

            susMung.AddEnemy(false, false, false);
            LoadedAssetsHandler.GetEnemy("SuspiciousMung_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Mung_EN").enemyTemplate;
            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("Added Suspicious Mung.");
            }
        }
    }
}
