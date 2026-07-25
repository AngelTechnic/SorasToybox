using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Events
{
    public class WhhvayFreeEvent
    {
        public static void Add()
        {
            string text = "Whhvay_Dialogue";
            string text2 = "Whhvay_FreeFool";
            string text3 = "Whhvay_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/ToyboxRooms/WhhvayRoom/WhhvayFree.prefab", text2, SorasToybox.assetbundle);
            YarnProgram yarnProgram = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/WhhvayRoom/WhhvayFreeScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "SorasToybox.Karma.TryHire");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("karma_menu", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Whhvay_CH",
            };
            freeFoolEncounterSO._freeFool = "Whhvay_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO = LoadedAssetsHandler.GetZoneDB("TheSiren") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO._FreeFoolsPool.Add(text2);
            Debug.Log("Free Fool Events | Siren | Whhvay");
        }
    }
}