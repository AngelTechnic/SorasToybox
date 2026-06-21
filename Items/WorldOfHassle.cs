using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using BrutalAPI.Items;
using UnityEngine;
using SorasToybox.CustomEffects;
using JetBrains.Annotations;

namespace SorasToybox.Items
{
    public class WorldOfHassle
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS getErasure = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getErasure._extraPassiveAbility = Passives.GetCustomPassive("Erasure_PA");

            HealEffect Heal = ScriptableObject.CreateInstance<HealEffect>();
            Heal._directHeal = true;
            Heal.usePreviousExitValue = true;
            Heal._onlyIfHasHealthOver0 = true;

            PerformEffectAddingResult_Item mickeyMilan = new PerformEffectAddingResult_Item("ST_WorldOfHassle_ID", null, false)
            {
                Item_ID = "WorldOfHassle_SW",
                Name = "World of Hassle",
                Flavour = "\"Step back into the limelight.\"",
                Description = "Gain Erasure as a passive.\nWhenever this party member takes damage, heal the Left and Right party members by an equal amount.\nHealing assumes the grid wraps around.",
                TriggerOn = TriggerCalls.OnDamaged,
                Effects = [Effects.GenerateEffect(Heal, 1, Targeting.GenerateSlotTarget([-4, -1, 0, 1, 4], true))],
                StartsLocked = true,
                ShopPrice = 9,
                Icon = ResourceLoader.LoadSprite("item_worldofhassle"),
            };

            //Unlock this
            string achievementID = "SorasToybox_Mercurie_Forgotten_ACH";
            string unlockID = "SorasToybox_Mercurie_Forgotten_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(mickeyMilan.item, new ItemModdedUnlockInfo(mickeyMilan.Item_ID, ResourceLoader.LoadSprite("item_worldofhassle_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, mickeyMilan.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [mickeyMilan.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Mercurie_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("World Of Hassle", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Nobody_Mercurie", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
        }
    }
}
