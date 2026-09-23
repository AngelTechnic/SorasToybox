using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using Yarn.Analysis;

namespace SorasToybox.Items
{
    public class ExtrudedPolygon
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect getPotential = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getPotential._Status = StatusField.GetCustomStatusEffect("Potential_ID");

            PerformEffect_Item polygon = new PerformEffect_Item("ST_Polygon_ID")
            {
                Item_ID = "ExtrudedPolygon_SW",
                Name = "Extruded Polygon",
                Flavour = "\"A primitive weapon for a supposedly civilized program.\"",
                Description = "On dealing damage, gain 1 Potential.",
                TriggerOn = TriggerCalls.OnDidApplyDamage,
                Effects =
                [
                    Effects.GenerateEffect(getPotential, 1, Targeting.Slot_SelfSlot),
                ],
                IsShopItem = true,
                ShopPrice = 9,
                OnUnlockUsesTHE = true,
                Icon = ResourceLoader.LoadSprite("item_polygon", null, 32, null),
                DoesPopUpInfo = false,
            };
            polygon.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Knife.ToString()
            ];

            //Unlock this
            string achievementID = "SorasToybox_Whhvay_Boundary_ACH";
            string unlockID = "SorasToybox_Whhvay_Boundary_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(polygon.item, new ItemModdedUnlockInfo(polygon.Item_ID, ResourceLoader.LoadSprite("item_polygon_locked", null, 32, null), achievementID));


            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, polygon.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [polygon.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Katalixi_BOSS", ResourceLoader.LoadSprite("KatalixiPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(polygon.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Katalixi_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("BoundaryTitleLabel", "The Boundary");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Extruded Polygon.");
            }
        }
    }
}
