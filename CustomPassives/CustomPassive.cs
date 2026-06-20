using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using BrutalAPI;
using SorasToybox.Custom_Passives;
using SorasToybox.CustomEffects;
using SorasToybox.CustomOther;
using HarmonyLib;
using UnityEngine;

namespace SorasToybox.CustomPassives
{
    public class CustomPassive
    {
        public static void Add()
        {
            //Karmic passive
            PerformEffectPassiveAbility karmic = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
            karmic.name = "Karmic_PA";
            karmic._passiveName = "Karmic";
            karmic.m_PassiveID = "Karmic";
            karmic.passiveIcon = ResourceLoader.LoadSprite("passive_karmic.png");
            karmic._characterDescription = "On receiving damage, apply equivalent Regeneration to all party members.";
            karmic._enemyDescription = "On receiving damage, apply equivalent Regeneration to all enemies.";
            karmic._triggerOn = [TriggerCalls.OnDamaged];
            karmic.conditions = [ScriptableObject.CreateInstance<KarmicCondition>()];
            karmic.effects = [];

            Passives.AddCustomPassiveToPool("Karmic_PA", "Karmic", karmic);

            //Dismal Passive
            DamageTypeImmunityPassiveAbility dismal = ScriptableObject.CreateInstance<DamageTypeImmunityPassiveAbility>();
            dismal.name = "Dismal_PA";
            dismal._passiveName = "Dismal";
            dismal.m_PassiveID = "Dismal";
            dismal.passiveIcon = ResourceLoader.LoadSprite("passive_dismal.png");
            dismal._characterDescription = "This party member does not take wrong pigment or overflow damage. On using the wrong pigment to perform this party member's own abilities, she targets herself instead of her Opponent.";
            dismal._enemyDescription = "This enemy is begging for the embrace of death.";
            dismal._damageType = CombatType_GameIDs.Dmg_Pigment.ToString();
            dismal.doesPassiveTriggerInformationPanel = false;
            dismal._triggerOn = [TriggerCalls.OnBeingDamaged];

            Passives.AddCustomPassiveToPool("Dismal_PA", "Dismal", dismal);
            GlossaryPassives STKarmicInfo = new GlossaryPassives("Karmic", "On receiving damage, apply equivalent Regeneration to all allies.", ResourceLoader.LoadSprite("passive_karmic"));
            LoadedDBsHandler.GlossaryDB.AddNewPassive(STKarmicInfo);

            //Search Party passive
            if (!LoadedDBsHandler.PassiveDB._PassivesPool.Contains("SearchParty_PA"))
            {
                SearchPartyPassiveAbility preserve = ScriptableObject.CreateInstance<SearchPartyPassiveAbility>();
                preserve._passiveName = "Search Party (1)";
                preserve.passiveIcon = ResourceLoader.LoadSprite("passive_searchparty.png");
                preserve._enemyDescription = "This enemy has additional attacks equal to the amount of units in combat with Search Party.";
                preserve._characterDescription = "https://www.youtube.com/watch?v=zSFlvUxBrJA";
                preserve.m_PassiveID = "SearchParty_PA";
                preserve.doesPassiveTriggerInformationPanel = true;
                preserve._triggerOn = new TriggerCalls[] { TriggerCalls.AttacksPerTurn };
                preserve._modifyVal = 1;
                //preserve.specialStoredData = lockstepState;
                Passives.AddCustomPassiveToPool("SearchParty_PA", "Search Party", preserve);
                GlossaryPassives STSearchPartyInfo = new GlossaryPassives("Search Party", "This character receives additional actions equal to the amount of other units in combat with Search Party.", ResourceLoader.LoadSprite("passive_searchparty"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(STSearchPartyInfo);
            }

            //Fragile passive woo yea woo
            if (LoadedDBsHandler.PigmentDB.GetPigment("Broken") != null)
            {
                // Fragile - broken pigment-related passive from Undivine Comedy (thanks WolfaCola)
                PerformDoubleEffectPassiveAbility Fragile = ScriptableObject.CreateInstance<PerformDoubleEffectPassiveAbility>();
                Fragile.name = "ST_Fragile_PA";
                Fragile._passiveName = "Fragile";
                Fragile.m_PassiveID = "Fragile";
                Fragile.passiveIcon = ResourceLoader.LoadSprite("Fragile");
                Fragile._characterDescription = "This party member's health will be Broken upon taking direct damage.\nThis party member also shatters all Broken pigment upon death.\nBroken Pigment naturally shatters upon overflow.";
                Fragile._enemyDescription = "This enemy's health will be Broken upon taking direct damage.\nThis enemy also shatters all Broken pigment upon death.\nBroken Pigment naturally shatters upon overflow.";

                ChangeToRandomHealthColorEffect setBroken = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
                setBroken._healthColors = [LoadedDBsHandler.PigmentDB.GetPigment("Broken")];

                Fragile._triggerOn = [TriggerCalls.OnDirectDamaged];
                Fragile._secondTriggerOn = [TriggerCalls.OnDeath];
                Fragile.effects =
                [
                    Effects.GenerateEffect(setBroken, 1, Targeting.Slot_SelfSlot),
                ];
                Fragile._secondEffects =
                [
                    Effects.GenerateEffect(ScriptableObject.CreateInstance<ShatterBrokenPigmentEffect>()),
                ];
                if (!LoadedDBsHandler.PassiveDB._PassivesPool.Contains("Fragile_PA")) { Passives.AddCustomPassiveToPool("Fragile_PA", "Fragile", Fragile); }
            }

            //godray
            if (LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Illuminated_ID"))
            {
                StatusEffectPassiveAbility godray = ScriptableObject.CreateInstance<StatusEffectPassiveAbility>();
                godray.name = "ST_Godray_PA";
                godray._passiveName = "Godray";
                godray._Status = StatusField.GetCustomStatusEffect("Illuminated_ID");
                godray.m_PassiveID = "Godray_ID";
                godray.passiveIcon = ResourceLoader.LoadSprite("passive_godray.png");
                godray._characterDescription = "This party member is permanently Illuminated and takes half damage from all sources.";
                godray._enemyDescription = "This enemy is permanently Illuminated and takes half damage from all sources.";
                godray.doesPassiveTriggerInformationPanel = true;


                Passives.AddCustomPassiveToPool("ST_Godray_PA", "Godray", godray);
                GlossaryPassives STGodrayInfo = new GlossaryPassives("Godray", "This party member/enemy is permanently Illuminated and takes half damage from all sources.", ResourceLoader.LoadSprite("passive_godray"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(STGodrayInfo);
            }


            if (LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Overclock_ID"))
            {
                StatusEffectPassiveAbility overtuned = ScriptableObject.CreateInstance<StatusEffectPassiveAbility>();
                overtuned.name = "ST_Overtuned_PA";
                overtuned._passiveName = "Overtuned";
                overtuned.m_PassiveID = "Overtuned_ID";
                overtuned._Status = StatusField.GetCustomStatusEffect("Overclock_ID");
                overtuned.passiveIcon = ResourceLoader.LoadSprite("passive_overtuned.png");
                overtuned._characterDescription = "This party member is permanently Overclocked and deals double direct damage.";
                overtuned._enemyDescription = "This enemy is permanently Overclocked and deals double direct damage.";
                overtuned.doesPassiveTriggerInformationPanel = true;
                Passives.AddCustomPassiveToPool("ST_Overtuned_PA", "Overtuned", overtuned);
                GlossaryPassives STOvertunedInfo = new GlossaryPassives("Overtuned", "This party member/enemy is permanently Overclocked and deals double direct damage.", ResourceLoader.LoadSprite("passive_overtuned"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(STOvertunedInfo);
            }

            //stole this wholesale from radio ooooops
            if (SorasToybox.CrossMod.SaltEnemies)
            {
                //string note = "until i figure out how the fuck to do this...sorry...";
                PerformEffectPassiveAbility itchy = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                itchy.name = "Itchy_PA";
                itchy._passiveName = "Itchy";
                itchy.passiveIcon = ResourceLoader.LoadSprite("passive_itchy.png");
                itchy._enemyDescription = "This enemy can't wait to act. In response to any party member movement or ability usage, they will perform their next action, and then gain another to replace the one they just performed.";
                //ch desc
                itchy.m_PassiveID = "Itchy_ID";
                itchy.doesPassiveTriggerInformationPanel = true;

                itchy._triggerOn = (LoadedAssetsHandler.GetEnemy("GreyBot_EN").passiveAbilities[0] as PerformEffectPassiveAbility)._triggerOn;
                itchy.effects = (LoadedAssetsHandler.GetEnemy("BlueSky_BOSS").passiveAbilities[0] as PerformEffectPassiveAbility).effects;
                Passives.AddCustomPassiveToPool("Itchy_PA", "Itchy", itchy);
                GlossaryPassives STItchyInfo = new GlossaryPassives("Itchy", "This enemy can't wait to act. In response to any party member manually moving or using an ability, they will perform their next action, and then gain another to replace the one they just performed.", ResourceLoader.LoadSprite("passive_itchy"));
                LoadedDBsHandler.GlossaryDB.AddNewPassive(STItchyInfo);
            }
            if (!LoadedDBsHandler.PassiveDB._PassivesPool.Contains("MadeOfFire_PA"))
            {
                DamageTypeImmunityPassiveAbility fireproofPassive = ScriptableObject.CreateInstance<DamageTypeImmunityPassiveAbility>();
                fireproofPassive.name = "MadeOfFire_PA";
                fireproofPassive._passiveName = "Made Of Fire";
                fireproofPassive.m_PassiveID = "MadeOfFire";
                fireproofPassive.passiveIcon = ResourceLoader.LoadSprite("IconFireskull");
                fireproofPassive._characterDescription = "This party member is unaffected by Fire and immune to fire damage.";
                fireproofPassive._enemyDescription = "This enemy is unaffected by Fire and immune to fire damage.";
                fireproofPassive.doesPassiveTriggerInformationPanel = false;
                fireproofPassive._triggerOn = [TriggerCalls.OnBeingDamaged];
                fireproofPassive._damageType = CombatType_GameIDs.Dmg_Fire.ToString();
            }
        }

        //Bloat Generator thank GOD
        public static BasePassiveAbilitySO BloatGenerator(int amount)
        {
            return GetOrCreatePassive(GeneratedBloat, amount, delegate (int x)
            {
                StatusEffect_Apply_Effect applyEdema = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
                applyEdema._Status = StatusField.GetCustomStatusEffect("Edema_ID");
                //PercentageEffectCondition
                PerformEffectPassiveAbility performEffectPassiveAbility = ScriptableObject.CreateInstance<PerformEffectPassiveAbility>();
                performEffectPassiveAbility.name = $"Bloat_{x}_PA";
                performEffectPassiveAbility.m_PassiveID = "Bloat_ID";
                performEffectPassiveAbility._passiveName = $"Bloat ({x})";
                performEffectPassiveAbility._characterDescription = $"This character gains {x} Edema at the end of each round.";
                performEffectPassiveAbility._enemyDescription = $"This enemy gains {x} Edema at the end of each round.";
                performEffectPassiveAbility.passiveIcon = ResourceLoader.LoadSprite("bloatPassive.png");
                performEffectPassiveAbility._triggerOn = new TriggerCalls[1] { TriggerCalls.OnRoundFinished };
                performEffectPassiveAbility.effects = new EffectInfo[1] {
                    Effects.GenerateEffect(applyEdema, x, Targeting.Slot_SelfSlot),
                };
                performEffectPassiveAbility.conditions = new EffectorConditionSO[0] { };
                performEffectPassiveAbility.doesPassiveTriggerInformationPanel = true;
                performEffectPassiveAbility.specialStoredData = LoadedDBsHandler.MiscDB.GetUnitStoreData("Bloat_SV");
                return performEffectPassiveAbility;
            });
        }


        // Bonus Suite: Multiple Bonus Attack Passive to replace Alt Attacks
        public static BasePassiveAbilitySO BonusSuiteGenerator(List<ExtraAbilityInfo> bonusAbilities)
        {
            List<string> names = [];
            List<ExtraAbilityInfo> weights = [];
            foreach (ExtraAbilityInfo ability in bonusAbilities)
            {
                if (ability == null || ability.ability == null)
                {
                    Debug.Log("Bonus ability " + ability.ability.name + " does not exist.");
                    return null;
                }
                names.Add(ability.ability._abilityName);
                for (int i = 0; i < ability.rarity.rarityValue; i++)
                {
                    weights.Add(ability);
                }
                if (ability.rarity.canBeRerolled)
                {
                    ability.rarity = Rarity.Impossible;
                }
                else
                {
                    ability.rarity = Rarity.ImpossibleNoReroll;
                }
            }

            BonusSuitePassiveAbility bonusSuitePassiveAbility = ScriptableObject.CreateInstance<BonusSuitePassiveAbility>();

            bonusSuitePassiveAbility.name = "BonusSuite_" + string.Join("_", names) + "_PA";
            bonusSuitePassiveAbility.m_PassiveID = "BonusSuite_" + string.Join("_", names);
            bonusSuitePassiveAbility._passiveName = "Bonus Suite";
            bonusSuitePassiveAbility._characterDescription = "This passive is not meant for party members.";
            bonusSuitePassiveAbility._enemyDescription = "This enemy will perform one of the following moves as a bonus action each turn:" + "\n" + string.Join("\n", names);
            bonusSuitePassiveAbility.passiveIcon = ResourceLoader.LoadSprite("IconBonusSuite");
            bonusSuitePassiveAbility._triggerOn = [TriggerCalls.ExtraAdditionalAttacks];
            bonusSuitePassiveAbility.conditions = [];
            bonusSuitePassiveAbility.doesPassiveTriggerInformationPanel = false;
            bonusSuitePassiveAbility.specialStoredData = null;
            bonusSuitePassiveAbility._suiteAbilities = weights;
            return bonusSuitePassiveAbility;
        }

        // Bonus Suite variants with reroll capabilities
        public static BasePassiveAbilitySO BonusSuiteRerollGenerator(string rerollType, List<ExtraAbilityInfo> bonusAbilities)
        {
            List<string> names = [];
            List<ExtraAbilityInfo> weights = [];
            foreach (ExtraAbilityInfo ability in bonusAbilities)
            {
                if (ability == null || ability.ability == null)
                {
                    Debug.Log("Bonus ability " + ability.ability.name + " does not exist.");
                    return null;
                }
                names.Add(ability.ability._abilityName);
                for (int i = 0; i < ability.rarity.rarityValue; i++)
                {
                    weights.Add(ability);
                }
                if (ability.rarity.canBeRerolled)
                {
                    ability.rarity = Rarity.Impossible;
                }
                else
                {
                    ability.rarity = Rarity.ImpossibleNoReroll;
                }
            }

            string typeID = "Invalid";
            string typeDisplay = "";
            string typeDesc = "Someone set the rerollType incorrectly!";
            string typeIcon = "IconBonusSuiteAlt";
            TriggerCalls[] typeTriggers = [];
            EffectorConditionSO[] typeConditions = [];
            switch (rerollType)
            {
                case "Formless":
                    typeID = rerollType;
                    typeDisplay = "Formless";
                    typeDesc = "On being directly damaged, reroll the ability added by this passive.";
                    typeIcon = "IconBonusSuiteFormless";
                    typeTriggers = [TriggerCalls.OnDirectDamaged];
                    typeConditions = [ScriptableObject.CreateInstance<UnitAliveEffectorCondition>()];
                    break;
                case "PartyAction":
                    typeID = rerollType;
                    typeDisplay = "Semimaterial";
                    typeDesc = "On any party member performing an ability, reroll the ability added by this passive.";
                    typeIcon = "IconBonusSuiteImmaterial";
                    typeTriggers = [TriggerCalls.OnAnyAbilityUsed];
                    typeConditions = [ScriptableObject.CreateInstance<UnitAliveEffectorCondition>(), ScriptableObject.CreateInstance<IsPlayerTurnEffectorCondition>()];
                    break;
            }

            BonusSuiteWithRerollConditionPassiveAbility bonusSuitePassiveAbility = ScriptableObject.CreateInstance<BonusSuiteWithRerollConditionPassiveAbility>();

            bonusSuitePassiveAbility.name = "BonusSuiteReroll_" + typeID + "_" + string.Join("_", names) + "_PA";
            bonusSuitePassiveAbility.m_PassiveID = "BonusSuiteReroll_" + typeID + "_" + string.Join("_", names);
            bonusSuitePassiveAbility._passiveName = "Bonus Suite (" + typeDisplay + ")";
            bonusSuitePassiveAbility._characterDescription = "This passive is still not meant for party members.";
            bonusSuitePassiveAbility._enemyDescription = "This enemy will perform one of the following moves as a bonus action each turn:" + "\n" + string.Join("\n", names) + "\n\n" + typeDesc;
            bonusSuitePassiveAbility.passiveIcon = ResourceLoader.LoadSprite(typeIcon);
            bonusSuitePassiveAbility._triggerOn = [TriggerCalls.ExtraAdditionalAttacks];
            bonusSuitePassiveAbility.conditions = [];
            bonusSuitePassiveAbility.doesPassiveTriggerInformationPanel = false;
            bonusSuitePassiveAbility.specialStoredData = null;
            bonusSuitePassiveAbility._suiteAbilities = weights;
            bonusSuitePassiveAbility._secondDoesPerformPopUp = true;
            bonusSuitePassiveAbility._secondTriggerOn = typeTriggers;
            bonusSuitePassiveAbility._secondPerformConditions = typeConditions;
            return bonusSuitePassiveAbility;
        }

        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedBloat = [];

        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedSaltLockstep = [];
        private static readonly Dictionary<int, BasePassiveAbilitySO> GeneratedSaltCadence = [];
        private static TValue GetOrCreatePassive<TKey, TValue>(IDictionary<TKey, TValue> readFrom, TKey key, Func<TKey, TValue> create)
            {
                if (readFrom.TryGetValue(key, out TValue value))
                {
                    return value;
                }

                return readFrom[key] = create(key);
            }                
        public static BasePassiveAbilitySO SaltLockstepGenerator(int amount)
        {
            return GetOrCreatePassive(GeneratedSaltLockstep, amount, delegate (int x)
            {
                //LockstepDir_SV for direction
                //LockstepAmount_SV for amount

                string storedValueName = "LockstepDir_SV";
                int goingLeft = 1;
                int goingRight = 2;

                MoveByCasterStoredValueEffect movement = ScriptableObject.CreateInstance<MoveByCasterStoredValueEffect>();
                movement.storedValue = storedValueName;
                movement.LeftValue = goingLeft;
                movement.RightValue = goingRight;

                SwapCasterStoredValueEffect swapdirection = ScriptableObject.CreateInstance<SwapCasterStoredValueEffect>();
                swapdirection.storedValue = storedValueName;
                swapdirection.firstValue = goingLeft;
                swapdirection.secondValue = goingRight;

                CasterSwapAnimationParametersByStoredValueEffect animator = ScriptableObject.CreateInstance<CasterSwapAnimationParametersByStoredValueEffect>();
                animator._parameterName = "Flip";//set this for the swap left right cusotm animator thing i think ur using that
                animator.storedValue = storedValueName;
                animator.LeftValue = goingLeft;
                animator.RightValue = goingRight;
                animator.setAsIfLeft = 0;//idk what ur setting these for if left or right for so i just kinda left it like this. change these 2 as needed.
                animator.setAsIfRight = 1;

                PreviousEffectCondition prev1 = ScriptableObject.CreateInstance<PreviousEffectCondition>();
                prev1.previousAmount = 1;
                prev1.wasSuccessful = false;
                PreviousEffectCondition prev2 = ScriptableObject.CreateInstance<PreviousEffectCondition>();
                prev2.previousAmount = 2;
                prev2.wasSuccessful = false;
                PreviousEffectCondition prev3 = ScriptableObject.CreateInstance<PreviousEffectCondition>();
                prev3.previousAmount = 3;
                prev3.wasSuccessful = false;

                EffectInfo[] step = [
                    Effects.GenerateEffect(movement, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(swapdirection, 0, null, prev1),
                    Effects.GenerateEffect(animator, 0, null, prev2),
                    Effects.GenerateEffect(movement, 1, Targeting.Slot_SelfSlot, prev3),
                    ];

                ExtraVariableForNext_SVEffect countsteps = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
                countsteps.m_unitStoredDataID = "LockstepAmount_SV";
                AltPerformEffectXTimesViaSubaction dosteps = ScriptableObject.CreateInstance<AltPerformEffectXTimesViaSubaction>();
                dosteps.usePreviousExit = true;
                dosteps.effects = step;

                StoredValueSetOnceEffectorCondition hopeandpray = ScriptableObject.CreateInstance<StoredValueSetOnceEffectorCondition>();
                hopeandpray.m_unitStoredDataID = "LockstepAmount_SV";
                CasterStoreValueSetterEffect initialize = ScriptableObject.CreateInstance<CasterStoreValueSetterEffect>();
                initialize.m_unitStoredDataID = "LockstepAmount_SV";
                initialize._ignoreIfContains = true;
                CasterStoreValuePreviousExitSetterEffect initialize2 = ScriptableObject.CreateInstance<CasterStoreValuePreviousExitSetterEffect>();
                initialize2.m_unitStoredDataID = "LockstepDir_SV";
                initialize2._ignoreIfContains = true;
                ExtraVariableForNext_RandomBetweenVariable_Effect evr = ScriptableObject.CreateInstance<ExtraVariableForNext_RandomBetweenVariable_Effect>();
                evr._MinValue = 1;
                evr._EntryInclusive = true;
                //PercentageEffectCondition
                PerformDoubleEffectPassiveAbility performEffectPassiveAbility = ScriptableObject.CreateInstance<PerformDoubleEffectPassiveAbility>();
                performEffectPassiveAbility.name = $"SaltLockstep_{x}_PA";
                performEffectPassiveAbility.m_PassiveID = "Lockstep_ID";
                performEffectPassiveAbility._passiveName = $"Lockstep ({x})";
                performEffectPassiveAbility._characterDescription = $"After performing an ability, this character will attempt to move {x} times in their Lockstep direction, switching directions if this movement fails.";
                performEffectPassiveAbility._enemyDescription = $"After performing an ability, this enemy will attempt to move {x} times in their Lockstep direction, switching directions if this movement fails.";
                performEffectPassiveAbility.passiveIcon = ResourceLoader.LoadSprite("passive_lockstep.png");
                performEffectPassiveAbility._triggerOn = new TriggerCalls[1] { TriggerCalls.OnAbilityUsed };
                performEffectPassiveAbility.effects = [
                    Effects.GenerateEffect(countsteps, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(dosteps, 1, Targeting.Slot_SelfSlot),
                    ];
                performEffectPassiveAbility._secondTriggerOn = [TriggerCalls.OnAbilityWillBeUsed];
                //performEffectPassiveAbility._secondPerformConditions = [hopeandpray];
                performEffectPassiveAbility._secondEffects = [
                    Effects.GenerateEffect(initialize, amount, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(evr, 2, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(initialize2, 1, Targeting.Slot_SelfSlot),
                    ];
                performEffectPassiveAbility.conditions = new EffectorConditionSO[0] { };
                performEffectPassiveAbility.doesPassiveTriggerInformationPanel = true;
                performEffectPassiveAbility._secondDoesPerformPopUp = false;
                performEffectPassiveAbility.specialStoredData = LoadedDBsHandler.MiscDB.GetUnitStoreData("LockstepDir_SV");
                return performEffectPassiveAbility;
            });
        }
    }
}
