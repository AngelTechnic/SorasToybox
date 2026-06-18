using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class SEARCHEncounters
    {
        public static void Add()
        {
            //SEARCH Encounter rules:
            //No more than three SEARCH per encounter. If you add more than that I will kill you.
            //Two are a contender for being the main enemy in a given encounter. Three MUST be.
            //One SEARCH is so pitiful by itself it could be medium-class filler in most fights.

            Portals.AddPortalSign("SEARCH_Sign", ResourceLoader.LoadSprite("timelineSEARCH", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API searchEasy = new(0, "H_Zone01_SEARCH_Easy_EnemyBundle", "SEARCH_Sign")
            {
                MusicEvent = "event:/SorasMusic/Enemies/SEARCHMusic/TimestopperTactics",
                RoarEvent = "event:/SorasSFX/Enemies/SEARCH/SEARCHRoar",
            };

            searchEasy.SimpleAddEncounter(1, "SEARCH_EN", 1, "Mung_EN");


            searchEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_SEARCH_Easy_EnemyBundle", 6, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);

        }
    }
}
