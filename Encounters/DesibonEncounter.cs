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
                MusicEvent = "event:/SorasMusic/Enemies/Bosses/DesibonMusic/RustyNails",
                RoarEvent = "event:/SorasSFX/Enemies/DendriteDesibon/DendriteDesibonBossIntro",
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
            Misc.AddCustom_VSAnimationData("DendriteDesibon_BOSS", new VsBossData
            {
                animation = SorasToybox.assetbundle.LoadAsset<AnimationClip>("Assets/ToyboxEnemies/Desibon/vsBoss_PopUpDesibon.anim"),
                roarTime = 7.25f,
                arenaSprite = ResourceLoader.LoadSprite("TreeArea", null, 32, null),
                //extraArenaSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null),
                bossSprite = ResourceLoader.LoadSprite("DesibonSplash", null, 32, null),
                signatureSprite = ResourceLoader.LoadSprite("DesibonSignature", null, 32, null),
                //extraSignatureSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null)
            });

            LoadedDBsHandler._PortalDB.AddBackgroundPortal("DendriteDesibon_BOSS", ResourceLoader.LoadSprite("DesibonPortal", new Vector2?(new Vector2(0.5f, 0f)), 50, null));

            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("DendriteDesibon_BOSS", 10, "TheSiren_Zone1", BundleDifficulty.Boss);
        }
    }
}
