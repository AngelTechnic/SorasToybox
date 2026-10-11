using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class SenekTasbeht
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

            Enemy senekTasbeht = new Enemy("Tasbeht", "SenekTasbeht_EN")
            {
                UnitTypes = ["Friendly", "Zoincaillan", "Robot"],
                Health = 1,
                Size = 1,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"),
                CombatSprite = ResourceLoader.LoadSprite("TimelineTasbeht", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("TasbehtDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineTasbeht", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/ImpertinenceHurt", 
                DeathSound = "event:/ImpertinenceDie",
                CombatEnterEffects = [Effects.GenerateEffect(impertinenceNoises)],

            };
            senekTasbeht.PrepareEnemyPrefab("Assets/ToyboxEnemies/Tasbeht/TasbehtEnemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());

            Ability senekTasbehtMoveLeft = new Ability("Rewind", "ST_SenekTasbehtLeft_A")
            {
                Description = "Moves Left.",
                Priority = Priority.ExtremelySlow,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(moveLeft, 1, Targeting.Slot_SelfSlot),
                ],
            };
            senekTasbehtMoveLeft.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Left"]);

            Ability senekTasbehtMoveRight = new Ability("Fast Forward", "ST_SenekTasbehtRight_A")
            {
                Description = "Moves Right.",
                Priority = Priority.ExtremelySlow,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(moveRight, 1, Targeting.Slot_SelfSlot),
                ],
            };
            senekTasbehtMoveRight.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Right"]);

            Ability senekTasbehtMoveSides = new Ability("Skip", "ST_SenekTasbehtSides_A")
            {
                Description = "Moves Left or Right.",
                Priority = Priority.ExtremelySlow,
                Rarity = Rarity.Uncommon,
                Effects =
                [
                    Effects.GenerateEffect(moveLeftOrRight, 1, Targeting.Slot_SelfSlot),
                ],
            };
            senekTasbehtMoveSides.AddIntentsToTarget(Targeting.Slot_SelfAll, ["Swap_Sides"]);

            Ability senekTasbehtShuffle = new Ability("Shuffle", "ST_SenekTasbehtShuffle_A")
            {
                Description = "Shuffles all enemy positions.",
                Priority = Priority.ExtremelySlow,
                Rarity = Rarity.VeryRare,
                Effects =
                [
                    Effects.GenerateEffect(scrambleEveryone, 1, Targeting.Unit_AllAllies),
                ],
            };
            senekTasbehtShuffle.AddIntentsToTarget(Targeting.Unit_AllAllies, [nameof(IntentType_GameIDs.Swap_Mass)]);

            senekTasbeht.AddPassive(Passives.GetCustomPassive("Condense_PA"));
            senekTasbeht.AddPassive(Passives.Constricting);
            senekTasbeht.AddEnemyAbilities([
                senekTasbehtMoveLeft,
                senekTasbehtMoveRight,
                senekTasbehtMoveSides,
                senekTasbehtShuffle,
                ]);

            senekTasbeht.AddEnemy(true, true, true);

        }
    }
}
