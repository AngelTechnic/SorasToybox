using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class InfinityTasbeht
    {
        public static void Add()
        {
            MassSwapZoneEffect scrambleEveryone = ScriptableObject.CreateInstance<MassSwapZoneEffect>();

            SwapToSidesEffect moveLeftOrRight = ScriptableObject.CreateInstance<SwapToSidesEffect>();

            SwapToOneSideEffect moveLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            moveLeft._swapRight = false;

            SwapToOneSideEffect moveRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            moveRight._swapRight = true;

            PlayCustomSoundEffect impertinenceNoises = ScriptableObject.CreateInstance<PlayCustomSoundEffect>();
            impertinenceNoises._Sound = "event:/ImpertinenceDx";

            Enemy infinityTasbeht = new Enemy("Infinity Tasbeht", "InfinityTasbeht_EN")
            {
                UnitTypes = ["Friendly", "Zoincaillan", "Robot"],
                Health = 8,
                Size = 1,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"),
                CombatSprite = ResourceLoader.LoadSprite("TimelineInfinityTasbeht", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("InfinityTasbehtDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineInfinityTasbeht", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/ImpertinenceHurt",
                DeathSound = "event:/ImpertinenceDie",
                CombatEnterEffects = [Effects.GenerateEffect(impertinenceNoises)],

            };
            infinityTasbeht.PrepareEnemyPrefab("Assets/ToyboxEnemies/Tasbeht/InfinityTasbehtEnemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());

            infinityTasbeht.enemy.abilities.Add(LoadedAssetsHandler.GetEnemy("SenekTasbeht_EN").abilities[0]);
            infinityTasbeht.enemy.abilities.Add(LoadedAssetsHandler.GetEnemy("SenekTasbeht_EN").abilities[1]);
            infinityTasbeht.enemy.abilities.Add(LoadedAssetsHandler.GetEnemy("SenekTasbeht_EN").abilities[2]);
            infinityTasbeht.enemy.abilities.Add(LoadedAssetsHandler.GetEnemy("SenekTasbeht_EN").abilities[3]);

            infinityTasbeht.AddPassive(Passives.Immortal);
            infinityTasbeht.AddPassive(Passives.GetCustomPassive("Condense_PA"));
            infinityTasbeht.AddPassive(Passives.Withering);
            infinityTasbeht.AddPassive(Passives.DecayGenerator(infinityTasbeht.enemy, 100, true));

            infinityTasbeht.AddEnemy(false, false, true);

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ENEMIES] Added the Infinity Tasbeht.");
            }
        }
    }
}
