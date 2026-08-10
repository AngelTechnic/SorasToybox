using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using SorasToybox;

namespace SorasToybox.Encounters
{
    public class CrashoutEncounters
    {
        public static void Add()
        {
            if (LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3"))
            {
                string fakelessSign = "Fakeless_Sign";
                Portals.AddPortalSign(fakelessSign, ResourceLoader.LoadSprite("timelineFakeFaceless.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

                EnemyEncounter_API fakelessHard = new EnemyEncounter_API(0, "H_ZoneAbyss_Fakeless_Hard_EnemyBundle", fakelessSign)
                {
                    MusicEvent = "event:/NewNewFacelessMusic",
                    RoarEvent = "event:/NFacelessRoar",
                };

                fakelessHard.SimpleAddEncounter(1, "Fakeless_EN", 1, "Sycophant_EN", 2, "Streetlight_EN");
                fakelessHard.SimpleAddEncounter(1, "Fakeless_EN", 1, "WanderFellow_EN", 1, "Streetlight_EN");
                fakelessHard.SimpleAddEncounter(1, "Fakeless_EN", 1, "Receiver_EN", 2, "Streetlight_EN");
                fakelessHard.SimpleAddEncounter(1, "Fakeless_EN", 2, "BasicElemental_EN");
                fakelessHard.SimpleAddEncounter(1, "Fakeless_EN", 1, "Faceless_EN");
                fakelessHard.SimpleAddEncounter(1, "Fakeless_EN", 2, "Sycophant_EN");


                fakelessHard.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_Fakeless_Hard_EnemyBundle", 5, "TheAbyss_Zone3", BundleDifficulty.Hard);

                if (SorasToybox.extradebug.Value)
                {
                    UnityEngine.Debug.Log("Faceless Encounters loaded.");
                }
            }
        }
    }
}
