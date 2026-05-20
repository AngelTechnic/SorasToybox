using BrutalAPI;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


namespace SorasToybox.Enemies
{
    public class SEARCH
    {
        public static void Add()
        {
            //Scars application
            StatusEffect_Apply_Effect applyScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            applyScars._Status = StatusField.Scars;
            
            //Shield application
            FieldEffect_Apply_Effect applyShield = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            applyShield._Field = StatusField.Shield;

            //Basic damage
            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();
            damage._indirect = false;

            //Proportional damage to split self with. Also here's the hidden Pomni
            ProportionalDamageEffect propDamage = ScriptableObject.CreateInstance<ProportionalDamageEffect>();
            propDamage._indirect = false;

            Enemy search = new Enemy("SEARCH", "SEARCH_EN")
            {
                Health = 8,
                HealthColor = Pigments.Red,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("timelineWug.png", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("noCorpse", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("timelineWug", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/Nothing",
                DeathSound = "event:/Nothing",
            };
        }
    }
}
