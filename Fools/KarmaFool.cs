using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomEffects;
using SorasToybox.CustomStatuses;
using Yarn;

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
            //Regeneration application
            StatusEffect_Apply_Effect regenerateMe = ScriptableObject .CreateInstance<StatusEffect_Apply_Effect>();
            regenerateMe._Status = StatusField.GetCustomStatusEffect("Regen_ID");


            //Oh god there's gonna be so many friendly fire scripts i'm gonna dieeeeeeeeee
            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            DamageEffect agonyDamage = ScriptableObject.CreateInstance<DamageEffect>();
            agonyDamage._usePreviousExitValue = true;

            //borrowing the yinyang animation OR AT LEAST TRYING TO! FUUUUUUCK!!!!
            //Note to self if salt ever adds yinyang to not a bonus attack, come back to this
            AttackVisualsSO rebalanceAnim = Visuals.Scales;



            AnimationVisualsEffect rebalanceVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            rebalanceVisuals._visuals = rebalanceAnim;
            rebalanceVisuals._animationTarget = Targeting.Slot_SelfSlot;

            AnimationVisualsEffect goEffYourself = ScriptableObject .CreateInstance<AnimationVisualsEffect>();
            goEffYourself._visuals = Visuals.Insult;
            goEffYourself._animationTarget = Targeting.Slot_SelfSlot;


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

            //Excess popup in case i decide to implement that
            PassivePopUpOnTargetEffect excessPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            excessPopup._name = "Excess";
            excessPopup._sprite = "passive_excess";
            excessPopup._isUnitCharacter = true;

            //Sprites for when she gets mad that you whiffed cuz her normal moves do nothing if not opposing an enemy
            ExtraCCSprites_BasicSO karmaExtra = ScriptableObject.CreateInstance<ExtraCCSprites_BasicSO>();
            karmaExtra._DefaultID = "karma_default";
            karmaExtra._frontSprite = ResourceLoader.LoadSprite("karma_front_mad.png");
            karmaExtra._SpecialID = "karma_special";
            karmaExtra._backSprite = ResourceLoader.LoadSprite("karma_back.png");

            SetCasterExtraSpritesEffect karmaSprites = ScriptableObject.CreateInstance<SetCasterExtraSpritesEffect>();
            karmaSprites._ExtraSpriteID = karmaExtra._SpecialID;
            SetCasterExtraSpritesEffect karmaDefault = ScriptableObject.CreateInstance<SetCasterExtraSpritesEffect>();
            karmaDefault._ExtraSpriteID = karmaExtra._DefaultID;


            Ability rebalance = new Ability("Rebalance", "ST_KarmaRebalance_A")
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
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ]

            };
            rebalance.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_1_2), nameof(IntentType_GameIDs.Misc)]);
            rebalance.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            


            //Character setup
            Character karma = new Character("Karma", "Karma_CH")
            {
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Broken"),
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = false,
                FrontSprite = ResourceLoader.LoadSprite("karma_front.png", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("karma_back.png", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("karma_overworld.png", new Vector2(0.5f, 0f), 32),
                ExtraSprites = karmaExtra,
                DamageSound = "event:/SorasSFX/Fools/KarmaHurt",
                DeathSound = "event:/SorasSFX/Fools/KarmaDie",
                DialogueSound = "event:/SorasSFX/Fools/KarmaDX",
                BasicAbility = rebalance,
                UnitTypes = ["FemaleID", "Sandwich_Fire", "Angel", "Primal"],
            };
            karma.GenerateMenuCharacter(ResourceLoader.LoadSprite("karma_menu.png"), ResourceLoader.LoadSprite("karma_menu_locked.png"));
            //add passives!
            karma.AddPassives([Passives.GetCustomPassive("Karmic_PA"), Passives.GetCustomPassive("Dismal_PA")]);


            //Hot Sauce effect shenanigans
            AnimationVisualsEffect hotsauceVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            hotsauceVisuals._visuals = Visuals.OilSlicked;
            hotsauceVisuals._animationTarget = Targeting.Slot_SelfAll_AndSides;

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
            Ability hotsauce1 = new Ability("Pour Some Mild Sauce", "ST_KarmaHotSauce1_A")
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
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ]
            };
            hotsauce1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce1.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Overclock"]);
            hotsauce1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            Ability hotsauce2 = new Ability("Pour Some Medium Sauce", "ST_KarmaHotSauce2_A")
            {
                Description = "Force the Opposing enemy to do the following:\nApply 1 Frail and 2 Overclock to the Left and Right allies, and 1 Frail to self.",
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
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ]
            };
            hotsauce2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce2.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Overclock"]);
            hotsauce2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            Ability hotsauce3 = new Ability("Pour Some Hot Sauce", "ST_KarmaHotSauce3_A")
            {
                Description = "Force the Opposing enemy to do the following:\nApply 2 Frail and 2 Overclock to the Left and Right allies, and 2 Frail to self.",
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
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ]
            };
            hotsauce3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce3.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce3.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Overclock"]);
            hotsauce3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            Ability hotsauce4 = new Ability("Pour Some Inferno Sauce", "ST_KarmaHotSauce4_A")
            {
                Description = "Force the Opposing enemy to do the following:\nApply 3 Frail and 3 Overclock to the Left and Right allies, and 2 Frail to self.",
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
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ]
            };
            hotsauce4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce4.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Frail)]);
            hotsauce4.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Overclock"]);
            hotsauce4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);


            //Agony effect shenanigans
            AnimationVisualsEffect agonyVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            agonyVisuals._visuals = Visuals.HeartBreaker;
            agonyVisuals._animationTarget = Targeting.Slot_Front;

            TargetPerformEffectViaSubaction agony1Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            agony1Effects.effects =
                [
                    Effects.GenerateEffect(agonyVisuals, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(agonyDamage, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteUp, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteUp, 1, Targeting.Slot_Front),
                ];

            TargetPerformEffectViaSubaction agony2Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            agony2Effects.effects =
                [
                    Effects.GenerateEffect(agonyVisuals, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(agonyDamage, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteUp, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteUp, 1, Targeting.GenerateSlotTarget([-1, 0, 4])),
                ];

            TargetPerformEffectViaSubaction agony3Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            agony3Effects.effects =
                [
                    Effects.GenerateEffect(agonyVisuals, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(agonyDamage, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteUp, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteUp, 1, Targeting.GenerateSlotTarget([-1, 0, 4])),
                ];

            TargetPerformEffectViaSubaction agony4Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            agony4Effects.effects =
                [
                    Effects.GenerateEffect(agonyVisuals, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(agonyDamage, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteUp, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(anteUp, 1, Targeting.GenerateSlotTarget([-4, -1, 0, 1, 4])),
                ];

            Ability agony1 = new Ability("Find Joy in Agony", "ST_KarmaAgony1_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 4 damage to the Opposing, then damage self for the total damage dealt.\nApply 1 Ante to self and the Opposing.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_agony.png"),
                Cost = [
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Yellow,
                    Pigments.Yellow,
                    ],

                Effects =
                    [
                        Effects.GenerateEffect(agony1Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(agony1Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
            };
            agony1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6), ("Status_Ante")]);
            agony1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Damage_3_6)]);
            agony1.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Ante"]);
            agony1.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            Ability agony2 = new Ability("Find Delight in Agony", "ST_KarmaAgony2_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 5 damage to the Opposing, then damage self for the total damage dealt.\nApply 1 Ante to self and the Opposing and Left. This assumes the grid wraps around.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_agony.png"),
                Cost = [
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Yellow,
                    Pigments.Yellow,
                    ],

                Effects =
                    [
                        Effects.GenerateEffect(agony2Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(agony2Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
            };
            agony2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_3_6), ("Status_Ante")]);
            agony2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Damage_3_6)]);
            agony2.AddIntentsToTarget(Targeting.GenerateSlotTarget([-1, 0, 4], true), ["Status_Ante"]);
            agony2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);


            Ability agony3 = new Ability("Find Delight in Agony", "ST_KarmaAgony3_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 7 damage to the Opposing, then damage self for the total damage dealt.\nApply 1 Ante to self and the Opposing and Left. This assumes the grid wraps around.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_agony.png"),
                Cost = [
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Yellow,
                    Pigments.Yellow,
                    ],

                Effects =
                    [
                        Effects.GenerateEffect(agony3Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(agony3Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
            };
            agony3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_7_10), ("Status_Ante")]);
            agony3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Damage_7_10)]);
            agony3.AddIntentsToTarget(Targeting.GenerateSlotTarget([-1, 0, 4], true), ["Status_Ante"]);
            agony3.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);


            Ability agony4 = new Ability("Find Enlightenment in Agony", "ST_KarmaAgony4_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 7 damage to the Opposing, then damage self for the total damage dealt.\nApply 1 Ante to self and the Opposing, Left, and Right. This assumes the grid wraps around.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_agony.png"),
                Cost = [
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Yellow,
                    Pigments.Yellow,
                    ],

                Effects =
                    [
                        Effects.GenerateEffect(agony4Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(agony4Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
            };
            agony4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_7_10), ("Status_Ante")]);
            agony4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Damage_7_10)]);
            agony4.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, -1, 0, 1, 4], true), ["Status_Ante"]);
            agony4.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden)]);

            //What's Coming effect shenanigans
            AnimationVisualsEffect whatsComingVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            whatsComingVisuals._visuals = Visuals.Misery;
            whatsComingVisuals._animationTarget = Targeting.Slot_SelfAll;

            TargetPerformEffectViaSubaction whatsComing1Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            whatsComing1Effects.effects = 
            [
                Effects.GenerateEffect(whatsComingVisuals, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 10, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(fuckYouGetCursed, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(overclockMe, 1, Targeting.Slot_Front),
                Effects.GenerateEffect(regenerateMe, 5, Targeting.Slot_Front),
            ];

            TargetPerformEffectViaSubaction whatsComing2Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            whatsComing2Effects.effects =
            [
                Effects.GenerateEffect(whatsComingVisuals, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 13, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(fuckYouGetCursed, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(overclockMe, 1, Targeting.Slot_Front),
                Effects.GenerateEffect(regenerateMe, 7, Targeting.Slot_Front),
            ];

            TargetPerformEffectViaSubaction whatsComing3Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            whatsComing3Effects.effects =
            [
                Effects.GenerateEffect(whatsComingVisuals, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 16, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(fuckYouGetCursed, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(overclockMe, 2, Targeting.Slot_Front),
                Effects.GenerateEffect(regenerateMe, 8, Targeting.Slot_Front),
            ];

            TargetPerformEffectViaSubaction whatsComing4Effects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            whatsComing4Effects.effects =
            [
                Effects.GenerateEffect(whatsComingVisuals, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(damage, 19, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(fuckYouGetCursed, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(overclockMe, 2, Targeting.Slot_Front),
                Effects.GenerateEffect(regenerateMe, 10, Targeting.Slot_Front),
            ];

            Ability whatsComing1 = new Ability("Get What's Coming", "ST_KarmaWhatsComing1_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 10 damage to self and become Cursed. Apply 1 Overclock and 5 Regeneration to the Opposing.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_coming.png"),
                Cost = [
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Purple,
                    ],

                Effects =
                    [
                        Effects.GenerateEffect(whatsComing1Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(whatsComing1Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
            };
            whatsComing1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10), nameof(IntentType_GameIDs.Status_Cursed)]);
            whatsComing1.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", "Status_Regeneration"]);

            Ability whatsComing2 = new Ability("Deserve What's Coming", "ST_KarmaWhatsComing2_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 13 damage to self and become Cursed. Apply 1 Overclock and 7 Regeneration to the Opposing.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_coming.png"),
                Cost = [
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Purple,
                    ],

                Effects =
                    [
                        Effects.GenerateEffect(whatsComing2Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(whatsComing2Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
            };
            whatsComing2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_11_15), nameof(IntentType_GameIDs.Status_Cursed)]);
            whatsComing2.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", "Status_Regeneration"]);

            Ability whatsComing3 = new Ability("Own What's Coming", "ST_KarmaWhatsComing3_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 16 damage to self and become Cursed. Apply 2 Overclock and 8 Regeneration to the Opposing.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_coming.png"),
                Cost = [
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Purple,
                    ],

                Effects =
                    [
                        Effects.GenerateEffect(whatsComing3Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(whatsComing3Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
            };
            whatsComing3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_16_20), nameof(IntentType_GameIDs.Status_Cursed)]);
            whatsComing3.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", "Status_Regeneration"]);

            Ability whatsComing4 = new Ability("Accept What's Coming", "ST_KarmaWhatsComing4_A")
            {
                Description = "Force the Opposing enemy to do the following:\nDeal 19 damage to self and become Cursed. Apply 2 Overclock and 10 Regeneration to the Opposing.",
                AbilitySprite = ResourceLoader.LoadSprite("karma_coming.png"),
                Cost = [
                    Pigments.Red,
                    Pigments.Red,
                    Pigments.Purple,
                    ],

                Effects =
                    [
                        Effects.GenerateEffect(whatsComing4Effects, 1, Targeting.Slot_Front, dismalFalse),
                        Effects.GenerateEffect(dismalPopup, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(whatsComing4Effects, 1, Targeting.Slot_SelfSlot, dismalTrue),
                        Effects.GenerateEffect(goEffYourself, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [1, 3])),
                        Effects.GenerateEffect(karmaSprites, 1, Targeting.Slot_SelfSlot, Effects.CheckMultiplePreviousEffectsCondition([false, false], [2, 4])),
                        Effects.GenerateEffect(karmaDefault, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
            };
            whatsComing4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_16_20), nameof(IntentType_GameIDs.Status_Cursed)]);
            whatsComing4.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Overclock", "Status_Regeneration"]);



            karma.AddLevelData(22, [hotsauce1, agony1, whatsComing1]); 
            karma.AddLevelData(27, [hotsauce2, agony2, whatsComing2]);
            karma.AddLevelData(33, [hotsauce3, agony3, whatsComing3]);
            karma.AddLevelData(40, [hotsauce4, agony4, whatsComing4]);


            //karma.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "SorasToybox_Karma_Witness_ACH");
            //karma.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "SorasToybox_Karma_Divine_ACH");
            if (SorasToybox.CrossMod.EnemyPack) { karma.AddFinalBossAchievementData("DoulaBoss", "SorasToybox_Karma_Abstraction_ACH"); }
            if (SorasToybox.CrossMod.GlitchsFreaks) { karma.AddFinalBossAchievementData("March_BOSS", "SorasToybox_Karma_Inevitable_ACH"); }
            if (SorasToybox.CrossMod.IntoTheAbyss) { karma.AddFinalBossAchievementData("Nobody_BOSS", "SorasToybox_Karma_Forgotten_ACH"); }
            if (SorasToybox.CrossMod.IntoTheAbyss) { karma.AddFinalBossAchievementData("Katalixi_BOSS", "SorasToybox_Karma_Boundary_ACH"); }
            if (SorasToybox.CrossMod.SaltEnemies) { karma.AddFinalBossAchievementData("BlueSky_BOSS", "SorasToybox_Karma_Dreamer_ACH"); }
            karma.AddFinalBossAchievementData("Deathmatch_BOSS", "SorasToybox_Karma_Antagonist_ACH");
            karma.AddCharacter(true, false);

            //yapshenanigans
            SpeakerBundle speakerBundleKarma = new SpeakerBundle();
            speakerBundleKarma.bundleTextColor = new Color32(255, 0, 0, 255);
            speakerBundleKarma.dialogueSound = LoadedAssetsHandler.GetCharacter("Karma_CH").dxSound;
            speakerBundleKarma.portrait = ResourceLoader.LoadSprite("karma_front", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleKarmaBlank = new SpeakerBundle();
            speakerBundleKarmaBlank.bundleTextColor = new Color32(255, 0, 0, 255);
            speakerBundleKarmaBlank.dialogueSound = LoadedAssetsHandler.GetCharacter("Karma_CH").dxSound;
            speakerBundleKarmaBlank.portrait = ResourceLoader.LoadSprite("karma_back", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleKarmaMad = new SpeakerBundle();
            speakerBundleKarmaMad.bundleTextColor = new Color32(255, 0, 0, 255);
            speakerBundleKarmaMad.dialogueSound = LoadedAssetsHandler.GetCharacter("Karma_CH").damageSound;
            speakerBundleKarmaMad.portrait = ResourceLoader.LoadSprite("karma_front_mad", new Vector2(0.5f, 0f), 32);



            Dialogues.CreateAndAddCustom_SpeakerData("Karma", speakerBundleKarma, true, true, new SpeakerEmote[2]
            {
                new SpeakerEmote
                {
                    emotion = "Blank",
                    bundle = speakerBundleKarmaBlank,
                },
                new SpeakerEmote
                {
                    emotion = "Mad",
                    bundle = speakerBundleKarmaMad,
                },
            });
        }
    }
}
