using BrutalAPI;
using HarmonyLib;
using SorasToybox.Encounters;
using SorasToybox.CustomOther;
using MonoMod.RuntimeDetour;
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
        public static void AddHeliotropeEncounter(ConditionEncounterSO ConditionEncounter)
        {
            ConditionEncounterSO conditionEncounterSO;
            bool flag = !LoadedAssetsHandler.LoadedConditionEncounters.TryGetValue(ConditionEncounter.name, out conditionEncounterSO);
            if (flag)
            {
                LoadedAssetsHandler.LoadedConditionEncounters.Add(ConditionEncounter.name, ConditionEncounter);
            }
            CardTypeInfo cardTypeInfo = new CardTypeInfo();
            cardTypeInfo._cardInfo = default(CardInfo);
            cardTypeInfo._cardInfo.cardType = HeliotropeCardManager.ForceAddType;
            cardTypeInfo._cardInfo.pilePosition = PilePositionType.Last; 
            cardTypeInfo._usePercentage = true;
            cardTypeInfo._percentage = 100;
            HeliotropeCardManager.HeliotropeCard = cardTypeInfo;
            HeliotropeCardManager.HeliotropeEncounter = ConditionEncounter;
            ZoneBGDataBaseSO zoneBGDataBaseSO = LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO;
            List<CardTypeInfo> list = zoneBGDataBaseSO._deckInfo._possibleCards.ToList<CardTypeInfo>();
            list.Add(cardTypeInfo);
            zoneBGDataBaseSO._deckInfo._possibleCards = list.ToArray();
        }


        // Token: 0x0600017F RID: 383 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
        public static void GenerateHeliotropeCard(ZoneBGDataBaseSO zone, CardInfo info)
        {
            if (zone == LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO)  {
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
            FuckingDeadEffectCondition mdk = ScriptableObject.CreateInstance<FuckingDeadEffectCondition>();

            GameBoolDataSetEffect setNobodyHTBool = ScriptableObject.CreateInstance<GameBoolDataSetEffect>();
            setNobodyHTBool._VariableName = "NobodyKillForHeliotrope";
            setNobodyHTBool._ValueSet = true;

            LoadedAssetsHandler.GetEnemy("Nobody_BOSS").exitEffects.AddItem(Effects.GenerateEffect(setNobodyHTBool, 1, Targeting.Slot_SelfSlot, mdk));
            LoadedAssetsHandler.GetEnemy("TrueNobody_BOSS").exitEffects.AddItem(Effects.GenerateEffect(setNobodyHTBool, 1, Targeting.Slot_SelfSlot, mdk));

            GameBoolDataSetEffect setKatalixiHTBool = ScriptableObject.CreateInstance<GameBoolDataSetEffect>();
            setKatalixiHTBool._VariableName = "KatalixiKillForHeliotrope";
            setKatalixiHTBool._ValueSet = true;

            LoadedAssetsHandler.GetEnemy("Nolocimes_Batretne_BOSS").exitEffects.AddItem(Effects.GenerateEffect(setKatalixiHTBool, 1, Targeting.Slot_SelfSlot, mdk));			
			
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
            YarnProgram yarnProgram = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/HeliotropeRoom/HeliotropeChoice.yarn", Array.Empty<object>()));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "SorasToybox.Heliotrope.Choose");
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

            //i'm gonna be honest idk what i'm doing
            string text4 = "Heliotrope_Unlock_Dialogue";
            string text5 = "Heliotrope_Unlock_Room";
            string text6 = "Heliotrope_Unlock_Sign";

            //both the free fool event and the unlock event use the same room prefab so we're calling that here, with room ID adjusted
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/ToyboxRooms/HeliotropeRoom/HeliotropeRoom.prefab", text4, SorasToybox.assetbundle);
            //to keep stuff clean the unlock event will use a different yarn file
            YarnProgram yarnProgram3 = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/HeliotropeRoom/HeliotropeUnlock.yarn"));
            Dialogues.AddCustom_DialogueProgram(text5, yarnProgram3);

            Dialogues.CreateAndAddCustom_DialogueSO(text5, yarnProgram3, text5, "SorasToybox.Heliotrope.QuestStart");
            ConditionEncounterSO unlockRoom = ScriptableObject.CreateInstance<ConditionEncounterSO>();
            unlockRoom.encounterEntityIDs = new string[]
            {
                "Heliotrope"
            };
            Portals.AddPortalSign(text6, ResourceLoader.LoadSprite("heliotrope_ow", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            unlockRoom.signID = text6;
            unlockRoom.m_QuestName = "Heliotrope";
            unlockRoom.m_QuestsCompletedNeeded = [];
            unlockRoom._dialogue = text5;
            unlockRoom.encounterRoom = text4;
            ModdedNPCs.AddCustom_ConditionEncounter(text4, unlockRoom);
            ZoneBGDataBaseSO zoneBGDataBaseSO3 = LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO3._QuestPool.Add(text4);
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
            dialogueRunner.AddCommandHandler("SetBossNobody", ChangeBossToNobody);
            dialogueRunner.AddCommandHandler("SetBossKatalixi", ChangeBossToKatalixi);
			dialogueRunner.AddCommandHandler("SetBossDeathmatch", ChangeBossToDeathmatch);
        }

        public static void ChangeBossToNobody(string[] info)
        {
            var manager = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            manager.GenerateZone(1, "TheAbyss");

        }
        public static void ChangeBossToKatalixi(string[] info)
        {
            var manager = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            manager.GenerateZone(1, "TheAbyss");

        }
        public static void ChangeBossToDeathmatch(string[] info)
        {
            var manager = UnityEngine.Object.FindObjectOfType<OverworldManagerBG>();
            manager.GenerateZone(1, "TheAbyss");

        }
    }





























}
