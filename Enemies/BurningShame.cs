using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace SorasToybox.Enemies
{
    public class BurningShame
    {
        public static void  Add()
        {
            Enemy burningShame = new Enemy("Burning Shame", "BurningShame_EN")
            {
                Health = 30,
                HealthColor = LoadedDBsHandler.PigmentDB.GetPigment("Broken"),
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineBurningShameBoss", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineBurningShameBoss", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/SorasSFX/Fools/KarmaHurt",
                DeathSound = "event:/SorasSFX/Fools/KarmaDie",
            };
            burningShame.AddPassives([Passives.Overexert10, Passives.Withering, Passives.GetCustomPassive("Fragile_PA")]);
        }
    }
}
