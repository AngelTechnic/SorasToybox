using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class OtherworldlyReefer
    {
        public static void Add()
        {
            ChangeToRandomHealthColorEffect colorsOutOfSpace = ScriptableObject.CreateInstance<ChangeToRandomHealthColorEffect>();
            colorsOutOfSpace._healthColors = [
                LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"),
                LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"),
                LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"),
                LoadedDBsHandler.PigmentDB.GetPigment("White")
            ];

            ExtraPassiveAbility_Wearable_SMS expelWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            expelWearable._extraPassiveAbility = Passives.GetCustomPassive("Expel_PA");

            PerformEffect_Item otherworldlyReefer = new("ST_Reefer_ID")
            {
                Item_ID = "OtherworldlyReefer_SW",
                Name = "Otherworldly Reefer",
                Flavour = "\"Dance for us, monkey.\"",
                Description = "Gain Expel as a passive.\nAt the start of each turn, turn this party member's health color to a random esoteric color.",
                Icon = ResourceLoader.LoadSprite("item_reefer"),
                IsShopItem = true,
                ShopPrice = 3,
                StartsLocked = true,
                OnUnlockUsesTHE = true,
                TriggerOn = TriggerCalls.OnTurnStart,
                Effects =
                [
                    Effects.GenerateEffect(colorsOutOfSpace, 1, Targeting.Slot_SelfSlot),
                ],
                EquippedModifiers = [expelWearable],
            };

            //Unlock this
            string achievementID = "SorasToybox_Yhaele_Antagonist_ACH";
            string unlockID = "SorasToybox_Yhaele_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(otherworldlyReefer.item, new ItemModdedUnlockInfo(otherworldlyReefer.Item_ID, ResourceLoader.LoadSprite("item_reefer_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, otherworldlyReefer.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [otherworldlyReefer.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("Yhaele_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(otherworldlyReefer.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Yhaele", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Yhaele_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Otherworldly Reefer.");
            }
        }
    }
}
