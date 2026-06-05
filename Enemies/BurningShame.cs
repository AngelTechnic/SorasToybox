using BrutalAPI;
using SorasToybox.CustomEffects;
using SorasToybox.CustomPassives;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace SorasToybox.Enemies
{
    public class BurningShame
    {
        public static void  Add()
        {
            Enemy burningShame = new Enemy("Burning Shame", "BurningShame_EN")
            {
                Health = 30,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Broken"),
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineBurningShameBoss", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("overworldburningshame", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Fools/KarmaHurt",
                DeathSound = "event:/SorasSFX/Fools/KarmaDie",
            };


            StatusEffect_Apply_Effect divineProtect = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            divineProtect._Status = StatusField.DivineProtection;

            StatusEffect_Apply_Effect anteUp = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteUp._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            AnimationVisualsEffect punchingVisuals = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            punchingVisuals._visuals = Visuals.Burn;
            punchingVisuals._animationTarget = Targeting.Slot_Front;

            TargetPerformEffectViaSubaction punchingBagPunchesYou = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            punchingBagPunchesYou.effects =
                [
                    Effects.GenerateEffect(punchingVisuals, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damage, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(anteUp, 1, Targeting.Slot_SelfSlot),
                ];

            SwapToOneSideEffect swapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapLeft._swapRight = false;

            SwapToOneSideEffect swapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapRight._swapRight = true;



            Ability cantForgive = new Ability("Can't Forgive", "ST_ShameLeft_A")
            {
                Description = "Move the strongest enemy or enemies Left, then make them deal a Painful amount of damage to their Opposing party members. Grant the affected enemies 1 Ante.",
                Rarity = Rarity.Impossible,
                Priority = Priority.Fast,
                Effects =
                [
                    Effects.GenerateEffect(swapLeft, 1, Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false)),
                    Effects.GenerateEffect(punchingBagPunchesYou, 1, Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false)),

                ]
            };
            cantForgive.AddIntentsToTarget(Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false), [nameof(IntentType_GameIDs.Swap_Left), nameof(IntentType_GameIDs.Damage_3_6), "Status_Ante"]);


            Ability wontForget = new Ability("Won't Forget", "ST_ShameRight_A")
            {
                Description = "Move the strongest enemy or enemies Right, then make them deal a Painful amount of damage to their Opposing party members. Grant the affected enemies 1 Ante.",
                Rarity = Rarity.Impossible,
                Priority= Priority.Fast,
                Effects =
                [
                    Effects.GenerateEffect(swapRight, 1, Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false)),
                    Effects.GenerateEffect(punchingBagPunchesYou, 1, Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false)),
                ]

            };
            wontForget.AddIntentsToTarget(Targeting.GenerateUnitTarget_Specific_Health(true, true, false, false), [nameof(IntentType_GameIDs.Swap_Right), nameof(IntentType_GameIDs.Damage_3_6), "Status_Ante"]);

            ExtraAbilityInfo shameextraleft = new()
            {
                ability = cantForgive.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            ExtraAbilityInfo shameextraright = new()
            {
                ability = wontForget.GenerateEnemyAbility().ability,
                rarity = Rarity.Common,
            };

            Ability imNotInTheWrongHere = new Ability("I'm Not In The Wrong Here", "ST_ShameProtection_A")
            {
                Description = "Gain 2 Divine Protection.\n\"Me? Wrong? How dare you!\"",
                Rarity = Rarity.Common,
                Visuals = Visuals.Weep,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(divineProtect, 2, Targeting.Slot_SelfSlot),
                ],
            };
            imNotInTheWrongHere.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Status_DivineProtection)]);

            DamageEffect damageByPrevious = ScriptableObject.CreateInstance<DamageEffect>();
            damageByPrevious._usePreviousExitValue = true;

            RemoveAllStatusEffectsEffect noGoodiesForYou = ScriptableObject.CreateInstance<RemoveAllStatusEffectsEffect>();

            TargetPerformEffectViaSubaction dontHideEffects = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            dontHideEffects.effects =
                [
                    Effects.GenerateEffect(noGoodiesForYou, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageByPrevious, 1, Targeting.Slot_SelfSlot),
                ];

            Ability dontHideFromMe = new Ability("Don't Hide From Me", "ST_ShameStripper_A")
            {
                Description = "Remove all status effects from the Left and Right party members. Damage them equal to the amount removed.",
                Rarity = Rarity.Common,
                Visuals = Visuals.Gnaw,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(dontHideEffects, 1, Targeting.Slot_OpponentSides),
                ],
            };
            dontHideFromMe.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Misc), ("Damage_Prop")]);

            StatusEffectCheckerEffect hasSealed = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasSealed._status = StatusField.GetCustomStatusEffect("Sealed_ID");

            RemoveStatusEffectEffect noSealed = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noSealed._status = StatusField.GetCustomStatusEffect("Sealed_ID");


            StatusEffect_Apply_Effect anteByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteByPrevious._Status = StatusField.GetCustomStatusEffect("Ante_ID");
            anteByPrevious._MultPreviousExitValueForEntry = true;

            StatusEffect_Apply_Effect applySealed = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            applySealed._Status = StatusField.GetCustomStatusEffect("Sealed_ID");

            Ability yourFaultForever = new Ability("Your Fault Forever", "ST_ShameSeal_A")
            {
                Description = "If the Opposing party member has any Sealed, remove it all and convert it into equal Ante. Otherwise, inflict 2 Sealed on them.",
                Rarity = Rarity.Common,
                Visuals = Visuals.Connection,
                AnimationTarget = Targeting.Slot_Front,
                Effects = 
                [
                    Effects.GenerateEffect(hasSealed, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(noSealed, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(anteByPrevious, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 2)),
                    Effects.GenerateEffect(applySealed, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 3)),
                ],
            };
            yourFaultForever.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), "Rem_Status_Sealed", "Status_Ante", "Status_Sealed"]);

            burningShame.PrepareEnemyPrefab("Assets/ToyboxEnemies/Burning Shame/BurningShame Enemy.prefab", SorasToybox.assetbundle, null);

            //adding some passives. Excess and bonus suite have to wait for now.
            burningShame.AddPassives([Passives.Withering, Passives.GetCustomPassive("Fragile_PA"), CustomPassive.BonusSuiteRerollGenerator("Formless", [shameextraleft, shameextraright])]);

            burningShame.AddEnemyAbilities(
                [
                    imNotInTheWrongHere,
                    dontHideFromMe,
                    yourFaultForever,
                ]);


            //these can be spawned by the sepulchre cuz it's funny
            burningShame.AddEnemy(true, true, false);
        }
    }
}
