using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox;
using UnityEngine;

namespace SorasToybox.Encounters
{
    public class LitanyEncounters
    {
        public static void Add()
        {
            if (LoadedDBsHandler.EnemyDB.DoesEncounterPoolExist("TheAbyss_Zone3"))
            {
                Portals.AddPortalSign("Litany_Sign", ResourceLoader.LoadSprite("timelineLitany", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            }
        }
    }
}
