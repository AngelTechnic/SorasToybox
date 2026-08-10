using SorasToybox.Custom_Effects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace SorasToybox.Fools
{
    public class VenzaFool
    {
        public static void Add()
        {
            //setting up effects and whatnot
            DirectDeathEffect stfuNoOneLikesYou = ScriptableObject.CreateInstance<DirectDeathEffect>();
            stfuNoOneLikesYou._obliterationDeath = true;

            UnboundedHealEffect unboundHeal = ScriptableObject.CreateInstance<UnboundedHealEffect>();

            StatusEffect_Apply_Effect getMiseryRandomly = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getMiseryRandomly._RandomBetweenPrevious = true;
            getMiseryRandomly._Status = StatusField.GetCustomStatusEffect("Misery_ID");

            StatusEffect_Apply_Effect getEcstasyRandomly = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getEcstasyRandomly._RandomBetweenPrevious = true;
            getEcstasyRandomly._Status = StatusField.GetCustomStatusEffect("Ecstasy_ID");

            StatusEffect_Apply_Effect getGutted = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getGutted._Status = StatusField.Gutted;

            RemoveStatusEffectEffect noGutted = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noGutted._status = StatusField.Gutted;

            ExtraVariableForNextEffect blank = ScriptableObject.CreateInstance<ExtraVariableForNextEffect>();

            ResurrectEffect getBackUpMF = ScriptableObject.CreateInstance<ResurrectEffect>();

            Character venza = new Character("Venza", "Venza_CH")
            {
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"),
                UsesAllAbilities = true,
                UsesBasicAbility = false,
                MovesOnOverworld = false,
                FrontSprite = ResourceLoader.LoadSprite("venza_front.png", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("venza_back.png", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("venza_overworld.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/Dozer/DozerHurt",
                DeathSound = "event:/SorasSFX/Enemies/Dozer/DozerDie",
                DialogueSound = "event:/SorasSFX/Enemies/Dozer/DozerRoar",
                UnitTypes = ["Fish"],
                StartsLocked = false,
            };
            venza.AddPassives([Passives.Withering, Passives.GetCustomPassive("Condense_PA")]);

            Ability words = new Ability("ST_VenzaWords_A")
            {
                Name = "The Right Words",
                Description = "Apply 1 Gutted to the weakest party member, then heal them at least 1 health. Then, remove Gutted from them.",
                AbilitySprite = ResourceLoader.LoadSprite("venza_apology.png"),
                Cost = [Pigments.Yellow, Pigments.Blue, Pigments.Grey],
                Visuals = LoadedAssetsHandler.GetEnemyAbility("taneFavor_A").visuals,
                AnimationTarget = Targeting.Spec_Unit_OtherAllies_Weakest,
                Effects =
                [
                    Effects.GenerateEffect(getGutted, 1, Targeting.Spec_Unit_OtherAllies_Weakest),
                    Effects.GenerateEffect(unboundHeal, 1, Targeting.Spec_Unit_OtherAllies_Weakest),
                    Effects.GenerateEffect(noGutted, 1, Targeting.Spec_Unit_OtherAllies_Weakest),
                ],
            };
            words.AddIntentsToTarget(Targeting.Spec_Unit_OtherAllies_Weakest, [nameof(IntentType_GameIDs.Status_Gutted), "Heal_Unbounded", nameof(IntentType_GameIDs.Rem_Status_Gutted)]);

            Ability acknowledgement = new Ability("ST_VenzaAcknowledgement_A")
            {
                Name = "Acknowledgement",
                Description = "Inflict 0-2 Misery to all enemy slots. Inflict 0-2 Ecstasy to all party member slots.",
                AbilitySprite = ResourceLoader.LoadSprite("venza_watching.png"),
                Cost = [Pigments.Yellow, Pigments.Yellow, Pigments.Grey, Pigments.Grey],
                Visuals = LoadedAssetsHandler.GetEnemyAbility("NolocimesSpread_A").visuals,
                AnimationTarget = Targeting.AllUnits,
                Effects =
                [
                    Effects.GenerateEffect(blank, 0),
                    Effects.GenerateEffect(getMiseryRandomly, 2, Targeting.Slot_OpponentAllSlots),
                    Effects.GenerateEffect(blank, 0),
                    Effects.GenerateEffect(getEcstasyRandomly, 2, Targeting.Slot_AllyAllSlots),
                ],
            };
            acknowledgement.AddIntentsToTarget(Targeting.Slot_OpponentAllSlots, ["Status_Misery"]);
            acknowledgement.AddIntentsToTarget(Targeting.Slot_AllyAllSlots, ["Status_Ecstasy"]);


            Ability mantle = new Ability("ST_VenzaMantle_A")
            {
                Name = "Mantle",
                Description = "Attempt to resurrect a party member in the left slot, if it is empty. If successful, kill Venza.",
                AbilitySprite = ResourceLoader.LoadSprite("venza_presence.png"),
                Cost = [Pigments.Purple, Pigments.Purple, Pigments.Grey, Pigments.Grey, Pigments.Grey],
                Effects =
                [
                    Effects.GenerateEffect(getBackUpMF, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(stfuNoOneLikesYou, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            mantle.AddIntentsToTarget(Targeting.Slot_AllyLeft, [nameof(IntentType_GameIDs.Other_Resurrect)]);
            mantle.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Damage_Death)]);
            
            
            venza.AddLevelData(2, [words, acknowledgement, mantle]);

            venza.AddCharacter(false, true);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Catharsis.");
            }
        }
    }
}
