using BrutalAPI;
using SorasToybox.Custom_Passives;
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

            //Glossary Entries!
            GlossaryPassives STKarmicInfo = new GlossaryPassives("Karmic", "On receiving damage, apply equivalent Regeneration to all allies.", ResourceLoader.LoadSprite("passive_karmic"));
            LoadedDBsHandler.GlossaryDB.AddNewPassive(STKarmicInfo);
        }
    }
}
