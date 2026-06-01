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
                Portals.AddPortalSign("Litany_Sign", ResourceLoader.LoadSprite("timelineLitany", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

                EnemyEncounter_API litanyMedium = new EnemyEncounter_API(0, "H_ZoneAbyss_Litany_Medium_EnemyBundle", "Litany_Sign")
                {
                    //Where would we be without good music?
                    MusicEvent = "event:/SorasMusic/Enemies/LitanyMusic/Singsong",
                    RoarEvent = "event:/SorasSFX/Enemies/Litany/LitanyRoar",
                };

                litanyMedium.CreateNewEnemyEncounterData(
                [
                    "WanderFellow_EN",
                    "Litany_EN",
                    "Streetlight_EN",
                ], null);

                litanyMedium.CreateNewEnemyEncounterData(
                [
                    "Litany_EN",
                    "Sycophant_EN",
                    "Sycophant_EN",
                ], null);
                litanyMedium.CreateNewEnemyEncounterData(["Litany_EN", "WRK_EN", "Streetlight_EN"], null);
                litanyMedium.CreateNewEnemyEncounterData(["Litany_EN", "WRK_EN", "Wug_EN"], null);
                if (SorasToybox.CrossMod.AApocrypha)
                {
                    litanyMedium.CreateNewEnemyEncounterData(["Litany_EN", "MachineGnomes_EN", "Streetlight_EN"], null);
                    litanyMedium.CreateNewEnemyEncounterData(["Litany_EN", "BFElemental_EN", "Wug_EN"], null);
                    litanyMedium.CreateNewEnemyEncounterData(["Litany_EN", "BasicElemental_EN", "Sycophant_EN"], null);
                }

                litanyMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_Litany_Medium_EnemyBundle", 4, "TheAbyss_Zone3", BundleDifficulty.Medium);
            }
        }
    }
}
