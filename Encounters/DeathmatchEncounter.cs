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

            string roomID = "AbyssBossPortalRoom";

            string deathmatchMusic = "";
            if (SorasToybox.altantagonistmusic.Value)
            {
                deathmatchMusic = "event:/SorasMusic/Enemies/Bosses/DeathmatchMusic/In The Depths";
            }
            else
            {
                deathmatchMusic = "event:/SorasMusic/Enemies/Bosses/DeathmatchMusic/Jugular Nimbus";
            }

            EnvironmentTools.PrepareCombatEnvPrefab("Assets/ToyboxEnvs/Deathmatch/Deathmatch_CombatEnv_Template.prefab", "DeathmatchEnv", SorasToybox.assetbundle);
            ControlAlphaByHealthPercent controller = LoadedAssetsHandler.TryGetCombatEnvironmentPrefab("DeathmatchEnv").gameObject.AddComponent<ControlAlphaByHealthPercent>();
            controller.Image = controller.transform.Find("NightSky").GetComponent<SpriteRenderer>();//thanks salt


            EnemyEncounter_API deathmatchBoss = new EnemyEncounter_API(EncounterType.Specific, "Deathmatch_BOSS", "Deathmatch_Sign")
            {
                MusicEvent = deathmatchMusic,
                RoarEvent = "event:/SorasSFX/Enemies/Deathmatch/DeathmatchRoar",
                UsesCustomOverworldRoom = true,
                CustomOverworldRoomID = roomID,
                BossID = "Deathmatch_BOSS",
                SpecialEnvironmentID = "DeathmatchEnv",
                UsesSpecialEnvironment = true,
            };

            deathmatchBoss.AddSpecialEnvironment("DeathmatchEnv");
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
            if (SorasToybox.extradebug.Value)
            {
                UnityEngine.Debug.Log("[ENCOUNTERS] Final Encounter loaded.");
            }
        }
    }
    public class ControlAlphaByHealthPercent : MonoBehaviour
    {
        public SpriteRenderer Image;
        public float DecaySpd = 0.05f;
        public float TickSpd = 0.1f;
        float Tick = 0f;
        public void Update()
        {
            Tick -= Time.deltaTime;
            if (Tick <= 0f)
            {
                Tick = TickSpd;
            }
            else return;

            if (CombatManager._instance == null) return;
            if (Image == null)
            {
                Debug.LogWarning("the sprite to control alpha for is missing!");
            }

            foreach (EnemyCombat enemy in CombatManager.Instance._stats.EnemiesOnField.Values)
            {
                if (enemy.Enemy.name == "Deathmatch_BOSS")
                {
                    float current_alpha = Image.color.a;
                    float health_percent = 1f - (float)enemy.CurrentHealth / (float)enemy.Enemy.health;

                    if (current_alpha > health_percent)
                    {
                        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Math.Max(health_percent, current_alpha - DecaySpd));
                    }
                    else if (current_alpha < health_percent)
                    {
                        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, Math.Min(health_percent, current_alpha + DecaySpd));
                    }

                    break;
                }
            }
        }
    }
}
