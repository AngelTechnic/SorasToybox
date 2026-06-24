using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class LitanyEncounters
    {
        public static void Add()
        {
            //encounter rules for Litany:
            //Only 1 Litany per encounter
            //try not to add too many grace enemies
            //enemies that are naturally infantile can be really funny so go ahead
            //enemies that are naturally parental are diabolical but go ahead

            if (LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3"))
            {
                //litany sign here VVVVV
                Portals.AddPortalSign("Litany_Sign", ResourceLoader.LoadSprite("timelineLitany", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                
                //initializing medium encounters
                EnemyEncounter_API litanyMedium = new EnemyEncounter_API(0, "H_ZoneAbyss_Litany_Medium_EnemyBundle", "Litany_Sign")
                {
                    //Where would we be without good music?
                    MusicEvent = "event:/SorasMusic/Enemies/LitanyMusic/Singsong",
                    RoarEvent = "event:/SorasSFX/Enemies/Litany/LitanyRoar",
                };

                litanyMedium.SimpleAddEncounter(1, "Litany_EN", 1, "WanderFellow_EN", 1, "Streetlight_EN");
                litanyMedium.SimpleAddEncounter(1, "Litany_EN", 2, "Sycophant_EN");
                litanyMedium.SimpleAddEncounter(1, "Litany_EN", 1, "WRK_EN", 1, "Streetlight_EN");
                litanyMedium.SimpleAddEncounter(1, "Litany_EN", 1, "WRK_EN", 1, "Wug_EN");
                litanyMedium.SimpleAddEncounter(1, "Litany_EN", 2, "GearYinimro_EN");
                if (SorasToybox.CrossMod.AApocrypha)
                {
                    litanyMedium.SimpleAddEncounter(1, "Litany_EN", 1, "MachineGnomes_EN", 1, "Streetlight_EN");
                    litanyMedium.SimpleAddEncounter(1, "Litany_EN", 1, "BFElemental_EN", 1, "Wug_EN");
                    litanyMedium.SimpleAddEncounter(1, "Litany_EN", 1, "BasicElemental_EN", 1, "Sycophant_EN");
                }

                litanyMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_Litany_Medium_EnemyBundle", 10, "TheAbyss_Zone3", BundleDifficulty.Medium);
            }
        }
    }
}
