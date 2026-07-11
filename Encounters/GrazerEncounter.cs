using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class GrazerEncounter
    {
        public static void Add()
        {
            Portals.AddPortalSign("CitygrazerSign", ResourceLoader.LoadSprite("timelineSusMung.png", null, 32, null), Portals.EnemyIDColor);
            string grazerSign = "CitygrazerSign";
            //Garden

            string grazerBundleHardName = "H_Zone03_Citygrazer_Hard_EnemyBundle";
            EnemyEncounter_API mainEncounters = new EnemyEncounter_API(0, grazerBundleHardName, grazerSign);
            mainEncounters.MusicEvent = "event:/SorasMusic/Enemies/GrazerMusic/NewSeeds";
            mainEncounters.RoarEvent = "event:/SorasSFX/Enemies/Citygrazer/CitygrazerRoar";

            mainEncounters.CreateNewEnemyEncounterData(new string[]
            {
                "Citygrazer_EN"
            }, null);

            mainEncounters.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(grazerBundleHardName, 0, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}
