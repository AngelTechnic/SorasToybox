using BrutalAPI;
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




            //Glossary Entries!
            GlossaryPassives STKarmicInfo = new GlossaryPassives("Karmic", "On receiving damage, apply equivalent Regeneration to all allies.", ResourceLoader.LoadSprite("passive_karmic"));
            LoadedDBsHandler.GlossaryDB.AddNewPassive(STKarmicInfo);
        }
    }
}
