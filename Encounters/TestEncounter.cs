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


            Portals.AddPortalSign("SoraTest_Sign", ResourceLoader.LoadSprite("TimelineSinful", new Vector2(0.5f, 0f), 32), "SoraTestColor");
            String soraTestSign = "SoraTest_Sign";

            EnemyEncounter_API testMedium = new EnemyEncounter_API(0, "H_Zone01_SoraTest_Medium_EnemyBundle", soraTestSign)
            {
                //Where would we be without good music?
                MusicEvent = "event:/SorasMusic/Enemies/SinfulMusic/BassThrombosis",
                RoarEvent = "event:/yBugRoar",

            };
            testMedium.SimpleAddEncounter(1, "SinfulReminder_EN", 1, "InHerImage_EN", 1, "InHerImage_EN");

            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_SoraTest_Medium_EnemyBundle", 0, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[ENCOUNTERS] Test Encounter loaded.");
            }
        }
    }
}
