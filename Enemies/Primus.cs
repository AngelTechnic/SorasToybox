using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UI.CanvasScaler;

namespace SorasToybox.Enemies
{
    public class Primus
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect anteMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteMe._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            StatusEffect_Apply_Effect illuminateMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            illuminateMe._Status = StatusField.GetCustomStatusEffect("Illuminated_ID");

            StatusEffect_Apply_Effect maddenMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            maddenMe._Status = StatusField.GetCustomStatusEffect("Madness_ID");

            FieldEffect_Apply_Effect absolveMe = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            absolveMe._Field = StatusField.GetCustomFieldEffect("Absolution_ID");

            ConsumeRandomManaEffect eatPigment = ScriptableObject.CreateInstance<ConsumeRandomManaEffect>();


            Enemy primusMiniboss = new Enemy("Traces of Primus", "Primus_EN")
            {
                Health = 409,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("White"),
                Size = 5,
                CombatSprite = LoadedAssetsHandler.GetEnemy("Supergiant_EN").enemySprite,
                OverworldDeadSprite = LoadedAssetsHandler.GetEnemy("Supergiant_EN").enemyOWCorpseSprite,
                OverworldAliveSprite = LoadedAssetsHandler.GetEnemy("Supergiant_EN").enemyOverworldSprite,
                DamageSound = LoadedAssetsHandler.GetEnemy("TrueNobody_BOSS").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("TrueNobody_BOSS").deathSound,
                UnitTypes = ["Primal"],
            };

            ChangeMusicEffect torealmus = ScriptableObject.CreateInstance<ChangeMusicEffect>();
            torealmus.musEvent = "event:/SorasMusic/Enemies/PrimusMusic/EventHorizon";

            ExtraCurrencyEffect prizemoney = ScriptableObject.CreateInstance<ExtraCurrencyEffect>();
            prizemoney._isMultiplier = false;

            //Enter and exit effects to change the music and reward mony.
            primusMiniboss.CombatEnterEffects = [
                Effects.GenerateEffect(torealmus, 1, Targeting.Slot_SelfSlot),
                ];
            primusMiniboss.CombatExitEffects = [
                Effects.GenerateEffect(prizemoney, 30, Targeting.Slot_SelfSlot)
                ];




            GenericTargetting_BySlot_Index ohtwofour = ScriptableObject.CreateInstance<GenericTargetting_BySlot_Index>();
            ohtwofour.slotPointerDirections = new int[] { 0, 2, 4 };
            ohtwofour.getAllies = false;

            GenericTargetting_BySlot_Index onethree = ScriptableObject.CreateInstance<GenericTargetting_BySlot_Index>();
            onethree.slotPointerDirections = new int[] { 1, 3 };
            onethree.getAllies = false;

            //Ok adding the abilities here.

            Ability primusAbsol024 = new Ability("Rays Filtering Through The Cumulus", "ST_PrimusAbsol024_A")
            {
                Description = "Applies 4 Absolution to the Far Left, Center, and Far Right party member positions.",
                Cost = [],
                AnimationTarget = ohtwofour,
                Visuals = Visuals.Excommunicate,
                Rarity = Rarity.Rare,
                Effects =
                [
                    Effects.GenerateEffect(absolveMe, 2, ohtwofour),
                ]
            };
            primusAbsol024.AddIntentsToTarget(ohtwofour, ["Field_Absolution"]);

            Ability primusAbsol13 = new Ability("Beams Passing Through The Nimbus", "ST_PrimusAbsol13_A")
            {
                Description = "Applies 4 Absolution to the Left and Right party member positions.",
                Cost = [],
                AnimationTarget = onethree,
                Visuals = Visuals.Excommunicate,
                Rarity = Rarity.Rare,
                Effects =
                [
                    Effects.GenerateEffect(absolveMe, 2, onethree),
                ]
            };
            primusAbsol13.AddIntentsToTarget(onethree, ["Field_Absolution"]);

            Ability primusBecomeMad = new Ability("Is This The Power Of A God?", "ST_PrimusMadness_A")
            {
                Description = "Applies 3 Madness to the highest health party member(s), and 4 Absolution to their position(s).",
                Cost = [],
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_Strongest,
                Visuals = Visuals.Providence,
                Effects =
                [
                    Effects.GenerateEffect(maddenMe, 3, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(absolveMe, 4, Targeting.Spec_Unit_AllOpponents_Strongest),
                ],
                Rarity = Rarity.Uncommon,
            };
            primusBecomeMad.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_Strongest, ["Status_Madness", "Field_Absolution"]);

            Ability primusYummers = new Ability("Gravitational Lock", "ST_PrimusYummers_A")
            {
                Description = "Consumes 5 pigment.",
                Cost = [],
                AnimationTarget = Targeting.Slot_SelfAll,
                Visuals = Visuals.Wriggle,
                Effects =
                [
                    Effects.GenerateEffect(eatPigment, 5, Targeting.Slot_SelfSlot),

                ],
                Rarity = Rarity.Uncommon,
            };
            primusYummers.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume)]);



            Ability primusIlluminate = new Ability("Pride And Joy", "ST_PrimusIlluminate_A")
            {
                Description = "Applies 1 Illuminated to all party members.",
                Visuals = Visuals.Innocence,
                AnimationTarget = Targeting.Unit_AllOpponents,
                Effects =
                [
                    Effects.GenerateEffect(illuminateMe, 1, Targeting.Unit_AllOpponents),
                ],
                Rarity = Rarity.Uncommon,
            };
            primusIlluminate.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Illuminated"]);

            Ability primusAllAnteAbility = new Ability("It's Beautiful", "ST_PrimusAllAnte_A")
            {
                Description = "Applies 1 Ante to all party members.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Purple, Pigments.Purple],
                Visuals = Visuals.Weep,
                AnimationTarget = Targeting.Unit_AllOpponents,
                Effects =
                [

                    Effects.GenerateEffect(anteMe,1,Targeting.Unit_AllOpponents),
                ],
                Rarity = Rarity.ImpossibleNoReroll,
                Priority = Priority.ExtremelySlow,
            };

            primusAllAnteAbility.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Ante"]);
            ExtraAbilityInfo primusBonus = new()
            {
                ability = primusAllAnteAbility.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };


            primusMiniboss.AddEnemyAbilities([
                primusAbsol024,
                primusAbsol13,
                primusBecomeMad,
                primusYummers,
                primusIlluminate,
                ]);

            primusMiniboss.AddPassives([Passives.MultiAttack2, Passives.GetCustomPassive("Tinted_PA"), Passives.BonusAttackGenerator(primusBonus)]);
            primusMiniboss.AddEnemy(false, false, false);
            LoadedAssetsHandler.GetEnemy("Primus_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Supergiant_EN").enemyTemplate;

        }
    }
}
