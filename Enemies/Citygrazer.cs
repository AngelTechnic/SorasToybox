using BepInEx;
using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomPassives;

namespace SorasToybox.Enemies
{
    public class Citygrazer
    {
        public static void Add()
        {
            Enemy citygrazer = new Enemy("Citygrazer", "Citygrazer_EN")
            {
                Health = 1000,
                HealthColor = Pigments.Blue,
                Size = 4,
                CombatSprite = ResourceLoader.LoadSprite("timelineSusMung.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineSusMung.png", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Xiphactinus_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Xiphactinus_EN").deathSound,
                UnitTypes = ["Zoincaillan", "Robot"],
            };

            AttackVisualsSO anchorVis = LoadedAssetsHandler.GetEnemyAbility("BlackAndBlue_A").visuals;

            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            CheckHasUnitEffect notEmpty = ScriptableObject.CreateInstance<CheckHasUnitEffect>();

            RandomizeAllColorPigmentEffect blueToRed = ScriptableObject.CreateInstance<RandomizeAllColorPigmentEffect>();
            blueToRed._colorToRandomize = Pigments.Blue;
            blueToRed._manaRandomOptions = [Pigments.Red];


            Ability grazerFirst = new("ST_GrazerFirst_A")
            {
                Name = "Bow Chaos",
                Description = "Deal a Painful amount of damage to the Far Left facing party member.\nIf there was a valid target, turn all Blue pigment into Red.",
                Visuals = anchorVis,
                AnimationTarget = Targeting.GenerateBigUnitSlotTarget([0]),
                Rarity = Rarity.Common,
                Priority = Priority.VerySlow,
                Effects =
                [
                    Effects.GenerateEffect(notEmpty, 1, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(damage, 4, Targeting.GenerateBigUnitSlotTarget([0])),
                    Effects.GenerateEffect(blueToRed, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
            };
            grazerFirst.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([0]), [nameof(IntentType_GameIDs.Damage_3_6)]);
            grazerFirst.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["ColoIntent"]);

            Ability grazerSecond = new("ST_GrazerSecond_A")
            {
                Name = "Starboard Havoc",
                Description = "Deal a Painful amount of damage to the Left facing party member.\nIf there was a valid target, turn all Blue pigment into Red.",
                Visuals = anchorVis,
                AnimationTarget = Targeting.GenerateBigUnitSlotTarget([1]),
                Rarity = Rarity.Common,
                Priority = Priority.VerySlow,
                Effects =
                [
                    Effects.GenerateEffect(notEmpty, 1, Targeting.GenerateBigUnitSlotTarget([1])),
                    Effects.GenerateEffect(damage, 4, Targeting.GenerateBigUnitSlotTarget([1])),
                    Effects.GenerateEffect(blueToRed, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
            };
            grazerSecond.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([1]), [nameof(IntentType_GameIDs.Damage_3_6)]);
            grazerSecond.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["ColoIntent"]);


            Ability grazerThird = new("ST_GrazerThird_A")
            {
                Name = "Port Madness",
                Description = "Deal a Painful amount of damage to the Right facing party member.\nIf there was a valid target, turn all Blue pigment into Red.",
                Visuals = anchorVis,
                AnimationTarget = Targeting.GenerateBigUnitSlotTarget([2]),
                Rarity = Rarity.Common,
                Priority = Priority.VerySlow,
                Effects =
                [
                    Effects.GenerateEffect(notEmpty, 1, Targeting.GenerateBigUnitSlotTarget([2])),
                    Effects.GenerateEffect(damage, 4, Targeting.GenerateBigUnitSlotTarget([2])),
                    Effects.GenerateEffect(blueToRed, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
            };
            grazerThird.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([2]), [nameof(IntentType_GameIDs.Damage_3_6)]);
            grazerThird.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["ColoIntent"]);


            Ability grazerFourth = new("ST_GrazerFourth_A")
            {
                Name = "Aft Hysteria",
                Description = "Deal a Painful amount of damage to the Far Right facing party member.\nIf there was a valid target, turn all Blue pigment into Red.",
                Visuals = anchorVis,
                AnimationTarget = Targeting.GenerateBigUnitSlotTarget([3]),
                Rarity = Rarity.Common,
                Priority = Priority.VerySlow,
                Effects =
                [
                    Effects.GenerateEffect(notEmpty, 1, Targeting.GenerateBigUnitSlotTarget([3])),
                    Effects.GenerateEffect(damage, 4, Targeting.GenerateBigUnitSlotTarget([3])),
                    Effects.GenerateEffect(blueToRed, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 2)),
                ],
            };
            grazerFourth.AddIntentsToTarget(Targeting.GenerateBigUnitSlotTarget([3]), [nameof(IntentType_GameIDs.Damage_3_6)]);
            grazerFourth.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["ColoIntent"]);

            AnimationVisualsEffect diliviumAnchorVis = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            diliviumAnchorVis._visuals = anchorVis;
            diliviumAnchorVis._animationTarget = Targeting.Slot_OpponentSides;

            StatusEffect_Apply_Effect getOil = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getOil._Status = StatusField.OilSlicked;

            RemoveAllStatusEffectsEffect noStatus = ScriptableObject.CreateInstance<RemoveAllStatusEffectsEffect>();

            RemoveStatusEffectEffect noDrowning = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noDrowning._status = StatusField.GetCustomStatusEffect("Drowning_ID");

            StatusEffect_Apply_Effect miseryByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            miseryByPrevious._Status = StatusField.GetCustomStatusEffect("Misery_ID");
            miseryByPrevious._MultPreviousExitValueForEntry = true;


            Ability grazerBonus = new("ST_GrazerBonus_A")
            {
                Name = "Diluvium",
                Description = "Drench everyone in 4 Oil Slicked.\nRemove all status effects from non-opposing party members.\nConvert all Drowning on self into Misery.",
                Cost = [],
                AnimationTarget = Targeting.AllUnits,
                Visuals = Visuals.OilSlicked,
                Effects =
                [
                    Effects.GenerateEffect(getOil, 4, Targeting.AllUnits),
                    Effects.GenerateEffect(diliviumAnchorVis, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(noStatus, 1, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(noDrowning, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(miseryByPrevious, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.VerySlow,
            };
            grazerBonus.AddIntentsToTarget(Targeting.Unit_AllOpponents, [nameof(IntentType_GameIDs.Status_OilSlicked)]);
            grazerBonus.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Misc)]);
            grazerBonus.AddIntentsToTarget(Targeting.Unit_AllAllies, [nameof(IntentType_GameIDs.Status_OilSlicked), "Rem_Status_Drowning", "Status_Misery"]);

            ExtraAbilityInfo grazerExtra = new()
            {
                ability = grazerBonus.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };

            citygrazer.AddEnemyAbilities([
                grazerFirst,
                grazerSecond,
                grazerThird,
                grazerFourth,
            ]);

            citygrazer.AddPassives([LoadedAssetsHandler.GetEnemy("BlackAndBlue_BOSS").passiveAbilities[0], Passives.BonusAttackGenerator(grazerExtra)]);
            citygrazer.AddEnemy(true, false, false);
            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("Added the Citygrazer.");
            }
            LoadedAssetsHandler.GetEnemy("Citygrazer_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Xiphactinus_EN").enemyTemplate;
        }
    }
}
