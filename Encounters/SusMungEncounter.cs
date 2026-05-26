using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class SusMungEncounter
    {
        public static void Add()
        {
            if (LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3"))
            {
                Portals.AddPortalSign("SusMung_Sign", ResourceLoader.LoadSprite("timelineSusMung.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

                EnemyEncounter_API susMungHard = new EnemyEncounter_API(0, "H_ZoneAbyss_Mung_Easy_EnemyBundle", "SusMung_Sign")
                {
                    MusicEvent = "event:/Music/Mx_Mung",
                    RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_Mung_Easy_EnemyBundle")._roarReference.roarEvent,
                };

                susMungHard.CreateNewEnemyEncounterData([
                    "SuspiciousMung_EN",
                    ], [2]);

                susMungHard.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_Mung_Easy_EnemyBundle", 1, "TheAbyss_Zone3", BundleDifficulty.Hard);
            }
        }
    }
}
