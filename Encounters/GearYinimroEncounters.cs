using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;


namespace SorasToybox.Encounters
{
    public class GearYinimroEncounters
    {
        public static void Add()
        {
            //rules for Gear Yinimro encounters:
            //Try to play with the fact that they produce red, but milking them for it is dangerous
            //mix them with enemies that have unreliable pigment production like iridescent health or whatever.

            //Gear Sign!
            Portals.AddPortalSign("GearYinimro_Sign", ResourceLoader.LoadSprite("TimelineGearYinimro.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            //initializing Garden Gear Yinimro encounters, here's the sound events too.
            EnemyEncounter_API gearYinimroGardenMedium = new EnemyEncounter_API(0, Garden.H.YinimroG.Med, "GearYinimro_Sign")
            {
                MusicEvent = "event:/SorasMusic/Enemies/YinimroMusic/Blackberry",
                RoarEvent = "event:/IntrusiveRoar",
            };

            //Won't somebody please help me?
            gearYinimroGardenMedium.SimpleAddEncounter(2, "GearYinimro_EN");
            gearYinimroGardenMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.YinimroG.Med, 9, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);



            if (Abyss.Exists)
            {


                //initializing Abyss Gear Yinimro encounters, here's the sound events too.
                EnemyEncounter_API gearYinimroAbyssMedium = new EnemyEncounter_API(0, Abyss.H.YinimroG.Med, "GearYinimro_Sign")
                {
                    MusicEvent = "event:/SorasMusic/Enemies/YinimroMusic/Blackberry",
                    RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_ZoneAbyss_Streetlight_Medium_EnemyBundle")._roarReference.roarEvent,
                };

                //Medium yinimro encounts go down here
                gearYinimroAbyssMedium.SimpleAddEncounter(1, "GearYinimro_EN", 1, "Wug_EN");
                gearYinimroAbyssMedium.SimpleAddEncounter(1, "GearYinimro_EN", 2, "AbandonedPuppet_EN");
                gearYinimroAbyssMedium.SimpleAddEncounter(2, "GearYinimro_EN");
                gearYinimroAbyssMedium.SimpleAddEncounter(1, "GearYinimro_EN", 1, "YesMan_EN", 1, "Sycophant_EN");
                gearYinimroAbyssMedium.SimpleAddEncounter(2, "GearYinimro_EN", 1, "Sycophant_EN");
                

                gearYinimroAbyssMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.YinimroG.Med, 9, "TheAbyss_Zone3", BundleDifficulty.Medium);
            }
        }
    }
}
