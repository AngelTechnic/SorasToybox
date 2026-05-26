using BrutalAPI;
using SorasToybox.Custom_Passives;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
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
            karmic.passiveIcon = ResourceLoader.LoadSprite("passive_karmic");
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
            dismal.passiveIcon = ResourceLoader.LoadSprite("passive_dismal");
            dismal._characterDescription = "This party member does not take wrong pigment or overflow damage. Instead, their abilities will be cast from themselves.";
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

            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Illuminated_ID"))
            {
                StatusEffectPassiveAbility godray = ScriptableObject.CreateInstance<StatusEffectPassiveAbility>();
                godray._Status = StatusField.GetCustomStatusEffect("Illuminated_ID");
                godray.m_PassiveID = "Godray_PA";
                godray.passiveIcon = ResourceLoader.LoadSprite("passive_godray.png");
                godray._characterDescription = "This party member is permanently Illuminated and takes half damage from all sources.";
                godray._enemyDescription = "This enemy is permanently Illuminated and takes half damage from all sources.";
                godray.doesPassiveTriggerInformationPanel = true;


                Passives.AddCustomPassiveToPool("Godray_PA", "Godray", godray);
            }
        }
    }
}
