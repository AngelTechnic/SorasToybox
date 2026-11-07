using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class ErebusEncounter
    {
        public static void Add()
        {
            string signName = "Erebus_Sign";
            Portals.AddPortalSign(signName, ResourceLoader.LoadSprite("TimelineErebus", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API erebusBoss = new EnemyEncounter_API(EncounterType.Specific, "Erebus_BOSS", signName)
            {
                MusicEvent = "event:/SorasMusic/Enemies/Bosses/ErebusMusic/ErebusMusic",
                RoarEvent = "event:/SorasSFX/Enemies/Erebus/ErebusRoar",
                BossID = "Erebus_BOSS",
            };
            erebusBoss.SpecialEnvironmentID = LoadedAssetsHandler.GetEnemyBundle("BOSS_Zone01_Hickory_EnemyBundle")._specialCombatEnvironment;
            erebusBoss.UsesSpecialEnvironment = true;

            erebusBoss.CreateNewEnemyEncounterData([
                "Erebus_BOSS",
            ], [2]);
            erebusBoss.AddEncounterToDataBases();
            Misc.AddCustom_VSAnimationData("Erebus_BOSS", new VsBossData
            {
                animation = SorasToybox.assetbundle.LoadAsset<AnimationClip>("Assets/ToyboxEnemies/Erebus/vsBoss_PopUpErebus.anim"),
                roarTime = 4f,
                arenaSprite = ResourceLoader.LoadSprite("Hickory_Env", null, 32, null),
                //extraArenaSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null),
                bossSprite = ResourceLoader.LoadSprite("ErebusSplash", null, 32, null),
                signatureSprite = ResourceLoader.LoadSprite("ErebusSignature", null, 32, null),
                //extraSignatureSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null)
            });

            LoadedDBsHandler._PortalDB.AddBackgroundPortal("Erebus_BOSS", ResourceLoader.LoadSprite("ErebusPortal", new Vector2?(new Vector2(0.5f, 0f)), 50, null));

            EnemyEncounterUtils.AddEncounterToZoneSelector("Erebus_BOSS", 10, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Boss);
        }
    }
}
