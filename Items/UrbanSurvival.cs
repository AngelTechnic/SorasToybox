using BrutalAPI;
using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class UrbanSurvival
    {
        public static void Add()
        {
            //Getting Red Color
            string redID = ColorUtility.ToHtmlStringRGB(Color.red);

            AddPassiveEffect getHostile = ScriptableObject.CreateInstance<AddPassiveEffect>();
            getHostile._passiveToAdd = Passives.GetCustomPassive("ST_Hostile_PA");

            PerformEffect_Item urbanSurvival = new("ST_UrbanSurvival_ID", null, false)
            {
                Item_ID = "UrbanSurvivalGuide_SW",
                Name = "<color=#" + redID + ">Urban Survival Guide</color>",
                Flavour = "\"No such thing as a fair fight.\"",
                Description = "Adds Hostile as a passive to everything on combat start.",
                ShopPrice = 18,
                IsShopItem = true,
                StartsLocked = true,
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(getHostile, 1, Targeting.AllUnits),
                ],
                Icon = ResourceLoader.LoadSprite("item_urbansurvival"),
                OnUnlockUsesTHE = true,
            };

            urbanSurvival.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Magic.ToString(),
            ];

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(urbanSurvival.item, new ItemModdedUnlockInfo("UrbanSurvivalGuide_SW", ResourceLoader.LoadSprite("item_urbansurvival_locked", null, 32, null), "DeathmatchBoss_ACH"));

        }
    }
}
