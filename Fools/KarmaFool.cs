using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Fools
{
    public class KarmaFool
    {
        public static void Add()
        {
            //Overclock application
            StatusEffect_Apply_Effect overclockMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            overclockMe._Status = StatusField.GetCustomStatusEffect("Overclock_ID");
            //Frail application
            StatusEffect_Apply_Effect frailMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            frailMe._Status = StatusField.Frail;
            //Ante application
            StatusEffect_Apply_Effect anteUp = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteUp._Status = StatusField.GetCustomStatusEffect("Ante_ID");
            //Cursed application
            StatusEffect_Apply_Effect fuckYouGetCursed = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            fuckYouGetCursed._Status = StatusField.Cursed;


            //Oh god there's gonna be so many friendly fire scripts i'm gonna dieeeeeeeeee
            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();


            //borrowing the yinyang animation OR AT LEAST TRYING TO! FUUUUUUCK!!!!
            //Note to self if salt ever adds yinyang to not a bonus attack, come back to this
            AttackVisualsSO rebalanceAnim = Visuals.Scales;
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                rebalanceAnim = LoadedAssetsHandler.GetEnemyAbility("Discord_A").visuals;
            }


            AnimationVisualsEffect rebalanceVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            rebalanceVisuals._visuals = rebalanceAnim;
            rebalanceVisuals._animationTarget = Targeting.Slot_SelfSlot;


            //anyway here's rebalance
            TargetPerformEffectViaSubaction rebalanceEffects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            rebalanceEffects.effects =
                [
                Effects.GenerateEffect(rebalanceVisuals, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(ScriptableObject.CreateInstance<CasterMoveStatusToTargetEffect>(), 1, Targeting.Slot_Front),
                ];

            //Wrong pigment conditions
            WrongPigmentedEffectCondition dismalTrue = ScriptableObject.CreateInstance<WrongPigmentedEffectCondition>();
            dismalTrue.trueIfWrongPig = true;

            WrongPigmentedEffectCondition dismalFalse = ScriptableObject.CreateInstance<WrongPigmentedEffectCondition>();
            dismalFalse.trueIfWrongPig = false;

            Ability rebalance = new Ability("Rebalance", "KarmaRebalance_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 1 damage to self. Transfer all status effects to the Opposing.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_rebalance.png"),
                Cost = [
                    Pigments.Yellow,
                    Pigments.Yellow,
                    ],
                Effects =
                    [
                        Effects.GenerateEffect(rebalanceEffects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(rebalanceEffects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                    ]

            };
            rebalance.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Misc)]);
            rebalance.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            //Character setup
            Character karma = new Character("Karma", "Karma_CH")
            {
                HealthColor = Pigments.Purple,
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = false,
                FrontSprite = ResourceLoader.LoadSprite("karma_front.png", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("karma_back.png", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("karma_overworld.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Fools/KarmaHurt",
                DeathSound = "event:/SorasSFX/Fools/KarmaDie",
                DialogueSound = "event:/SorasSFX/Fools/KarmaDX",
                BasicAbility = rebalance,
                UnitTypes = ["FemaleID", "Sandwich_Fire", "Angel", "Primal"],
            };
            //add passives!
            karma.AddPassives([Passives.GetCustomPassive("Karmic_PA")]);

            //Hot Sauce effect shenanigans
            AnimationVisualsEffect hotsauceVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            hotsauceVisuals._visuals = Visuals.OilSlicked;
            hotsauceVisuals._animationTarget = Targeting.Slot_SelfSlot;

            TargetPerformEffectViaSubaction hotsauce1Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            hotsauce1Effects.effects =
            [
                Effects.GenerateEffect(hotsauceVisuals, 1, Targeting.Slot_AllySides),
                Effects.GenerateEffect(frailMe, 1, Targeting.Slot_AllySides),
                Effects.GenerateEffect(overclockMe, 1, Targeting.Slot_AllySides),
                Effects.GenerateEffect(frailMe, 1, Targeting.Slot_SelfSlot)

            ];



            karma.AddLevelData(22, new Ability[] { }); 
            karma.AddLevelData(27, new Ability[] { });
            karma.AddLevelData(33, new Ability[] { });
            karma.AddLevelData(40, new Ability[] { });

            karma.AddCharacter(true, false);
        }
    }
}
