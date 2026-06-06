using BrutalAPI;
using SorasToybox.CustomStatuses;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SorasToybox.CustomStatusField
{
    public class CustomStatus
    {
        public static void Add()
        {
            //Overclock Status Effect
            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Overclock_ID"))
            {
                //Basic Info (Initialization, Name, Description, Icon)
                StatusEffectInfoSO OverclockInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                OverclockInfo._statusName = "Overclock";
                OverclockInfo._description = "While Overclocked, all direct damage dealt is doubled.\nPerforming an ability reduces Overclock by 1.";
                OverclockInfo.icon = ResourceLoader.LoadSprite("status_overclock.png");

                //Inheriting info from basegame status (Linked)
                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect("Linked_ID", out StatusEffect_SO linked);
                StatusEffectInfoSO baseinfo = linked.EffectInfo;

                //Sound events! Once I learn FMOD, _applied_SE_Event will become its own sound effect.
                OverclockInfo._applied_SE_Event = "event:/SorasSFX/StatusSFX/ApplyOverclock";
                OverclockInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                OverclockInfo._updated_SE_Event = baseinfo._updated_SE_Event;

                //Creates instance of the status effect from the specific file.
                OverclockSE_SO OverclockSO = ScriptableObject.CreateInstance<OverclockSE_SO>();
                OverclockSO._StatusID = "Overclock_ID";
                OverclockSO._EffectInfo = OverclockInfo;
                
                //Adds the status to the database
                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(OverclockSO, true);

                //setting up Overclock intent
                IntentInfoBasic OverclockIntent = new()
                {
                    _color = Color.white,
                    _sprite = OverclockInfo.icon
                };
                //adds to the database
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Overclock", OverclockIntent);

                IntentInfoBasic RemOverclockIntent = new()
                {
                    _color = Color.gray,
                    _sprite = OverclockInfo.icon
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Overclock", RemOverclockIntent);
            }

            //Regeneration Status Effect
            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Regen_ID"))
            {
                //Basic Info (Initialization, Name, Description, Icon)
                StatusEffectInfoSO ModuInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                ModuInfo._statusName = "Regeneration";
                ModuInfo._description = "At the end of each round, this unit will heal for half of their stacks of Regeneration and then lose stacks of Regeneration equivalent to the amount they healed. Will always attempt to heal at least 1 health.\n" +
                    "At the end of combat, receive all remaining Regeneration as healing.";
                ModuInfo.icon = ResourceLoader.LoadSprite("status_regen.png");

                //Inheriting info from basegame status (Linked)
                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(StatusField.Linked._StatusID, out StatusEffect_SO frail);
                StatusEffectInfoSO baseinfo = frail.EffectInfo;

                ModuInfo._applied_SE_Event = baseinfo._applied_SE_Event;
                ModuInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                ModuInfo._updated_SE_Event = baseinfo._updated_SE_Event;
                //ModuInfo.
                Regeneration modu = ScriptableObject.CreateInstance<Regeneration>();
                modu._StatusID = "Regen_ID";
                modu._EffectInfo = ModuInfo;
                //modu.IsPositive = false;

                //Adds the status to the database
                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(modu, true);

                IntentInfoBasic RegenerationIntent = new()
                {
                    _color = Color.white,
                    _sprite = ResourceLoader.LoadSprite("status_regen.png")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Regeneration", RegenerationIntent);

                IntentInfoBasic RegenerationRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = ResourceLoader.LoadSprite("status_regen.png")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Regeneration", RegenerationRemIntent);
            }

            //Ante Status Effect
            if (!LoadedDBsHandler.StatusFieldDB.StatusEffects.ContainsKey("Ante_ID"))
            {
                StatusEffectInfoSO ModuInfo = ScriptableObject.CreateInstance<StatusEffectInfoSO>();
                ModuInfo._statusName = "Ante";
                ModuInfo._description = "All damage and healing dealt and recieved is increased by the amount of Ante. Ante does not naturally decrease.";
                ModuInfo.icon = ResourceLoader.LoadSprite("status_ante.png");


                LoadedDBsHandler.StatusFieldDB.TryGetStatusEffect(StatusField.DivineProtection._StatusID, out StatusEffect_SO frail);
                StatusEffectInfoSO baseinfo = frail.EffectInfo;

                ModuInfo._applied_SE_Event = "event:/ApplyAnte";
                ModuInfo._removed_SE_Event = baseinfo._removed_SE_Event;
                ModuInfo._updated_SE_Event = baseinfo._updated_SE_Event;
                //ModuInfo.
                Ante modu = ScriptableObject.CreateInstance<Ante>();
                modu._StatusID = "Ante_ID";
                modu._EffectInfo = ModuInfo;
                //modu.IsPositive = false;

                LoadedDBsHandler.StatusFieldDB.AddNewStatusEffect(modu, true);

                IntentInfoBasic AnteIntent = new()
                {
                    _color = Color.white,
                    _sprite = ResourceLoader.LoadSprite("status_ante.png")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Status_Ante", AnteIntent);

                IntentInfoBasic AnteRemIntent = new()
                {
                    _color = Color.gray,
                    _sprite = ResourceLoader.LoadSprite("status_ante.png")
                };
                LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Status_Ante", AnteRemIntent);
            }



        }

    }
}
