using SorasToybox.CustomEffects;
using SorasToybox.CustomPassives;
using System;
using System.Collections.Generic;
using System.Text;
using static SorasToybox.Encounters.Garden.H;

namespace SorasToybox.Enemies
{
    public class DendriteDesibon
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect getRuptured = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getRuptured._Status = StatusField.Ruptured;

            StatusEffect_Apply_Effect getAnte = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getAnte._Status = StatusField.GetCustomStatusEffect("Ante_ID");

            Enemy desibonRevenge = new Enemy("Desibon's Revenge", "DesibonsRevenge_BOSS")
            {
                Health = 100,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineDesibonRevengeBoss.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineDesibonRevengeBoss.png", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("DarkYoung_BOSS").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("DarkYoung_BOSS").deathSound,
                UnitTypes = ["FemaleID", "Robot", "Zoincaillan"],
            };
            desibonRevenge.PrepareEnemyPrefab("Assets/ToyboxEnemies/DesibonRevenge/DesibonRevenge Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());
            desibonRevenge.AddPassives([Passives.Masochism1, CustomPassive.SaltLockstepGenerator(1)]);

            // The absolute agony that is Lockstep
            CasterStoreValueSetterEffect fuck = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            fuck.m_unitStoredDataID = "LockstepDir_SV";
            CasterStoreValueSetterEffect initialize = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
            initialize.m_unitStoredDataID = "LockstepAmount_SV";
            initialize._ignoreIfContains = true;
            desibonRevenge.CombatEnterEffects = [
                Effects.GenerateEffect(fuck, 1, Targeting.Slot_SelfSlot, null),
                Effects.GenerateEffect(initialize, 1, Targeting.Slot_SelfSlot),
            ];


            HealEffect thisIsGoodNewsMark = ScriptableObject.CreateInstance<HealEffect>();
            thisIsGoodNewsMark._directHeal = true;

            StatusEffect_Apply_Effect whiplashByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            whiplashByPrevious._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");
            whiplashByPrevious._MultPreviousExitValueForEntry = true;

            Ability baa = new Ability("ST_DesibonBaa_A")
            {
                Name = "BAAHAAHAA",
                Description = "Heals the Opposing party member, then inflicts Whiplash on them equal to the amount healed.\nIf no healing was dealt, inflicts 2 Ruptured to the Left and Right party members.\nGains 1 Ante.",
                Rarity = Rarity.Common,
                Visuals = Visuals.Slap,
                Effects =
                [
                    Effects.GenerateEffect(thisIsGoodNewsMark, 7, Targeting.Slot_Front),
                    Effects.GenerateEffect(whiplashByPrevious, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(getRuptured, 2, Targeting.Slot_OpponentSides),
                    Effects.GenerateEffect(getAnte, 1, Targeting.Slot_SelfSlot),
                ],
            };
            baa.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Heal_5_10), "Status_Whiplash"]);
            baa.AddIntentsToTarget(Targeting.Slot_OpponentSides, [nameof(IntentType_GameIDs.Status_Ruptured)]);
            baa.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Status_Ante"]);

            desibonRevenge.AddEnemyAbilities([baa]);
            desibonRevenge.AddEnemy(true, false, false);
            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("Sheep shenanigans.");
            }

            Enemy desibon = new Enemy("Dendrite Desibon", "DendriteDesibon_BOSS")
            {
                Health = 100,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineDesibonBoss.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse.png", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineDesibonBoss.png", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("DarkYoung_BOSS").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("DarkYoung_BOSS").deathSound,
                UnitTypes = ["FemaleID", "Robot", "Zoincaillan"],
            };
            //prefab for this binch
            desibon.PrepareEnemyPrefab("Assets/ToyboxEnemies/Desibon/DesibonEnemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/Yinimro/YinimroGibs.prefab").GetComponent<ParticleSystem>());
            //the below changes the highlight to surround a specific part of this enemy's prefab
            desibon.enemy.enemyTemplate.m_Data.m_Renderer = desibon.enemy.enemyTemplate.m_Data.m_Locator.transform.Find("Sprite").Find("BodyAnchor").Find("Body").GetComponent<SpriteRenderer>();

            //yeet clown, only baby now
            SpawnEnemyAnywhereEffect spawnGertar = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            spawnGertar.enemy = LoadedAssetsHandler.GetEnemy("StoneGertar_EN");

            AnimationVisualsEffect jumpscareHaha = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            jumpscareHaha._visuals = Visuals.Messiah;
            jumpscareHaha._animationTarget = Targeting.Slot_SelfSlot;


            SpawnEnemyAnywhereEffect move = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            move.givesExperience = false;
            move.enemy = LoadedAssetsHandler.GetEnemy("DesibonsRevenge_BOSS");
            move._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();

            //second phase fuck youuuuuuu
            PerformEffectPassiveAbility desibonGetsPissedTheFuckOff = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            desibonGetsPissedTheFuckOff._passiveName = "Decay";
            desibonGetsPissedTheFuckOff.passiveIcon = Passives.Example_Decay_MudLung.passiveIcon;
            desibonGetsPissedTheFuckOff.m_PassiveID = Passives.Example_Decay_MudLung.m_PassiveID;
            desibonGetsPissedTheFuckOff._enemyDescription = "On death, this enemy gets her revenge.";
            desibonGetsPissedTheFuckOff._characterDescription = "I don't care how many children you have you cannot tap into my maternal rage like that XOXO -Des";
            desibonGetsPissedTheFuckOff._triggerOn = new TriggerCalls[] { TriggerCalls.OnDeath };
            desibonGetsPissedTheFuckOff.doesPassiveTriggerInformationPanel = true;
            desibonGetsPissedTheFuckOff.effects = new EffectInfo[]
            {
                Effects.GenerateEffect(jumpscareHaha, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(move, 1, Targeting.Slot_SelfSlot),
            };

            //adding passives


            //effect set up
            SwapToOneSideEffect moveLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            moveLeft._swapRight = false;

            SwapToOneSideEffect moveRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            moveRight._swapRight = true;



            GenerateColorManaEffect makeRed = ScriptableObject.CreateInstance<GenerateColorManaEffect>();
            makeRed.mana = Pigments.Red;

            TargetPerformEffectViaSubaction makeRedSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            makeRedSubAct.effects =
                [
                    Effects.GenerateEffect(makeRed, 2, Targeting.Slot_SelfSlot),
                ];

            StatusEffectCheckerEffect hasRuptured = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            hasRuptured._status = StatusField.Ruptured;
            
            //the moves
            Ability beep = new Ability("ST_DesibonBeep_A")
            {
                Name = "BEEP BEEP",
                Description = "Moves Left. Inflicts 1 Ruptured to the Right enemy. If that enemy then isn't Ruptured, force them to produce 2 Red pigment.\nInflicts 2 Ruptured to the Opposing party member.",
                Cost = [],
                Priority = Priority.Fast,
                AnimationTarget = Targeting.Slot_SelfAll,
                Visuals = Visuals.StompLeft,    
                Effects =
                [
                    Effects.GenerateEffect(moveLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getRuptured, 1, Targeting.Slot_AllyRight),
                    Effects.GenerateEffect(hasRuptured, 1, Targeting.Slot_AllyRight),
                    Effects.GenerateEffect(makeRedSubAct, 1, Targeting.Slot_AllyRight, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(getRuptured, 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
            };
            beep.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            beep.AddIntentsToTarget(Targeting.Slot_AllyRight, [nameof(IntentType_GameIDs.Status_Ruptured), nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Mana_Generate)]);
            beep.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability meep = new Ability("ST_DesibonMeep_A")
            {
                Name = "MEEP MEEP",
                Description = "Moves Right. Inflicts 1 Ruptured to the Left enemy. If that enemy then isn't Ruptured, force them to produce 2 Red pigment.\nInflicts 2 Ruptured to the Opposing party member.",
                Cost = [],
                Priority = Priority.Fast,
                AnimationTarget = Targeting.Slot_SelfAll,
                Visuals = Visuals.StompRight,
                Effects =
                [
                    Effects.GenerateEffect(moveRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getRuptured, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(hasRuptured, 1, Targeting.Slot_AllyLeft),
                    Effects.GenerateEffect(makeRedSubAct, 1, Targeting.Slot_AllyLeft, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(getRuptured, 2, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Common,
            };
            meep.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            meep.AddIntentsToTarget(Targeting.Slot_AllyLeft, [nameof(IntentType_GameIDs.Status_Ruptured), nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Mana_Generate)]);
            meep.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Ruptured)]);

            Ability sheep = new Ability("ST_DesibonSheep_A")
            {
                Name = "SHEEP SHEEP",
                Description = "Spawns a Stone Gertar.\nInflicts 1 Ruptured to the Opposing party member.",
                Cost = [],
                Priority = Priority.Fast,
                AnimationTarget = Targeting.Slot_SelfAll,
                Visuals = Visuals.Exsanguinate,
                Effects =
                [
                    Effects.GenerateEffect(spawnGertar, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(getRuptured, 3, Targeting.Slot_Front),
                ],
                Rarity = Rarity.Rare,
            };
            sheep.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Other_Spawn)]);
            sheep.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Status_Ruptured)]);

            ExtraAbilityInfo sheepExtra = new()
            {
                ability = sheep.GenerateEnemyAbility().ability,
                rarity = Rarity.Impossible,
            };


            desibon.AddEnemyAbilities([
                beep, meep,
                ]);

            desibon.AddPassives([desibonGetsPissedTheFuckOff, Passives.ParentalGenerator(sheepExtra), Passives.GetCustomPassive("ST_Family_PA")]);

            desibon.AddEnemy(true, false, false);

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("Added the Dendrite Desibon.");
            }

            BackwardsUnlockCompatibility.TryLockItemBehindAchievement("DesibonBoss_ACH", "StoneWateringCan_SW");
            UnlockableModData desibonBossUnlockData = new UnlockableModData("DesibonsRevenge_BOSS");
            desibonBossUnlockData.hasModdedAchievementUnlock = true;
            desibonBossUnlockData.moddedAchievementID = "DesibonBoss_ACH";
            desibonBossUnlockData.hasItemUnlock = true;
            desibonBossUnlockData.items = ["StoneWateringCan_SW"];

            ListedUnlockCheck desibonUnlockCheck = ScriptableObject.CreateInstance<ListedUnlockCheck>();
            desibonUnlockCheck.unlockID = "DesibonsRevenge_BOSS";
            desibonUnlockCheck.unlockData = desibonBossUnlockData;
            Unlocks.AddUnlock_BeatBoss(desibonUnlockCheck);

            ModdedAchievements desibonBossAchievement = new ModdedAchievements("The Herd", "Destroy the Dendrite Desibon.", ResourceLoader.LoadSprite("Ach_Boss_Desibon", null, 32, null), "DesibonBoss_ACH");
            desibonBossAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.BossesTitleLabel);

            string[] desibonTips =
            [
                "Remember, Whiplash triggers on ANY damage received, even stuff from other statuses like Ruptured..",
                "The Petrification from the Gertars may be annoying, but it can temporarily make you immune to Ruptured.",
                "Ok seriously who the hell made this boss?", // thanks stoat
            ];
            BrutalAPI.Dialogues.AddCustom_GameOver_BossLines("Desibon_BOSS", desibonTips);
        }
    }
}
