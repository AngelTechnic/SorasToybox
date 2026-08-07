using SorasToybox.CustomEffects;
using BepInEx;
using BrutalAPI;
using FMOD;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class FakeFaceless
    {
        public static void Add()
        {
            Enemy fakeless = new Enemy("Faceless", "Fakeless_EN")
            {
                Health = 35,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineFakeFaceless.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("timelineFacelessNoFace.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineFakeFaceless.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/NFacelessHurt",
                DeathSound = "event:/NFacelessDie",
            };

            PopupPassiveIconEffect popupPassiveIconEffect = ScriptableObject.CreateInstance<PopupPassiveIconEffect>();

            CasterTransformationEffect getPissedTheFuckOff = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            getPissedTheFuckOff._fullyHeal = true;
            getPissedTheFuckOff._maintainMaxHealth = false;
            getPissedTheFuckOff._currentToMaxHealth = false;
            getPissedTheFuckOff._maintainTimelineAbilities = false;
            getPissedTheFuckOff._enemyTransformation = LoadedAssetsHandler.GetEnemy("Crashout_EN");

            AnimationVisualsEffect headsplitter = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            headsplitter._visuals = Visuals.FeelTheRhythm;
            headsplitter._animationTarget = Targeting.Slot_SelfAll;

            PerformEffectPassiveAbility performEffectPassiveAbility = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            performEffectPassiveAbility._passiveName = "Quick-Tempered";
            performEffectPassiveAbility.m_PassiveID = "CrashoutPissed_PA";
            performEffectPassiveAbility.passiveIcon = ResourceLoader.LoadSprite("passive_quicktempered.png");
            performEffectPassiveAbility._enemyDescription = "On receiving any damage, this enemy completely loses it.";
            performEffectPassiveAbility._characterDescription = "This party member has a temper.";
            performEffectPassiveAbility.doesPassiveTriggerInformationPanel = false;
            //need to slip this in here real quick
            popupPassiveIconEffect._passiveToPopup = performEffectPassiveAbility;
            performEffectPassiveAbility.effects = new EffectInfo[]
            {
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CheckIsAliveEffect>(), 1, Targeting.Slot_SelfSlot, null),
                Effects.GenerateEffect(popupPassiveIconEffect, 1, Targeting.Slot_SelfSlot, ScriptableObject.CreateInstance<PreviousEffectCondition>()),
                Effects.GenerateEffect(headsplitter, 1, Targeting.Slot_SelfSlot,  ScriptableObject.CreateInstance<PreviousEffectCondition>()),
                Effects.GenerateEffect(getPissedTheFuckOff, 1, Targeting.Slot_SelfSlot, ScriptableObject.CreateInstance<PreviousEffectCondition>()),

                //Effects.GenerateEffect(add,1),
            };
            performEffectPassiveAbility._triggerOn = new TriggerCalls[]
            {
                TriggerCalls.OnDamaged,
            };
            Passives.AddCustomPassiveToPool("CrashoutPissed_PA", "Quick-Tempered", performEffectPassiveAbility);

            // The absolute agony that is Lockstep
            CasterStoreValueSetterEffect fuck = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            fuck.m_unitStoredDataID = "LockstepDir_SV";
            CasterStoreValueSetterEffect initialize = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            initialize.m_unitStoredDataID = "LockstepAmount_SV";
            initialize._ignoreIfContains = true;
            fakeless.CombatEnterEffects = [
                Effects.GenerateEffect(fuck, 1, Targeting.Slot_SelfSlot, null),
                Effects.GenerateEffect(initialize, 1, Targeting.Slot_SelfSlot),
            ];

            fakeless.enemy.abilities.Add(LoadedAssetsHandler.GetEnemy("Faceless_EN").abilities[0]);
            fakeless.AddPassives([Passives.GetCustomPassive("CrashoutPissed_PA"), CustomPassives.CustomPassive.SaltLockstepGenerator(1), Passives.Slippery, Passives.GetCustomPassive("Vandal_PA")]);
            fakeless.AddEnemy(false, false, false);
            LoadedAssetsHandler.GetEnemy("Fakeless_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Faceless_EN").enemyTemplate;
            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("Added Fakeless.");
            }
        }
    }
}
