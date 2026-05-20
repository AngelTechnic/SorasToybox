using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class TestEncounter
    {
        public static void Add()
        { 
            Portals.AddPortalSign("SEARCH_Sign", ResourceLoader.LoadSprite("timelineSEARCH"), Portals.BossIDColor);
            EnemyEncounter_API testMedium = new EnemyEncounter_API(EncounterType.Specific, "H_Zone01_SoraTest_Medium_EnemyBundle", "SEARCH_Sign")
            {
                MusicEvent = "event:/SorasMusic/Enemies/SEARCHMusic/TimestopperTactics",
                RoarEvent = "event:/SorasSFX/Enemies/SEARCH/SEARCHRoar",

            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "SEARCH_EN",
                "SEARCH_EN",
                "SEARCH_EN",
            ], [1, 2, 3]);
            //testMedium.SimpleAddEncounter(1, "Threshold_EN");
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_SoraTest_Medium_EnemyBundle", 9999, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
