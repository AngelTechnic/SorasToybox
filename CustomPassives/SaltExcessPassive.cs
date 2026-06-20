using MonoMod.RuntimeDetour;
using SorasToybox;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using BrutalAPI;
using static UnityEngine.UI.CanvasScaler;
using SorasToybox.CustomPassives;

namespace SorasToybox
{
    public static class ExcessNotificationHook
    {
        public static void CalculateOverflow(Action<PlayerTurnEndSecondPartAction, CombatStats> orig, PlayerTurnEndSecondPartAction self, CombatStats stats)
        {
            bool startedInOverflow = stats.overflowMana.OverflowManaAmount > 0;
            orig(self, stats);
            if (startedInOverflow && stats.overflowMana.OverflowManaAmount <= 0)
            {
                foreach (CharacterCombat chara in CombatManager.Instance._stats.CharactersOnField.Values) CombatManager.Instance.PostNotification(OnExcessTriggered.ToString(), chara, null);
                foreach (EnemyCombat chara in CombatManager.Instance._stats.EnemiesOnField.Values) CombatManager.Instance.PostNotification(OnExcessTriggered.ToString(), chara, null);
            }
        }

        public static TriggerCalls OnExcessTriggered => (TriggerCalls)6682573;
        public static void Setup()
        {
            IDetour hook = new Hook(typeof(PlayerTurnEndSecondPartAction).GetMethod(nameof(PlayerTurnEndSecondPartAction.CalculateOverflow), ~BindingFlags.Default), typeof(ExcessNotificationHook).GetMethod(nameof(CalculateOverflow), ~BindingFlags.Default));
        }
    }

    public static class SaltExcessPassive
    {
        public static void Add()
        {
            ExcessNotificationHook.Setup();
            /*
            Ability template = new Ability("Template", "ExcessTemplate_A")
            {
                Description = "Does nothing. Change that, would you kindly?",
                Cost = [],
                Effects =
                [
                ],
                Rarity = Rarity.Common,
                Priority = Priority.VerySlow,
            };
            template.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Misc_Nothing"]);

            UseSpecificAbilityByEntryEffect queueacro = ScriptableObject.CreateInstance<UseSpecificAbilityByEntryEffect>();
            queueacro.usePrev = false;
            PerformEffectPassiveAbility excess = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            excess.name = "Excess_Template_PA";
            excess._passiveName = "Excess (Template)";
            excess.m_PassiveID = "Excess_PA";
            excess.passiveIcon = ResourceLoader.LoadSprite("passive_excess.png");
            excess._enemyDescription = "Whenever overflow is triggered, this enemy will queue the ability \"Template\".";
            excess._characterDescription = "nah";
            excess._triggerOn = [ExcessNotificationHook.OnExcessTriggered]; 
            excess.effects = [
                Effects.GenerateEffect(queueacro,1,Slots.Self),
                ];
            excess.doesPassiveTriggerInformationPanel = true;
            ExtraAbilityInfo extraAbilityInfo = new ExtraAbilityInfo();
            extraAbilityInfo.ability = template.ability;
            extraAbilityInfo.cost = new ManaColorSO[]
            {
                Pigments.Purple,
                Pigments.Purple
            };
            extraAbilityInfo.rarity = Rarity.ImpossibleNoReroll;
            queueacro._parentalAbility = extraAbilityInfo;
            Passives.AddCustomPassiveToPool("Excess_Template_PA", "Excess (Template)", excess);*/
        }
    }
}
