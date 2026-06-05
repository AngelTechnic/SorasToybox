using System;
using System.Reflection;
using BrutalAPI;
using SorasToybox;
using SorasToybox.CustomEffects;
using UnityEngine;


namespace SorasToybox.Enemies
{
    public class Deathmatch
    {
        public static void Add()
        {
            AddPassiveEffect youAndMeBabyAintNothinButMammals = ScriptableObject.CreateInstance<AddPassiveEffect>();
            youAndMeBabyAintNothinButMammals._passiveToAdd = Passives.GetCustomPassive("Mammal_PA");

            StatusEffectCheckerEffect hasAnte = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasAnte._status = StatusField.GetCustomStatusEffect("Ante_ID");

            StatusEffect_Apply_Effect anteByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteByPrevious._MultPreviousExitValueForEntry = true;
            anteByPrevious._Status = StatusField.GetCustomStatusEffect("Ante_ID");


            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            DirectDeathEffect hangman = ScriptableObject.CreateInstance<DirectDeathEffect>();

            //subactions for deathmatch
            AnimationVisualsEffect judgementVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            judgementVis._visuals = Visuals.Decimate;
            judgementVis._animationTarget = Targeting.Slot_AllyLeft;


            TargetPerformEffectViaSubaction judgementEffect = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            judgementEffect.effects =
            [
                Effects.GenerateEffect(judgementVis, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 2, Targeting.Slot_AllyLeft),
                Effects.GenerateEffect(anteByPrevious, 1, Targeting.Slot_SelfSlot)
            ];

            AnimationVisualsEffect prosecutionVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            prosecutionVis._visuals = Visuals.Decimate;
            prosecutionVis._animationTarget = Targeting.Slot_AllyRight;

            TargetPerformEffectViaSubaction prosecutionEffect = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            prosecutionEffect.effects =
            [
                Effects.GenerateEffect(prosecutionVis, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 2, Targeting.Slot_AllyRight),
                Effects.GenerateEffect(anteByPrevious, 1, Targeting.Slot_SelfSlot)
            ];

            AnimationVisualsEffect trialErrorVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            trialErrorVis._visuals = Visuals.RendRight;
            trialErrorVis._animationTarget = Targeting.Slot_SelfSlot;

            TargetPerformEffectViaSubaction trialerrorEffect = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            trialerrorEffect.effects =
            [
                Effects.GenerateEffect(trialErrorVis, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(anteByPrevious, 1, Targeting.Slot_SelfSlot),
            ];


            //Move it!
            SwapToOneSideEffect swapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapLeft._swapRight = false;

            SwapToOneSideEffect swapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapRight._swapRight = true;


            //Instinct passive and whatnot
            PerformEffectPassiveAbility deathmatchInstinct = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            deathmatchInstinct._passiveName = "Instinct";
            deathmatchInstinct.m_PassiveID = "DMInstinct_PA";
            deathmatchInstinct._enemyDescription = "On entering combat, apply Mammal as a passive to all party members.";
            deathmatchInstinct.passiveIcon = ResourceLoader.LoadSprite("Instinct.png");
            deathmatchInstinct._triggerOn = [TriggerCalls.OnCombatStart];
            deathmatchInstinct.effects = [Effects.GenerateEffect(youAndMeBabyAintNothinButMammals, 1, Targeting.Unit_AllOpponents)];


            //Basic unit setup
            Enemy deathmatchEnemy = new Enemy("Deathmatch", "Deathmatch_BOSS")
            {
                Health = 1024,
                HealthColor = Pigments.Red,
                CombatSprite = ResourceLoader.LoadSprite("timelineDeathmatch", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineDeathmatch", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Lilith_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Lilith_CH").deathSound,
                UnitTypes = ["FemaleID"],
            };
            //deathmatch has no gibs rn
            deathmatchEnemy.PrepareEnemyPrefab("Assets/ToyboxEnemies/Deathmatch/Deathmatch Boss.prefab", SorasToybox.assetbundle, null);







            Ability judgement = new Ability("Judgement", "ST_DMJudgement_A")
            {
                Description = "Makes the Opposing party member deal a little damage to their left party member, then gain Ante equal to the result. Moves Left.",
                Effects =
                [
                    Effects.GenerateEffect(judgementEffect, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,

            };
            judgement.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Damage_1_2)]);
            judgement.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Ante"]);

            Ability prosecution = new Ability("Prosecution", "ST_DMProsecution_A")
            {
                Description = "Makes the Opposing party member deal a little damage to their right party member, then gain Ante equal to the result. Moves Right.",
                Effects =
                [
                    Effects.GenerateEffect(prosecutionEffect, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Uncommon,
            };
            prosecution.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Damage_1_2)]);
            prosecution.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Ante"]);

            Ability trial = new Ability("Trial", "ST_DMTrial_A")
            {
                Description = "Moves the Left party member to the Right, then makes the Opposing party member deal almost no damage to themselves, gaining Ante equal to the result.\r\n",
                Rarity = Rarity.VeryRare,
                Effects =
                [
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(trialerrorEffect, 1, Targeting.Slot_Front),
                ],
            };

            Ability error = new Ability("Error", "ST_DMError_A")
            {
                Description = "Moves the Right party member to the Left, then makes the Opposing party member deal almost no damage to themselves, gaining Ante equal to the result.\r\n",
                Rarity = Rarity.VeryRare,
                Effects =
                [
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(trialerrorEffect, 1, Targeting.Slot_Front),
                ],
            };

            deathmatchEnemy.AddPassives([Passives.GetCustomPassive("BrokenBlooded_1_PA")]);
        }
    }
}
