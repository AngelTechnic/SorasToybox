using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static SorasToybox.Encounters.Abyss.H;

namespace SorasToybox.Enemies
{
    public class Freezer
    {
        public static void Add()
        {
            Enemy cooler = new("Cooler", "Cooler_EN")
            {
                Health = 12,
                HealthColor = Pigments.Blue,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineCooler.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DeadCooler.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineCooler.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                DamageSound = "event:/SorasSFX/Enemies/Freezer/FreezerHurt",
                DeathSound = "event:/SorasSFX/Enemies/Freezer/FreezerDie",
            };
            cooler.PrepareEnemyPrefab("Assets/ToyboxEnemies/Freezer/Cooler Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Freezer/FreezerGibs.prefab").GetComponent<ParticleSystem>());

            cooler.AddPassives([Passives.Formless, Passives.Inanimate, Passives.Withering]);

            HealEffect healByPrevious = ScriptableObject.CreateInstance<HealEffect>();
            healByPrevious.usePreviousExitValue = true;
            healByPrevious._onlyIfHasHealthOver0 = true;

            ConsumeColorManaEffect eatSomeBlue = ScriptableObject.CreateInstance<ConsumeColorManaEffect>();
            eatSomeBlue.mana = Pigments.Blue;


            SpecificAlliesByPassiveTargeting InanimateAllies = ScriptableObject.CreateInstance<SpecificAlliesByPassiveTargeting>();
            InanimateAllies._passive = Passives.Inanimate;
            InanimateAllies.targetUnitAllySlots = true;
            InanimateAllies.slotOffsets = [0];

            SpecificAlliesByPassiveTargeting OpposingInanimateAllies = ScriptableObject.CreateInstance<SpecificAlliesByPassiveTargeting>();
            OpposingInanimateAllies._passive = Passives.Inanimate;
            OpposingInanimateAllies.targetUnitAllySlots = false;
            OpposingInanimateAllies.slotOffsets = [0];

            Ability condensation = new("ST_Condensation_A")
            {
                Name = "Condensation",
                Description = "Consume 3 Blue pigment and heal the Opposing party member an equal amount.",
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(eatSomeBlue, 3, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(healByPrevious, 1, Targeting.Slot_Front),
                    ]
            };
            condensation.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Mana_Consume"]);
            condensation.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_1_4)]);

            StatusEffect_Apply_Effect getWhiplash = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getWhiplash._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");

            HealEffect heal = ScriptableObject.CreateInstance<HealEffect>();

            Ability crystalization = new("ST_Crystalization_A")
            {
                Name = "Crystalization",
                Description = "Heal the Opposing party member a little amount. If healing failed, inflict 3 Whiplash to all party members Opposing inanimate enemies.",
                Rarity = Rarity.Common,
                Visuals = Visuals.Melt,
                AnimationTarget = OpposingInanimateAllies,
                Effects =
                [
                    Effects.GenerateEffect(heal, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(getWhiplash, 3, OpposingInanimateAllies, Effects.CheckPreviousEffectCondition(false, 1)),
                ],
            };
            crystalization.AddIntentsToTarget(Targeting.Slot_Front, ["Heal_1_4"]);
            crystalization.AddIntentsToTarget(OpposingInanimateAllies, ["Status_Whiplash"]);

            cooler.AddEnemyAbilities([
                condensation, crystalization,
                ]);

            cooler.AddEnemy(true, true, true);


            //freezer
            Enemy freezer = new("Freezer", "Freezer_EN")
            {
                Health = 24,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineFreezerRed.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                OverworldDeadSprite = ResourceLoader.LoadSprite("DeadCooler.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineFreezerRed.png", (Vector2?)new Vector2(0.5f, 0f), 32, (Assembly)null),
                DamageSound = "event:/SorasSFX/Enemies/Freezer/FreezerHurt",
                DeathSound = "event:/SorasSFX/Enemies/Freezer/FreezerDie",
            };
            freezer.AddPassives([Passives.TwoFaced, Passives.Slippery, Passives.DecayGenerator(cooler.enemy, 100)]);
            freezer.PrepareEnemyPrefab("Assets/ToyboxEnemies/Freezer/Freezer Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Freezer/FreezerGibs.prefab").GetComponent<ParticleSystem>());

            StatusEffect_Apply_Effect getPetrified = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getPetrified._Status = StatusField.GetCustomStatusEffect("Petrified_ID");



            Ability glassSnow = new("ST_GlassSnow_A")
            {
                Name = "Glass Snow",
                Description = "Consume 1 Blue pigment.\nIf this fails, petrify the Opposing party member for two turns.",
                Priority = Priority.Slow,
                Visuals = Visuals.Thorns,
                AnimationTarget = Targeting.Slot_SelfAll,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(eatSomeBlue, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getPetrified, 2, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 1)),
                ],
            };
            glassSnow.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Mana_Consume"]);
            glassSnow.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Petrified"]);

            SpecificUnitsByPassiveTargeting AllInanimate = ScriptableObject.CreateInstance<SpecificUnitsByPassiveTargeting>();
            AllInanimate._passive = Passives.Inanimate;
            AllInanimate.targetUnitAllySlots = true;
            AllInanimate.slotOffsets = [0];

            DamageEffect damageByPrevious = ScriptableObject.CreateInstance<DamageEffect>();
            damageByPrevious._usePreviousExitValue = true;

            AttackVisualsSO praiseVis = ScriptableObject.CreateInstance<AttackVisualsSO>();
            if (SorasToybox.CrossMod.Siren)
            {
                praiseVis = LoadedAssetsHandler.GetEnemyAbility("PraiseAB").visuals;
            }
            else
            {
                praiseVis = Visuals.UglyOnTheInside;
            }
            CheckHasUnitEffect checkEm = ScriptableObject.CreateInstance<CheckHasUnitEffect>();



            Ability arcticRave = new("ST_ArcticRave_A")
            {
                Name = "Arctic Rave",
                Description = "Deal 2 damage to the Opposing party member for each Inanimate enemy or party member.\n"
                + "Heal 1 health to the Opposing party member for each Inanimate enemy or party member if they survived.",
                Visuals = praiseVis,
                Rarity = Rarity.Common,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(checkEm, 1, AllInanimate),
                    Effects.GenerateEffect(damageByPrevious, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(checkEm, 1, AllInanimate),
                    Effects.GenerateEffect(healByPrevious, 1, Targeting.Slot_Front),
                ]
            };
            arcticRave.AddIntentsToTarget(AllInanimate, ["Misc_Hidden"]);
            arcticRave.AddIntentsToTarget(Targeting.Slot_Front, ["Damage_3_6", nameof(IntentType_GameIDs.Heal_5_10)]);

            FieldEffectCheckEffect amIConstricted = ScriptableObject.CreateInstance<FieldEffectCheckEffect>();
            amIConstricted._fields = [StatusField.Constricted];

            FieldEffect_Apply_Effect getConstricted = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            getConstricted._Field = StatusField.Constricted;

            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            Ability eject = new("ST_EjectShards_A")
            {
                Name = "Eject Shards",
                Description = "Constrict this enemy, if it isn't already.\nDeal almost no damage to everything Inanimate.",
                Priority = Priority.Fast,
                Visuals = Visuals.Wriggle,
                AnimationTarget = AllInanimate,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(amIConstricted, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getConstricted, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(damage, 1, AllInanimate),
                ]
            };
            eject.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Field_Constricted", "Misc_Hidden"]);
            eject.AddIntentsToTarget(AllInanimate, ["Damage_1_2"]);

            AddPassiveEffect becomeInanimate = ScriptableObject.CreateInstance<AddPassiveEffect>();
            becomeInanimate._passiveToAdd = Passives.Inanimate;

            CheckPassiveAbilityEffect amIInanimate = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();

            ConsumeAllManaEffect yumyumyum = ScriptableObject.CreateInstance<ConsumeAllManaEffect>();

            //todo: grab the frostbite anim later
            AttackVisualsSO vitrifyVis = ScriptableObject.CreateInstance<AttackVisualsSO>();
            if (SorasToybox.CrossMod.GlitchsFreaks)
            {
                vitrifyVis = LoadedAssetsHandler.GetEnemyAbility("PraiseAB").visuals;
            }
            else
            {
                vitrifyVis = Visuals.Writhe;
            }

            Ability vitrify = new("ST_Vitrify_A")
            {
                Name = "Vitrify",
                Description = "This enemy becomes Inanimate. Once Inanimate, it will consume all pigment.",
                Visuals = Visuals.Writhe,
                AnimationTarget = Targeting.Slot_SelfAll,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(becomeInanimate, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(yumyumyum, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),

                ]
            };
            vitrify.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Status_Petrified", "Mana_Consume"]);

            freezer.AddEnemyAbilities([
                glassSnow, arcticRave, eject, vitrify,
                ]);

            freezer.AddEnemy(true, true, false);

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[ENEMIES] Added the Freezer (and Cooler).");
            }

        }
    }
}
