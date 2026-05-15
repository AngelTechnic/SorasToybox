using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;

namespace SorasToybox.Items
{
    public class BastardNimbus
    {
        public static void Add()
        {
            //Getting Red Color
            string redID = ColorUtility.ToHtmlStringRGB(Color.red);

            //Burning (1) by retrieving from Firebird (salt enemies)
            ExtraPassiveAbility_Wearable_SMS iBleedFire = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            iBleedFire._extraPassiveAbility = Passives.GetCustomPassive("");

            //Cold-Blooded
            ExtraPassiveAbility_Wearable_SMS butIllNeverBurn = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            butIllNeverBurn._extraPassiveAbility = Passives.GetCustomPassive("");

            PerformEffect_Item rrod = new PerformEffect_Item("BastardNimbus_ID", null, false)
            {
                Item_ID = "BastardNimbus_TW",
                Name = "Bastard Nimbus",
                Flavour = "<color=#" + redID + "O</color>",
                Description = "Gain Burning (1) and Cold-Blooded as passives. (WIP, doesn't do anything. Looks cool tho)",
                IsShopItem = false,
                ShopPrice = 7,
                StartsLocked = false,

                Icon = ResourceLoader.LoadSprite("item_bastard_nimbus"),
            };
            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(rrod.item, new ItemModdedUnlockInfo(rrod.Item_ID, ResourceLoader.LoadSprite("item_bastard_nimbus_locked", null, 32, null), "SorasToybox_Karma_Dreamer_ACH"));
        }
    }
}
