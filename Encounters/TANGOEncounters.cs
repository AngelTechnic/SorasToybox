using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class TANGOEncounters
    {
        public static void Add()
        {
            //rules for TANGO fights:
            //give the player initial usable pigment. I know the statues have whiteblooded but you can give them some extra juice.
            //3 TANGO at most.

            //TANGO sign
            string tangoSign = "TANGO_Sign";
            Portals.AddPortalSign(tangoSign, ResourceLoader.LoadSprite("TimelineTANGO.png", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            //initializing TANGO encounters, here's the sound events too.
            EnemyEncounter_API tangoMedium = new EnemyEncounter_API(0, Garden.H.TANGO.Med, tangoSign)
            {
                MusicEvent = "event:/SorasMusic/Enemies/TANGOMusic/APartFalling",
                RoarEvent = "event:/SorasSFX/Enemies/TANGO/TangoRoar",
            };

            tangoMedium.SimpleAddEncounter(1, "TANGO_EN", 3, "PawnA_EN");
            tangoMedium.SimpleAddEncounter(1, "TANGO_EN", 1, "InHerImage_EN", 1, "InHisImage_EN");
            tangoMedium.SimpleAddEncounter(2, "TANGO_EN");
            tangoMedium.SimpleAddEncounter(1, "TANGO_EN", 1, "GearYinimro_EN", 2, "NextOfKin_EN");
            tangoMedium.SimpleAddEncounter(1, "TANGO_EN", 1, "GigglingMinister_EN");
            tangoMedium.SimpleAddEncounter(2, "TANGO_EN", 1, "Phobia_Phobias_EN");
            tangoMedium.SimpleAddEncounter(1, "TANGO_EN", 1, "SomeoneSister_EN", 1, "NooneSister_EN");
            tangoMedium.SimpleAddEncounter(2, "TANGO_EN", 2, "Git_EN");

            tangoMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.TANGO.Med, 12, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);


            //hard guys
            EnemyEncounter_API tangoHard = new EnemyEncounter_API(0, Garden.H.TANGO.Hard, tangoSign)
            {
                MusicEvent = "event:/SorasMusic/Enemies/TANGOMusic/APartFalling",
                RoarEvent = "event:/SorasSFX/Enemies/TANGO/TangoRoar",
            };
            tangoHard.SimpleAddEncounter(2, "TANGO_EN", 1, "SkinningHomunculus_EN");
            tangoHard.SimpleAddEncounter(2, "TANGO_EN", 1, "ParadoxYinimro_EN");
            tangoHard.SimpleAddEncounter(2, "TANGO_EN", 1, "Psychopomp_EN");
            tangoHard.SimpleAddEncounter(2, "TANGO_EN", 1, "SkeweringHomunculus_EN");
            tangoHard.SimpleAddEncounter(1, "TANGO_EN", 1, "InTheDark_EN", 1, "Lunoscope_EN");
            tangoHard.SimpleAddEncounter(2, "TANGO_EN", 2, "GigglingMinister_EN");
            tangoHard.SimpleAddEncounter(3, "TANGO_EN", 1, "MachineGnomes_EN");
            tangoHard.SimpleAddEncounter(2, "TANGO_EN", 2, "SullenPrioress_EN");
            tangoHard.SimpleAddEncounter(2, "TANGO_EN", 1, "FrowningChancellor_EN", 1, "Vagabond_EN");
            tangoHard.SimpleAddEncounter(2, "TANGO_EN", 2, "Attrition_EN", 1, "WildlifeSign_EN");
            tangoHard.SimpleAddEncounter(3, "TANGO_EN", 1, "StopSign_EN");
            tangoHard.SimpleAddEncounter(3, "TANGO_EN", 1, "WildlifeSign_EN");
            tangoHard.SimpleAddEncounter(3, "TANGO_EN", 1, "YieldSign_EN");
            tangoHard.SimpleAddEncounter(3, "TANGO_EN", 1, "ParkingSign_EN");
            tangoHard.SimpleAddEncounter(3, "TANGO_EN", 1, "HospitalSign_EN");
            tangoHard.SimpleAddEncounter(3, "TANGO_EN", 1, "ExitSign_EN");

            tangoHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.TANGO.Hard, 8, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);


            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("TANGO Encounters loaded.");
            }
        }
    }
}
