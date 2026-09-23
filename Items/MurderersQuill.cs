using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class MurderersQuill
    {
        public static void Add()
        {
            RemovePassiveEffect noConfusion = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            noConfusion.m_PassiveID = Passives.Confusion.m_PassiveID;

            RemovePassiveEffect noObscured = ScriptableObject.CreateInstance<RemovePassiveEffect>();
            noObscured.m_PassiveID = Passives.Obscure.m_PassiveID;

            StatusEffect_Apply_Effect doParanoia = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            doParanoia._Status = StatusField.GetCustomStatusEffect("Paranoia_ID");

            DamageEffect indirecty = ScriptableObject.CreateInstance<DamageEffect>();
            indirecty._indirect = true;

            AttackVisualsSO slashficVis = ScriptableObject.CreateInstance<AttackVisualsSO>();
            if (LoadedAssetsHandler.GetCharacterAbility("ImpertinenceCowboy") != null)
            {
                slashficVis = LoadedAssetsHandler.GetCharacterAbility("ImpertinenceCowboy").visuals;
            }
            else
            {
                slashficVis = Visuals.Scream;
            }

            Ability slashfic = new Ability("ST_Slashfic_A")
            {
                Name = "Slasherfic",
                Description = "Remove Confusion and Obscured from the Opposing enemy.\nIf removal was successful, apply 3 Paranoia to the Opposing enemy.\nDeal 6 indirect damage to the Opposing enemy.",
                Cost = [Pigments.Red, Pigments.Red, Pigments.Blue],
                Visuals = slashficVis,
                AnimationTarget = Targeting.Slot_Front,
                AbilitySprite = ResourceLoader.LoadSprite("quill_slasherfic.png"),
                Effects =
                [
                    Effects.GenerateEffect(noConfusion, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(doParanoia, 3, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(noObscured, 1, Targeting.Slot_Front),
                    Effects.GenerateEffect(doParanoia, 3, Targeting.Slot_Front, Effects.CheckPreviousEffectCondition(true, 1)),
                    Effects.GenerateEffect(indirecty, 6, Targeting.Slot_Front),
                ],
            };
            slashfic.AddIntentsToTarget(Targeting.Slot_Front, ["DNA", "Status_Paranoia", nameof(IntentType_GameIDs.Damage_3_6)]);

            ExtraAbility_Wearable_SMS slashficWearable = ScriptableObject.CreateInstance<ExtraAbility_Wearable_SMS>();
            slashficWearable._extraAbility = slashfic.GenerateCharacterAbility();

            PerformEffect_Item murderersQuill = new PerformEffect_Item("ST_Quill_ID")
            {
                Item_ID = "MurderersQuill_TW",
                Name = "Murderer's Quill",
                Flavour = "\"Mightier than the sword? Small comfort to the victims.\"",
                Description = "Gain \"Slasherfic\", a terrifying ability that scares the hell out of your opponent.",
                EquippedModifiers = [slashficWearable],
                TriggerOn = TriggerCalls.Count,
                ShopPrice = 22,
                IsShopItem = false,
                OnUnlockUsesTHE = true,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_quill"),
            };

            //unlock this
            string achievementID = "SorasToybox_Whhvay_Inevitable_ACH";
            string unlockID = "SorasToybox_Whhvay_Inevitable_Unlock";

            ItemUtils.AddItemToTreasureStatsCategoryAndGamePool(murderersQuill.item, new ItemModdedUnlockInfo(murderersQuill.Item_ID, ResourceLoader.LoadSprite("item_quill_locked", null, 32, null), achievementID));

            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, murderersQuill.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [murderersQuill.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("March_BOSS", ResourceLoader.LoadSprite("MarchPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(murderersQuill.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_March_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("InevitableTitleLabel", "The Inevitable");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Murderer's Quill.");
            }
        }
    }
}
