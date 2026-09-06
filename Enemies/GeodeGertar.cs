using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class GeodeGertar
    {
        public static void Add()
        {

            Enemy geodeGertar = new Enemy("Geode Gertar", "GeodeGertar_EN")
            {
                Health = 10,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineGeodeGertarBoss.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineGeodeGertarBoss.png", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("Tumult_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("Tumult_EN").deathSound,
                UnitTypes = ["Robot", "Zoincaillan"],
            };
            geodeGertar.PrepareEnemyPrefab("Assets/ToyboxEnemies/GeodeGertar/GeodeGertar Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());
            geodeGertar.AddPassives([Passives.InfantileGenerator(5), Passives.Forgetful, Passives.Inanimate]);

            HealEffect thisIsGoodNewsMark = ScriptableObject.CreateInstance<HealEffect>();
            thisIsGoodNewsMark._directHeal = true;

            StatusEffect_Apply_Effect whiplashByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            whiplashByPrevious._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");
            whiplashByPrevious._MultPreviousExitValueForEntry = true;

            SwapToSidesEffect harlemShuffle = ScriptableObject.CreateInstance<SwapToSidesEffect>();
            



            Ability gertar1 = new Ability("ST_geodeGertarReverseLL_A")
            {
                Name = "Reverse Life Leech",
                Description = "Heals the Opposing party member. Inflicts Whiplash on them equal to twice the amount healed.\nIf no health was gained, moves the Opposing party member Left or Right.",
                Rarity = Rarity.ExtremelyCommon,
                Visuals = Visuals.Malpractice,
                AnimationTarget = Targeting.Slot_Front,
                Priority = Priority.Slow,
                Effects =
                [
                    Effects.GenerateEffect(thisIsGoodNewsMark, 8, Targeting.Slot_Front),
                    Effects.GenerateEffect(whiplashByPrevious, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(harlemShuffle, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                ],
            };
            gertar1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10), "Status_Whiplash", nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Swap_Sides)]);

            ChangeHealthColorEffect eiffel65 = ScriptableObject.CreateInstance<ChangeHealthColorEffect>();
            eiffel65.color = Pigments.Blue;


            RemovePassiveEffect noInanimate = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            noInanimate.m_PassiveID = Passives.Inanimate.m_PassiveID;

            StatusEffect_Apply_Effect getPetrified = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getPetrified._Status = StatusField.GetCustomStatusEffect("Petrified_ID");

            //gertar2 used to be here but was moved down below

            ProportionalCurHealthDamageEffect fuckMyShitUpFam = ScriptableObject.CreateInstance<ProportionalCurHealthDamageEffect>();
            fuckMyShitUpFam._indirect = true;

            StatusEffect_Apply_Effect sayNoMore = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            sayNoMore._MultPreviousExitValueForEntry = true;
            sayNoMore._Status = StatusField.GetCustomStatusEffect("Unified_ID");

            GenerateCasterHealthManaEffect spillBlood = ScriptableObject.CreateInstance<GenerateCasterHealthManaEffect>();
            

            Ability gertar3 = new Ability("ST_geodeGertarSeeding_A")
            {
                Rarity = Rarity.Impossible,
                Name = "Explosive Seeding",
                Description = "Damage this enemy equal its current health, then generate pigment equal to the damage dealt.",
                Visuals = Visuals.Exsanguinate,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(fuckMyShitUpFam, 100, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(sayNoMore, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(spillBlood, 0, Targeting.Slot_SelfSlot),
                    
                ]
            };
            gertar3.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Damage_PropEx", nameof(IntentType_GameIDs.Mana_Generate)]);

            ExtraAbilityInfo gertarExtra = new()
            {
                ability = gertar3.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };

            AddPassiveEffect gainBonus = ScriptableObject.CreateInstance<AddPassiveEffect>();
            gainBonus._passiveToAdd = Passives.BonusAttackGenerator(gertarExtra);

            Ability gertar2 = new Ability("ST_geodeGertarBlossom_A")
            {
                Name = "Blossom",
                Description = "This enemy loses Inanimate, becomes Blue, inflicts 2 Petrified to the Opposing party member, and learns \"Explosive Seeding\"as a Bonus Attack.",
                AnimationTarget = Targeting.Slot_SelfAll,
                Visuals = Visuals.ShedSkin,
                Rarity = Rarity.AbsurdlyRare,
                Priority = Priority.Slow,
                Effects =
                [
                    Effects.GenerateEffect(noInanimate, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(eiffel65, 1,Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getPetrified, 2, Targeting.Slot_Front),
                    Effects.GenerateEffect(gainBonus, 1, Targeting.Slot_SelfSlot),

                ],

            };
            gertar2.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Misc), nameof(IntentType_GameIDs.Mana_Modify)]);
            gertar2.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Petrified"]);

            geodeGertar.AddEnemyAbilities([
                gertar1, gertar2,
                ]);

            geodeGertar.AddEnemy(true, true, true);
        }
    }
}
