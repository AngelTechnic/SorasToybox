using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class TestEncounter
    {
        public static void Add()
        {
            Portals.AddPortalColor("SoraTestColor", Color.gray);


            Portals.AddPortalSign("SoraTest_Sign", ResourceLoader.LoadSprite("TimelineParadoxYinimro", new Vector2(0.5f, 0f), 32), "SoraTestColor");
            EnemyEncounter_API testMedium = new EnemyEncounter_API(0, "H_Zone01_SoraTest_Medium_EnemyBundle", "SoraTest_Sign")
            {
                //Where would we be without good music?
                MusicEvent = "event:/SorasMusic/Enemies/YinimroMusic/Blackberry",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_ZoneAbyss_Streetlight_Medium_EnemyBundle")._roarReference.roarEvent,

            };
            testMedium.CreateNewEnemyEncounterData(["ParadoxYinimro_EN",], null);

            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_SoraTest_Medium_EnemyBundle", 0, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
