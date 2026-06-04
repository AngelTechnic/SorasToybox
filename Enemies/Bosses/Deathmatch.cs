using System;
using System.Reflection;
using BrutalAPI;
using SorasToybox;
using SorasToybox.CustomEffects;
using UnityEngine;


namespace SorasToybox.Enemies
{
    public class Deathmatch
    {
        public static void Add()
        {
            AddPassiveEffect youAndMeBabyAintNothinButMammals = ScriptableObject.CreateInstance<AddPassiveEffect>();
            youAndMeBabyAintNothinButMammals._passiveToAdd = Passives.GetCustomPassive("Mammal_PA");

            StatusEffectCheckerEffect hasAnte = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasAnte._status = StatusField.GetCustomStatusEffect("Ante_ID");

            StatusEffect_Apply_Effect anteByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            anteByPrevious._MultPreviousExitValueForEntry = true;
            anteByPrevious._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            DirectDeathEffect hangman = ScriptableObject.CreateInstance<DirectDeathEffect>();

            TargetPerformEffectViaSubaction judgementEffect = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();



            //Basic unit setup
            Enemy deathmatchEnemy = new Enemy("Deathmatch", "Deathmatch_BOSS")
            {
                Health = 1024,
                HealthColor = Pigments.Red,
                CombatSprite = ResourceLoader.LoadSprite("timelineDeathmatch", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineDeathmatch", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Lilith_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Lilith_CH").deathSound,
            };
            deathmatchEnemy.AddUnitType("FemaleID");
            deathmatchEnemy.AddPassives([Passives.GetCustomPassive("BrokenBlooded_1_PA")]);

            //Borrowing Wriggling Sacrifice assets until I learn unity.
            LoadedAssetsHandler.GetEnemy("Deathmatch_BOSS").enemyTemplate = LoadedAssetsHandler.GetEnemy("WrigglingSacrifice_EN").enemyTemplate;
        }
    }
}
