using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.SocialPlatforms;

namespace SorasToybox.Items.Vanilla_Fool_DM_Unlocks
{
    public class Identitypolitik
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS getOvertuned = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            getOvertuned._extraPassiveAbility = Passives.GetCustomPassive("ST_Overtuned_PA");


            PerformEffect_Item bloodyMask = new PerformEffect_Item("ST_Identitypolitik_ID", null, false)
            {
                Item_ID = "Identitypolitik_SW",
                Name = "Identitypolitik",
                Flavour = "\"For all the good that being 'you' does you.\"",
                Description = "Gain Overtuned as a passive.\nOn dealing damage, gain equal Whiplash. Can you take what you dish out?",
                TriggerOn = TriggerCalls.OnDidApplyDamage,
                Conditions = [ScriptableObject.CreateInstance<IdentitypolitikCondition>()],
                EquippedModifiers = [getOvertuned],
                StartsLocked = true,
                ShopPrice = 10,
                Icon = ResourceLoader.LoadSprite("item_identitypolitik"),
                IsShopItem = true,
                OnUnlockUsesTHE = false,
            };


            //unlock this
            string achievementID = "SorasToybox_Leviat_Antagonist_ACH";
            string unlockID = "SorasToybox_Leviat_Antagonist_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(bloodyMask.item, new ItemModdedUnlockInfo(bloodyMask.Item_ID, ResourceLoader.LoadSprite("item_identitypolitik_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, bloodyMask.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [bloodyMask.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Leviat", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements("Identitypolitik", "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Leviat", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Leviat_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Identitypolitik.");
            }
        }
    }
}
