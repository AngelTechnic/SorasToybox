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


            Portals.AddPortalSign("SoraTest_Sign", ResourceLoader.LoadSprite("timeline_tango", new Vector2(0.5f, 0f), 32), "SoraTestColor");
            String soraTestSign = "SoraTest_Sign";

            EnemyEncounter_API testMedium = new EnemyEncounter_API(0, "H_Zone01_SoraTest_Medium_EnemyBundle", soraTestSign)
            {
                //Where would we be without good music?
                MusicEvent = "event:/SorasMusic/Enemies/TANGOMusic/APartFalling",
                RoarEvent = "event:/SorasSFX/Enemies/TANGO/TangoRoar",

            };
            testMedium.CreateNewEnemyEncounterData(["TANGO_EN", "TANGO_EN", "SkinningHomunculus_EN"], null);

            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_SoraTest_Medium_EnemyBundle", 0, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("Test Encounter loaded.");
            }
        }
    }
}
