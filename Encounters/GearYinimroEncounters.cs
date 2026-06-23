using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;


namespace SorasToybox.Encounters
{
    public class GearYinimroEncounters
    {
        public static void Add()
        {
            //rules for Gear Yinimro encounters:
            //Try to play with the fact that they produce red, but milking them for it is dangerous
            //mix them with enemies that have unreliable pigment production like iridescent health or whatever.

            if (LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3"))
            {
                //Gear Sign!
                Portals.AddPortalSign("GearYinimro_Sign", ResourceLoader.LoadSprite("TimelineGearYinimro.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

                //initializing mediumdozer encounters, here's the sound events too.
                EnemyEncounter_API gearYinimroMedium = new EnemyEncounter_API(0, "H_ZoneAbyss_GearYinimro_Medium_EnemyBundle", "GearYinimro_Sign")
                {
                    MusicEvent = "event:/SorasMusic/Enemies/YinimroMusic/Blackberry",
                    RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_ZoneAbyss_Streetlight_Medium_EnemyBundle")._roarReference.roarEvent,
                };

                //Medium yinimro encounts go down here
                gearYinimroMedium.SimpleAddEncounter(1, "GearYinimro_EN", 1, "Wug_EN");


                gearYinimroMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_GearYinimro_Medium_EnemyBundle", 9, "TheAbyss_Zone3", BundleDifficulty.Medium);
            }
        }
    }
}
