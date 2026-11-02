using SorasToybox.CustomPassives;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class Erebus
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect powerByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            powerByPrevious._Status = StatusField.GetCustomStatusEffect("Power_ID");

            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            SwapToOneSideEffect moveLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            moveLeft._swapRight = false;

            SwapToOneSideEffect moveRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            moveRight._swapRight = true;

            SwapToSidesEffect moveSides = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            StatusEffect_Apply_Effect strengthByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            strengthByPrevious._Status = StatusField.GetCustomStatusEffect("Strength_ID");
            strengthByPrevious._MultPreviousExitValueForEntry = true;

            RemoveStatusEffectEffect noStrength = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noStrength._status = StatusField.GetCustomStatusEffect("Strength_ID");

            ConsumeAllColorManaEffect eatAllThePurples = ScriptableObject.CreateInstance<ConsumeAllColorManaEffect>();
            eatAllThePurples._consumeMana = Pigments.Purple;


            Enemy erebus = new Enemy("Erebus", "Erebus_BOSS")
            {
                Health = 86,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineErebus.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineErebus.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/Erebus/ErebusHurt",
                DeathSound = "event:/SorasSFX/Enemies/Erebus/ErebusDie",
                UnitTypes = ["MaleID", "Primal"],
            };
            erebus.PrepareEnemyPrefab("Assets/ToyboxEnemies/Erebus/Erebus Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());
            erebus.AddPassives([Passives.GetCustomPassive("Shortwave_PA"), Passives.GetCustomPassive("Forgotten_PA"), Passives.GetCustomPassive("PurpleBlooded_1_PA")]);

            Ability messWithTheBull = new("ST_ErebusBull_A")
            {
                Name = "Mess With The Bull",
                Description = "Moves Left or Right, then deals a Painful amount of damage to the Opposing party member.",
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
                Visuals = Visuals.Writhe,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(moveSides, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 4, Targeting.Slot_Front),
                ]
            };
            messWithTheBull.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Sides"]);
            messWithTheBull.AddIntentsToTarget(Targeting.Slot_Front, ["Damage_3_6"]);

            Ability getTheHorns = new("ST_ErebusHorns_A")
            {
                Name = "Get The Horns",
                Description = "Deals a Little damage to the Far Left and Far Right party members. Moves Left or Right.",
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
                Visuals = Visuals.Skewer,
                AnimationTarget = Targeting.Slot_OpponentFarSides,
                Effects =
                [
                    Effects.GenerateEffect(damage, 2, Targeting.Slot_OpponentFarSides),
                    Effects.GenerateEffect(moveSides, 1, Targeting.Slot_SelfSlot),
                ]
            };
            getTheHorns.AddIntentsToTarget(Targeting.Slot_OpponentFarSides, ["Damage_1_2"]);
            getTheHorns.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Sides"]);

            Ability satansBanner = new("ST_ErebusBanner_A")
            {
                Name = "Satan's Banner",
                Description = "Consume all Purple pigment.\nGain Strength equal to the amount consumed.\nApply equal amounts of Strength to the Left and Right party members.",
                Visuals = Visuals.Lighthouse,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Rarity = Rarity.Common,
                Priority = Priority.Slow,
                Effects = [
                    Effects.GenerateEffect(eatAllThePurples, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(strengthByPrevious, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(strengthByPrevious, 1, Targeting.Slot_OpponentSides),
                    ],
            };
            satansBanner.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Mana_Consume), "Status_Strength"]);
            satansBanner.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Strength"]);

            Ability chargeHeadOn = new("ST_ErebusCharge_A")
            {
                Name = "Charge Head On",
                Description = "Moves Left Twice, then deals a Painful amount of damage to the Opposing party member and removes all Strength from them.",
                Visuals = Visuals.StompLeft,
                AnimationTarget = Targeting.Slot_SelfAll,
                Priority = Priority.Slow,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(moveLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(moveLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(noStrength, 1, Targeting.Slot_Front),
                ]
            };
            chargeHeadOn.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Left", "Swap_Left", "Damage_3_6"]);
            chargeHeadOn.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Status_Strength"]);

            Ability rushStraightForward = new("ST_ErebusRush_A")
            {
                Name = "Rush Straight Forward",
                Description = "Moves Right Twice, then deals a Painful amount of damage to the Opposing party member and removes all Strength from them.",
                Visuals = Visuals.StompRight,
                AnimationTarget = Targeting.Slot_SelfAll,
                Priority = Priority.Slow,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(moveRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(moveRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(noStrength, 1, Targeting.Slot_Front),
                ]
            };
            rushStraightForward.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Right", "Swap_Right", "Damage_3_6"]);
            rushStraightForward.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Status_Strength"]);

            erebus.AddEnemyAbilities([
                messWithTheBull,
                getTheHorns,
                satansBanner,
                chargeHeadOn,
                rushStraightForward,
                ]);

            erebus.AddEnemy(true, false, false);

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[BOSSES] Added Erebus.");
            }

        }
    }
}
