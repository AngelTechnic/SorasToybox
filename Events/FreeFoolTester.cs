using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Events
{
    public class FreeFoolEventTester
    {
        public static void Add()
        {
            CardInfo info = new CardInfo()
            {
                pilePosition = PilePositionType.First,
                cardType = CardType.EventFreeFool
            };
            CardTypeInfo card = new CardTypeInfo();
            card._cardInfo = info;
            card._minimumAmount = 25;
            card._maximumAmount = 25;
            (LoadedAssetsHandler.GetZoneDB("ZoneDB_01") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("ZoneDB_01") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray();
            (LoadedAssetsHandler.GetZoneDB("ZoneDB_02") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("ZoneDB_02") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray();
            //(LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray();
            (LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_02") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_02") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray();
            (LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_03") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray();
            if (SorasToybox.CrossMod.Siren && LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheSiren_Zone02")) { (LoadedAssetsHandler.GetZoneDB("TheSiren") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("TheSiren") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray(); }
            if (SorasToybox.CrossMod.IntoTheAbyss && LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3")) { (LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO)._deckInfo._possibleCards = new List<CardTypeInfo>((LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO)._deckInfo._possibleCards) { card }.ToArray(); }
        }
    }
}