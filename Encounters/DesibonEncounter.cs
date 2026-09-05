using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class DesibonEncounter
    {
        public static void Add()
        {
            Portals.AddPortalSign("Desibon_Sign", ResourceLoader.LoadSprite("TimelineDendriteDesibon", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API desibonBoss = new EnemyEncounter_API(EncounterType.Specific, "DendriteDesibon_BOSS", "Desibon_Sign")
            {
                MusicEvent = "event:/VoxPopuliMusic",
                RoarEvent = "event:/VoxPopuliRoar",
                UsesCustomOverworldRoom = true,
                CustomOverworldRoomID = "SirenBossPortalRoom",
                BossID = "DendriteDesibon_BOSS",
            };
            desibonBoss.CreateNewEnemyEncounterData([
                "StoneGertar_EN", "DendriteDesibon_BOSS", "StoneGertar_EN",
            ], [1, 2, 3]);
            desibonBoss.AddEncounterToDataBases();

            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("DendriteDesibon_BOSS", 0, "TheSiren_Zone1", BundleDifficulty.Boss);
        }
    }
}
