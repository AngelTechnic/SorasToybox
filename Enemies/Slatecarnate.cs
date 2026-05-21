using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace SorasToybox.Enemies
{
    public class Slatecarnate
    {
        public static void Add()
        {
            Enemy slatecarnate = new Enemy("Slatecarnate", "HeavensSlate_EN")
            {
                Health = 30,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineSlatecarnate", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineSlatecarnate", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("HeavensGatePurple_BOSS").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("HeavensGatePurple_BOSS").deathSound,
                UnitTypes = ["Friendly"],
            };
            slatecarnate.AddPassives([Passives.Slippery, Passives.Dying, Passives.Withering]);

            StatusEffect_Apply_Effect gritMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            gritMe._Status = StatusField.GetCustomStatusEffect("Grit_ID");

            StatusEffect_Apply_Effect venganceMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            venganceMe._Status = StatusField.GetCustomStatusEffect("VenganceMark_ID");

            //Generating red pigment
            GenerateColorManaEffect redMana = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            redMana.mana = Pigments.Red;

            //Generating purple pigment
            GenerateColorManaEffect purpleMana = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            purpleMana.mana = Pigments.Purple;

            //Checking if there's room
            CheckEntryOrMoreEmptyManaSlotsEffect slotcheck = ScriptableObject.CreateInstance<CheckEntryOrMoreEmptyManaSlotsEffect>();

            //The Big
            Ability slateBig = new Ability("The Big", "SlatecarnateBig_A")
            {
                Description = "Applies Vengeance Mark to the Left and Right enemies.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Effects = 
                [
                    Effects.GenerateEffect(venganceMe, 1, Targeting.Slot_AllySides),
                ],
                Visuals = Visuals.Decimate,
                AnimationTarget = Targeting.Slot_AllySides,
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            slateBig.AddIntentsToTarget(Targeting.Slot_AllySides, ["Status_VenganceMark"]);

            //The Little
            Ability slateLittle = new Ability("The Little", "SlatecarnateLittle_A")
            {
                Description = "Applies 3 Grit to the Opposing party member.",
                Cost = [Pigments.Purple, Pigments.Purple],
                Effects =
                [
                    Effects.GenerateEffect(gritMe, 3, Targeting.Slot_Front),
                ],
                Visuals = Visuals.Flex,
                AnimationTarget = Targeting.Slot_Front,
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            slateLittle.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Grit"]);

            //The Young
            Ability slateYoung = new Ability("The Young", "SlatecarnateYoung_A")
            {
                Description = "Generates 3 Red Pigment. Afterwards, if there is room, generates 1 Purple pigment.",
                Cost = [Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(redMana, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(slotcheck, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(purpleMana, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
                Visuals = Visuals.Wriggle,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };
            slateYoung.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            slatecarnate.AddEnemyAbilities
            (
                [
                    slateBig,
                    slateLittle,
                    slateYoung,
                ]
            );

            slatecarnate.AddEnemy(true, false, false);
            Debug.Log("Slatecarnate real. Make sure you remove this!");
            LoadedAssetsHandler.GetEnemy("HeavensSlate_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("HeavensGatePurple_BOSS").enemyTemplate;
            
        }
    }
}

