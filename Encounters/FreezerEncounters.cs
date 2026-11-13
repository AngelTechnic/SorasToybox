using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class FreezerEncounters
    {
        public static void Add()
        {
            string freezerSign = "Freezer_Sign";

            Portals.AddPortalSign(freezerSign, ResourceLoader.LoadSprite("TimelineFreezerRed", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API freezerMedium = new EnemyEncounter_API(0, Siren.H.Freezer.Med, freezerSign)
            {
                MusicEvent = "event:/SorasMusic/Enemies/FreezerMusic/Storming",
                RoarEvent = "event:/SorasSFX/Enemies/Freezer/FreezerRoar",
            };
            freezerMedium.SimpleAddEncounter(3, "Freezer_EN", 1, "Cooler_EN");
            freezerMedium.SimpleAddEncounter(3, "Freezer_EN");
            freezerMedium.SimpleAddEncounter(3, "Freezer_EN", 2, Enemies.Puker);
            freezerMedium.SimpleAddEncounter(2, "Freezer_EN", 3, Enemies.Puker);
            freezerMedium.SimpleAddEncounter(2, "Freezer_EN", 1, Enemies.Puker, 1, "Olmic_EN");
            freezerMedium.SimpleAddEncounter(2, "Freezer_EN", 1, "Cooler_EN", 1, "Tassnn_EN");
            freezerMedium.SimpleAddEncounter(2, "Freezer_EN", 2, "Boiler_EN");

            freezerMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Siren.H.Freezer.Med, 12, "TheSiren_Zone1", BundleDifficulty.Medium);
        }
    }
}
