using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class Crashout
    {
        public static void Add()
        {
            Enemy crashout = new Enemy("Crashout", "Crashout_EN")
            {
                Health = 150,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineFakeFaceless.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("timelineFacelessNoFace.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineFakeFaceless.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/Crashout/CrashoutHurt",
                DeathSound = "event:/SorasSFX/Enemies/Crashout/CrashoutDeath",
            };

            PlayCustomSoundEffect getMadMFer = ScriptableObject.CreateInstance<PlayCustomSoundEffect>();
            getMadMFer._Sound = "event:/SorasSFX/Enemies/Crashout/CrashoutRoar";

            ExtraLootEffect Treasure = ScriptableObject.CreateInstance<ExtraLootEffect>();
            Treasure._isTreasure = true;
            Treasure._getLocked = true;

            ChangeMusicEffect weUltrachurchNow = ScriptableObject.CreateInstance<ChangeMusicEffect>();
            weUltrachurchNow.musEvent = "event:/NewFacelessMusic";

            TargetPerformEffectViaSubaction combatEnterShit = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            combatEnterShit.effects =
                [
                    Effects.GenerateEffect(getMadMFer), Effects.GenerateEffect(weUltrachurchNow)
                ];

            ExtraCurrencyEffect prizemoney = ScriptableObject.CreateInstance<ExtraCurrencyEffect>();
            prizemoney._isMultiplier = false;

            crashout.CombatEnterEffects = [Effects.GenerateEffect(combatEnterShit, 1, Targeting.Slot_SelfSlot)];
            crashout.CombatExitEffects = [Effects.GenerateEffect(Treasure, 2), Effects.GenerateEffect(prizemoney, 10, Targeting.Slot_SelfSlot)];

            StatusEffect_Apply_Effect getLinked = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getLinked._Status = StatusField.Linked;

            StatusEffect_Apply_Effect getDivine = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getDivine._Status = StatusField.DivineProtection;

            ProportionalCurHealthDamageEffect propDamage = ScriptableObject.CreateInstance<ProportionalCurHealthDamageEffect>();

            SwapToOneSideEffect swapLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapLeft._swapRight = false;

            SwapToOneSideEffect swapRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            swapRight._swapRight = true;

            ChangeHealthColorByCasterColorEffect urRedNow = ScriptableObject.CreateInstance<ChangeHealthColorByCasterColorEffect>();

            CheckHasUnitEffect checkHasUnit = ScriptableObject.CreateInstance<CheckHasUnitEffect>();

            CopyAndSpawnCustomCharacterAnywhereEffect strawmake = ScriptableObject.CreateInstance<CopyAndSpawnCustomCharacterAnywhereEffect>();
            strawmake._characterCopy = "Strawman_CH";
            strawmake._permanentSpawn = false;
            strawmake._rank = 0;
            strawmake._extraModifiers = [];

            GenerateTargetHealthManaEffect makePigmentOfYou = ScriptableObject.CreateInstance<GenerateTargetHealthManaEffect>();
            
            FullHealEffect fullHeal = ScriptableObject.CreateInstance<FullHealEffect>();

            DirectDeathEffect calloutPostOnTwitter = ScriptableObject.CreateInstance<DirectDeathEffect>();
            calloutPostOnTwitter._obliterationDeath = true;

            GenerateCasterHealthManaEffect gimmePigment = ScriptableObject.CreateInstance<GenerateCasterHealthManaEffect>();


            SpecificUnitsByPassiveTargeting allExamples = ScriptableObject.CreateInstance<SpecificUnitsByPassiveTargeting>();
            allExamples._passive = Passives.GetCustomPassive("Example_PA");
            allExamples.targetUnitAllySlots = true;
            allExamples.slotOffsets = [0];


            //ok let's make abilities now.
            Ability kys = new Ability("ST_CrashoutKYS_A")
            {
                Name = "Keep Yourself Safe!",
                Description = "Apply 1 Linked to the Opposing party member, then fully heal them.\nKill all Strawmen.",
                Rarity = Rarity.Rare,
                Visuals = Visuals.Malpractice,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(getLinked, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(fullHeal, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(calloutPostOnTwitter, 1, allExamples),
                ]
            };
            kys.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Linked), nameof(IntentType_GameIDs.Heal_21)]);
            kys.AddIntentsToTarget(allExamples, [nameof(IntentType_GameIDs.Damage_Death)]);

            AnimationVisualsEffect ripAndTear = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            ripAndTear._visuals = Visuals.RendRight;
            ripAndTear._animationTarget = Targeting.Slot_Front;

            Ability cti = new Ability("ST_CrashoutCTI_A")
            {
                Name = "I Can't Take It Anymore",
                Description = "Change the Opposing party member's health color to match this enemy's, then damage then for 25% of their current health twice in a row.",
                Rarity = Rarity.Rare,
                Effects =
                [
                    Effects.GenerateEffect(urRedNow, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(checkHasUnit, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(ripAndTear, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(propDamage, 25, Targeting.Slot_Front),
                    Effects.GenerateEffect(propDamage, 25, Targeting.Slot_Front),
                ],
            };
            cti.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Mana_Modify), "Damage_Prop", "Damage_Prop"]);

            Ability crashLeft = new Ability("ST_CrashoutLeft_A")
            {
                Name = "Leftward-Leaning Antipathy",
                Description = "Moves the Left party member to the Right, then deals 25% of their health as damage.\nIf damage was dealt, apply 2 Divine Protection to them.",
                Rarity = Rarity.Uncommon,
                Visuals = Visuals.StompRight,
                AnimationTarget = Targeting.Slot_OpponentLeft,
                Effects =
                [
                    Effects.GenerateEffect(swapRight, 1, Targeting.Slot_OpponentLeft),
                    Effects.GenerateEffect(propDamage, 25, Targeting.Slot_Front),
                    Effects.GenerateEffect(getDivine, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            crashLeft.AddIntentsToTarget(Targeting.Slot_OpponentLeft, [nameof(IntentType_GameIDs.Swap_Right)]);
            crashLeft.AddIntentsToTarget(Targeting.Slot_Front, ["Damage_Prop", nameof(IntentType_GameIDs.Status_DivineProtection)]);

            Ability crashRight = new Ability("ST_CrashoutRight_A")
            {
                Name = "Rightward-Facing Animosity",
                Description = "Moves the Right party member to the Left, then deals 25% of their health as damage.\nIf damage was dealt, apply 2 Divine Protection to them.",
                Rarity = Rarity.Uncommon,
                Visuals = Visuals.StompLeft,
                AnimationTarget = Targeting.Slot_OpponentRight,
                Effects =
                [
                    Effects.GenerateEffect(swapLeft, 1, Targeting.Slot_OpponentRight),
                    Effects.GenerateEffect(propDamage, 25, Targeting.Slot_Front),
                    Effects.GenerateEffect(getDivine, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            crashRight.AddIntentsToTarget(Targeting.Slot_Front, ["Damage_Prop", nameof(IntentType_GameIDs.Status_DivineProtection)]);
            crashRight.AddIntentsToTarget(Targeting.Slot_OpponentRight, [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability patternSeeking = new Ability("ST_CrashoutPattern_A")
            {
                Name = "Pattern-Seeking Behaviour",
                Description = "Inflict 3 Linked on the Left and Right party members.\nDeal 33% of the Opposing party member's health as damage to them.",
                Visuals = Visuals.Connection,
                Rarity = Rarity.Common,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(getLinked, 3, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(ripAndTear, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(propDamage, 33, Targeting.Slot_Front),
                ],
            };
            patternSeeking.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Linked)]);
            patternSeeking.AddIntentsToTarget(Targeting.Slot_Front, ["Damage_Prop"]);

            CasterStoredValueChangeEffect reduceAbomination = ScriptableObject.CreateInstance<CasterStoredValueChangeEffect>();
            reduceAbomination.m_unitStoredDataID = UnitStoredValueNames_GameIDs.AbominationPA.ToString();
            reduceAbomination._minimumValue = 1;
            reduceAbomination._exitValueIsChange = false;
            reduceAbomination._increase = false;
            reduceAbomination._randomBetweenPrevious = false;
            reduceAbomination._usePreviousExitValue = false;
            reduceAbomination._exitValueIsChange = false;

            Ability kms = new Ability("ST_CrashoutKMS_A")
            {
                Name = "Self-Realization Leads to Destruction",
                Description = "Spawn a Strawman, and reduce Abomination by 1 if that worked.\nGenerate two pigment of this enemy's health color.",
                Rarity = Rarity.Uncommon,
                Visuals = Visuals.BodySnatcher,
                AnimationTarget = Targeting.Slot_SelfAll,
                Effects =
                [
                    Effects.GenerateEffect(strawmake, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(reduceAbomination, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(gimmePigment, 2, Targeting.Slot_SelfSlot),
                ],
            };
            kms.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Other_Spawn)]);
            kms.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Generate)]);

            crashout.AddEnemyAbilities([
                kys,
                cti,
                crashLeft,
                crashRight,
                patternSeeking,
                kms,
                ]);
            crashout.AddPassives([CustomPassives.CustomPassive.SaltLockstepGenerator(1), Passives.Abomination1, Passives.GetCustomPassive("Erasure_PA"), Passives.GetCustomPassive("ST_Hostile_PA")]);
            crashout.AddEnemy(true, false, false);
            LoadedAssetsHandler.GetEnemy("Crashout_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Faceless_EN").enemyTemplate;
        }
    }
}
