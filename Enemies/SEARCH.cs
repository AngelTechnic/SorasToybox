using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;


namespace SorasToybox.Enemies
{
    public class SEARCH
    {
        public static void Add()
        {
            //Scars application
            StatusEffect_Apply_Effect applyScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            applyScars._Status = StatusField.Scars;
            
            //Shield application
            FieldEffect_Apply_Effect applyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            applyShield._Field = StatusField.Shield;

            //Basic damage
            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();
            damage._indirect = false;

            //Proportional damage to split self with. Also here's the hidden Pomni
            ProportionalDamageEffect propDamage = ScriptableObject.CreateInstance<ProportionalDamageEffect>();
            propDamage._indirect = false;




            Enemy search = new Enemy("SEARCH", "SEARCH_EN")
            {
                Health = 8,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineSEARCH.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineSEARCH", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Nothing",
                DeathSound = "event:/Nothing",
            };
            search.AddPassives([Passives.GetCustomPassive("SearchParty_ID")]);

            //Checking if still alive
            CheckIsAliveEffect amIAlive = ScriptableObject.CreateInstance<CheckIsAliveEffect>();


            //The actual becoming two effect, defined after the enemy is defined.
            SpawnEnemyAnywhereWithHealthByPreviousEffect splitMe = ScriptableObject.CreateInstance<SpawnEnemyAnywhereWithHealthByPreviousEffect>();
            splitMe.enemy = search.enemy;
            splitMe._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();



            //SPLIT ability
            Ability searchSplit = new Ability("SPLIT", "SEARCHSplit_A")
            {
                Description = "Deals direct damage to this unit equal to half of its current health.If it survives, spawn an exact copy of this unit.",
                Cost = [Pigments.Purple, Pigments.Red, Pigments.Blue,],
                Visuals = Visuals.Mitosis,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(propDamage, 50, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(amIAlive, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(splitMe, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Normal
            };
            searchSplit.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Damage_Prop", nameof(IntentType_GameIDs.Other_Spawn)]);

            AnimationVisualsEffect shardsliverVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            shardsliverVisuals._visuals = Visuals.Crush;
            shardsliverVisuals._animationTarget = Targeting.Slot_Front;

            SwapToOneSideEffect swapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapLeft._swapRight = false;

            SwapToOneSideEffect swapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapRight._swapRight = true;

            //SHARD ability
            Ability searchShard = new Ability("SHARD", "SEARCHShard_A")
            {
                Description = "Moves Left. Deals a Painful amount of damage to the Opposing party member.",
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shardsliverVisuals, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 3, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal
            };
            searchShard.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            searchShard.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Damage_3_6)]);

            //SLIVER ability
            Ability searchSliver = new Ability("SLIVER", "SEARCHSliver_A")
            {
                Description = "Moves Right. Deals a Painful amount of damage to the Opposing party member.",
                Cost = [Pigments.Red, Pigments.Red],
                Effects =
                [
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(shardsliverVisuals, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 3, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal
            };
            searchShard.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            searchShard.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Damage_3_6)]);

            //SHELL ability
            Ability searchShell = new Ability("SHELL", "SEARCHShell_A")
            {
                Description = "Applies 3 Shield to the Left and Right positions.",
                Cost = [Pigments.Red, Pigments.Blue],
                Visuals = Visuals.Shield,
                AnimationTarget = Targeting.Slot_AllySides,
                Effects =
                [
                    Effects.GenerateEffect(applyShield, 3, Targeting.Slot_AllySides),
                ],
                Rarity = Rarity.Uncommon,
                Priority = Priority.Normal
            };
            searchShell.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Field_Shield)]);

            //setting up healing cuz i forgor :skull:
            HealEffect heal = ScriptableObject.CreateInstance<HealEffect>();
            heal._directHeal = true;

            //SUBSTANCE ability
            Ability searchSubstance = new Ability("SUBSTANCE", "SEARCHSubstance_A")
            {
                Description = "Scars and slightly heals all enemies.",
                Cost = [Pigments.Blue, Pigments.Blue],
                Visuals = Visuals.Genesis,
                AnimationTarget = Targeting.Unit_AllAllies,
                Effects =
                [
                    Effects.GenerateEffect(applyScars, 1, Targeting.Unit_AllAllies),
                    Effects.GenerateEffect(heal, 2, Targeting.Unit_AllAllies),
                ],
                Rarity = Rarity.Rare,
                Priority = Priority.Normal
            };
            searchSubstance.AddIntentsToTarget(Targeting.Unit_AllAllies, [nameof(IntentType_GameIDs.Status_Scars), nameof(IntentType_GameIDs.Heal_1_4)]);

            search.AddEnemyAbilities
            (
                [
                    searchSplit, searchShard, searchSliver, searchShell, searchSubstance,
                ]
            );

            search.AddEnemy(true, true, true);
            LoadedAssetsHandler.GetEnemy("SEARCH_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("JumbleGuts_Hollowing_EN").enemyTemplate;
        }
    }
}
