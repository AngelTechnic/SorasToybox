using BrutalAPI.Items;
using SorasToybox.CustomOther;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items.Vanilla_Fool_DM_Unlocks
{
    public class StretchMarks
    {
        public static void Add()
        {
            PerformEffect_Item stretchMarks = new PerformEffect_Item("ST_StretchMarks_ID", null, false)
            {
                Item_ID = "StretchMarks_TW",
                Name = "Stretch Marks",
                Flavour = "\"Should it hurt like that when I sneeze?\"",
                Description = "On healing something, increase their maximum health by 3 and inflict 1 Scar to them.",
                IsShopItem = false,
                ShopPrice = 6,
                DoesPopUpInfo = true,
                StartsLocked = false,
                Icon = ResourceLoader.LoadSprite("item_stretchmarks"), //item goes here
                TriggerOn = TriggerCalls.OnWillApplyHeal,
                Conditions = [ScriptableObject.CreateInstance<StretchMarksCondition>()],
                OnUnlockUsesTHE = false,
            };

            //unlock this
            string achievementID = "SorasToybox_Hans_Antagonist_ACH";
            string unlockID = "SorasToybox_Hans_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(stretchMarks.item, new ItemModdedUnlockInfo(stretchMarks.Item_ID, ResourceLoader.LoadSprite("item_stretchmarks_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, stretchMarks.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [stretchMarks.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Hans", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Stretch Marks", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Hans", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Hans_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Stretch Marks.");
            }
        }
    }
}
