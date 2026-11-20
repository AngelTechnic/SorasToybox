using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class Birdhouse
    {
        public static void Add()
        {
            UnitStoreData_ModIntSO desyncTracker = ScriptableObject.CreateInstance<UnitStoreData_ModIntSO>();
            desyncTracker.m_Text = "Desync: {0}";
            desyncTracker._UnitStoreDataID = "MonsterBirdhouseStoredValue";
            desyncTracker.m_TextColor = new Color32(200, 200, 200, 255);
            desyncTracker.m_CompareDataToThis = -1;
            desyncTracker.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("MonsterBirdhouseStoredValue", desyncTracker);

            CasterStoredValueSetEffect desyncSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            desyncSet._valueName = "MonsterBirdhouseStoredValue";

            ExtraVariableForNext_SVEffect desyncGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            desyncGet.m_unitStoredDataID = "MonsterBirdhouseStoredValue";

            CasterStoredValueChangeEffect desyncGoUp = ScriptableObject.CreateInstance<CasterStoredValueChangeEffect>();
            desyncGoUp.m_unitStoredDataID = "MonsterBirdhouseStoredValue";
            desyncGoUp._minimumValue = 0;
            desyncGoUp._exitValueIsChange = false;
            desyncGoUp._increase = true;
            desyncGoUp._randomBetweenPrevious = false;
            desyncGoUp._usePreviousExitValue = false;
            desyncGoUp._exitValueIsChange = false;


            StatusEffect_Apply_Effect entropyByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            entropyByPrevious._Status = StatusField.GetCustomStatusEffect("Salt_Entropy_ID");
            entropyByPrevious._MultPreviousExitValueForEntry = true;

            StatusEffect_Apply_Effect wheresMyPigment = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            wheresMyPigment._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");

            RefreshAbilityUseEffect refresh = ScriptableObject.CreateInstance<RefreshAbilityUseEffect>();

            Ability reallySlowSlap = new("ST_ReallySlowSlap_A")
            {
                Name = "Really Slow Slap",
                Cost = [Pigments.Yellow],
                Description = "Inflict 1 Whiplash and 1 Entropy to the Opposing enemy.\n25% chance to refresh. If that fails, increase the Entropy applied by this ability.",
                Visuals = Visuals.Slap,
                AnimationTarget = Targeting.Slot_Front,
                Effects = [
                    Effects.GenerateEffect(wheresMyPigment, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(desyncGet, 1, Targeting.Slot_SelfSlot),
                    Effects.GenerateEffect(entropyByPrevious, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(refresh, 1, Targeting.Slot_SelfSlot, Effects.ChanceCondition(25)),
                    Effects.GenerateEffect(desyncGoUp, 1, Targeting.Slot_SelfSlot, Effects.CheckPreviousEffectCondition(false, 1)),
                    ],
                AbilitySprite = ResourceLoader.LoadSprite("monsterbirdhouse_reallyslowslap"),
            };
            reallySlowSlap.AddIntentsToTarget(Targeting.Slot_Front, ["Status_Salt_Entropy", "Status_Whiplash"]);
            reallySlowSlap.AddIntentsToTarget(Targeting.Slot_SelfAll, [nameof(IntentType_GameIDs.Other_Refresh), nameof(IntentType_GameIDs.Misc_State_Stand)]);

            BasicAbilityChange_Wearable_SMS slowSlapWearable = ScriptableObject.CreateInstance<BasicAbilityChange_Wearable_SMS>();
            slowSlapWearable._basicAbility = reallySlowSlap.GenerateCharacterAbility();


            //Construct shenanigans i think
            Connection_PerformEffectPassiveAbility connection_PerformEffectPassiveAbility = Passives.Construct as Connection_PerformEffectPassiveAbility;
            CasterAddRandomExtraAbilityEffect casterAddRandomExtraAbilityEffect = connection_PerformEffectPassiveAbility.connectionEffects[1].effect as CasterAddRandomExtraAbilityEffect;
            casterAddRandomExtraAbilityEffect._slapData = [.. casterAddRandomExtraAbilityEffect._slapData, slowSlapWearable];

            PerformEffect_Item monsterBirdhouse = new("ST_Birdhouse_ID")
            {
                Item_ID = "MonsterBirdhouse_TW",
                IsShopItem = false,
                StartsLocked = true,
                DoesPopUpInfo = false,
                OnUnlockUsesTHE = false,
                Name = "Monster Birdhouse",
                Flavour = "\"Guys I promise I'm not throwing it's just my ping-\"",
                Description = "Replaces Slap with \"Really Slow Slap,\" a strange Entropy-applying ability.",
                Icon = ResourceLoader.LoadSprite("item_monsterbirdhouse"),
                TriggerOn = TriggerCalls.OnCombatStart,
                Effects =
                [
                    Effects.GenerateEffect(desyncSet, 1, Targeting.Slot_SelfSlot),
                ],
                ShopPrice = 12,
                EquippedModifiers = [slowSlapWearable],

            };

            //unlock this
            string achievementID = "SorasToybox_Tragedy_Paradox_ACH";
            string unlockID = "SorasToybox_Tragedy_Paradox_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(monsterBirdhouse.item, new ItemModdedUnlockInfo(monsterBirdhouse.Item_ID, ResourceLoader.LoadSprite("item_monsterbirdhouse_locked", null, 32, null), achievementID));
            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, monsterBirdhouse.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [monsterBirdhouse.Item_ID],
            };

            ModdedAchievements unlockAchievement = new ModdedAchievements("Any Second Now", "Force a Paradox Yinimro to attack ten times in one round.", ResourceLoader.LoadSprite("Ach_Tragedy_Paradox", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToInGameCategory(AchievementCategoryIDs.TragediesTitleLabel);

            Unlocks.AddUnlock_ByID(unlockData);

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Monster Birdhouse.");
            }
        }
    }
}
