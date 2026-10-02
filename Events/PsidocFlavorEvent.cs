using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Events
{
    public class PsidocFlavorEvent
    {
        public static void Add()
        {
            string text = "Psidoc_Dialogue";
            string text2 = "Psidoc_Flavor";
            string text3 = "Psidoc_Sign";

            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/ToyboxRooms/Psidoc/PsidocFlavor.prefab", text2, SorasToybox.assetbundle);
            YarnProgram yarnProgram = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/PsidocScript/PsidocScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "SorasToybox.Psidoc.Start");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("psidoc_overworld", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Psidoc_CH",
            };
            freeFoolEncounterSO._freeFool = "Psidoc_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO._FreeFoolsPool.Add(text2);
        }
    }
}
