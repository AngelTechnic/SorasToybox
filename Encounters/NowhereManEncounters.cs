using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class NowhereManEncounters
    {
        public static void Add()
        {
            //Rules for Nowhere Man encounters:
            //They're skinning homunculus-esque. Treat them as such.
            //One of the more dangerous enemies due to their ability to attack and move very often.

            //Gear Sign!
            Portals.AddPortalSign("NowhereMan_Sign", ResourceLoader.LoadSprite("timelineNowhere.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            string nowhereSign = "NowhereMan_Sign";
            if (Abyss.Exists)
            {
                //initializing Abyss Nowhere Man encounters, here's the sound events too.
                EnemyEncounter_API nowhereManMedium = new EnemyEncounter_API(0, Abyss.H.Nowhere.Med, nowhereSign)
                {
                    MusicEvent = "event:/SorasMusic/Enemies/NowhereMusic/Sewer",
                    RoarEvent = "event:/SorasSFX/Enemies/NowhereMan/NowhereRoar", 
                };

                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 2, "Sycophant_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 2, "Wug_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "Sycophant_EN", 1, "Streetlight_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "Wug_EN", 1, "Streetlight_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "GearYinimro_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "WRK_EN", 1, "Wug_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "YesMan_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "WRK_EN", 1, "Streetlight_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "Sycophant_EN", 1, "BurningShame_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "Wug_EN", 1, "BurningShame_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "RedSibling_EN", 1, "Wug_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "BlueSibling_EN", 1, "Wug_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "YellowSibling_EN", 1, "Wug_EN");
                nowhereManMedium.SimpleAddEncounter(1, "NowhereMan_EN", 1, "PurpleSibling_EN", 1, "Wug_EN");


                nowhereManMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Nowhere.Med, 10, "TheAbyss_Zone3", BundleDifficulty.Medium);

                EnemyEncounter_API nowhereManHard = new EnemyEncounter_API(0, Abyss.H.Nowhere.Hard, nowhereSign)
                {
                    MusicEvent = "event:/SorasMusic/Enemies/NowhereMusic/Sewer",
                    RoarEvent = "event:/SorasSFX/Enemies/NowhereMan/NowhereRoar",
                };

                nowhereManHard.SimpleAddEncounter(2, "NowhereMan_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "Parfait_EN", 1, "BurningShame_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "RedSibling_EN", 1, "BurningShame_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "BlueSibling_EN", 1, "BurningShame_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "YellowSibling_EN", 1, "BurningShame_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "PurpleSibling_EN", 1, "BurningShame_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "YesMan_EN", 2, "BurningShame_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "ParadoxYinimro_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "Kcolclock_EN");
                nowhereManHard.SimpleAddEncounter(1, "NowhereMan_EN", 1, "HandOfGod_EN");

                nowhereManHard.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Nowhere.Hard, 7, "TheAbyss_Zone3", BundleDifficulty.Hard);

                Portals.AddPortalSign("NowhereQuartet_Sign", ResourceLoader.LoadSprite("timelineFourNowheres.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

                EnemyEncounter_API nowhereManFunny = new EnemyEncounter_API(0, "H_ZoneAbyss_NowhereBarbershop_Joke_EnemyBundle", "NowhereQuartet_Sign")
                {
                    MusicEvent = "event:/UpdatedSmilerMusic",
                    RoarEvent = "event:/SorasSFX/Enemies/NowhereMan/NowhereRoar",
                };
                nowhereManFunny.SimpleAddEncounter(4, "NowhereMan_EN");
                nowhereManFunny.AddEncounterToDataBases();

                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_NowhereBarbershop_Joke_EnemyBundle", 1, "TheAbyss_Zone3", BundleDifficulty.Hard);
                if (SorasToybox.extradebug.Value)
                {
                    UnityEngine.Debug.Log("Nowhere Man Encounters loaded.");
                }
            }
        }
    }
}
