using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class SymbolOnFlesh
    {
        public static void Add()
        {

            FullHealEffect imsecretlyintothis = ScriptableObject.CreateInstance<FullHealEffect>();
            imsecretlyintothis._directHeal = true;

            ConsumeItemEffect ruined = ScriptableObject.CreateInstance<ConsumeItemEffect>();

            CurrentHealthEffectorCondition dead = ScriptableObject.CreateInstance<CurrentHealthEffectorCondition>();
            dead.healthUnderThreshold = true;
            dead.healthThreshold = 1;

            CountTargetSlotsEffect countempty = ScriptableObject.CreateInstance<CountTargetSlotsEffect>();
            countempty.m_CountOnlyEmptySlots = true;

            //spawning Shames
            SpawnEnemyAnywhereEffect shameFollows = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            shameFollows.enemy = LoadedAssetsHandler.GetEnemy("BurningShame_EN");
            shameFollows._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            shameFollows.givesExperience = false;

            PerformEffect_Item symbolOnFlesh = new PerformEffect_Item("ST_SymbolOnFlesh_ID")
            {
                Item_ID = "SymbolOnFlesh_TW",
                Name = "Symbol On Flesh",
                Flavour = "\"Your life could have been the most beautiful story ever told.\"",
                ShopPrice = 10,
                StartsLocked = true,
                IsShopItem = false,

            };
        }
    }
}
