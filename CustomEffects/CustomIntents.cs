using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using UnityEngine;

namespace SorasToybox.CustomEffects
{
    class CustomIntents
    {
        public static bool dintExists(string intent)
        {
            return LoadedDBsHandler.IntentDB.m_IntentDamagePool.ContainsKey(intent);
        }

        public static bool intExists(string intent)
        {
            return LoadedDBsHandler.IntentDB.m_IntentBasicPool.ContainsKey(intent);
        }
        public static void Add()
        {
            //Intent for abilities that don't do anything/waste a turn.
            IntentInfoBasic DoNothingIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("doNothing_intent")
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Misc_Nothing", DoNothingIntent);

            IntentInfoBasic TheftPassiveIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("theft_passive")
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Theft", TheftPassiveIntent);

            //IntentInfoBasic CondIntent = new()
            //{
            //    _color = Color.white,
            //    _sprite = ResourceLoader.LoadSprite("if_intent")
            //};
            //LoadedDBsHandler.IntentDB.AddNewBasicIntent("Cond_If", CondIntent);

            //IntentInfoBasic CondFailIntent = new()
            //{
            //    _color = Color.white,
            //    _sprite = ResourceLoader.LoadSprite("ifFail_intent")
            //};
            //LoadedDBsHandler.IntentDB.AddNewBasicIntent("Cond_Fail", CondFailIntent);

            //IntentInfoBasic CondSucceedIntent = new()
            //{
            //    _color = Color.white,
            //    _sprite = ResourceLoader.LoadSprite("ifSucceed_intent")
            //};
            //LoadedDBsHandler.IntentDB.AddNewBasicIntent("Cond_Success", CondSucceedIntent);

            IntentInfoBasic colophonIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("colophon_intent")
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("ColoIntent", colophonIntent);


            //Proportional damage (stuff that does a portion of the target's current health as damage.
            if (!dintExists("Damage_Prop"))
            {
                IntentInfoDamage Damage_Proportional = new()
                {
                    _color = LoadedDBsHandler.IntentDB.TryGetIntentInfo("Damage_1_2")._color,
                    _enemyColor = Color.red,
                    _sprite = ResourceLoader.LoadSprite("prop_intent"),
                    _enemySprite = ResourceLoader.LoadSprite("prop_intent"),
                };
                //LoadedDBsHandler.IntentDB.AddNewDamageIntent("Damage_Prop", Damage_Proportional);
                Intents.AddCustom_Damage_IntentToPool("Damage_Prop", Damage_Proportional);
            }

            //Proportional damage (Extreme). Like proportional, but does AT LEAST 100% of the target's current health.
            if (!dintExists("Damage_PropEx"))
            {
                IntentInfoDamage Damage_Proportional_Extreme = new()
                {
                    _color = LoadedDBsHandler.IntentDB.TryGetIntentInfo("Damage_1_2")._color,
                    _enemyColor = Color.red,
                    _sprite = ResourceLoader.LoadSprite("propx_intent.png"),
                    _enemySprite = ResourceLoader.LoadSprite("propx_intent.png"),
                };
                //LoadedDBsHandler.IntentDB.AddNewDamageIntent("Damage_PropEx", Damage_Proportional_Extreme);
                Intents.AddCustom_Damage_IntentToPool("Damage_PropEx", Damage_Proportional_Extreme);
            }

            if (!dintExists("Damage_Timeline"))
            {
                IntentInfoDamage Damage_Timeline = new()
                {
                    _color = LoadedDBsHandler.IntentDB.TryGetIntentInfo("Damage_1_2")._color,
                    _enemyColor = Color.red,
                    _sprite = ResourceLoader.LoadSprite("timelineDamage_intent"),
                    _enemySprite = ResourceLoader.LoadSprite("timelineDamage_intent"),
                };
                //LoadedDBsHandler.IntentDB.AddNewDamageIntent("Damage_Timeline", Damage_Timeline);
                Intents.AddCustom_Damage_IntentToPool("Damage_Timeline", Damage_Timeline);
            }

            if (!dintExists("Damage_Round"))
            {
                IntentInfoDamage Damage_Round = new()
                {
                    _color = LoadedDBsHandler.IntentDB.TryGetIntentInfo("Damage_1_2")._color,
                    _enemyColor = Color.red,
                    _sprite = ResourceLoader.LoadSprite("damage_round_intent"),
                    _enemySprite = ResourceLoader.LoadSprite("damage_round_intent"),
                };
                //LoadedDBsHandler.IntentDB.AddNewDamageIntent("Damage_Round", Damage_Round);
                Intents.AddCustom_Damage_IntentToPool("Damage_Round", Damage_Round);
            }

            if (!dintExists("Damage_Unbound"))
            {
                IntentInfoDamage Damage_Unbound = new()
                {
                    _color = LoadedDBsHandler.IntentDB.TryGetIntentInfo("Damage_1_2")._color,
                    _enemyColor = Color.red,
                    _sprite = ResourceLoader.LoadSprite("IntentUnboundDamage"),
                    _enemySprite = ResourceLoader.LoadSprite("IntentUnboundDamage"),
                };
                //LoadedDBsHandler.IntentDB.AddNewDamageIntent("Damage_Round", Damage_Round);
                Intents.AddCustom_Damage_IntentToPool("Damage_Unbound", Damage_Unbound);
            }


            IntentInfoBasic itemIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("intentItem")
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Item", itemIntent);

            IntentInfoBasic basilIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_containsbasil.png")
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Basil", basilIntent);

            

            IntentInfoBasic splIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("splatterPassive.png")
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Splatter", splIntent);

            IntentInfoBasic magIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("magnitudePassive.png")
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Magnitude", magIntent);

            IntentInfoBasic bonusIntent = new()
            {
                _color = Color.white,
                _sprite = Passives.Example_BonusAttack_Mangle.passiveIcon,
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Bonus", bonusIntent);

            IntentInfoBasic lockstepIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_lockstep.png"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Lockstep", lockstepIntent);

            IntentInfoBasic collisionIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("collision_passive.png"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Collision", collisionIntent);

            IntentInfoBasic nobodyIntent = new()
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("modIcon.png"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Nobody", nobodyIntent);

            IntentInfoBasic data = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passives_fungus.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Fungus", data);

            IntentInfoBasic modular = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("modular_passive.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Modular", modular);

            IntentInfoBasic fhint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_foolhardy.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Foolhardy", fhint);

            if (!dintExists("Damage_Random"))
            {
                IntentInfoDamage Damage_Random = new()
                {
                    _color = LoadedDBsHandler.IntentDB.TryGetIntentInfo("Damage_1_2")._color,
                    _enemyColor = Color.red,
                    _sprite = ResourceLoader.LoadSprite("Intent_Dice.png"),
                    _enemySprite = ResourceLoader.LoadSprite("Intent_Dice.png"),
                };
                //LoadedDBsHandler.IntentDB.AddNewDamageIntent("Damage_Random", Damage_Random);
                Intents.AddCustom_Damage_IntentToPool("Damage_Random", Damage_Random);
            }

            

            IntentInfoBasic nsint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("NegativeStatusIntent.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("NegativeStatusIntent", nsint);

            IntentInfoBasic crint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("IntentCostReroll.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("HIFCostReroll", crint);

            IntentInfoBasic dnint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("DNAIntent.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("DNA", dnint);

            IntentInfoBasic rpint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("repeat_intent.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Repeat", rpint);

            IntentInfoBasic aidsint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("aid_passive.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Aid", aidsint);

            IntentInfoBasic strhint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_stronghold.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Stronghold", strhint);

            IntentInfoBasic fragint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_fragmentation.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Fragmentation", fragint);

            IntentInfoBasic contrint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_contradictory.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Contradictory", fragint);


            IntentInfoBasic mournint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("mourn_passive.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Mourn", mournint);

            IntentInfoBasic lashint = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("lashout_passive.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_LashOut", lashint);

            IntentInfoBasic blooded = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_blood_base.png", null, 32, null)
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Blooded", blooded);

            //Passives.MultiAttack2
            IntentInfoBasic actualmatk = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = Passives.MultiAttack2.passiveIcon,
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Actual_MultiAttack", actualmatk);

            IntentInfoBasic immaterial = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("Immaterial.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Immaterial", immaterial);

            IntentInfoBasic rimmaterial = new IntentInfoBasic
            {
                _color = Color.gray,
                _sprite = ResourceLoader.LoadSprite("Immaterial.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_Immaterial", rimmaterial);

            IntentInfoBasic violent = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("ViolentPassive.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Violent", violent);

            IntentInfoBasic freewill = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("freewill_passive.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_FreeWilled", freewill);

            IntentInfoBasic rfreewill = new IntentInfoBasic
            {
                _color = Color.grey,
                _sprite = ResourceLoader.LoadSprite("freewill_passive.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_FreeWilled", rfreewill);


            IntentInfoBasic schol = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_Scholar.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Scholar", schol);

            IntentInfoBasic rschol = new IntentInfoBasic
            {
                _color = Color.grey,
                _sprite = ResourceLoader.LoadSprite("passive_Scholar.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_Scholar", rschol);

            IntentInfoBasic rinan = new IntentInfoBasic
            {
                _color = Color.grey,
                _sprite = Passives.Inanimate.passiveIcon,
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_Inanimate", rinan);

            IntentInfoBasic rinf = new IntentInfoBasic
            {
                _color = Color.grey,
                _sprite = Passives.Infantile.passiveIcon,
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_Infantile", rinf);

            IntentInfoBasic spart = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_searchparty.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_SearchParty", spart);

            IntentInfoBasic rspart = new IntentInfoBasic
            {
                _color = Color.grey,
                _sprite = ResourceLoader.LoadSprite("passive_searchparty.png", null, 32, null),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Rem_Passive_SearchParty", rspart);

            IntentInfoBasic awaken = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_awaken.png"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Awaken", awaken);

            IntentInfoBasic suicidal = new IntentInfoBasic
            {
                _color = Color.white,
                _sprite = ResourceLoader.LoadSprite("passive_suicidal.png"),
            };
            LoadedDBsHandler.IntentDB.AddNewBasicIntent("Passive_Suicidal", suicidal);

        }
    }
}
