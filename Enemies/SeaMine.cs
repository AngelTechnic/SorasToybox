using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class SeaMine
    {
        public static void Add()
        {
            HealEffect heal = ScriptableObject.CreateInstance<HealEffect>();

            HealEffect healByPrevious = ScriptableObject.CreateInstance<HealEffect>();
            healByPrevious.usePreviousExitValue = true;

            ProportionalCurHealthDamageEffect boomDamage = ScriptableObject.CreateInstance<ProportionalCurHealthDamageEffect>();

            DamageEffect damageByPrevious = ScriptableObject.CreateInstance<DamageEffect>();
            damageByPrevious._usePreviousExitValue = true;

            Enemy seaMine = new("Sea Mine", "ST_SeaMine_EN")
            {
                Health = 10,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("White"),
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineSeaMine", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineSeaMine", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/SeaMine/SeaMineHurt",
                DeathSound = "event:/SorasSFX/Enemies/SeaMine/SeaMineDie",
                UnitTypes = ["Friendly"],
            };
            seaMine.PrepareEnemyPrefab("Assets/ToyboxEnemies/Sea Mine/SeaMine_Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Sea Mine/SeaMine GIbs.prefab").GetComponent<ParticleSystem>());

            seaMine.AddPassives([Passives.Withering, Passives.GetCustomPassive("Reflex_PA"), Passives.GetCustomPassive("Houdini_PA"), Passives.GetCustomPassive("Rugous_PA")]);

            Ability prime = new("ST_SeaMinePrime_A")
            {
                Name = "Prime",
                Description = "Heal the Opposing party member 3 health. Heal this enemy the same amount.",
                Rarity = Rarity.Common,
                Visuals = Visuals.Wriggle,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(heal, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(healByPrevious, 1, Targeting.Slot_SelfSlot),
                ]
            };
            prime.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_1_4)]);
            prime.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Heal_1_4)]);

            AttackVisualsSO detonateAnim;
            if (SorasToybox.CrossMod.IntoTheAbyss)
            {
                detonateAnim = LoadedAssetsHandler.GetEnemyAbility("DeathRattle_A").visuals;
            }
            else
            {
                detonateAnim = Visuals.Crush;
            }

            Ability detonate = new("ST_SeaMineBoom")
            {
                Name = "Detonate",
                Description = "Decimate this enemy's health. Deal equal damage to the left and right enemies.",
                Visuals = detonateAnim,
                AnimationTarget = Targeting.Slot_SelfAll,
                Rarity = Rarity.VeryRare,
                Effects =
                [
                    Effects.GenerateEffect(boomDamage, 90, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageByPrevious, 1, Targeting.Slot_AllySides),
                ]
            };
            detonate.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Damage_PropEx"]);
            detonate.AddIntentsToTarget(Targeting.Slot_AllySides, ["Damage_Prop"]);

            seaMine.AddEnemyAbilities([
                prime,
                detonate,
                ]);

            seaMine.AddEnemy(true, true, true);

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[ENEMIES] Added the Sea Mine (Yay!).");
            }

        }
    }
}
