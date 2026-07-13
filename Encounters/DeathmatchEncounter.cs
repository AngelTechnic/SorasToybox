using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using UnityEngine;


namespace SorasToybox.Encounters
{
    public class DeathmatchEncounter
    {
        public static void Add()
        {
            Portals.AddPortalSign("Deathmatch_Sign", ResourceLoader.LoadSprite("TimelineDeathmatchBoss", null, 32, null), Portals.BossIDColor);
            OverworldRooms.Prepare_Boss_RoomPrefab("Assets/ToyboxRooms/NewAbyssBossGate/AbyssBossRoom2.prefab", "AbyssBossGate2", SorasToybox.assetbundle);
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Prepared prefab");
            }
            string roomID = "AbyssBossGate2";
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("RoomID assigned to string.");
            }
            BaseRoomHandler room = LoadedAssetsHandler.GetRoomPrefab(CardType.Boss, roomID); //Get the room that just got prepared
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Getting room...");
            }
            Boss_RoomHandlerModData data = room.GetComponent<Boss_RoomHandlerModData>(); //Get the modded portal data
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Getting portal data");
            }
            Material portalMaterial = LoadedDBsHandler.MiscDB.GetMaterial(Misc.MaterialIDs.Portal.ToString()); //Get the portal material
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Getting portal material...");
                Debug.Log(data.m_BossPortalRenderer.gameObject.name);
                Debug.Log(data.m_BossPortalRenderer.transform.parent.gameObject.name);
            }

            //THIS MIGHT BE POSSIBLY WRONG but you just have to navigate to get the portal Renderer and do something like this
            data.m_BossPortalRenderer.material = portalMaterial;
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Assigning portal material to boss portal");
            }
            data.m_ZonePortalRenderer.material = portalMaterial;

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Assigning portal material to next zone portal");
            }


            EnemyEncounter_API deathmatchBoss = new EnemyEncounter_API(EncounterType.Specific, "Deathmatch_BOSS", "Deathmatch_Sign")
            {
                MusicEvent = "event:/SorasMusic/Enemies/Bosses/DeathmatchMusic/In The Depths",
                RoarEvent = "event:/SorasSFX/Enemies/Deathmatch/DeathmatchRoar",
                UsesCustomOverworldRoom = true,
                CustomOverworldRoomID = roomID,
                BossID = "Deathmatch_BOSS",
                SpecialEnvironmentID = "MinotaurEnv",
                UsesSpecialEnvironment = true,
            };
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Enemyencounterapi thingy");
            }
            deathmatchBoss.AddSpecialEnvironment("MinotaurEnv");
            deathmatchBoss.CreateNewEnemyEncounterData([
                "Deathmatch_BOSS",
            ], [2]);
            deathmatchBoss.AddEncounterToDataBases();
            Misc.AddCustom_VSAnimationData("Deathmatch_BOSS", new VsBossData
            {
                animation = SorasToybox.assetbundle.LoadAsset<AnimationClip>("Assets/ToyboxEnemies/Deathmatch/vsBoss_PopUpDeathmatch.anim"),
                roarTime = 7.25f,
                arenaSprite = ResourceLoader.LoadSprite("DeathmatchArenaIntroThingy", null, 32, null),
                //extraArenaSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null),
                bossSprite = ResourceLoader.LoadSprite("deathmatch_splash", null, 32, null),
                signatureSprite = ResourceLoader.LoadSprite("deathmatch_nameplate", null, 32, null),
                //extraSignatureSprite = ResourceLoader.LoadSprite("AcolyteTimeline", null, 32, null)
            });
            LoadedDBsHandler._PortalDB.AddBackgroundPortal("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPortal", new Vector2?(new Vector2(0.5f, 0f)), 50, null));
            
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("Deathmatch_BOSS", 10, "TheAbyss_Zone3", BundleDifficulty.Boss);
        }
    }
}
