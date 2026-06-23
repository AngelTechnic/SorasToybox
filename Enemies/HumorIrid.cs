using BrutalAPI;
using SorasToybox.CustomEffects;
using SorasToybox.CustomPassives;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace SorasToybox.Enemies
{
    public class HumorIrid
    {

        public static void Add()
        {
            StatusEffect_Apply_Effect celerityMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            celerityMe._Status = StatusField.GetCustomStatusEffect("Celerity_ID");

            StatusEffect_Apply_Effect destabilizeMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            destabilizeMe._Status = StatusField.GetCustomStatusEffect("Destabilized_ID");

            StatusEffect_Apply_Effect destabilizeByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            destabilizeByPrevious._Status = StatusField.GetCustomStatusEffect("Destabilized_ID");
            destabilizeByPrevious._MultPreviousExitValueForEntry = true;

            StatusEffect_Apply_Effect edemaByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            edemaByPrevious._Status = StatusField.GetCustomStatusEffect("Edema_ID");
            edemaByPrevious._MultPreviousExitValueForEntry = true;



            AddPassiveEffect getIridBlooded = ScriptableObject.CreateInstance<AddPassiveEffect>();
            getIridBlooded._passiveToAdd = Passives.GetCustomPassive("IridBlooded_1_PA");


            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            SwapToSidesEffect swapSides = ScriptableObject.CreateInstance<SwapToSidesEffect>();


            CheckEntryOrMoreEmptyManaSlotsEffect slotcheck = ScriptableObject.CreateInstance<CheckEntryOrMoreEmptyManaSlotsEffect>();

            //Consume most common
            ConsumeCommonColorManaEffect eatCookies = ScriptableObject.CreateInstance<ConsumeCommonColorManaEffect>();

            //exsanguinate/skew the humors anim if generating mana
            //slice/lobotomy anim if dealing damage
            AttackVisualsSO dooranim = ScriptableObject.CreateInstance<AttackVisualsSO>();
            if (SorasToybox.CrossMod.GlitchsFreaks)
            {
                //Debug.Log("organ failure visuals");
                dooranim = LoadedAssetsHandler.GetEnemyAbility("DrainFluidsAB").visuals;
            }
            else
            {
                dooranim = Visuals.Exsanguinate;
            }
            AttackVisualsSO dooranim2 = ScriptableObject.CreateInstance<AttackVisualsSO>();
            if (SorasToybox.CrossMod.GlitchsFreaks)
            {
                //Debug.Log("knife board visuals");
                dooranim2 = LoadedAssetsHandler.GetEnemyAbility("PracticedAgonyAB").visuals;
            }
            else
            {
                dooranim2 = Visuals.SliceAndDice;
            }


            AnimationVisualsEffect manaAnim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            manaAnim._visuals = dooranim;
            manaAnim._animationTarget = Targeting.Slot_Front;
            AnimationVisualsEffect dmganim = ScriptableObject.CreateInstance<AnimationVisualsEffect>();
            dmganim._visuals = dooranim2;
            dmganim._animationTarget = Targeting.Slot_Front;
            GenerateTargetHealthManaEffect frontmana = ScriptableObject.CreateInstance<GenerateTargetHealthManaEffect>();


            //after all the effects are done now we set up the enemy!
            Enemy iridHumor = new Enemy("Psychedelic Humour", "HumourPsychedelic_EN")
            {
                Health = 50,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"),
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineHumorIridescent", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineHumorIridescent", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetEnemy("WRK_EN").damageSound,
                DeathSound = LoadedAssetsHandler.GetEnemy("WRK_EN").deathSound,
            };
            //the passives will need to be fuddled with
            iridHumor.AddPassives([Passives.Pure, CustomPassive.BloatGenerator(1), Passives.GetCustomPassive("Condense_PA"), LoadedAssetsHandler.GetEnemy("Sinker_EN").passiveAbilities[0]]);


            Ability scramble = new Ability("Scramble", "ST_HumorScramble_A")
            {
                Description = "Moves Left or Right. Applies 3 Destabilzied to the Opposing party member." +
                "\nAttempts to generate 4 pigment of their health color; instead dealing a \"Painful\" amount of damage if this would contribute to Overflow." +
                "\nIf damage was dealt, gain equivalent Edema.",
                Rarity = Rarity.Common,
                Cost = null, //Millie if you're reading this do something funny with it.
                Effects =
                [
                    Effects.GenerateEffect(swapSides, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(destabilizeMe, 3, Targeting.Slot_Front),
                    Effects.GenerateEffect(slotcheck, 4, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(dmganim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    Effects.GenerateEffect(damage, 6, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(false, 2)),
                    Effects.GenerateEffect(edemaByPrevious, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 3)),
                    Effects.GenerateEffect(manaAnim, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(true, 4)),
                    Effects.GenerateEffect(frontmana, 4, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 5)),
                ],
            };

            scramble.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Swap_Sides), "Status_Edema"]);
            scramble.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Destabilized", nameof(IntentType_GameIDs.Mana_Generate), nameof(IntentType_GameIDs.Damage_7_10)]);

            Ability simulate = new Ability("Simulate", "ST_HumorSimulate_A")
            {
                Description = "Consumes all of the most common pigment. Grants thie enemy Edema equal to the amount consumed.",
                Cost = null, //Millie same here
                Rarity = Rarity.Common,
                Visuals = Visuals.Gulp,
                AnimationTarget = Targeting.Slot_SelfSlot,
                Effects =
                [
                    Effects.GenerateEffect(eatCookies, 10, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(edemaByPrevious, 1, Targeting.Slot_SelfSlot),
                ],
            };
            simulate.AddIntentsToTarget(Targeting.Slot_SelfSlot, [nameof(IntentType_GameIDs.Mana_Consume), "Status_Edema"]);

            RemoveStatusEffectEffect noEdema = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
            noEdema._status = StatusField.GetCustomStatusEffect("Edema_ID");

            AttackVisualsSO GlitchVisuals = ScriptableObject.CreateInstance<AttackVisualsSO>();
            if (SorasToybox.CrossMod.Siren)
            {
                GlitchVisuals = LoadedAssetsHandler.GetEnemyAbility("PraiseAB").visuals;
            }
            else
            {
                GlitchVisuals = LoadedAssetsHandler.GetCharacterAbility("SamDefrag_A").visuals;
            }

            StatusEffectCheckerEffect didIHaveEdema = ScriptableObject.CreateInstance<StatusEffectCheckerEffect>();
            didIHaveEdema._status = StatusField.GetCustomStatusEffect("Edema_ID");

            StatusEffect_Apply_Effect celerityByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            celerityByPrevious._Status = StatusField.GetCustomStatusEffect("Celerity_ID");
            celerityByPrevious._MultPreviousExitValueForEntry = true;

            Ability inviteStorm = new Ability("Invite Storm", "ST_InviteStormHumor_A")
            {
                Description = "Applies Iridescent-Blooded to the Left and Right enemies.\nApplies 1 Destabilized onto all units.\nExchanges Edema for Celerity on the weakest other enemy.",
                Cost = null, //Millie same here
                Rarity = Rarity.Common,
                Priority = Priority.ExtremelyFast,
                Visuals = GlitchVisuals,
                AnimationTarget = Targeting.AllUnits,
                Effects =
                [
                    Effects.GenerateEffect(getIridBlooded, 1, Targeting.Slot_AllySides),
                    Effects.GenerateEffect(destabilizeMe, 1, Targeting.AllUnits),
                    Effects.GenerateEffect(noEdema, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(celerityByPrevious, 1, Targeting.GenerateUnitTarget_Specific_Health(true, true, false, true)),
                ]
            };
            inviteStorm.AddIntentsToTarget(Targeting.Slot_AllySides, ["Passive_Blooded"]);
            inviteStorm.AddIntentsToTarget(Targeting.Unit_AllAllies, ["Status_Destabilized"]);
            inviteStorm.AddIntentsToTarget(Targeting.Unit_AllOpponents, ["Status_Destabilized"]);
            inviteStorm.AddIntentsToTarget(Targeting.Slot_SelfSlot, ["Rem_Status_Edema"]);
            inviteStorm.AddIntentsToTarget(Targeting.GenerateUnitTarget_Specific_Health(true, true, false, true), ["Status_Celerity"]);

            ExtraAbilityInfo psychedelicExtra = new()
            {
                ability = inviteStorm.GenerateEnemyAbility().ability,
                rarity = Rarity.ImpossibleNoReroll,
            };

            iridHumor.AddEnemyAbilities([
                scramble,
                simulate,
                ]);

            iridHumor.AddPassive(Passives.BonusAttackGenerator(psychedelicExtra));
            iridHumor.AddEnemy(true, true, false);

            LoadedAssetsHandler.GetEnemy("HumourPsychedelic_EN").enemyTemplate = LoadedAssetsHandler.GetEnemy("HumourCholeric_EN").enemyTemplate;
        }
    }
}
