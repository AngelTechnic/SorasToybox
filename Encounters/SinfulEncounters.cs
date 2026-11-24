using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class SinfulEncounters
    {
        public static void Add()
        {
            //rules for Sinful Reminder fights:
            //they have only hard fights sorry.
            //try not to pair them with enemies that use mirroring movement
            //they hurt other enemies a lot.
            //try to leave at least 1 slot empty for a shame to spawn.

            string sinfulSign = "Sinful_Sign";
            Portals.AddPortalSign(sinfulSign, ResourceLoader.LoadSprite("TimelineSinful.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API sinfulHard = new(0, Garden.H.Reminder.Deathmatch.Hard, sinfulSign)
            {
                MusicEvent = "event:/SorasMusic/Enemies/SinfulMusic/BassThrombosis",
                RoarEvent = "event:/NemesisDx",
            };

            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 2, "InHerImage_EN");
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 2, "InHisImage_EN");
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, Enemies.Shivering);
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 2, Enemies.Tango);
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, Enemies.Minister);
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 2, "SterileBud_EN");
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, "Bonsai_EN");
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, "FrowningChancellor_EN");
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, Martians.Grey, 1, "NextOfKin_EN");
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, "Drippings_EN");
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, Enemies.Phobia);
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, "Idiotware_EN", 1, "SkeweringHomunculus_EN");

            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 2, "Eater_Invis_EN");

            //salt crossovers that i suggested
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 2, "Vivarium_EN");
            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 3, "TortureMeNot_EN");

            sinfulHard.SimpleAddEncounter(1, Reminder.Deathmatch, 1, "Marut_EN");

            sinfulHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Reminder.Deathmatch.Hard, 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}
