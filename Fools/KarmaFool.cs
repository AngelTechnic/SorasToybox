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

            //Dismal popup
            PassivePopUpOnTargetEffect dismalPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            dismalPopup._name = "Dismal";
            dismalPopup._sprite = "passive_dismal";
            dismalPopup._isUnitCharacter = true;

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
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
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
            karma.AddPassives([Passives.GetCustomPassive("Karmic_PA"), Passives.GetCustomPassive("Dismal_PA")]);

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

            TargetPerformEffectViaSubaction hotsauce2Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            hotsauce2Effects.effects =
            [
                Effects.GenerateEffect(hotsauceVisuals, 1, Targeting.Slot_AllySides),
                Effects.GenerateEffect(frailMe, 1, Targeting.Slot_AllySides),
                Effects.GenerateEffect(overclockMe, 2, Targeting.Slot_AllySides),
                Effects.GenerateEffect(frailMe, 1, Targeting.Slot_SelfSlot)

            ];

            TargetPerformEffectViaSubaction hotsauce3Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            hotsauce3Effects.effects =
            [
                Effects.GenerateEffect(hotsauceVisuals, 1, Targeting.Slot_AllySides),
                Effects.GenerateEffect(frailMe, 2, Targeting.Slot_AllySides),
                Effects.GenerateEffect(overclockMe, 2, Targeting.Slot_AllySides),
                Effects.GenerateEffect(frailMe, 2, Targeting.Slot_SelfSlot)

            ];

            TargetPerformEffectViaSubaction hotsauce4Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            hotsauce4Effects.effects =
            [
                Effects.GenerateEffect(hotsauceVisuals, 1, Targeting.Slot_AllySides),
                Effects.GenerateEffect(frailMe, 3, Targeting.Slot_AllySides),
                Effects.GenerateEffect(overclockMe, 3, Targeting.Slot_AllySides),
                Effects.GenerateEffect(frailMe, 2, Targeting.Slot_SelfSlot)

            ];
            //Hotsauce ability (real no fake)
            Ability hotsauce1 = new Ability("Pour Some Mild Sauce", "KarmaHotSauce1_A")
            {
                Description = "Force the Opposing enemy to do the following:\nApply 1 Frail and 1 Overclock to the Left and Right allies, and 1 Frail to self.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_sauce.png"),
                Cost = [
                    Pigments.Blue,
                    Pigments.Blue,
                    Pigments.Red,
                    ],
                Effects =
                    [
                        Effects.GenerateEffect(hotsauce1Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(hotsauce1Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                    ]
            };
            hotsauce1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce1.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Overclock"]);
            hotsauce1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            Ability hotsauce2 = new Ability("Pour Some Mild Sauce", "KarmaHotSauce1_A")
            {
                Description = "Force the Opposing enemy to do the following:\nApply 1 Frail and 1 Overclock to the Left and Right allies, and 1 Frail to self.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_sauce.png"),
                Cost = [
                    Pigments.Blue,
                    Pigments.Blue,
                    Pigments.Red,
                    ],
                Effects =
                    [
                        Effects.GenerateEffect(hotsauce2Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(hotsauce2Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                    ]
            };
            hotsauce2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce2.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Overclock"]);
            hotsauce2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            Ability hotsauce3 = new Ability("Pour Some Mild Sauce", "KarmaHotSauce1_A")
            {
                Description = "Force the Opposing enemy to do the following:\nApply 1 Frail and 1 Overclock to the Left and Right allies, and 1 Frail to self.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_sauce.png"),
                Cost = [
                    Pigments.Blue,
                    Pigments.RedBlue,
                    Pigments.Red,
                    ],
                Effects =
                    [
                        Effects.GenerateEffect(hotsauce3Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(hotsauce3Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                    ]
            };
            hotsauce3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce3.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce3.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Overclock"]);
            hotsauce3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            Ability hotsauce4 = new Ability("Pour Some Mild Sauce", "KarmaHotSauce1_A")
            {
                Description = "Force the Opposing enemy to do the following:\nApply 1 Frail and 1 Overclock to the Left and Right allies, and 1 Frail to self.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_sauce.png"),
                Cost = [
                    Pigments.Blue,
                    Pigments.RedBlue,
                    Pigments.Red,
                    ],
                Effects =
                    [
                        Effects.GenerateEffect(hotsauce4Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(hotsauce4Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                    ]
            };
            hotsauce4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce4.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce4.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Overclock"]);
            hotsauce4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            karma.AddLevelData(22, [hotsauce1]); 
            karma.AddLevelData(27, [hotsauce2]);
            karma.AddLevelData(33, [hotsauce3]);
            karma.AddLevelData(40, [hotsauce4]);

            karma.AddCharacter(true, false);
        }
    }
}
