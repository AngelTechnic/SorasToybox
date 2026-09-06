using BrutalAPI.Items;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class StoneWateringCan
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect getEdema = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getEdema._Status = StatusField.GetCustomStatusEffect("Edema_ID");

            ChangeToRandomHealthColorEffect notGrey = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            notGrey._healthColors = [Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple];

            SpecificOpponentsByHealthColorIDTargeting greyEnemies = ScriptableObject.CreateInstance<SpecificOpponentsByHealthColorIDTargeting>();
            greyEnemies._colorID = "Grey";
            greyEnemies.getAllUnitSelfSlots = false;
            greyEnemies.targetUnitAllySlots = true;
            greyEnemies.slotOffsets = [0];

            DoublePerformEffect_Item wateringCan = new DoublePerformEffect_Item("ST_StoneWateringCan_ID")
            {
                Name = "Stone Watering Can",
                Item_ID = "StoneWateringCan_SW",
                Flavour = "\"And now the flowers will grow!\"",
                Description = "Inflict 1 Edema to all enemies at the start of each turn.\nOn combat start, turn all Grey enemies into a different color.",
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(notGrey, 1, greyEnemies),
                ],
                SecondaryTriggerOn = [TriggerCalls.OnTurnStart],
                SecondaryEffects =
                [
                    Effects.GenerateEffect(getEdema, 1, Targeting.Unit_AllOpponents),
                ],
                OnUnlockUsesTHE = true,
                ShopPrice = 4,
                IsShopItem = true,
                Icon = ResourceLoader.LoadSprite("item_stonewateringcan.png", null, 32, null),
            };
            ItemUtils.AddItemToShopStatsCategoryAndGamePool(wateringCan.item, new ItemModdedUnlockInfo(wateringCan.Item_ID, ResourceLoader.LoadSprite("item_stonewateringcan_locked", null, 32, null), "DesibonBoss_ACH"));
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Stone Watering Can.");
            }
        }
    }
}
