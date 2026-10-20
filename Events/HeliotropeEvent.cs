using BrutalAPI;
using HarmonyLib;
using MonoMod.RuntimeDetour;
using SorasToybox.CustomOther;
using SorasToybox.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;


namespace SorasToybox
{

    [HarmonyPatch]
    public class HeliotropeCardManager
    {
        // Token: 0x0600017C RID: 380 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
        [HarmonyPrefix]
        [HarmonyPatch(typeof(ZoneBGDataBaseSO), "TryGenerateNewCard")]
        public static bool CardGenPatch(ZoneBGDataBaseSO __instance, CardInfo info)
        {
            bool flag = info.cardType == HeliotropeCardManager.ForceAddType;
            bool result;
            if (flag)
            {
                HeliotropeCardManager.GenerateHeliotropeCard(__instance, info);
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }

        // Token: 0x17000044 RID: 68
        // (get) Token: 0x0600017D RID: 381 RVA: 0x0001D215 File Offset: 0x0001B415
        public static CardType ForceAddType
        {
            get
            {
                return (CardType)1337;
            }
        }

        // Token: 0x0600017E RID: 382 RVA: 0x0001D21C File Offset: 0x0001B41C
        public static void AddHeliotropeEncounter(BasicEncounterSO BasicEncounter)
        {
            BasicEncounterSO basicEncounterSO;
            bool flag = !LoadedAssetsHandler.LoadedBasicEncounters.TryGetValue(BasicEncounter.name, out basicEncounterSO);
            if (flag)
            {
                LoadedAssetsHandler.LoadedBasicEncounters.Add(BasicEncounter.name, BasicEncounter);
            }
            CardTypeInfo cardTypeInfo = new CardTypeInfo();
            cardTypeInfo._cardInfo = default(CardInfo);
            cardTypeInfo._cardInfo.cardType = HeliotropeCardManager.ForceAddType;
            cardTypeInfo._cardInfo.pilePosition = PilePositionType.Last;
            cardTypeInfo._usePercentage = true;
            cardTypeInfo._percentage = 100;

            HeliotropeCardManager.HeliotropeCard = cardTypeInfo;
            HeliotropeCardManager.HeliotropeEncounter = BasicEncounter;
            ZoneBGDataBaseSO zoneBGDataBaseSO = LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO;
            List<CardTypeInfo> list = zoneBGDataBaseSO._deckInfo._possibleCards.ToList<CardTypeInfo>();
            list.Add(cardTypeInfo);
            zoneBGDataBaseSO._deckInfo._possibleCards = list.ToArray();
        }


        // Token: 0x0600017F RID: 383 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
        public static void GenerateHeliotropeCard(ZoneBGDataBaseSO zone, CardInfo info)
        {
            if (zone == LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO)
            {
                string[] encounterEntityIDs = HeliotropeCardManager.HeliotropeEncounter.encounterEntityIDs;
                foreach (string item in encounterEntityIDs)
                {
                    zone._EntitiesInRun.Add(item);
                }
                TalkingEntityContentData talkingEntityContentData = new TalkingEntityContentData(HeliotropeCardManager.HeliotropeEncounter.DialoguePath);
                int num = zone._zoneData.AddDialoguePathData(talkingEntityContentData);
                Card card = new Card(zone._zoneData.CardCount, num, 0, info.pilePosition, HeliotropeCardManager.HeliotropeEncounter.signID, HeliotropeCardManager.HeliotropeEncounter.encounterRoom);
                zone._zoneData.AddCard(card);
            }
        }

        // Token: 0x04000079 RID: 121
        public static CardTypeInfo HeliotropeCard;

        // Token: 0x0400007A RID: 122
        public static BasicEncounterSO HeliotropeEncounter;
    }

    public class Heliotrope
    {

        public static void Setup()
        {
            IDetour detour = new Hook(typeof(InGameDataSO).GetMethod("InitializeDialogueFunctions", (BindingFlags)(-1)), typeof(Heliotrope).GetMethod("InitializeHeliotropeDialogueFunctions", (BindingFlags)(-1)));
        }





        public static void HeliotropeRoom()
        {
            string text = "Heliotrope_Dialogue";
            string text2 = "Heliotrope_Room";
            string text3 = "Heliotrope_Sign";

            SpeakerBundle speakerBundleHeliotrope = new SpeakerBundle();
            speakerBundleHeliotrope.bundleTextColor = new Color32(0, 255, 255, 192);
            speakerBundleHeliotrope.dialogueSound = "event:/SorasSFX/Fools/HeliotropeD";
            speakerBundleHeliotrope.portrait = ResourceLoader.LoadSprite("heliotrope_alive.png", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleHeliotropeDead = new SpeakerBundle();
            speakerBundleHeliotropeDead.bundleTextColor = new Color32(0, 255, 255, 64);
            speakerBundleHeliotropeDead.dialogueSound = "event:/SorasSFX/Fools/HeliotropeDie";
            speakerBundleHeliotropeDead.portrait = ResourceLoader.LoadSprite("heliotrope_dead", new Vector2(0.5f, 0f), 32);

            Dialogues.CreateAndAddCustom_SpeakerData("Heliotrope", speakerBundleHeliotrope, true, true, new SpeakerEmote[1]
            {
                new SpeakerEmote
                {
                    emotion = "Dead",
                    bundle = speakerBundleHeliotropeDead,
                },
            });

            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/ToyboxRooms/HeliotropeRoom/HeliotropeRoom.prefab", text2, SorasToybox.assetbundle);
            YarnProgram yarnProgram = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/HeliotropeRoom/HeliotropeUnlock.yarn", Array.Empty<object>()));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "SorasToybox.Heliotrope.QuestStart");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("heliotrope_ow.png", new Vector2?(new Vector2(0.5f, 0f)), 32, null), Portals.NPCIDColor);
            ConditionEncounterSO basicEncounterSO = ScriptableObject.CreateInstance<ConditionEncounterSO>();
            basicEncounterSO.encounterEntityIDs = new string[]
            {
                "Heliotrope"
            };
            basicEncounterSO.encounterRoom = text2;
            basicEncounterSO.signID = text3;
            basicEncounterSO._dialogue = text;
            basicEncounterSO.m_QuestsCompletedNeeded = ["Heliotrope"];
            ModdedNPCs.AddCustom_ConditionEncounter(text, basicEncounterSO);


            HeliotropeCardManager.AddHeliotropeEncounter(basicEncounterSO);

            BackwardsUnlockCompatibility.TryLockItemBehindAchievement("SorasToybox_Strangers_Heliotrope_ACH", "Everpresence_TW");
            UnlockableModData heliotropeUnlockData = new UnlockableModData("HeliotropeStranger");
            heliotropeUnlockData.hasModdedAchievementUnlock = true;
            heliotropeUnlockData.moddedAchievementID = "SorasToybox_Strangers_Heliotrope_ACH";
            heliotropeUnlockData.hasItemUnlock = true;
            heliotropeUnlockData.items = ["Everpresence_TW"];

            ListedUnlockCheck heliotropeUnlockCheck = ScriptableObject.CreateInstance<ListedUnlockCheck>();
            heliotropeUnlockCheck.unlockID = "HeliotropeStranger";
            heliotropeUnlockCheck.unlockData = heliotropeUnlockData;
            Unlocks.AddUnlock_ByID(heliotropeUnlockData);

            ModdedAchievements heliotropeAchievement = new ModdedAchievements("Godhead Perfect Sun", "Activate the Heliotrope.", ResourceLoader.LoadSprite("Ach_Quest_Heliotrope", null, 32, null), "SorasToybox_Strangers_Heliotrope_ACH");
            heliotropeAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.StrangersTitleLabel);
        }











        public static void InitializeHeliotropeDialogueFunctions(Action<InGameDataSO, DialogueRunner_BO> orig, InGameDataSO self, DialogueRunner_BO dialogueRunner)
        {
            orig(self, dialogueRunner);
            dialogueRunner.AddCommandHandler("ChangeCurrentBossST", SwapCurrentZoneBossByDialogue);
        }

        public static void SwapCurrentZoneBossByDialogue(string[] info)
        {
            if (info.Length < 1)
            {
                return;
            }
            string text = info[0];
            RunDataSO run = LoadedDBsHandler.InfoHolder.Run;
            if (run.CurrentZoneID >= run.zoneData.Count)
            {
                return;
            }
            RunZoneData runZoneData = run.zoneData[run.CurrentZoneID];
            ZoneDataBaseSO zoneDataBaseSO = runZoneData.LoadZoneDB();
            if (zoneDataBaseSO == null || zoneDataBaseSO.Equals(null))
            {
                return;
            }
            EnemyCombatBundle[] bossBundleArray = runZoneData.BossBundleArray;
            if (bossBundleArray.Length == 0)
            {
                return;
            }
            EnemyCombatBundle enemyCombatBundle = bossBundleArray[bossBundleArray.Length - 1];
            if (!(text == enemyCombatBundle.BossID))
            {
                EnemyCombatBundle enemyCombatBundle2 = zoneDataBaseSO.TryGenerateBossBundle(text);
                if (enemyCombatBundle2 != null)
                {
                    runZoneData.SwapBossBundle(bossBundleArray.Length - 1, enemyCombatBundle2);
                }
            }
        }
    }
}
