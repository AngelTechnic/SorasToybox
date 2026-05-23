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
            if (!LoadedDBsHandler.PortalDB.m_PortalColors.ContainsKey("MalachaiPortalColor"))
            {
                Portals.AddPortalColor("MalachaiPortalColor", Color.gray);
            }

            Portals.AddPortalSign("SoraTest_Sign", ResourceLoader.LoadSprite("timelineLitany"), "MalachaiPortalColor");
            EnemyEncounter_API testMedium = new EnemyEncounter_API(EncounterType.Specific, "H_Zone01_SoraTest_Medium_EnemyBundle", "Litany_Sign")
            {
                //Where would we be without good music?
                MusicEvent = "event:/SorasMusic/Enemies/LitanyMusic/Singsong",
                RoarEvent = "event:/SorasSFX/Enemies/Litany/LitanyRoar",

            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "WanderFellow_EN",
                "Litany_EN",
                "WanderFellow_EN",
            ], [1, 2, 3]);

            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_SoraTest_Medium_EnemyBundle", 0, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}
