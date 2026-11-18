using BrutalAPI.Items;
using SorasToybox.CustomStatuses;
using System;
using System.Collections.Generic;
using System.Text;
using Utility.SerializableCollection;

namespace SorasToybox.Items
{
    public class DickheadInACan
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS vandalWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            vandalWearable._extraPassiveAbility = Passives.GetCustomPassive("Vandal_PA");

            ExtraPassiveAbility_Wearable_SMS chilly2Wearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            chilly2Wearable._extraPassiveAbility = CustomPassives.CustomPassive.ChillyGenerator(2);

            PerformEffect_Item dickhead = new("ST_Dickhead_ID")
            {
                Name = "Dickhead In A Can",
                Item_ID = "DickheadInACan_SW",
                Flavour = "\"Deserved. Probably.\"",
                Description = "Gain Vandal and Chilly (2) as passives.",
                StartsLocked = true,
                IsShopItem = true,
                ShopPrice = 7,
                Icon = ResourceLoader.LoadSprite("item_dickhead"),
                TriggerOn = TriggerCalls.Count,
                EquippedModifiers = [vandalWearable, chilly2Wearable],
                OnUnlockUsesTHE = true,
            };

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(dickhead.item, new ItemModdedUnlockInfo(dickhead.Item_ID, ResourceLoader.LoadSprite("item_dickhead_locked", null, 32, null), "SorasToybox_Miniboss_Crashout_ACH"));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement("SorasToybox_Miniboss_Crashout_ACH", dickhead.Item_ID);

            UnlockableModData unlockData = new UnlockableModData("MinibossCrashout")
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = "SorasToybox_Miniboss_Crashout_ACH",
                hasItemUnlock = true,
                items = [dickhead.Item_ID],
            };

            EnemyDeathUnlockCheck deathUnlock = ScriptableObject.CreateInstance<EnemyDeathUnlockCheck>();
            deathUnlock.usesSimpleDeathData = true;
            deathUnlock.enemyID = "Crashout_EN";
            deathUnlock.simpleDeathData = unlockData;
            deathUnlock.specialDeathData = new SerializableDictionary<string, UnlockableModData>();

            ModdedAchievements achievement = new ModdedAchievements("Your Account Has Been Terminated", "Call out a Crashout.", ResourceLoader.LoadSprite("Ach_Comedy_Crashout", null, 32, null), "SorasToybox_Miniboss_Crashout_ACH");
            achievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.ComediesTitleLabel);

            Unlocks.AddUnlock_EnemyDeath(deathUnlock);
        }
    }
}
