using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SorasToybox.CustomPassives;

namespace SorasToybox.Enemies
{
    public class Dozer
    {
        public static void Add()
        {
            //Tracked passive
            StatusEffect_Apply_Effect trackMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            trackMe._Status = StatusField.GetCustomStatusEffect("Tracked_ID");


            Enemy dozerSleep = new Enemy("Dozer", "Dozer_EN")
            {
                Health = 48,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineDozer.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("deadDozer.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineDozer.png", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/Dozer/DozerHurt",
                DeathSound = "event:/SorasSFX/Enemies/Dozer/DozerDie",
            };

            dozerSleep.AddPassives([Passives.GetCustomPassive("YellowBlooded_1_PA"), CustomPassive.SaltLockstepGenerator(1), Passives.GetCustomPassive("Reflex_PA")]);

            // The absolute agony that is Lockstep
            CasterStoreValueSetterEffect fuck = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            fuck.m_unitStoredDataID = "LockstepDir_SV";
            CasterStoreValueSetterEffect initialize = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            initialize.m_unitStoredDataID = "LockstepAmount_SV";
            initialize._ignoreIfContains = true;
            dozerSleep.CombatEnterEffects = [
                Effects.GenerateEffect(fuck, 1, Targeting.Slot_SelfSlot, null),
                Effects.GenerateEffect(initialize, 1,Targeting.Slot_SelfSlot),
            ];

            //Ok abilities go here.
            Ability dozersleepZees = new Ability("ZZZ", "ST_DozerZZZ_A")
            {
                Description = "Applies 1 Tracked to the Opposing party member.",
                Rarity = Rarity.Common,
                Visuals = null,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(trackMe, 1, Targeting.Slot_Front),
                ],
            };
            dozersleepZees.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Tracked"]);


            dozerSleep.AddEnemyAbilities([
                dozersleepZees
                ]);

            dozerSleep.AddEnemy(false, false, false);
            LoadedAssetsHandler.GetEnemy("Dozer_En").enemyTemplate = LoadedAssetsHandler.GetEnemy("Litany_EN").enemyTemplate;

        }
    }
}
