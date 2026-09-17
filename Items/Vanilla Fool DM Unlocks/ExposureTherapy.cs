using BrutalAPI.Items;
using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items.Vanilla_Fool_DM_Unlocks
{
    public class ExposureTherapy
    {
        public static void Add()
        {
            FieldEffect_Apply_Effect getCrosshairs = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
            getCrosshairs._Field = StatusField.GetCustomFieldEffect("Crosshairs_ID");

            TargetPerformEffectViaSubaction exposureSubAct = ScriptableObject.CreateInstance<TargetPerformEffectViaSubaction>();
            exposureSubAct.effects =
                [
                    Effects.GenerateEffect(getCrosshairs, 8, Targeting.Slot_SelfAll),
                ];



            PerformEffect_Item exposureTherapy = new PerformEffect_Item("ST_ExposureTherapy_ID")
            {
                Name = "Exposure Therapy",
                Flavour = "\"They're out to get you.\"",
                Description = "On killing an enemy, make the highest health remaining enemies apply 8 Crosshairs to all slots they occupy.",
                Item_ID = "ExposureTherapy_TW",
                TriggerOn = TriggerCalls.OnKill,
                Effects =
                [
                    Effects.GenerateEffect(exposureSubAct, 1, Targeting.Spec_Unit_AllOpponents_Strongest),
                ],
                Icon = ResourceLoader.LoadSprite("item_exposure", null, 32, null),
                IsShopItem = false,
                ShopPrice = 6,
                StartsLocked = true,
                OnUnlockUsesTHE = false,
            };
            exposureTherapy.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Magic.ToString(),
            ];

            //Unlock this
            string achievementID = "SorasToybox_Fennec_Antagonist_ACH";
            string unlockID = "SorasToybox_Fennec_Antagonist_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(exposureTherapy.item, new ItemModdedUnlockInfo(exposureTherapy.Item_ID, ResourceLoader.LoadSprite("item_exposure_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, exposureTherapy.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [exposureTherapy.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("Deathmatch_BOSS", ResourceLoader.LoadSprite("DeathmatchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Fennec", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(exposureTherapy.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Deathmatch_Fennec", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AntagonistTitleLabel", "The Antagonist");

            LoadedAssetsHandler.GetCharacter("Fennec_CH").m_BossAchData.Add(new("Deathmatch_BOSS", achievementID));

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added Exposure Therapy.");
            }
        }
    }
}