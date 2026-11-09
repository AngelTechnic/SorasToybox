using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class SERVICE
    {
        public static void Add()
        {
            Enemy service = new Enemy("SERVICE", "SERVICE_EN")
            {
                Health = 28,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineSERVICE", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("tango_dead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineSERVICE", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/SERVICE/ServiceHurt",
                DeathSound = "event:/SorasSFX/Enemies/SERVICE/ServiceDie",
                UnitTypes = ["Zoincaillan"],
            };
            service.PrepareEnemyPrefab("Assets/ToyboxEnemies/SERVICE/SERVICE Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/SEARCH/Ichor_Gibs.prefab").GetComponent<ParticleSystem>());
            service.AddPassive(Passives.GetCustomPassive("BlueBlooded_1_PA"));
            service.AddPassive(Passives.Skittish);

            StatusEffect_Apply_Effect whiplashByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            whiplashByPrevious._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");
            whiplashByPrevious._MultPreviousExitValueForEntry = true;

            StatusEffect_Apply_Effect gimmeWhiplash = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            gimmeWhiplash._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");

            FieldEffect_Apply_Effect gimmeAria = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            gimmeAria._Field = StatusField.GetCustomFieldEffect("Aria_ID");

            GenerateColorManaEffect gimmePigment = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            gimmePigment.mana = Pigments.Blue;
            gimmePigment.usePreviousExitValue = true;


            GenerateTargetHealthManaEffect getTheirPigment = ScriptableObject.CreateInstance<GenerateTargetHealthManaEffect>();


            RemoveStatusEffectEffect noWhiplash = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noWhiplash._status = StatusField.GetCustomStatusEffect("Whiplash_ID");

            Ability spin = new("ST_SPIN_A")
            {
                Name = "SPIN",
                Description = "Apply 2 Aria to the Left and Right positions.\nRemove Whiplash from the Opposing party member.\nGenerate Blue pigment equal to the amount removed.",
                Visuals = Visuals.Strum,
                AnimationTarget = Targeting.Slot_OpponentSides,
                Effects =
                [
                    Effects.GenerateEffect(gimmeAria, 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(noWhiplash, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(gimmePigment, 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                
            };
            spin.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Field_Aria"]);
            spin.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Mana_Generate"]);
            spin.AddIntentsToTarget(Targeting.Slot_Front, ["Rem_Status_Whiplash"]);

            Check_CountPigmentEffect howMuchBlue = ScriptableObject.CreateInstance<Check_CountPigmentEffect>();
            howMuchBlue._colourPigment = Pigments.Blue;
            
            SpecificOpponentsByFieldTargeting oppsInAria = ScriptableObject.CreateInstance<SpecificOpponentsByFieldTargeting>();
            oppsInAria._fieldEffectID = "Aria_ID";
            oppsInAria.targetUnitAllySlots = false;
            oppsInAria.slotOffsets = [0];

            Ability weave = new("ST_WEAVE_A")
            {
                Name = "WEAVE",
                Description = "Inflict 1 Whiplash to all party members in Aria\nApply 2 Aria to the Opposing position.",
                Visuals = Visuals.Wriggle,
                AnimationTarget = oppsInAria,
                Effects =
                [
                    Effects.GenerateEffect(gimmeWhiplash, 1, oppsInAria),
                    Effects.GenerateEffect(gimmeAria, 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
            };
            weave.AddIntentsToTarget(oppsInAria, ["Status_Whiplash"]);
            weave.AddIntentsToTarget(Targeting.Slot_Front, ["Field_Aria"]);
        
            CheckHasUnitEffect countGang = ScriptableObject.CreateInstance<CheckHasUnitEffect>();
            

            Ability serve = new("ST_SERVE_A")
            {
                Name = "SERVE",
                Description = "Inflict 2 Whiplash to Left and Right party members.\nGenerate Blue Pigment equal to the amount of party members in Aria.",
                Visuals = Visuals.OilSlicked,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(gimmeWhiplash, 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(countGang, 1, oppsInAria),
                    Effects.GenerateEffect(gimmePigment, 1, Targeting.Slot_SelfSlot),

                ],
            };
            serve.AddIntentsToTarget(Targeting.Slot_OpponentSides, ["Status_Whiplash"]);
            serve.AddIntentsToTarget(oppsInAria, ["Misc_Hidden"]);
            serve.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Mana_Generate"]);

            service.AddEnemyAbilities([
                spin, weave, serve,
                ]);

            service.AddEnemy(true, true, false);

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[ENEMIES] Added SERVICE.");
            }
        }
    }
}
