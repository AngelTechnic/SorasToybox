using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class SERVICEEncounters
    {
        public static void Add()
        {
            //rules for TANGO fights:
            //give the player initial usable pigment. I know the statues have whiteblooded but you can give them some extra juice.
            //3 TANGO at most.

            //TANGO sign
            string serviceSign = "SERVICE_Sign";
            Portals.AddPortalSign(serviceSign, ResourceLoader.LoadSprite("TimelineSERVICE.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            //initializing TANGO encounters, here's the sound events too.
            EnemyEncounter_API serviceMedium = new EnemyEncounter_API(0, Orph.H.SERVICE.Med, serviceSign)
            {
                MusicEvent = "event:/SorasMusic/Enemies/SERVICEMusic/TheWallsAreTryingToKillMe",
                RoarEvent = "event:/SorasSFX/Enemies/TANGO/TangoRoar",
            };

            serviceMedium.SimpleAddEncounter(2, Enemies.Service, 2, Enemies.MusicMan);
            serviceMedium.SimpleAddEncounter(2, Enemies.Service, 1, Humor.Red, 1, Humor.Blue);
            serviceMedium.SimpleAddEncounter(1, Enemies.Service, 2, "Scrungie_EN");

            serviceMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.SERVICE.Med, 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);


            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[ENCOUNTERS] SERVICE Encounters loaded.");
            }
        }
    }
}