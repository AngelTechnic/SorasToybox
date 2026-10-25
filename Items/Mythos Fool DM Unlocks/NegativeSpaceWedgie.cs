using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI.Items;
using SorasToybox.CustomEffects;

namespace SorasToybox.Items
{
    public class NegativeSpaceWedgie
    {
        public static void Add()
        {
            UnboundedHealEffect unboundHeal = ScriptableObject.CreateInstance<UnboundedHealEffect>();
            unboundHeal._repeatChance = 75;
            unboundHeal._directHeal = true;
            unboundHeal._cycles = 16;

            GetRandomTargetFromSet randomPartyMember = GetRandomTargetFromSet.Create(Targeting.Unit_AllAllies);

            TargetPerformEffectViaSubaction unboundHealSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            unboundHealSubAct.effects =
                [
                    Effects.GenerateEffect(unboundHeal, 1, Targeting.Slot_SelfSlot),
                ];

            UnboundedDamageEffect unboundDamage = ScriptableObject.CreateInstance<UnboundedDamageEffect>();
            unboundDamage._repeatChance = 66;
            unboundDamage._cycles = 16;

            GetRandomTargetFromSet randomEnemy = GetRandomTargetFromSet.Create(Targeting.Unit_AllOpponents);

            TargetPerformEffectViaSubaction unboundDamageSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            unboundDamageSubAct.effects =
                [
                    Effects.GenerateEffect(unboundDamage, 1, Targeting.Slot_SelfSlot),
                ];

            DoublePerformEffect_Item negativeSpaceWedige = new("ST_NegativeSpace_ID")
            {
                Item_ID = "NegativeSpaceWedgie_TW",
                Name = "Negative Space Wedgie",
                Flavour = "\"Well, you got me. By all accounts, it doesn't make sense.\"",
                Description = "On dealing damage, 44% chance a random party member heals at least 1 health.\nOn being healed, 66% chance a random enemy takes at least 1 damage.",
                IsShopItem = false,
                ShopPrice = 888,
                DoesPopUpInfo = false,
                TriggerOn = TriggerCalls.OnDidApplyDamage,
                Effects =
                [
                    Effects.GenerateEffect(unboundHealSubAct, 1, randomPartyMember, Effects.ChanceCondition(44)),
                ],
                SecondaryDoesPopUpInfo = false,
                SecondaryTriggerOn = [TriggerCalls.OnBeingHealed],
                OnUnlockUsesTHE = true,
                Icon = ResourceLoader.LoadSprite("item_negativespace"),
                SecondaryEffects =
                [
                    Effects.GenerateEffect(unboundDamageSubAct, 1, randomEnemy, Effects.ChanceCondition(66)),
                ],
            };

            //Unlock this
            string achievementID = "SorasToybox_992_Antagonist_ACH";
            string unlockID = "SorasToybox_992_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(negativeSpaceWedige.item, new ItemModdedUnlockInfo(negativeSpaceWedige.Item_ID, ResourceLoader.LoadSprite("item_negativespace_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, negativeSpaceWedige.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [negativeSpaceWedige.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData(LoadedAssetsHandler.GetCharacter("992_CH").entityID, unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(negativeSpaceWedige.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_992", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("992_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("[ITEMS] Added the Negative Space Wedgie.");
            }

        }
    }
}
