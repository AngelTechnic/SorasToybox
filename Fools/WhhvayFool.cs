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



            HealEffect heal = ScriptableObject.CreateInstance<HealEffect>();

            Ability mutatedComplexion = new Ability("Mutated Complexion", "ST_MutatedComplexion_A")
            {
                Description = "Heal the Opposing party member(s) 2 health.\n50% chance to inflict 2 Irradiated on the Left and Right allies.",
                Rarity = Rarity.Impossible,
                Visuals = Visuals.UglyOnTheInside,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(heal, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_AllySides, Effects.ChanceCondition(50)),
                ],
            };
            mutatedComplexion.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_1_4)]);
            mutatedComplexion.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            AddEnemyAbilityFromListEffect addMutatedToTarget = ScriptableObject.CreateInstance<AddEnemyAbilityFromListEffect>();
            addMutatedToTarget._abilityList = [mutatedComplexion];

            QueueTimelineAbilityByNameEffect queueMutated = ScriptableObject.CreateInstance<QueueTimelineAbilityByNameEffect>();
            queueMutated._abilityName = "Mutated Complexion";


            AnimationVisualsEffect microwaveVisual = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            microwaveVisual._visuals = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_Radiotherapy_A").visuals;
            microwaveVisual._animationTarget = Targeting.Slot_Front;

            //Salvation Lies Within/Ahead/Beyond/Above: Force all enemies with status effects to queue up the ability "Mutated Complexion"/twice/thrice/four times. If this fails, inflict the Opposing enemy with 2/2/3/4 Irradiated.
            Ability salvation1 = new Ability("Salvation Lies Within", "ST_WhhvaySalvation1_A")
            {
                Description = "Force all enemies with status effects to queue up the ability \"Mutated Complexion\" once.\nInflict the Opposing enemy with 2 Irradiated.",
                Visuals = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_Pygmalion_A").visuals,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_charge.png"),
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Blue],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_All_Status,
                Effects =
                [
                    Effects.GenerateEffect(addMutatedToTarget, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(queueMutated, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(microwaveVisual, 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_Front),
                ],
            };
            salvation1.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_All_Status, [nameof(IntentType_GameIDs.Other_Refresh)]);
            salvation1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            Ability salvation2 = new Ability("Salvation Lies Ahead", "ST_WhhvaySalvation2_A")
            {
                Description = "Force all enemies with status effects to queue up the ability \"Mutated Complexion\" twice.\nInflict the Opposing enemy with 2 Irradiated.",
                Visuals = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_Pygmalion_A").visuals,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_charge.png"),
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Blue],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_All_Status,
                Effects =
                [
                    Effects.GenerateEffect(addMutatedToTarget, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(queueMutated, 2, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(microwaveVisual, 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(getIrradiated, 2, Targeting.Slot_Front),
                ],
            };
            salvation2.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_All_Status, [nameof(IntentType_GameIDs.Other_Refresh)]);
            salvation2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            Ability salvation3 = new Ability("Salvation Lies Beyond", "ST_WhhvaySalvation3_A")
            {
                Description = "Force all enemies with status effects to queue up the ability \"Mutated Complexion\" thrice.\nInflict the Opposing enemy with 3 Irradiated.",
                Visuals = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_Pygmalion_A").visuals,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_charge.png"),
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Blue],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_All_Status,
                Effects =
                [
                    Effects.GenerateEffect(addMutatedToTarget, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(queueMutated, 3, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(microwaveVisual, 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(getIrradiated, 3, Targeting.Slot_Front),
                ],
            };
            salvation3.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_All_Status, [nameof(IntentType_GameIDs.Other_Refresh)]);
            salvation3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);

            Ability salvation4 = new Ability("Salvation Lies Above", "ST_WhhvaySalvation4_A")
            {
                Description = "Force all enemies with status effects to queue up the ability \"Mutated Complexion\" four times.\nInflict the Opposing enemy with 4 Irradiated.",
                Visuals = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_Pygmalion_A").visuals,
                AbilitySprite = ResourceLoader.LoadSprite("whhvay_charge.png"),
                Cost = [Pigments.Yellow, Pigments.Red, Pigments.Blue],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_All_Status,
                Effects =
                [
                    Effects.GenerateEffect(addMutatedToTarget, 1, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(queueMutated, 4, Targeting.Spec_Unit_AllOpponents_All_Status),
                    Effects.GenerateEffect(microwaveVisual, 1, Targeting.Slot_SelfAll),
                    Effects.GenerateEffect(getIrradiated, 4, Targeting.Slot_Front),
                ],
            };
            salvation4.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_All_Status, [nameof(IntentType_GameIDs.Other_Refresh)]);
            salvation4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), "Status_Irradiated"]);
            //Deal 3/4/5/6 damage to the Left and Right enemies, boosted by their amount of Irradiated. Make them inflict 1/2/2/3 Irradiated to themselves and their Left and Right allies.


            whhvay.AddLevelData(4, [bonds1, salvation1, ]);
            whhvay.AddLevelData(5, [bonds2, salvation2, ]);
            whhvay.AddLevelData(6, [bonds3, salvation3, ]);
            whhvay.AddLevelData(7, [bonds4, salvation4, ]);
            whhvay.AddCharacter(true, false);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Avenger.");
            }
        }
    }
}
