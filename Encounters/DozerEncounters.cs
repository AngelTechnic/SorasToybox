using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;


namespace SorasToybox.Encounters
{
    public class DozerEncounters
    {
        public static void Add()
        {
            //rules for Dozer encounters:
            //Only 1 Dozer per encounter
            //Dozer is arrogant! He takes priority over everything.
            //try to play with warping player movement so Dozer's tracked is more dangerous.
            if (LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3"))
            {
                Portals.AddPortalSign("Dozer_Sign", ResourceLoader.LoadSprite("timelineDozer.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

                EnemyEncounter_API dozerMedium = new EnemyEncounter_API(0, "H_ZoneAbyss_Dozer_Medium_EnemyBundle", "Dozer_Sign")
                {
                    MusicEvent = "event:/SorasMusic/Enemies/DozerMusic/GrapeGrappleGravity",
                    RoarEvent = "event:/SorasSFX/Enemies/Dozer/DozerRoar",
                };

                dozerMedium.CreateNewEnemyEncounterData(
                    [
                        "Parfait_EN",
                        "Dozer_EN",
                        "Wug_EN",
                    ], null);


                dozerMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_Dozer_Medium_EnemyBundle", 4, "TheAbyss_Zone3", BundleDifficulty.Medium);
            }
        }
    }
}
