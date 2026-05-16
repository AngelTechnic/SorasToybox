using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;

namespace SorasToybox.Items
{
    public class SentientArcanite
    {
        public static void Add()
        {
            //Creating function that calls for Overclock
            StatusEffect_Apply_Effect overclockMe = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            overclockMe._Status = StatusField.GetCustomStatusEffect("Overclock_ID");

            //defining initial overclock stack amount here
            int overclockStacks = 2;

            //Item setup
            PerformEffect_Item gayCrystal = new PerformEffect_Item("SentientArcanite_ID", null, false)
            {
                Item_ID = "SentientArcanite_TW",
                Name = "Sentient Arcanite",
                Flavour = "\"It wants to go home.\"",
                Description = "On combat start, gain 2 Overclock.\nIt resonates in the presence of certain souls...?",
                IsShopItem = false,
                ShopPrice = 7,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = ResourceLoader.LoadSprite("item_sentient_arcanite"),
                OnUnlockUsesTHE = true,
                //When does this item do its stuff?
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects = 
                [
                    //Apply Overclock, use overclockStacks as amount, target Self.
                    Effects.GenerateEffect(overclockMe, overclockStacks, Targeting.Slot_SelfSlot),
                ]
            };
            //adds to treasure pool, and other stuff related to unlocks (but it's unlocked by default so don't worry about it)
            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(gayCrystal.item, new ItemModdedUnlockInfo(gayCrystal.Item_ID, ResourceLoader.LoadSprite("item_sentient_arcanite", null, 32, null), "SorasToybox_Misc_SentientArcanite_ACH"));

        }
    }
}
