using BrutalAPI.Items;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class Everpresence
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS revelatorWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            revelatorWearable._extraPassiveAbility = Passives.GetCustomPassive("Revelator_PA");

            ExtraPassiveAbility_Wearable_SMS witheringWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            witheringWearable._extraPassiveAbility = Passives.Withering;

            PerformEffect_Item wateringCan = new PerformEffect_Item("ST_Everpresence_ID")
            {
                Name = "Everpresence",
                Item_ID = "Everpresence_TW",
                Flavour = "\"Feel it all around...\"",
                Description = "Gain Revelator and Withering as passives.",
                TriggerOn = TriggerCalls.Count,
                EquippedModifiers = [revelatorWearable, witheringWearable],
                OnUnlockUsesTHE = false,
                ShopPrice = 4,
                IsShopItem = false,
                Icon = ResourceLoader.LoadSprite("item_everpresence.png", null, 32, null),
            };
            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(wateringCan.item, new ItemModdedUnlockInfo(wateringCan.Item_ID, ResourceLoader.LoadSprite("item_everpresence_locked", null, 32, null), "SorasToybox_Strangers_Heliotrope_ACH"));
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added Everpresence.");
            }
        }
    }
}