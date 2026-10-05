using FMOD;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using static SorasToybox.Enemies.ParadoxYinimro;

namespace SorasToybox.Enemies
{
    public class TANGO
    {
        public class TangoCrazyMusicEffect : EffectSO
        {
            public static int Amount = 0;
            public static void Reset() => Amount = 0;
            public bool Add = true;
            public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
            {
                exitAmount = 0;
                if (CombatManager.Instance._stats.audioController.MusicCombatEvent.getParameterByName("TangoCrazy", out float num) == FMOD.RESULT.OK)
                {
                    CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("TangoCrazy", Add ? num + entryVariable : (entryVariable > num ? 0 : num - entryVariable));
                }
                return true;
            }
        }

        public class TangoFirstTransformMusicEffect : EffectSO
        {
            public static int Amount = 0;
            public static void Reset() => Amount = 0;
            public bool Add = true;
            public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
            {
                exitAmount = 0;
                if (CombatManager.Instance._stats.audioController.MusicCombatEvent.getParameterByName("TangoFirstActivate", out float num) == FMOD.RESULT.OK)
                {
                    CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("TangoFirstActivate", Add ? num + entryVariable : (entryVariable > num ? 0 : num - entryVariable));
                }
                return true;
            }
        }
        public static void Add()
        {
            StatusEffect_Apply_Effect doFrail = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            doFrail._Status = StatusField.Frail;

            StatusEffect_Apply_Effect doFocused = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            doFocused._Status = StatusField.Focused;

            RemoveStatusEffectEffect noFocus = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noFocus._status = StatusField.Focused;

            DamageWithStatusBonusEffect damageFrailBonus = ScriptableObject.CreateInstance<DamageWithStatusBonusEffect>();
            damageFrailBonus._status = StatusField.Frail;
            damageFrailBonus._bonusStacking = true;
            damageFrailBonus._bonusAmount = 1;

            StatusEffectCheckerEffect checkFrail = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            checkFrail._status = StatusField.Frail;

            

            //set up enemies here
            Enemy tangoStatue = new Enemy("TANGO", "TANGO_EN")
            {
                Health = 35,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineTANGO", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("tango_dead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineTANGO", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
                UnitTypes = ["Zoincaillan"],
            };
            tangoStatue.PrepareEnemyPrefab("Assets/ToyboxEnemies/TANGO/TANGOStatue_Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/SEARCH/Ichor_Gibs.prefab").GetComponent<ParticleSystem>());

            Enemy tangoHostile = new Enemy("TANGO", "TANGOHostile_EN")
            {
                Health = 35,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("TimelineTANGOEvil", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("tango_dead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("TimelineTANGOEvil", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Enemies/TANGO/TangoHurt",
                DeathSound = "event:/SorasSFX/Enemies/TANGO/TangoDie",
                UnitTypes = ["Zoincaillan"],
            };
            tangoHostile.PrepareEnemyPrefab("Assets/ToyboxEnemies/TANGO/TANGOHostile_Enemy.prefab", SorasToybox.assetbundle, SorasToybox.assetbundle.LoadAsset<GameObject>("Assets/ToyboxEnemies/SEARCH/Ichor_Gibs.prefab").GetComponent<ParticleSystem>());


            //music
            TangoCrazyMusicEffect add1 = ScriptableObject.CreateInstance<TangoCrazyMusicEffect>();
            add1.Add = true;

            TangoCrazyMusicEffect remove1 = ScriptableObject.CreateInstance<TangoCrazyMusicEffect>();
            remove1.Add = false;

            TangoFirstTransformMusicEffect add2 = ScriptableObject.CreateInstance<TangoFirstTransformMusicEffect>();
            add2.Add = true;

            TargetPerformEffectViaSubaction tangoInSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            tangoInSubAct.effects =
            [
                Effects.GenerateEffect(add1,1),
                Effects.GenerateEffect(add2, 1),
            ];

            TargetPerformEffectViaSubaction tangoOutSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            tangoOutSubAct.effects =
            [
                Effects.GenerateEffect(remove1,1),
            ];

            tangoHostile.CombatEnterEffects = new EffectInfo[]
            {
                Effects.GenerateEffect(tangoInSubAct, 1, Targeting.Slot_SelfSlot),
            };

            tangoHostile.CombatExitEffects = new EffectInfo[]
            {
                Effects.GenerateEffect(tangoOutSubAct, 1, Targeting.Slot_SelfSlot),
            };



            //mercurial handled here
            CasterTransformationEffect becomeStatue = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            becomeStatue._maintainMaxHealth = true;
            becomeStatue._maintainTimelineAbilities = true;
            becomeStatue._currentToMaxHealth = false;
            becomeStatue._fullyHeal = false;
            becomeStatue._enemyTransformation = tangoStatue.enemy;
            becomeStatue._characterTransformation = "Nowak_CH";

            CasterTransformationEffect becomeHostile = ScriptableObject.CreateInstance<CasterTransformationEffect>();
            becomeHostile._maintainMaxHealth = true;
            becomeHostile._maintainTimelineAbilities = true;
            becomeHostile._currentToMaxHealth = false;
            becomeHostile._fullyHeal = false;
            becomeHostile._enemyTransformation = tangoHostile.enemy;
            becomeHostile._characterTransformation = "Nowak_CH";

            CheckHasUnitEffect isThereAnybodyOutThere = ScriptableObject.CreateInstance<CheckHasUnitEffect>();

            PassivePopUpOnTargetEffect MercurialPopup = ScriptableObject.CreateInstance<PassivePopUpOnTargetEffect>();
            MercurialPopup._name = "Mercurial";
            MercurialPopup._sprite = "IconTransformPassive";
            MercurialPopup._isUnitCharacter = false;

            AttackVisualsSO gazeVis = ScriptableObject.CreateInstance<AttackVisualsSO>();
            if (SorasToybox.CrossMod.AApocrypha)
            {
                gazeVis = LoadedAssetsHandler.GetEnemyAbility("AApocrypha_InvokeChaos_A").visuals;
            }
            else
            {
                gazeVis = Visuals.Providence;
            }

            AnimationVisualsEffect gazeEffect = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            gazeEffect._visuals = gazeVis;
            gazeEffect._animationTarget = Targeting.Slot_Front;


            PerformEffectPassiveAbility mercurialTangoToHostile = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            mercurialTangoToHostile.name = "ST_Mercurial_TangoToHostile";
            mercurialTangoToHostile._passiveName = "Mercurial";
            mercurialTangoToHostile.m_PassiveID = "Mercurial";
            mercurialTangoToHostile.passiveIcon = ResourceLoader.LoadSprite("IconTransformPassive");
            mercurialTangoToHostile._characterDescription = "steal focus and you too can become the depressed polish man";
            mercurialTangoToHostile._enemyDescription = "At the end of the timeline, this enemy will attempt to remove Focused from the Opposing party member. If that fails, it awakens.";
            mercurialTangoToHostile._triggerOn = [TriggerCalls.TimelineEndReached];
            mercurialTangoToHostile.doesPassiveTriggerInformationPanel = false;
            mercurialTangoToHostile.effects = [
                Effects.GenerateEffect(MercurialPopup, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(gazeEffect, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(noFocus, 1, Targeting.Slot_Front),
                Effects.GenerateEffect(becomeHostile, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                //Effects.GenerateEffect(ScriptableObject.CreateInstance<AddTurnCasterToTimelineEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
            ];
            Passives.AddCustomPassiveToPool("ST_Mercurial_TangoToHostile_PA", "Mercurial", mercurialTangoToHostile);

            PerformEffectPassiveAbility mercurialTangoToStatue = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            mercurialTangoToStatue.name = "ST_Mercurial_TangoToStatue";
            mercurialTangoToStatue._passiveName = "Mercurial";
            mercurialTangoToStatue.m_PassiveID = "Mercurial";
            mercurialTangoToStatue.passiveIcon = ResourceLoader.LoadSprite("IconTransformPassive");
            mercurialTangoToStatue._characterDescription = "have no friends and you too can become the depressed polish man";
            mercurialTangoToStatue._enemyDescription = "At the end of the timeline, if this enemy has no opponent, it sleeps.";
            mercurialTangoToStatue._triggerOn = [TriggerCalls.TimelineEndReached];
            mercurialTangoToStatue.doesPassiveTriggerInformationPanel = false;
            mercurialTangoToStatue.effects = [
                Effects.GenerateEffect(MercurialPopup, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(gazeEffect, 1, Targeting.Slot_SelfSlot),
                Effects.GenerateEffect(isThereAnybodyOutThere, 1, Targeting.Slot_Front),
                Effects.GenerateEffect(becomeStatue, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                //Effects.GenerateEffect(ScriptableObject.CreateInstance<AddTurnCasterToTimelineEffect>(), 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 3)),
            ];
            Passives.AddCustomPassiveToPool("ST_Mercurial_TangoToStatue_PA", "Mercurial", mercurialTangoToStatue);


            //ok now that we have the enemies and mercurial passives set up, let's do this.
            tangoStatue.AddPassives([Passives.Inanimate, Passives.Infantile, Passives.GetCustomPassive("WhiteBlooded_1_PA"), mercurialTangoToHostile]);
            tangoHostile.AddPassives([Passives.Slippery, Passives.Infantile, Passives.Masochism1, mercurialTangoToStatue]);


            Ability tangoStatueAbilLeft = new Ability("ST_ISawItBlink_A")
            {
                Name = "I Saw It Blink",
                Description = "Inflicts 1 Frail to all party members to the left of this enemy.\nTargets that didn't already have Frail get 1 more.",
                Visuals = Visuals.Lullaby,
                AnimationTarget = Targeting.Slot_OpponentAllLefts,
                Rarity = Rarity.Common,
                Effects = 
                [
                    Effects.GenerateEffect(checkFrail, 1, Targeting.GenerateSlotTarget([-1], false)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.GenerateSlotTarget([-1], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(checkFrail, 1, Targeting.GenerateSlotTarget([-2], false)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.GenerateSlotTarget([-2], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(checkFrail, 1, Targeting.GenerateSlotTarget([-3], false)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.GenerateSlotTarget([-3], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(checkFrail, 1, Targeting.GenerateSlotTarget([-4], false)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.GenerateSlotTarget([-4], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.Slot_OpponentAllLefts),
                ]
            };
            tangoStatueAbilLeft.AddIntentsToTarget(Targeting.Slot_OpponentAllLefts, [nameof(IntentType_GameIDs.Status_Frail)]);

            Ability tangoStatueAbilRight = new Ability("ST_ISawItGlance_A")
            {
                Name = "I Saw It Glance",
                Description = "Inflicts 1 Frail to all party members to the right of this enemy.\nTargets that didn't already have Frail get 1 more.",
                Visuals = Visuals.Lullaby,
                AnimationTarget = Targeting.Slot_OpponentAllRights,
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(checkFrail, 1, Targeting.GenerateSlotTarget([1], false)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.GenerateSlotTarget([1], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(checkFrail, 1, Targeting.GenerateSlotTarget([2], false)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.GenerateSlotTarget([2], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(checkFrail, 1, Targeting.GenerateSlotTarget([3], false)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.GenerateSlotTarget([3], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(checkFrail, 1, Targeting.GenerateSlotTarget([4], false)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.GenerateSlotTarget([4], false), Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(doFrail, 1, Targeting.Slot_OpponentAllRights),
                ]
            };
            tangoStatueAbilRight.AddIntentsToTarget(Targeting.Slot_OpponentAllRights, [nameof(IntentType_GameIDs.Status_Frail)]);

            Ability tangoStatueAbilFront = new Ability("ST_ItsLookingAtMe_A")
            {
                Name = "It's Looking At Me",
                Description = "If the Opposing party member is already Frail, they gain Focused.\nInflict 2 Frail to the Opposing party member.",
                Visuals = Visuals.Lullaby,
                AnimationTarget = Targeting.Slot_Front,
                Rarity = Rarity.Rare,
                Effects =
                [
                    Effects.GenerateEffect(checkFrail, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(doFocused, 1, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(doFrail, 2, Targeting.Slot_Front),
                ],
            };
            tangoStatueAbilFront.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Misc_Hidden), nameof(IntentType_GameIDs.Status_Focused), nameof(IntentType_GameIDs.Status_Frail)]);

            tangoStatue.AddEnemyAbilities([
                tangoStatueAbilLeft, tangoStatueAbilRight, tangoStatueAbilFront,
                ]);


            //ok now for the evil guy
            DamageEffect hitSelf = ScriptableObject.CreateInstance<DamageEffect>();
            hitSelf._indirect = true;
            
            SwapToOneSideEffect moveLeft = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            moveLeft._swapRight = false;

            SwapToOneSideEffect moveRight = ScriptableObject.CreateInstance<SwapToOneSideEffect>();
            moveRight._swapRight = true;

            AnimationVisualsEffect decimationEffect = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            decimationEffect._visuals = Visuals.Decimate;
            decimationEffect._animationTarget = Targeting.Slot_Front;

            Ability tangoHostileAbilLeft = new Ability("ST_SLIDETOTHELEFT_A")
            {
                Name = "SLIDE TO THE LEFT",
                Description = "Moves Left.\nDeals a barely Painful amount of damage to the Opposing party member, boosted by the amount of Frail they have.\nAfter, if the Opponent is still Frail, take almost no damage.",
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(moveLeft, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(decimationEffect, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageFrailBonus, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(checkFrail, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(hitSelf, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            tangoHostileAbilLeft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Left)]);
            tangoHostileAbilLeft.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Rem_Status_Frail), nameof(IntentType_GameIDs.Misc_Hidden)]);
            tangoHostileAbilLeft.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_1_2)]);

            Ability tangoHostileAbilRight = new Ability("ST_SLIDETOTHERIGHT_A")
            {
                Name = "SLIDE TO THE RIGHT",
                Description = "Moves Right.\nDeals a barely Painful amount of damage to the Opposing party member, boosted by the amount of Frail they have.\nAfter, if the Opponent is still Frail, take almost no damage.",
                Rarity = Rarity.Common,
                Effects =
                [
                    Effects.GenerateEffect(moveRight, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(decimationEffect, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(damageFrailBonus, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(checkFrail, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(hitSelf, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 1)),
                ],
            };
            tangoHostileAbilRight.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Right)]);
            tangoHostileAbilRight.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Rem_Status_Frail), nameof(IntentType_GameIDs.Misc_Hidden)]);
            tangoHostileAbilRight.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Damage_1_2)]);

            RemoveStatusEffectEffect noFrail = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noFrail._status = StatusField.Frail;

            Ability tangoHostileAbilFront = new Ability("ST_DANSEMACABRE_A")
            {
                Name = "DANSE MACABRE",
                Description = "Deals a Painful amount of damage to the Opposing party member, boosted by their amount of Frail. Survivors lose all Frail and get Focused.",
                Visuals = Visuals.SliceAndDice,
                AnimationTarget = Targeting.Slot_Front,
                Effects =
                [
                    Effects.GenerateEffect(damageFrailBonus, 5, Targeting.Slot_Front),
                    Effects.GenerateEffect(noFrail, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(doFocused, 1, Targeting.Slot_Front),
                ]
            };
            tangoHostileAbilFront.AddIntentsToTarget(Targeting.Slot_Front, [nameof(IntentType_GameIDs.Damage_3_6), nameof(IntentType_GameIDs.Rem_Status_Frail), nameof(IntentType_GameIDs.Status_Focused)]);

            tangoHostile.AddEnemyAbilities([
                tangoHostileAbilLeft, tangoHostileAbilRight, tangoHostileAbilFront,
                ]);

            tangoStatue.AddEnemy(true, true, false);
            tangoHostile.AddEnemy(true, true, false);

            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("Added TANGO.");
            }
        }
    }
}
