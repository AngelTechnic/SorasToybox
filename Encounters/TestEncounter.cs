using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class TestEncounter
    {
        public static void Add()
        {
            Portals.AddPortalColor("MalachaiPortalColor", Color.gray);
            Portals.AddPortalSign("SEARCH_Sign", ResourceLoader.LoadSprite("timelineLitany"), "MalachaiPortalColor");
            EnemyEncounter_API testMedium = new EnemyEncounter_API(EncounterType.Specific, "H_Zone01_SoraTest_Medium_EnemyBundle", "SEARCH_Sign")
            {
                //Where would we be without good music?
                MusicEvent = "event:/SorasMusic/Enemies/LitanyMusic/Singsong",
                RoarEvent = "event:/SorasSFX/Enemies/SEARCH/SEARCHRoar",

            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "Litany_EN",
                "SEARCH_EN",
                "SEARCH_EN",
            ], [0, 1, 3]);

            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_SoraTest_Medium_EnemyBundle", 0, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
