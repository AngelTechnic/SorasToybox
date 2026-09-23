using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using Yarn.Analysis;
using static SorasToybox.Encounters.Garden.H.Jumble;

namespace SorasToybox.Items
{
    public class MyFace
    {
        public static void Add()
        {
            ExtraPassiveAbility_Wearable_SMS hostileWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            hostileWearable._extraPassiveAbility = Passives.GetCustomPassive("ST_Hostile_PA");

            HealEffect heal = ScriptableObject.CreateInstance<HealEffect>();
            heal._directHeal = true;

            DamageEffect damage = ScriptableObject.CreateInstance<DamageEffect>();

            PerformEffect_Item myFace = new PerformEffect_Item("ST_MyFace_ID")
            {
                Item_ID = "MyFace_TW",
                Name = "My Face",
                Flavour = "\"So-called \'        \'\"",
                Description = "This party member is now Hostile.\nAt the start of each turn, take 1 damage and 66% chance to heal 1 health.",
                Icon = ResourceLoader.LoadSprite("item_myface"),
                TriggerOn = TriggerCalls.OnTurnStart,
                EquippedModifiers = [hostileWearable],
                OnUnlockUsesTHE = false,
                IsShopItem = false,
                ShopPrice = 0,
                StartsLocked = true,
                Effects =
                [
                    Effects.GenerateEffect(damage, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(heal, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(66)),
                ],

            };

            myFace.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Face.ToString()
            ];


            //unlock this
            string achievementID = "SorasToybox_Arnold_Antagonist_ACH";
            string unlockID = "SorasToybox_Arnold_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(myFace.item, new ItemModdedUnlockInfo(myFace.Item_ID, ResourceLoader.LoadSprite("item_myface_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, myFace.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [myFace.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Arnold", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(myFace.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Arnold", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Arnold_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added My Face.");
            }

        }
    }
}
