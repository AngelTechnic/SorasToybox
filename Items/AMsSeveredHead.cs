using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;

namespace SorasToybox.Items
{
    public class AMsSeveredHead
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect getScars = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getScars._Status = StatusField.Scars;

            ExtraPassiveAbility_Wearable_SMS dissoluteWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            dissoluteWearable._extraPassiveAbility = Passives.GetCustomPassive("Dissolute_PA");

            PerformEffect_Item amsServeredHead = new PerformEffect_Item("ST_AMsSeveredHead_ID", null, false)
            {
                Name = "Extinction of Enmity",
                Item_ID = "AMsSeveredHead_TW",
                Flavour = "\"Let me tell you how much I've come to hate you...\"",
                Description = "This party member is now Dissolute.\nOn using wrong pigment to perform an ability, gain 1 Scar.",
                IsShopItem = false,
                ShopPrice = 47,
                StartsLocked = true,
                EquippedModifiers = [dissoluteWearable], 
                Icon = ResourceLoader.LoadSprite("item_amsseveredhead"),
                OnUnlockUsesTHE = true,
                TriggerOn = TriggerCalls.OnWillReceiveCostDamage,
                Effects = [
                    Effects.GenerateEffect(getScars, 1, Targeting.Slot_SelfSlot),
                    ]
            };

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(amsServeredHead.item, new ItemModdedUnlockInfo(amsServeredHead.Item_ID, ResourceLoader.LoadSprite("item_amsseveredhead_locked", null, 32, null), "SorasToybox_Deathmatch_Tragedy_ACH"));

            //unlock this
            string AMachievementID = "SorasToybox_Deathmatch_Tragedy_ACH";



            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(AMachievementID, amsServeredHead.Item_ID);

            UnlockableModData unlockData = new UnlockableModData("SorasToybox_Deathmatch_Tragedy_Unlock")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = AMachievementID,
                hasItemUnlock = true,
                items = [amsServeredHead.Item_ID],
            };


            ModdedAchievements unlockAchievement = new ModdedAchievements("Defiant To The End", "Do not give in and admit to the Antagonist.", ResourceLoader.LoadSprite("Ach_Tragedy_Deathmatch", null, 32, null), AMachievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.TragediesTitleLabel);

            Unlocks.AddUnlock_ByID(unlockData);

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Extinction of Enmity.");
            }
        }
    }
}
