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
                //dozer's sign! it's called this VVVVV
                Portals.AddPortalSign("Dozer_Sign", ResourceLoader.LoadSprite("timelineDozer.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

                //initializing mediumdozer encounters, here's the sound events too.
                EnemyEncounter_API dozerMedium = new EnemyEncounter_API(0, "H_ZoneAbyss_Dozer_Medium_EnemyBundle", "Dozer_Sign")
                {
                    MusicEvent = "event:/SorasMusic/Enemies/DozerMusic/GrapeGrappleGravity",
                    RoarEvent = "event:/SorasSFX/Enemies/Dozer/DozerRoar",
                };

                //Medium dozer encounts go down here
                dozerMedium.SimpleAddEncounter(1, "Dozer_EN", 1, "Parfait_EN", 1, "Wug_EN");
                dozerMedium.SimpleAddEncounter(1, "Dozer_EN", 1, "WanderFellow_EN", 1, "Wug_EN");
                dozerMedium.SimpleAddEncounter(1, "Dozer_EN", 2, "Wug_EN", 1, "Streetlight_EN");
                dozerMedium.SimpleAddEncounter(1, "Dozer_EN", 2 ,"EyePalm_EN");
                dozerMedium.SimpleAddEncounter(1, "Dozer_EN", 1, "GearYinimro_EN", 1, "Wug_EN");
                if (SorasToybox.CrossMod.AApocrypha)
                {
                    dozerMedium.SimpleAddEncounter(1, "Dozer_EN", 1, "WanderFellow_EN", 1, "MachineGnomes_EN");
                    dozerMedium.SimpleAddEncounter(1, "Dozer_EN", 1, "BasicElemental_EN", 1, "Wug_EN");
                    dozerMedium.SimpleAddEncounter(1, "Dozer_EN", 1, "BasicElemental_EN", 1, "MachineGnomes_EN");
                }


                dozerMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_Dozer_Medium_EnemyBundle", 9, "TheAbyss_Zone3", BundleDifficulty.Medium);
            }
        }
    }
}
