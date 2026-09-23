using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{ 
    public class PunchBowl
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS condenseWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            condenseWearable._extraPassiveAbility = Passives.GetCustomPassive("Condense_PA");

            AddPassiveEffect getCondense = ScriptableObject.CreateInstance<AddPassiveEffect>();
            getCondense._passiveToAdd = Passives.GetCustomPassive("Condense_PA");

            PerformEffect_Item punchBowl = new PerformEffect_Item("ST_PunchBowl_ID")
            {
                Item_ID = "PunchBowl_SW",
                Name = "Punch Bowl",
                Flavour = "\"Don't drink the water. They put something in it, to make you forget.\"",
                Description = "Gain Condense as a passive.\nOn combat start, add Condense to all enemies present.",
                Icon = ResourceLoader.LoadSprite("item_punchbowl"),
                TriggerOn = TriggerCalls.OnCombatStart,
                EquippedModifiers = [condenseWearable],
                OnUnlockUsesTHE = true,
                IsShopItem = true,
                ShopPrice = 5,
                StartsLocked = true,
                Effects =
                [
                    Effects.GenerateEffect(getCondense, 1, Targeting.Unit_AllOpponents),
                ],

            };

            punchBowl.item._ItemTypeIDs =
            [
                "FoodID",
                "Drink",
            ];


            //unlock this
            string achievementID = "SorasToybox_Dimitri_Antagonist_ACH";
            string unlockID = "SorasToybox_Dimitri_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(punchBowl.item, new ItemModdedUnlockInfo(punchBowl.Item_ID, ResourceLoader.LoadSprite("item_punchbowl_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, punchBowl.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [punchBowl.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Dimitri", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(punchBowl.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Dimitri", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Dimitri_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Punch Bowl.");
            }

        }
    }
}
