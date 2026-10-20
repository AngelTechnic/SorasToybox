using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Events
{
    public class GazerFlavorEvent
    {
        public static void Add()
        {
            //adapting from the free fool tester
            CardInfo info = new CardInfo()
            {
                pilePosition = PilePositionType.Any,
                cardType = CardType.Flavour,
            };
            CardTypeInfo card = new CardTypeInfo();
            card._cardInfo = info;
            card._minimumAmount = 0;
            card._maximumAmount = 1;
            if (SorasToybox.CrossMod.IntoTheAbyss && LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3")) { (LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray(); }



            string text = "Gazer_Dialogue";
            string text2 = "Gazer_Flavor";
            string text3 = "Gazer_Sign";

            SpeakerBundle speakerBundleGazer = new SpeakerBundle();
            speakerBundleGazer.bundleTextColor = new Color32(255, 32, 32, 192);
            speakerBundleGazer.dialogueSound = "event:/SorasSFX/Enemies/TANGO/TangoHurt";
            speakerBundleGazer.portrait = ResourceLoader.LoadSprite("GazerTalk1.png", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleGazer1 = new SpeakerBundle();
            speakerBundleGazer1.bundleTextColor = new Color32(255, 32, 32, 192);
            speakerBundleGazer1.dialogueSound = "event:/SorasSFX/Enemies/TANGO/TangoHurt";
            speakerBundleGazer1.portrait = ResourceLoader.LoadSprite("GazerTalk2", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleGazer2 = new SpeakerBundle();
            speakerBundleGazer2.bundleTextColor = new Color32(255, 32, 32, 192);
            speakerBundleGazer2.dialogueSound = "event:/SorasSFX/Enemies/TANGO/TangoHurt";
            speakerBundleGazer2.portrait = ResourceLoader.LoadSprite("GazerTalk3", new Vector2(0.5f, 0f), 32);

            Dialogues.CreateAndAddCustom_SpeakerData("Gazer", speakerBundleGazer, true, true, new SpeakerEmote[2]
            {
                new SpeakerEmote
                {
                    emotion = "One",
                    bundle = speakerBundleGazer1,
                },

                new SpeakerEmote
                {
                    emotion = "Two",
                    bundle = speakerBundleGazer2,
                },
            });


            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/ToyboxRooms/GazerRoom/GazerFlavor.prefab", text2, SorasToybox.assetbundle);
            YarnProgram yarnProgram = SorasToybox.assetbundle.LoadAsset<YarnProgram>(string.Format("Assets/ToyboxRooms/GazerRoom/GazerFlavor.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "SorasToybox.Gazer.Start");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("Gazer_OW", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);

            BasicEncounterSO basicEncounterSO = ScriptableObject.CreateInstance<BasicEncounterSO>();
            basicEncounterSO.encounterEntityIDs = new string[]
            {
                "Gazer",
            };
            basicEncounterSO.signID = text3;
            basicEncounterSO._dialogue = text;
            basicEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_BasicEncounter(text2, basicEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO = LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO._FlavourPool.Add(text2);
            Debug.Log("[EVENTS] Added the Gazer.");
        }
    }
}
