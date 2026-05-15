using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;

namespace SorasToybox.Items
{
    public class Setset
    {
        public static void Add()
        {

            ExtraPassiveAbility_Wearable_SMS wearablePassive = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            wearablePassive._extraPassiveAbility = Passives.GetCustomPassive("Karmic_PA");
            PerformEffect_Item sufferingBuildsCharacter = new PerformEffect_Item("Setset_ID", null, false)
            {
                Item_ID = "Setset_TW",
                Name = "Setset",
                Flavour = "\"I will outlive these bastards.\"",
                Description = "Gain Karmic as a passive.\nThe rest comes later.",
                IsShopItem = false,
                ShopPrice = 7,
                StartsLocked = false,

                Icon = ResourceLoader.LoadSprite("item_setset"),

                EquippedModifiers = [wearablePassive],
            };
            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(sufferingBuildsCharacter.item, new ItemModdedUnlockInfo(sufferingBuildsCharacter.Item_ID, ResourceLoader.LoadSprite("item_setset_locked", null, 32, null), "SorasToybox_Karma_Antagonist_ACH"));
        } 
    }
}
