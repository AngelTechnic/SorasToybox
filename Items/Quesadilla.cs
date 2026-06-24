using BrutalAPI;
using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class Quesadilla
    {
        public static void Add()
        {
            ExtraLootOptionsEffect anotherQuesadilla = ScriptableObject.CreateInstance<ExtraLootOptionsEffect>();
            anotherQuesadilla._itemName = "SlapdashQuesadilla_SW";

            PercentageEffectorCondition TheChance = ScriptableObject.CreateInstance<PercentageEffectorCondition>();
            TheChance.triggerPercentage = 50;

            ExtraPassiveAbility_Wearable_SMS getGodray = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getGodray._extraPassiveAbility = Passives.GetCustomPassive("ST_Godray_PA");

            ConsumeItemEffect yummers = ScriptableObject.CreateInstance<ConsumeItemEffect>();

            PerformEffect_Item quesadilla = new PerformEffect_Item("ST_Quesadilla_ID", null, false)
            {
                Item_ID = "SlapdashQuesadilla_SW",
                Name = "Slapdash Quesadilla",
                Flavour = "\"Microwaved? Really?\"",
                Description = "Reduces all damage received by 50%.\n35% chance to be consumed on hit.\n50% chance to duplicate on hit.",
                ShopPrice = 5,
                IsShopItem = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_quesadilla"),
                EquippedModifiers = [getGodray],
                TriggerOn = TriggerCalls.OnBeingDamaged,
                Effects =
                [
                    Effects.GenerateEffect(anotherQuesadilla, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(50)),
                    Effects.GenerateEffect(yummers, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(35)),
                ]

            };

            //unlock this
            string achievementID = "SorasToybox_Karma_Witness_ACH";
            string unlockID = "SorasToybox_Karma_Witness_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(quesadilla.item, new ItemModdedUnlockInfo(quesadilla.Item_ID, ResourceLoader.LoadSprite("item_quesadilla_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, quesadilla.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [quesadilla.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_OsmanFinalBoss();
            unlockCheck.AddUnlockData("Karma_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("SlapdashQuesadilla", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Osman_Karma", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.WitnessTitleLabel);

        }
    }
}
