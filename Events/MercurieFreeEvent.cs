using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Events
{
    public class MercurieFreeEvent
    {
        public static void Add()
        {
            string text = "Mercurie_Dialogue";
            string text2 = "Mercurie_FreeFool";
            string text3 = "Mercurie_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/ToyboxRooms/MercurieRoom/MercurieFree.prefab", text2, SorasToybox.assetbundle);
            YarnProgram yarnProgram = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/MercurieRoom/MercurieFreeFool.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "SorasToybox.Mercurie.TryHire");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("mercurie_overworld", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Mercurie_CH",
            };
            freeFoolEncounterSO._freeFool = "Mercurie_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO2 = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO2._FreeFoolsPool.Add(text2);
            Debug.Log("Free Fool Events | Far Shore | Mercurie");
        }
    }
}