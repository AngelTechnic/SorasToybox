using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using UnityEngine;


namespace SorasToybox.Encounters
{
    public class GardenDeathmatchEncounter
    {
        public static void Add()
        {
            Portals.AddPortalSign("GDeathmatch_Sign", ResourceLoader.LoadSprite("TimelineDeathmatchBoss", null, 32, null), Portals.BossIDColor);
            EnemyEncounter_API deathmatchBoss = new EnemyEncounter_API(EncounterType.Specific, "GardenDeathmatch_BOSS", "GDeathmatch_Sign")
            {
                MusicEvent = "event:/SorasMusic/Enemies/Bosses/DeathmatchMusic/In The Depths",
                RoarEvent = "event:/SorasSFX/Enemies/Deathmatch/DeathmatchRoar",
                BossID = "Deathmatch_BOSS",
                SpecialEnvironmentID = "MinotaurEnv",
                UsesSpecialEnvironment = true,
            };
            deathmatchBoss.AddSpecialEnvironment("MinotaurEnv");
            deathmatchBoss.CreateNewEnemyEncounterData([
                "Deathmatch_BOSS",
            ], [2]);
            deathmatchBoss.AddEncounterToDataBases();
            Misc.AddCustom_VSAnimationData("GardenDeathmatch_BOSS", new VsBossData
            {
                animation = SorasToybox.assetbundle.LoadAsset<AnimationClip>("Assets/ToyboxEnemies/Deathmatch/vsBoss_PopUpDeathmatch.anim"),
                roarTime = 7.25f,
                arenaSprite = ResourceLoader.LoadSprite("DeathmatchArenaIntroThingy", null, 32, null),
                //extraArenaSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null),
                bossSprite = ResourceLoader.LoadSprite("deathmatch_splash", null, 32, null),
                signatureSprite = ResourceLoader.LoadSprite("deathmatch_nameplate", null, 32, null),
                //extraSignatureSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null)
            });
            LoadedDBsHandler._PortalDB.AddBackgroundPortal("GardenDeathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPortal", new Vector2?(new Vector2(0.5f, 0f)), 50, null));
            
            EnemyEncounterUtils.AddEncounterToZoneSelector("GardenDeathmatch_BOSS", 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Boss);
        }
    }
}
