using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class DesibonEncounter
    {
        public static void Add()
        {
            Portals.AddPortalSign("Desibon_Sign", ResourceLoader.LoadSprite("TimelineDesibonBoss", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API desibonBoss = new EnemyEncounter_API(EncounterType.Specific, "DendriteDesibon_BOSS", "Desibon_Sign")
            {
                MusicEvent = "event:/VoxPopuliMusic",
                RoarEvent = "event:/VoxPopuliRoar",
                UsesCustomOverworldRoom = true,
                CustomOverworldRoomID = "SirenBossPortalRoom",
                BossID = "DendriteDesibon_BOSS",
                SpecialEnvironmentID = "TreeCombatEnv",
                UsesSpecialEnvironment = true,
            };
            desibonBoss.AddSpecialEnvironment("TreeCombatEnv");
            desibonBoss.CreateNewEnemyEncounterData([
                "GeodeGertar_EN", "DendriteDesibon_BOSS", "GeodeGertar_EN",
            ], [1, 2, 3]);
            desibonBoss.AddEncounterToDataBases();

            LoadedDBsHandler._PortalDB.AddBackgroundPortal("DendriteDesibon_BOSS", ResourceLoader.LoadSprite("DesibonPortal", new Vector2?(new Vector2(0.5f, 0f)), 50, null));

            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("DendriteDesibon_BOSS", 0, "TheSiren_Zone1", BundleDifficulty.Boss);
        }
    }
}
