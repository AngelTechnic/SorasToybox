using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class ParadoxYinimroEncounters
    {
        public static void Add()
        {
            //rules for Paradox Yinimro encounters:
            //If you pair them with Images of any kind I'll kill you.

            //Paradox Sign!
            Portals.AddPortalSign("ParadoxYinimro_Sign", ResourceLoader.LoadSprite("TimelineParadoxYinimro.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            string paradoxSign = "ParadoxYinimro_Sign";

            //initializing Garden Gear Yinimro encounters, here's the sound events too.
            EnemyEncounter_API paradoxYinimroGardenMedium = new EnemyEncounter_API(0, Garden.H.YinimroP.Med, paradoxSign)
            {
                MusicEvent = "event:/SorasMusic/Enemies/YinimroMusic/Blackberry",
                RoarEvent = "event:/IntrusiveRoar",
            };

            if (Abyss.Exists)
            {
                //initializing Abyss Paradox Yinimro encounters, here's the sound events too.
                EnemyEncounter_API paradoxYinimroAbyssMedium = new EnemyEncounter_API(0, Abyss.H.YinimroP.Med, paradoxSign)
                {
                    MusicEvent = "event:/SorasMusic/Enemies/YinimroMusic/Blackberry",
                    RoarEvent = "event:/IntrusiveRoar",
                };

                //Medium yinimro encounts go down here
                paradoxYinimroAbyssMedium.SimpleAddEncounter(1, "ParadoxYinimro_EN", 2, "Wug_EN");



                paradoxYinimroAbyssMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.YinimroP.Med, 7, "TheAbyss_Zone3", BundleDifficulty.Medium);

                EnemyEncounter_API paradoxYinimroAbyssHard = new EnemyEncounter_API(0, Abyss.H.YinimroP.Hard, paradoxSign)
                {
                    MusicEvent = "event:/SorasMusic/Enemies/YinimroMusic/Blackberry",
                    RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_ZoneAbyss_Streetlight_Medium_EnemyBundle")._roarReference.roarEvent,
                };

                paradoxYinimroAbyssHard.SimpleAddEncounter(1, "ParadoxYinimro_EN", 1, "BurningShame_EN");

                paradoxYinimroAbyssHard.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.YinimroP.Hard, 3, "TheAbyss_Zone3", BundleDifficulty.Hard);
            }
        }
    }
}
