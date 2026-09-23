using BepInEx;
using BrutalAPI;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class SelfishHighHeels
    {
        public static void Add()
        {
            UnitStoreData_ModIntSO stilettoTracker = ScriptableObject.CreateInstance<UnitStoreData_ModIntSO>();
            stilettoTracker.m_Text = "Stiletto Length: {0}";
            stilettoTracker._UnitStoreDataID = "SelfishHighHeelsStoredValue";
            stilettoTracker.m_TextColor = new Color32(200, 200, 200, 255);
            stilettoTracker.m_CompareDataToThis = -1;
            stilettoTracker.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("SelfishHighHeelsStoredValue", stilettoTracker);

            CasterStoredValueSetEffect stilettoSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            stilettoSet._valueName = "SelfishHighHeelsStoredValue";

            ExtraVariableForNext_SVEffect stilettoGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            stilettoGet.m_unitStoredDataID = "SelfishHighHeelsStoredValue";

            CasterStoredValueChangeEffect stilettoGoUp = ScriptableObject.CreateInstance<CasterStoredValueChangeEffect>();
            stilettoGoUp.m_unitStoredDataID = "SelfishHighHeelsStoredValue";
            stilettoGoUp._minimumValue = 0;
            stilettoGoUp._exitValueIsChange = false;
            stilettoGoUp._increase = true;
            stilettoGoUp._randomBetweenPrevious = false;
            stilettoGoUp._usePreviousExitValue = false;
            stilettoGoUp._exitValueIsChange = false;

            StatusEffect_Apply_Effect whiplashByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            whiplashByPrevious._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");
            whiplashByPrevious._MultPreviousExitValueForEntry = true;

            DoublePerformEffect_Item selfishHighHeels = new DoublePerformEffect_Item("ST_HighHeels_ID")
            {
                Item_ID = "SelfishHighHeels_SW",
                Name = "Selfish High-Heels",
                Flavour = "\"Honorary Stilettos\"",
                Description = "This party member is now one of the girls.\nWhen performing an ability, inflict 1 Whiplash to all enemy slots. This number increases each time an ability is performed.",
                TriggerOn = TriggerCalls.OnBeforeCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(stilettoSet, 1, Targeting.Slot_SelfSlot),
                ],
                DoesPopUpInfo = false,
                SecondaryTriggerOn = [TriggerCalls.OnAbilityUsed],
                SecondaryDoesPopUpInfo = true,
                SecondaryEffects =
                [
                    Effects.GenerateEffect(stilettoGet, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(whiplashByPrevious, 1, Targeting.Unit_AllOpponentSlots),
                    Effects.GenerateEffect(stilettoGoUp, 1, Targeting.Slot_SelfSlot),
                ],
                StartsLocked = true,
                ShopPrice = 7,
                Icon = ResourceLoader.LoadSprite("item_selfishhighheels"),
                IsShopItem = true,
                OnUnlockUsesTHE = true,
            };

            selfishHighHeels.item._ItemTypeIDs =
            [
                UnitType_GameIDs.FemaleLooking.ToString(),
                ItemType_GameIDs.Knife.ToString(),
            ];

            //Unlock this
            string achievementID = "SorasToybox_Whhvay_Forgotten_ACH";
            string unlockID = "SorasToybox_Whhvay_Forgotten_Unlock";

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(selfishHighHeels.item, new ItemModdedUnlockInfo(selfishHighHeels.Item_ID, ResourceLoader.LoadSprite("item_selfishhighheels_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, selfishHighHeels.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [selfishHighHeels.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Nobody_BOSS", ResourceLoader.LoadSprite("NobodyPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(selfishHighHeels.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Nobody_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("ForgottenTitleLabel", "The Forgotten");
            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Selfish High-Heels.");
            }
        }
    }
}
