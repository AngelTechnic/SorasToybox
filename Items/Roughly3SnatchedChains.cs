using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.Enemies;
using UnityEngine;

namespace SorasToybox.Items
{
    public class Roughly3SnatchedChains
    {
        public static void Add()
        {
            //invoking the mighty Slatecarnate
            SpawnEnemyAnywhereEffect theMiddleNeverForgets = ScriptableObject.CreateInstance<SpawnEnemyAnywhereEffect>();
            theMiddleNeverForgets.enemy = LoadedAssetsHandler.GetEnemy("HeavensSlate_EN");
            theMiddleNeverForgets._spawnTypeID = CombatType_GameIDs.Spawn_Basic.ToString();
            theMiddleNeverForgets.givesExperience = false;

            //Turning this guy Grey
            HealthColorChange_Wearable_SMS werePixiesSinceThe1960s = ScriptableObject.CreateInstance<HealthColorChange_Wearable_SMS>();
            werePixiesSinceThe1960s._healthColor = Pigments.Grey;

            PerformEffect_Item snatchedChains = new PerformEffect_Item("Roughly 3 Snatched Chains", null, false)
            {
                Item_ID = "Roughly3SnatchedChains_SW",
                Name = "Roughly 3 Snatched Chains",
                Flavour = "\"I don't know how to feel about this.\"",
                Description = "Turns this party member's health color Grey.\nOn combat start, attempts to summon a friendly Slatecarnate???",
                IsShopItem = true,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = false, //note to self: when Deathmatch becomes a thing, make this locked.
                Icon = ResourceLoader.LoadSprite("noCorpse"), //item goes here
                TriggerOn = TriggerCalls.OnCombatStart,
                EquippedModifiers = [werePixiesSinceThe1960s],
                Effects =
                [
                    Effects.GenerateEffect(theMiddleNeverForgets, 1, Targeting.Slot_Front),
                ]
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(snatchedChains.item, new ItemModdedUnlockInfo(snatchedChains.Item_ID, ResourceLoader.LoadSprite("noCorpse", null, 32, null), "SorasToybox_Slate_Antagonist_ACH"));
            Debug.Log("Roughly 3 Snatched Chains loaded! Make sure you remove this from its file later.");
        }
    }
}
