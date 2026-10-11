using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using SorasToybox.CustomPassives;

namespace SorasToybox.Items
{

    public class Latchknife
    {

        public static void Add()
        {


            ExtraPassiveAbility_Wearable_SMS locksmithWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            locksmithWearable._extraPassiveAbility = Passives.GetCustomPassive("Locksmith_PA");

            PerformEffect_Item notKeyblade = new PerformEffect_Item("ST_Latchknife_ID")
            {
                Item_ID = "Latchknife_SW",
                Name = "Latchknife",
                Flavour = "\"Did someone mention the D- er, Portal of Penumbrae?\"",
                Description = "Grants this party member the Locksmith passive.",
                TriggerOn = TriggerCalls.Count,
                EquippedModifiers = [locksmithWearable],
                IsShopItem = true,
                ShopPrice = 7,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_latchknife"),
                OnUnlockUsesTHE = true,

            };

            notKeyblade.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Knife.ToString(),
            ];

            //unlock this
            string achievementID = "SorasToybox_Whhvay_Abstraction_ACH";
            string unlockID = "SorasToybox_Whhvay_Abstraction_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(notKeyblade.item, new ItemModdedUnlockInfo(notKeyblade.Item_ID, ResourceLoader.LoadSprite("item_latchknife_locked", null, 32, null), achievementID));


            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, notKeyblade.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [notKeyblade.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(notKeyblade.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Doula_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Latchknife.");
            }
        }

    }
}
