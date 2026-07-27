using BrutalAPI;
using SorasToybox;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using SorasToybox.CustomStatuses;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Yarn;
using static UnityEngine.LightProbeProxyVolume;

namespace SorasToybox.Fools
{
    public class WhhvayFool
    {
        public static void Add()
        {
            //Irradiated applicaiton
            StatusEffect_Apply_Effect getIrradiated = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getIrradiated._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");

            StatusEffect_Apply_Effect irradiatedByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            irradiatedByPrevious._Status = StatusField.GetCustomStatusEffect("Irradiated_ID");
            irradiatedByPrevious._MultPreviousExitValueForEntry = true;

            ChangeMaxHealthEffect reduceHealth = ScriptableObject.CreateInstance<ChangeMaxHealthEffect>();
            reduceHealth._increase = false;

            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            Character whhvay = new Character("Whhvay", "Whhvay_CH")
            {
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"),
                UsesBasicAbility = true,
                UsesAllAbilities = false,
                MovesOnOverworld = true,
                FrontSprite = ResourceLoader.LoadSprite("whhvay_front.png", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("whhvay_back.png", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("whhvay_overworld.png", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Leviat_CH").dxSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Leviat_CH").deathSound,
                DialogueSound = LoadedAssetsHandler.GetCharacter("Leviat_CH").dxSound,
                UnitTypes = ["FemaleID", "MaleID", "Zoincaillan",],
                StartsLocked = false,
            };
            whhvay.AddPassives([Passives.GetCustomPassive("ST_Godray_PA")]);
            whhvay.GenerateMenuCharacter(ResourceLoader.LoadSprite("whhvay_menu.png"), ResourceLoader.LoadSprite("whhvay_menu_locked.png"));

            //Detacher/Dismantler/Destabilizer/Devourer of Bonds: Reduce the Opposing enemy's health by 5/7/10/12, then deal 4/6/8/10 damage to them. Inflict 2/3/4/5 Irradiated on the Left and Right enemies.
            Ability bonds1 = new Ability("Detacher of Bonds", "ST_WhhvayBonds1_A")
            {
                Description = "Reduce the Opposing enemy's max health by 5, then deal 4 damage to them.\nInflict 2 Irradiated on the Left and Right enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_injection.png"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(reduceHealth, 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_OpponentSides),
                ],
            };
            bonds1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_MaxHealth), nameof(IntentType_GameIDs.Damage_7_10)]);
            bonds1.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Irradiated"]);

            Ability bonds2 = new Ability("Dismantler of Bonds", "ST_WhhvayBonds2_A")
            {
                Description = "Reduce the Opposing enemy's max health by 7, then deal 6 damage to them.\nInflict 3 Irradiated on the Left and Right enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_injection.png"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(reduceHealth, 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 6, Targeting.Slot_Front),
                    Effects.GenerateEffect(getIrradiated, 3, Targeting.Slot_OpponentSides),
                ],
            };
            bonds2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_MaxHealth), nameof(IntentType_GameIDs.Damage_11_15)]);
            bonds2.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Irradiated"]);

            Ability bonds3 = new Ability("Destabilizer of Bonds", "ST_WhhvayBonds3_A")
            {
                Description = "Reduce the Opposing enemy's max health by 10, then deal 8 damage to them.\nInflict 4 Irradiated on the Left and Right enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_injection.png"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(reduceHealth, 10, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(getIrradiated, 4, Targeting.Slot_OpponentSides),
                ],
            };
            bonds3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_MaxHealth), nameof(IntentType_GameIDs.Damage_16_20)]);
            bonds3.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Irradiated"]);

            Ability bonds4 = new Ability("Devourer of Bonds", "ST_WhhvayBonds4_A")
            {
                Description = "Reduce the Opposing enemy's max health by 12, then deal 10 damage to them.\nInflict 5 Irradiated on the Left and Right enemies.",
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_injection.png"),
                Cost = [Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = Visuals.Melt,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(reduceHealth, 12, Targeting.Slot_Front),
                    Effects.GenerateEffect(damage, 10, Targeting.Slot_Front),
                    Effects.GenerateEffect(getIrradiated, 5, Targeting.Slot_OpponentSides),
                ],
            };
            bonds4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_MaxHealth), nameof(IntentType_GameIDs.Damage_21)]);
            bonds4.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Irradiated"]);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("bonds");
            }
            HealEffect heal = ScriptableObject.CreateInstance<HealEffect>();

            Sprite abilitySprite = LoadedAssetsHandler.GetEnemyAbility("Chomp_A").abilitySprite;

            //Piking it
            StatusEffect_Apply_Effect getMisery = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getMisery._Status = StatusField.GetCustomStatusEffect("Misery_ID");

            Ability bonus1 = new Ability("Mutated Surface", "WhhvayBonus1_A")
            {
                Description = "Heal the Opposing party member 3 health, and gain 1 Misery.\n40% Chance to inflict 2 Irradiated on the Left and Right enemies.",
                Cost = [Pigments.Blue],
                AbilitySprite = abilitySprite,
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(getMisery, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_AllySides, Effects.ChanceCondition(40)),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            bonus1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_1_4)]);
            bonus1.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Misery"]);
            bonus1.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            Ability bonus2 = new Ability("Mutated Complexion", "WhhvayBonus2_A")
            {
                Description = "Heal the Opposing party member 5 health, and gain 2 Misery.\n50% Chance to inflict 2 Irradiated on the Left and Right enemies.",
                Cost = [Pigments.Blue],
                AbilitySprite = abilitySprite,
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(getMisery, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_AllySides, Effects.ChanceCondition(50)),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            bonus2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10)]);
            bonus2.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Misery"]);
            bonus2.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            Ability bonus3 = new Ability("Mutated Expression", "WhhvayBonus3_A")
            {
                Description = "Heal the Opposing party member 7 health, and gain 3 Misery.\n66% Chance to inflict 2 Irradiated on the Left and Right enemies.",
                Cost = [Pigments.Blue],
                AbilitySprite = abilitySprite,
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(getMisery, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_AllySides, Effects.ChanceCondition(66)),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            bonus3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10)]);
            bonus3.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Misery"]);
            bonus3.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            Ability bonus4 = new Ability("Mutated Perspective", "WhhvayBonus4_A")
            {
                Description = "Heal the Opposing party member 9 health, and gain 4 Misery.\nInflict 2 Irradiated on the Left and Right enemies.",
                Cost = [Pigments.Blue],
                AbilitySprite = abilitySprite,
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 9, Targeting.Slot_Front),
                    Effects.GenerateEffect(getMisery, 4, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_AllySides),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            bonus4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10)]);
            bonus4.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Misery"]);
            bonus4.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            ExtraAbilityInfo extra1 = new()
            {
                ability = bonus1.ability,
                //priority = Priority.ExtremelyFast,
                rarity = Rarity.ImpossibleNoReroll,
            };
            ExtraAbilityInfo extra2 = new()
            {
                ability = bonus2.ability,
                //priority = Priority.ExtremelyFast,
                rarity = Rarity.ImpossibleNoReroll,
            };
            ExtraAbilityInfo extra3 = new()
            {
                ability = bonus3.ability,
                //priority = Priority.ExtremelyFast,
                rarity = Rarity.ImpossibleNoReroll,
            };
            ExtraAbilityInfo extra4 = new()
            {
                ability = bonus4.ability,
                //priority = Priority.ExtremelyFast,
                rarity = Rarity.ImpossibleNoReroll,
            };

            AddPassiveEffect add1 = ScriptableObject.CreateInstance<AddPassiveEffect>();
            add1._passiveToAdd = Passives.BonusAttackGenerator(extra1);

            AddPassiveEffect add2 = ScriptableObject.CreateInstance<AddPassiveEffect>();
            add2._passiveToAdd = Passives.BonusAttackGenerator(extra2);
            AddPassiveEffect add3 = ScriptableObject.CreateInstance<AddPassiveEffect>();
            add3._passiveToAdd = Passives.BonusAttackGenerator(extra3);
            AddPassiveEffect add4 = ScriptableObject.CreateInstance<AddPassiveEffect>();
            add4._passiveToAdd = Passives.BonusAttackGenerator(extra4);

            PopupPassiveIconEffect do1 = ScriptableObject.CreateInstance<PopupPassiveIconEffect>();
            do1._passiveToPopup = add1._passiveToAdd;
            PopupPassiveIconEffect do2 = ScriptableObject.CreateInstance<PopupPassiveIconEffect>();
            do2._passiveToPopup = add2._passiveToAdd;
            PopupPassiveIconEffect do3 = ScriptableObject.CreateInstance<PopupPassiveIconEffect>();
            do3._passiveToPopup = add3._passiveToAdd;
            PopupPassiveIconEffect do4 = ScriptableObject.CreateInstance<PopupPassiveIconEffect>();
            do4._passiveToPopup = add4._passiveToAdd;

            TargetPerformEffectViaSubaction salvationSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            salvationSubAct.effects =
                [
                    Effects.GenerateEffect(add1, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(do1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(reduceHealth, 2, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                ];

            AnimationVisualsEffect microwaveVisual = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            microwaveVisual._visuals = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_Radiotherapy_A").visuals;
            microwaveVisual._animationTarget = Targeting.Slot_Front;

            AnimationVisualsEffect microwaveVisual2 = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            microwaveVisual2._visuals = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_Radiotherapy_A").visuals;
            microwaveVisual2._animationTarget = Targeting.Slot_FrontAndSides;

            //Salvation Lies Within/Ahead/Beyond/Above: Force all enemies with status effects to queue up the ability "Mutated Complexion"/twice/thrice/four times. If this fails, inflict the Opposing enemy with 2/2/3/4 Irradiated.
            Ability salvation1 = new Ability("Salvation Lies Within", "ST_WhhvaySalvation1_A")
            {
                Description = "Force all enemies with status effects to learn the Bonus Attack \"Mutated Surface\"; if that fails, they lose 2 max health.\nInflict the Opposing enemy with 2 Irradiated.",
                Visuals = Visuals.Scream,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_charge.png"),
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Blue],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_All_Status,
                Effects =
                [
                    Effects.GenerateEffect(salvationSubAct, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(microwaveVisual, 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_Front),
                ],
            };
            salvation1.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_All_Status, [nameof(IntentType_GameIDs.Other_Refresh)]);
            salvation1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("salv 1");
            }

            TargetPerformEffectViaSubaction salvation2SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            salvation2SubAct.effects =
                [
                    Effects.GenerateEffect(add2, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(do2, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(reduceHealth, 3, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                ];
            Ability salvation2 = new Ability("Salvation Lies Ahead", "ST_WhhvaySalvation2_A")
            {
                Description = "Force all enemies with status effects to learn the Bonus Attack \"Mutated Complexion\"; if that fails, they lose 3 max health.\nInflict the Opposing enemy with 2 Irradiated.",
                Visuals = Visuals.Scream,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_charge.png"),
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Blue],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_All_Status,
                Effects =
                [
                    Effects.GenerateEffect(salvation2SubAct, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(microwaveVisual, 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_Front),
                ],
            };
            salvation2.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_All_Status, [nameof(IntentType_GameIDs.Other_Refresh)]);
            salvation2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("salv 2");
            }

            TargetPerformEffectViaSubaction salvation3SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            salvation3SubAct.effects =
                [
                    Effects.GenerateEffect(add3, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(do1, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(reduceHealth, 4, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                ];
            Ability salvation3 = new Ability("Salvation Lies Beyond", "ST_WhhvaySalvation3_A")
            {
                Description = "Force all enemies with status effects to learn the Bonus Attack \"Mutated Expression\"; if that fails, they lose 4 max health.\nInflict the Opposing enemy with 3 Irradiated and the Left and Right enemies with 1.",
                Visuals = Visuals.Scream,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_charge.png"),
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Blue],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_All_Status,
                Effects =
                [
                    Effects.GenerateEffect(salvation3SubAct, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(microwaveVisual2, 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(getIrradiated, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(getIrradiated, 1, Targeting.Slot_OpponentSides),
                ],
            };
            salvation3.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_All_Status, [nameof(IntentType_GameIDs.Other_Refresh)]);
            salvation3.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("salv 3");
            }
            TargetPerformEffectViaSubaction salvation4SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            salvation4SubAct.effects =
                [
                    Effects.GenerateEffect(add4, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(do4, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(reduceHealth, 5, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                ];
            Ability salvation4 = new Ability("Salvation Lies Above", "ST_WhhvaySalvation4_A")
            {
                Description = "Force all enemies with status effects to learn the Bonus Attack \"Mutated Perspective\"; if that fails, they lose 5 max health.\nInflict the Opposing enemy with 4 Irradiated and the Left and Right enemies with 2.",
                Visuals = Visuals.Scream,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_charge.png"),
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Blue],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_All_Status,
                Effects =
                [
                    Effects.GenerateEffect(salvation4SubAct, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(microwaveVisual2, 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(getIrradiated, 4, Targeting.Slot_Front),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_OpponentSides),
                ],
            };
            salvation4.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_All_Status, [nameof(IntentType_GameIDs.Other_Refresh)]);
            salvation4.AddIntentsToTarget(Targeting.Slot_FrontAndSides, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("salvation");
            }

            //Deal 3/4/5/6 damage to the Left and Right enemies, boosted by their amount of Irradiated, then trim all excess health from them. Make them inflict 1/1/2/3 Irradiated to themselves and their Left and Right allies.
            ChangeMaxHealthByCurrentHealthEffect trimHealth = ScriptableObject.CreateInstance<ChangeMaxHealthByCurrentHealthEffect>();

            DamageWithStatusBonusEffect radDamage = ScriptableObject.CreateInstance<DamageWithStatusBonusEffect>();
            radDamage._status = StatusField.GetCustomStatusEffect("Irradiated_ID");
            radDamage._bonusAmount = 1;
            radDamage._bonusStacking = true;

            TargetPerformEffectViaSubaction warp1SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            warp1SubAct.effects =
                [
                    Effects.GenerateEffect(getIrradiated, 1, Targeting.Slot_SelfAndSides),
                ];

            Ability warp1 = new Ability("Warp Essence", "ST_WhhvayWarp1_A")
            {
                Description = "Deal 3 damage to the Left and Right enemies, boosted by their amount of Irradiated, then trim all excess health from them.\nMake them inflict 1 Irradiated to themselves and their Left and Right allies.",
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = LoadedAssetsHandler.GetEnemyAbility("taneFavor_A").visuals,
                AnimationTarget = Targeting.Slot_OpponentSides,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_cytoplastic.png"),
                Effects =
                [
                    Effects.GenerateEffect(radDamage, 3, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(trimHealth, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(warp1SubAct, 1, Targeting.Slot_OpponentSides),
                ],
            };
            warp1.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Other_MaxHealth_Alt), "Status_Irradiated"]);

            TargetPerformEffectViaSubaction warp2SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            warp2SubAct.effects =
                [
                    Effects.GenerateEffect(getIrradiated, 1, Targeting.Slot_SelfAndSides),
                ];

            Ability warp2 = new Ability("Warp Matter", "ST_WhhvayWarp2_A")
            {
                Description = "Deal 5 damage to the Left and Right enemies, boosted by their amount of Irradiated, then trim all excess health from them.\nMake them inflict 1 Irradiated to themselves and their Left and Right allies.",
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = LoadedAssetsHandler.GetEnemyAbility("taneFavor_A").visuals,
                AnimationTarget = Targeting.Slot_OpponentSides,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_cytoplastic.png"),
                Effects =
                [
                    Effects.GenerateEffect(radDamage, 5, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(trimHealth, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(warp2SubAct, 1, Targeting.Slot_OpponentSides),
                ],
            };
            warp2.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Other_MaxHealth_Alt), "Status_Irradiated"]);


            TargetPerformEffectViaSubaction warp3SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            warp3SubAct.effects =
                [
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_SelfAndSides),
                ];

            Ability warp3 = new Ability("Warp Energy", "ST_WhhvayWarp3_A")
            {
                Description = "Deal 6 damage to the Left and Right enemies, boosted by their amount of Irradiated, then trim all excess health from them.\nMake them inflict 2 Irradiated to themselves and their Left and Right allies.",
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = LoadedAssetsHandler.GetEnemyAbility("taneFavor_A").visuals,
                AnimationTarget = Targeting.Slot_OpponentSides,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_cytoplastic.png"),
                Effects =
                [
                    Effects.GenerateEffect(radDamage, 6, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(trimHealth, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(warp3SubAct, 1, Targeting.Slot_OpponentSides),
                ],
            };
            warp3.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Other_MaxHealth_Alt), "Status_Irradiated"]);

            TargetPerformEffectViaSubaction warp4SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            warp4SubAct.effects =
                [
                    Effects.GenerateEffect(getIrradiated, 3, Targeting.Slot_SelfAndSides),
                ];

            Ability warp4 = new Ability("Warp What Makes Them Real", "ST_WhhvayWarp4_A")
            {
                Description = "Deal 7 damage to the Left and Right enemies, boosted by their amount of Irradiated, then trim all excess health from them.\nMake them inflict 3 Irradiated to themselves and their Left and Right allies.",
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Red, Pigments.Red],
                Visuals = LoadedAssetsHandler.GetEnemyAbility("taneFavor_A").visuals,
                AnimationTarget = Targeting.Slot_OpponentSides,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_cytoplastic.png"),
                Effects =
                [
                    Effects.GenerateEffect(radDamage, 7, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(trimHealth, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(warp4SubAct, 1, Targeting.Slot_OpponentSides),
                ],
            };
            warp4.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Damage_7_10), nameof(IntentType_GameIDs.Other_MaxHealth_Alt), "Status_Irradiated"]);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("warp");
            }

            whhvay.AddLevelData(4, [bonds1, salvation1, warp1]);
            whhvay.AddLevelData(5, [bonds2, salvation2, warp2]);
            whhvay.AddLevelData(6, [bonds3, salvation3, warp3]);
            whhvay.AddLevelData(7, [bonds4, salvation4, warp4]);

            //cheevos
            //whhvay.AddFinalBossAchievementData(BossType_GameIDs.OsmanSinnoks.ToString(), "SorasToybox_Whhvay_Witness_ACH");
            //whhvay.AddFinalBossAchievementData(BossType_GameIDs.Heaven.ToString(), "SorasToybox_Whhvay_Divine_ACH");
            //if (SorasToybox.CrossMod.EnemyPack) { whhvay.AddFinalBossAchievementData("DoulaBoss", "SorasToybox_Whhvay_Abstraction_ACH"); }
            //if (SorasToybox.CrossMod.GlitchsFreaks) { whhvay.AddFinalBossAchievementData("March_BOSS", "SorasToybox_Whhvay_Inevitable_ACH"); }
            //if (SorasToybox.CrossMod.IntoTheAbyss) { whhvay.AddFinalBossAchievementData("Nobody_BOSS", "SorasToybox_Whhvay_Forgotten_ACH"); }
            //if (SorasToybox.CrossMod.IntoTheAbyss) { whhvay.AddFinalBossAchievementData("Katalixi_BOSS", "SorasToybox_Whhvay_Boundary_ACH"); }
            //if (SorasToybox.CrossMod.SaltEnemies) { whhvay.AddFinalBossAchievementData("BlueSky_BOSS", "SorasToybox_Whhvay_Dreamer_ACH"); }
            //whhvay.AddFinalBossAchievementData("Deathmatch_BOSS", "SorasToybox_Whhvay_Antagonist_ACH");
            whhvay.AddCharacter(true, false);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Avenger.");
            }

            //yapshenanigans
            SpeakerBundle speakerBundleWhhvay = new SpeakerBundle();
            speakerBundleWhhvay.bundleTextColor = new Color32(0, 255, 255, 255);
            speakerBundleWhhvay.dialogueSound = LoadedAssetsHandler.GetCharacter("Whhvay_CH").dxSound;
            speakerBundleWhhvay.portrait = ResourceLoader.LoadSprite("whhvay_fronttalk", new Vector2(0.5f, 0f), 32);

            Dialogues.CreateAndAddCustom_SpeakerData("Whhvay", speakerBundleWhhvay, true, true, new SpeakerEmote[0]);

        }
    }
}
