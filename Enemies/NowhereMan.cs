using BepInEx;
using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Enemies
{
    public class NowhereMan
    {
        public static void Add()
        {

            string hurmt = "";
            string death = "";


            if (SorasToybox.CrossMod.SaltEnemies)
            {
                hurmt = LoadedAssetsHandler.GetEnemy("InTheDark_EN").damageSound;
                death = LoadedAssetsHandler.GetEnemy("InTheDark_EN").deathSound;

            }
            else
            {
                hurmt = LoadedAssetsHandler.GetEnemy("Visage_Father_EN").damageSound;
                death = LoadedAssetsHandler.GetEnemy("Visage_Father_EN").deathSound;

            }



            Enemy nowhereMan = new Enemy("Nowhere Man", "NowhereMan_EN")
            {
                Health = 25,
                HealthColor = Pigments.RedYellow,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineNowhere.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("deadNowhere.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineNowhere.png", new Vector2(0.5f, 0f), 32),
                DamageSound = hurmt,
                DeathSound = death,

            };
            //currently uses yinimro gibs
            nowhereMan.PrepareEnemyPrefab("Assets/ToyboxEnemies/Nowhere Man/Nowhere Man Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());

            // The absolute agony that is Lockstep
            CasterStoreValueSetterEffect fuck = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            fuck.m_unitStoredDataID = "LockstepDir_SV";
            CasterStoreValueSetterEffect initialize = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            initialize.m_unitStoredDataID = "LockstepAmount_SV";
            initialize._ignoreIfContains = true;
            nowhereMan.CombatEnterEffects = [
                Effects.GenerateEffect(fuck, 1, Targeting.Slot_SelfSlot, null),
                Effects.GenerateEffect(initialize, 3, Targeting.Slot_SelfSlot),
            ];

            AttackVisualsSO dooranim2;
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                dooranim2 = LoadedAssetsHandler.GetEnemyAbility("TheClassic_A").visuals;
            }
            else
            {
                dooranim2 = Visuals.Crush;
            }
            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();


            Ability shoot1 = new Ability("ST_NowhereShoot1_A")
            {
                Name = "Shoot the Sheriff",
                Description = "Deal an Agonizing amount of damage to the Far Left party member.\nThis assumes the grid wraps around.",
                Rarity = Rarity.Common,
                Visuals = dooranim2,
                AnimationTarget = Targeting.GenerateSlotTarget([-2, 3]),
                Effects =
                [
                    Effects.GenerateEffect(damage, 7, Targeting.GenerateSlotTarget([-2, 3])),
                ],
            };
            shoot1.AddIntentsToTarget(Targeting.GenerateSlotTarget([-2, 3]), [nameof(IntentType_GameIDs.Damage_7_10)]);
            shoot1.AddIntentsToTarget(Targeting.GenerateSlotTarget([-1, 4]), [nameof(IntentType_GameIDs.Swap_Left)]);
            shoot1.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability shoot2 = new Ability("ST_NowhereShoot2_A")
            {
                Name = "Shoot the Deputy",
                Description = "Deal an Agonizing amount of damage to the Left party member.\nThis assumes the grid wraps around.",
                Rarity = Rarity.Common,
                Visuals = dooranim2,
                AnimationTarget = Targeting.GenerateSlotTarget([-1, 4]),
                Effects =
                [
                    Effects.GenerateEffect(damage, 7, Targeting.GenerateSlotTarget([-1, 4])),
                ],
            };
            shoot2.AddIntentsToTarget(Targeting.GenerateSlotTarget([-1, 4]), [nameof(IntentType_GameIDs.Damage_7_10)]);
            shoot2.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability shoot3 = new Ability("ST_NowhereShoot3_A")
            {
                Name = "Shoot the Judge",
                Description = "Deal an Agonizing amount of damage to the Opposing party member.",
                Rarity = Rarity.Common,
                Visuals = dooranim2,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(damage, 7, Targeting.Slot_Front),
                ],
            };
            shoot3.AddIntentsToTarget(Targeting.GenerateSlotTarget([-1, 4]), [nameof(IntentType_GameIDs.Swap_Right)]);
            shoot3.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_7_10)]);
            shoot3.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, 1]), [nameof(IntentType_GameIDs.Swap_Left)]);

            Ability shoot4 = new Ability("ST_NowhereShoot4_A")
            {
                Name = "Shoot the Hangman",
                Description = "Deal an Agonizing amount of damage to the Right party member.\nThis assumes the grid wraps around.",
                Rarity = Rarity.Common,
                Visuals = dooranim2,
                AnimationTarget = Targeting.GenerateSlotTarget([-4, 1]),
                Effects =
                [
                    Effects.GenerateEffect(damage, 7, Targeting.GenerateSlotTarget([-4, 1])),
                ],
            };
            shoot4.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Right)]);
            shoot4.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, 1]), [nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability shoot5 = new Ability("ST_NowhereShoot5_A")
            {
                Name = "Shoot the Undertaker",
                Description = "Deal an Agonizing amount of damage to the Far Right party member.\nThis assumes the grid wraps around.",
                Rarity = Rarity.Common,
                Visuals = dooranim2,
                AnimationTarget = Targeting.GenerateSlotTarget([-3, 2]),
                Effects =
                [
                    Effects.GenerateEffect(damage, 7, Targeting.GenerateSlotTarget([-3, 2])),
                ],
            };
            shoot5.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Swap_Right)]);
            shoot5.AddIntentsToTarget(Targeting.GenerateSlotTarget([-4, 1]), [nameof(IntentType_GameIDs.Swap_Right)]);
            shoot5.AddIntentsToTarget(Targeting.GenerateSlotTarget([-3, 2]), [nameof(IntentType_GameIDs.Damage_7_10)]);

            FieldEffect_Apply_Effect getConstricted = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            getConstricted._Field = StatusField.Constricted;

            Ability shoot6 = new Ability("ST_NowhereShoot6_A")
            {
                Name = "Shoot the...",
                Description = "Deal an Agonizing amount of damage to the... wait. Out of ammo.\n(Gains 1 Constricted.)",
                Rarity = Rarity.AbsurdlyRare,
                Effects = [Effects.GenerateEffect(getConstricted, 1, Targeting.Slot_SelfSlot)],
            };
            shoot6.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Field_Constricted)]);

            nowhereMan.AddEnemyAbilities([
                shoot1,
                shoot2,
                shoot3,
                shoot4,
                shoot5,
                shoot6,
                ]);

            nowhereMan.AddPassives([Passives.GetCustomPassive("Itchy_PA"), Passives.Forgetful, CustomPassives.CustomPassive.SaltLockstepGenerator(3)]);
            nowhereMan.AddEnemy(true, true, false);

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("Added the Nowhere Man.");
            }
        }
    }
}
