using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Enemies
{
    public class Zizlet
    {
        public static void Add()
        {
            Ability zizletMirrorAbility = new Ability("Reflection of Soul and Body", "ZizletMirror_A")
            {
                Description = "Mirrors this enemy's position.",
                Cost = Array.Empty<ManaColorSO>(),
                Visuals = Visuals.Flay,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects = 
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<MirrorPositionEffect>(), 1, Targeting.Slot_SelfSlot),
                ],
                Rarity = Rarity.Common,
                Priority = Priority.Normal,
            };

            if (SorasToybox.CrossMod.IntoTheAbyss && LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Velocity_ID"))
            {
                //Figures that a giant bird would be living in a densely-woven next. Anyway initializing: Health, color, size, timeline, sounds, unittypes
                Enemy zizlet = new Enemy("Zizlet", "Zizlet_EN")
                {
                    Health = 60,
                    HealthColor = Pigments.Red,
                    Size = 1,
                    CombatSprite = ResourceLoader.LoadSprite("timelineZizlet", new Vector2(0.5f, 0f), 32),
                    OverworldDeadSprite = ResourceLoader.LoadSprite("deadZizlet", new Vector2(0.5f, 0f), 32),
                    OverworldAliveSprite = ResourceLoader.LoadSprite("timelineZizlet", new Vector2(0.5f, 0f), 32),
                    DamageSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dmg",
                    DeathSound = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Dth",
                    UnitTypes = ["Bird", "Robot", "Primal"],
                };

                zizlet.AddEnemyAbilities(
                    [
                        zizletMirrorAbility.GenerateEnemyAbility(true),
                    ]
                );

                zizlet.AddPassives([Passives.Inanimate, Passives.MultiAttack2]);
                LoadedAssetsHandler.GetEnemy("Zizlet_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("Scrungie_EN").enemyTemplate;
                zizlet.AddEnemy(true, true, false);
            }
        }
    }   
}
