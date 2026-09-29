using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Events
{
    public class KarmaFreeEvent
    {
        public static void Add()
        {
            string text = "Karma_Dialogue";
            string text2 = "Karma_FreeFool";
            string text3 = "Karma_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/ToyboxRooms/KarmaRoom/KarmaFree.prefab", text2, SorasToybox.assetbundle);
            YarnProgram yarnProgram = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/KarmaRoom/KarmaFreeScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "SorasToybox.Karma.TryHire");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("karma_sign", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Karma_CH",
            };
            freeFoolEncounterSO._freeFool = "Karma_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;

            //generating this free fool event then adding it to the abyss
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO = LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO._FreeFoolsPool.Add(text2);

            //quest time.
            //setting up string variables for karma's unlock dialogue
            string text4 = "Karma_Unlock";
            string text5 = "Karma_Unlock_Dialogue";
            string text6 = "Karma_Unlock_Sign";

            //both the free fool event and the unlock event use the same room prefab so we're calling that here, with room ID adjusted
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/ToyboxRooms/KarmaRoom/KarmaFree.prefab", text4, SorasToybox.assetbundle);
            //to keep stuff clean the unlock event will use a different yarn file
            YarnProgram yarnProgram3 = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/KarmaRoom/KarmaUnlock.yarn"));
            Dialogues.AddCustom_DialogueProgram(text5, yarnProgram3);

            //karma's unlock yarn file uses a few branching if statements that check
            //whether you've visited her in previous runs and if you've already spoken to her.
            //it all starts at the following node
            Dialogues.CreateAndAddCustom_DialogueSO(text5, yarnProgram3, text5, "SorasToybox.Karma.Start");
            ConditionEncounterSO unlockRoom = ScriptableObject.CreateInstance<ConditionEncounterSO>();
            unlockRoom.encounterEntityIDs = new string[]
            {
                "Karma_CH"
            };
            Portals.AddPortalSign(text6, ResourceLoader.LoadSprite("karma_menu", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            unlockRoom.signID = text6;
            unlockRoom.m_QuestName = "Karma";
            unlockRoom.m_QuestsCompletedNeeded = [];
            unlockRoom._dialogue = text5;
            unlockRoom.encounterRoom = text4;
            ModdedNPCs.AddCustom_ConditionEncounter(text4, unlockRoom);
            ZoneBGDataBaseSO zoneBGDataBaseSO3 = LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO3._QuestPool.Add(text4);


            Debug.Log("Free Fool Events | Abyss | Karma");
        }
    }
}