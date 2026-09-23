using BrutalAPI.Items;
using SorasToybox.Items.Vanilla_Fool_DM_Unlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class Diagnopsis
    {
        public static void Add()
        {
            ConsumeItemEffect yummy = ScriptableObject.CreateInstance<ConsumeItemEffect>();

            CopyAndSpawnCustomCharacterAnywhereEffect summonPsidoc = ScriptableObject.CreateInstance<CopyAndSpawnCustomCharacterAnywhereEffect>();
            summonPsidoc._characterCopy = "Psidoc_CH";
            summonPsidoc._permanentSpawn = true;
            summonPsidoc._rank = 0;
            summonPsidoc._extraModifiers = [];

            PerformEffect_Item diagnopsis = new PerformEffect_Item("ST_Diagnopsis_ID", null, false)
            {
                Item_ID = "Diagnopsis_SW",
                Name = "Diagnopsis",
                Flavour = "\"Without a mind to think and writhe.\"",
                Description = "At the start of combat, summon an Aidek Psidoc to assist you in combat. Destroyed on activation.",
                TriggerOn = TriggerCalls.OnCombatStart,
                IsShopItem = true,
                ShopPrice = 8,
                OnUnlockUsesTHE = true,
                ConsumeOnTrigger = TriggerCalls.Count,
                ConsumeOnUse = true,
                Icon = ResourceLoader.LoadSprite("item_diagnopsis", null, 32, null),
                DoesPopUpInfo = false,
                Effects =
                [
                    Effects.GenerateEffect(summonPsidoc, 1, Targeting.Slot_SelfSlot),
                ],
            };

            diagnopsis.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Knife.ToString()
            ];

            //unlock this
            string achievementID = "SorasToybox_Whhvay_Divine_ACH";
            string unlockID = "SorasToybox_Whhvay_Divine_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(diagnopsis.item, new ItemModdedUnlockInfo(diagnopsis.Item_ID, ResourceLoader.LoadSprite("item_diagnopsis_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, diagnopsis.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [diagnopsis.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetUnlock_HeavenFinalBoss();
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(diagnopsis.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Heaven_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.DivineTitleLabel);

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Diagnopsis.");
            }
        }
    }
}
