using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using static SorasToybox.Encounters.Abyss.H.Reminder;

namespace SorasToybox.Fools
{
    public class PsidocFool
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect whiplashByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            whiplashByPrevious._MultPreviousExitValueForEntry = true;
            whiplashByPrevious._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");

            StatusEffect_Apply_Effect getWhiplash = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getWhiplash._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");

            HealEffect getHeal = ScriptableObject.CreateInstance<HealEffect>();

            DamageEffect slapMyShit = ScriptableObject.CreateInstance<DamageEffect>();

            Ability psidocBasic = new Ability("ST_PsidocBasic_A")
            {
                Name = "Transcribe",
                Description = "Heal this party member 1 health. Inflict equal Whiplash to the Opposing enemy.\nIf this party member didn't heal, deal 1 damage to the Opposing enemy.",
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_basic.png"),
                AnimationTarget = Targeting.Slot_Front,
                Visuals = Visuals.Slap,
                Cost = [Pigments.Yellow],
                Effects =
                [
                    Effects.GenerateEffect(getHeal, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(whiplashByPrevious, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(slapMyShit, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                ],
            };
            psidocBasic.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);
            psidocBasic.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Whiplash"]);

            Character psidoc = new Character("Psidoc", "Psidoc_CH")
            {
                UsesBasicAbility = true,
                BasicAbility = psidocBasic,
                UnitTypes = ["Zoincaillan",],
                FrontSprite = ResourceLoader.LoadSprite("psidoc_front.png", new Vector2(0.5f, 0f), 32),
                BackSprite = ResourceLoader.LoadSprite("psidoc_back.png", new Vector2(0.5f, 0f), 32),
                OverworldSprite = ResourceLoader.LoadSprite("psidoc_overworld.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/rBugHurt",
                DeathSound = "event:/rBugDie",
                DialogueSound = "event:/rBugRoar",
                HealthColor = Pigments.Blue,
            };
            psidoc.AddPassives([Passives.GetCustomPassive("YellowBlooded_1_PA")]);

            //Transfer Bruises/Cuts/Injuries/Woes
            //Heal the Left party member / left and right party members(at level 3 and up) 4 / 5 / 6 / 7 health.Inflict Whiplash to the Opposing enemy equal to the amount healed.

            Ability transfer1 = new Ability("ST_PsidocTransfer1_A")
            {
                Name = "Transfer Bruises",
                Description = "Heal the Left party member 4 health. Inflict Whiplash to the Opposing enemy equal to the amount healed.",
                Cost = [Pigments.Blue, Pigments.Blue],
                AnimationTarget = Targeting.Slot_AllyLeft,
                Visuals = Visuals.Flay,
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_transfer.png"),
                Effects = 
                [
                    Effects.GenerateEffect(getHeal, 4, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(whiplashByPrevious, 1, Targeting.Slot_Front),
                    
                ],
            };
            transfer1.AddIntentsToTarget(Targeting.Slot_AllyLeft, [nameof(IntentType_GameIDs.Heal_1_4)]);
            transfer1.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Whiplash"]);

            Ability transfer2 = new Ability("ST_PsidocTransfer2_A")
            {
                Name = "Transfer Cuts",
                Description = "Heal the Left party member 5 health. Inflict Whiplash to the Opposing enemy equal to the amount healed.",
                Cost = [Pigments.Blue, Pigments.Blue],
                AnimationTarget = Targeting.Slot_AllyLeft,
                Visuals = Visuals.Flay,
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_transfer.png"),
                Effects =
                [
                    Effects.GenerateEffect(getHeal, 5, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(whiplashByPrevious, 1, Targeting.Slot_Front),

                ],
            };
            transfer2.AddIntentsToTarget(Targeting.Slot_AllyLeft, [nameof(IntentType_GameIDs.Heal_5_10)]);
            transfer2.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Whiplash"]);

            Ability transfer3 = new Ability("ST_PsidocTransfer3_A")
            {
                Name = "Transfer Injuries",
                Description = "Heal the Left and Right party members 6 health. Inflict Whiplash to the Opposing enemy equal to the amount healed.",
                Cost = [Pigments.Blue, Pigments.Blue],
                AnimationTarget = Targeting.Slot_AllySides,
                Visuals = Visuals.Flay,
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_transfer.png"),
                Effects =
                [
                    Effects.GenerateEffect(getHeal, 6, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(whiplashByPrevious, 1, Targeting.Slot_Front),

                ],
            };
            transfer3.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10)]);
            transfer3.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Whiplash"]);

            Ability transfer4 = new Ability("ST_PsidocTransfer4_A")
            {
                Name = "Transfer Woes",
                Description = "Heal the Left and Right party members 7 health. Inflict Whiplash to the Opposing enemy equal to the amount healed.",
                Cost = [Pigments.Blue, Pigments.Blue],
                AnimationTarget = Targeting.Slot_AllySides,
                Visuals = Visuals.Flay,
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_transfer.png"),
                Effects =
                [
                    Effects.GenerateEffect(getHeal, 7, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(whiplashByPrevious, 1, Targeting.Slot_Front),

                ],
            };
            transfer4.AddIntentsToTarget(Targeting.Slot_AllySides, [nameof(IntentType_GameIDs.Heal_5_10)]);
            transfer4.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Whiplash"]);

            //Psi-Scalpel/Scissors/Suture/Stapler
            //Inflict 3/5/7/9 Whiplash to all enemy slots to the Right. Each time this succeeds, heal the Right party member 1/2/3/4 health.
            Ability psi1 = new Ability("ST_PsidocPsi1_A")
            {
                Name = "Psi-Scalpel",
                Description = "Inflict 3 Whiplash to all enemy slots to the Right. Heal the Right party member 1 health for each valid target.",
                Cost = [Pigments.Yellow, Pigments.BlueYellow, Pigments.Blue],
                AnimationTarget = Targeting.Slot_OpponentAllRights,
                Visuals = Visuals.InvadeTheVeins,
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_psi.png"),
                Effects =
                [
                    Effects.GenerateEffect(getWhiplash, 3, Targeting.GenerateSlotTarget([1], false)),
                    Effects.GenerateEffect(getHeal, 1, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 3, Targeting.GenerateSlotTarget([2], false)),
                    Effects.GenerateEffect(getHeal, 1, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 3, Targeting.GenerateSlotTarget([3], false)),
                    Effects.GenerateEffect(getHeal, 1, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 3, Targeting.GenerateSlotTarget([4], false)),
                    Effects.GenerateEffect(getHeal, 1, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            psi1.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, ["Status_Whiplash"]);
            psi1.AddIntentsToTarget(Targeting.Slot_AllyRight, [nameof(IntentType_GameIDs.Heal_1_4)]);

            Ability psi2 = new Ability("ST_PsidocPsi2_A")
            {
                Name = "Psi-Scissors",
                Description = "Inflict 5 Whiplash to all enemy slots to the Right. Heal the Right party member 2 health for each valid target.",
                Cost = [Pigments.Yellow, Pigments.BlueYellow, Pigments.Blue],
                AnimationTarget = Targeting.Slot_OpponentAllRights,
                Visuals = Visuals.InvadeTheVeins,
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_psi.png"),
                Effects =
                [
                    Effects.GenerateEffect(getWhiplash, 5, Targeting.GenerateSlotTarget([1], false)),
                    Effects.GenerateEffect(getHeal, 2, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 5, Targeting.GenerateSlotTarget([2], false)),
                    Effects.GenerateEffect(getHeal, 2, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 5, Targeting.GenerateSlotTarget([3], false)),
                    Effects.GenerateEffect(getHeal, 2, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 5, Targeting.GenerateSlotTarget([4], false)),
                    Effects.GenerateEffect(getHeal, 2, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            psi2.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, ["Status_Whiplash"]);
            psi2.AddIntentsToTarget(Targeting.Slot_AllyRight, [nameof(IntentType_GameIDs.Heal_5_10)]);

            Ability psi3 = new Ability("ST_PsidocPsi3_A")
            {
                Name = "Psi-Suture",
                Description = "Inflict 7 Whiplash to all enemy slots to the Right. Heal the Right party member 3 health for each valid target.",
                Cost = [Pigments.Yellow, Pigments.BlueYellow, Pigments.Blue],
                AnimationTarget = Targeting.Slot_OpponentAllRights,
                Visuals = Visuals.InvadeTheVeins,
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_psi.png"),
                Effects =
                [
                    Effects.GenerateEffect(getWhiplash, 7, Targeting.GenerateSlotTarget([1], false)),
                    Effects.GenerateEffect(getHeal, 3, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 7, Targeting.GenerateSlotTarget([2], false)),
                    Effects.GenerateEffect(getHeal, 3, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 7, Targeting.GenerateSlotTarget([3], false)),
                    Effects.GenerateEffect(getHeal, 3, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 7, Targeting.GenerateSlotTarget([4], false)),
                    Effects.GenerateEffect(getHeal, 3, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            psi3.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, ["Status_Whiplash"]);
            psi3.AddIntentsToTarget(Targeting.Slot_AllyRight, [nameof(IntentType_GameIDs.Heal_11_20)]);

            Ability psi4 = new Ability("ST_PsidocPsi4_A")
            {
                Name = "Psi-Stapler",
                Description = "Inflict 9 Whiplash to all enemy slots to the Right. Heal the Right party member 4 health for each valid target.",
                Cost = [Pigments.Yellow, Pigments.BlueYellow, Pigments.Blue],
                AnimationTarget = Targeting.Slot_OpponentAllRights,
                Visuals = Visuals.InvadeTheVeins,
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_psi.png"),
                Effects =
                [
                    Effects.GenerateEffect(getWhiplash, 9, Targeting.GenerateSlotTarget([1], false)),
                    Effects.GenerateEffect(getHeal, 4, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 9, Targeting.GenerateSlotTarget([2], false)),
                    Effects.GenerateEffect(getHeal, 4, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 9, Targeting.GenerateSlotTarget([3], false)),
                    Effects.GenerateEffect(getHeal, 4, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(getWhiplash, 9, Targeting.GenerateSlotTarget([4], false)),
                    Effects.GenerateEffect(getHeal, 4, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            psi4.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, ["Status_Whiplash"]);
            psi4.AddIntentsToTarget(Targeting.Slot_AllyRight, [nameof(IntentType_GameIDs.Heal_11_20)]);


            //First/Second/Extra/Final Opinion
            //Heal the highest health enemy 5/6/8/11 health. Inflict twice as much Whiplash to them, and make them heal their Opponents 4/5/7/8 health.
            TargetPerformEffectViaSubaction opinion1SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            opinion1SubAct.effects =
            [
                Effects.GenerateEffect(getHeal, 4, Targeting.Slot_Front),
            ];

            Ability opinion1 = new Ability("ST_PsidocOpinion1_A")
            {
                Name = "First Opinion",
                Description = "Heal the highest health enemy 5 health. Inflict twice as much Whiplash to them, and make them heal their Opponents 4 health.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue],
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_opinion.png"),
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_Strongest,
                Visuals = Visuals.HeartBreaker,
                Effects =
                [
                    Effects.GenerateEffect(opinion1SubAct, 1, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(getHeal, 5, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(whiplashByPrevious, 2, Targeting.Spec_Unit_AllOpponents_Strongest),
                ],
            };
            opinion1.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_Strongest, [nameof(IntentType_GameIDs.Heal_5_10), "Status_Whiplash", nameof(IntentType_GameIDs.Misc)]);

            TargetPerformEffectViaSubaction opinion2SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            opinion2SubAct.effects =
            [
                Effects.GenerateEffect(getHeal, 5, Targeting.Slot_Front),
            ];

            Ability opinion2 = new Ability("ST_PsidocOpinion2_A")
            {
                Name = "Second Opinion",
                Description = "Heal the highest health enemy 6 health. Inflict twice as much Whiplash to them, and make them heal their Opponents 5 health.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue],
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_opinion.png"),
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_Strongest,
                Visuals = Visuals.HeartBreaker,
                Effects =
                [
                    Effects.GenerateEffect(opinion2SubAct, 1, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(getHeal, 6, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(whiplashByPrevious, 2, Targeting.Spec_Unit_AllOpponents_Strongest),

                ],
            };
            opinion2.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_Strongest, [nameof(IntentType_GameIDs.Heal_5_10), "Status_Whiplash", nameof(IntentType_GameIDs.Misc)]);

            TargetPerformEffectViaSubaction opinion3SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            opinion3SubAct.effects =
            [
                Effects.GenerateEffect(getHeal, 7, Targeting.Slot_Front),
            ];

            Ability opinion3 = new Ability("ST_PsidocOpinion3_A")
            {
                Name = "Extra Opinion",
                Description = "Heal the highest health enemy 8 health. Inflict twice as much Whiplash to them, and make them heal their Opponents 7 health.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue],
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_opinion.png"),
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_Strongest,
                Visuals = Visuals.HeartBreaker,
                Effects =
                [
                    Effects.GenerateEffect(opinion3SubAct, 1, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(getHeal, 8, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(whiplashByPrevious, 2, Targeting.Spec_Unit_AllOpponents_Strongest),
                ],
            };
            opinion3.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_Strongest, [nameof(IntentType_GameIDs.Heal_5_10), "Status_Whiplash", nameof(IntentType_GameIDs.Misc)]);

            TargetPerformEffectViaSubaction opinion4SubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            opinion4SubAct.effects =
            [
                Effects.GenerateEffect(getHeal, 8, Targeting.Slot_Front),
            ];

            Ability opinion4 = new Ability("ST_PsidocOpinion4_A")
            {
                Name = "Final Opinion",
                Description = "Heal the highest health enemy 11 health. Inflict twice as much Whiplash to them, and make them heal their Opponents 8 health.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Blue, Pigments.Blue],
                AbilitySprite = ResourceLoader.LoadSprite("psidoc_opinion.png"),
                AnimationTarget = Targeting.Spec_Unit_AllOpponents_Strongest,
                Visuals = Visuals.HeartBreaker,
                Effects =
                [
                    Effects.GenerateEffect(opinion3SubAct, 1, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(getHeal, 11, Targeting.Spec_Unit_AllOpponents_Strongest),
                    Effects.GenerateEffect(whiplashByPrevious, 2, Targeting.Spec_Unit_AllOpponents_Strongest),
                ],
            };
            opinion4.AddIntentsToTarget(Targeting.Spec_Unit_AllOpponents_Strongest, [nameof(IntentType_GameIDs.Heal_11_20), "Status_Whiplash", nameof(IntentType_GameIDs.Misc)]);

            psidoc.AddLevelData(7, [transfer1, psi1, opinion1]);
            psidoc.AddLevelData(9, [transfer2, psi2, opinion2]);
            psidoc.AddLevelData(11, [transfer3, psi3, opinion3]);
            psidoc.AddLevelData(13, [transfer4, psi4, opinion4]);

            psidoc.AddCharacter(false, true);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Unbreaker.");
            }
        }
    }
}
