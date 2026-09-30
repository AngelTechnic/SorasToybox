using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class LocksmithPassiveAbility : BasePassiveAbilitySO
    {
        string note = "based on dominance from greasy fools";
        public override bool IsPassiveImmediate
        {
            get
            {
                return true;
            }
        }

        // Token: 0x17000012 RID: 18
        // (get) Token: 0x060000ED RID: 237 RVA: 0x000065D8 File Offset: 0x000047D8
        public override bool DoesPassiveTrigger
        {
            get
            {
                return true;
            }
        }

        // Token: 0x060000EE RID: 238 RVA: 0x000065EB File Offset: 0x000047EB
        public override void OnPassiveConnected(IUnit unit)
        {
        }


        public override void OnPassiveDisconnected(IUnit unit)
        {
        }


        public override void TriggerPassive(object sender, object args)
        {
            IUnit unit = sender as IUnit;
            DamageDealtValueChangeException ex = args as DamageDealtValueChangeException;
            bool flag = ex != null && (ex.damagedUnit.ContainsPassiveAbility(Passives.Constricting.m_PassiveID) || ex.casterUnit.ContainsFieldEffect(StatusField.Constricted.FieldID));
            if (flag)
            {
                ex.AddModifier(new PercentageValueModifier(true, 50, true));
                
            }
        }
    }
    public class Latchknife
    {

        public static void Add()
        {
            LocksmithPassiveAbility locksmith = ScriptableObject.CreateInstance<LocksmithPassiveAbility>();
            locksmith.name = "Locksmith_PA";
            locksmith._passiveName = "Locksmith";
            locksmith.passiveIcon = ResourceLoader.LoadSprite("passive_locksmith.png", new Vector2?(new Vector2(0.5f, 0f)), 32, null);
            locksmith.m_PassiveID = "Locksmith_ID";
            locksmith._triggerOn = new TriggerCalls[]
            {
                TriggerCalls.OnWillApplyDamage
            };
            locksmith._enemyDescription = "This enemy feels an immense hatred for specific movement-impairing effects and deals 50% extra damage to Constricting targets, or when under the effects of Constricted.";
            locksmith._characterDescription = "This party member feels an immense hatred for specific movement-impairing effects and deals 50% extra damage to Constricting targets, or when under the effects of Constricted.";
            locksmith.doesPassiveTriggerInformationPanel = false;
            Passives.AddCustomPassiveToPool("Locksmith_PA", "Locksmith", locksmith);
            GlossaryPassives passiveInfo = new GlossaryPassives("Locksmith", "This character feels an immense hatred for specific movement-impairing effects and deals 50% extra damage to Constricting targets, or when under the effects of Constricted.", ResourceLoader.LoadSprite("passive_locksmith.png", new Vector2?(new Vector2(0.5f, 0f)), 32, null));
            LoadedDBsHandler.GlossaryDB.AddNewPassive(passiveInfo);

            ExtraPassiveAbility_Wearable_SMS locksmithWearable = ScriptableObject.CreateInstance<ExtraPassiveAbility_Wearable_SMS>();
            locksmithWearable._extraPassiveAbility = locksmith;

            PerformEffect_Item notKeyblade = new PerformEffect_Item("ST_Latchknife_ID")
            {
                Item_ID = "Latchknife_SW",
                Name = "Latchknife",
                Flavour = "\"Did someone mention the D- er, Portal of Penumbrae?\"",
                Description = "Grants this party member the Locksmith passive.",
                TriggerOn = TriggerCalls.Count,
                EquippedModifiers = [locksmithWearable],
                IsShopItem = true,
                ShopPrice = 7,
                StartsLocked = true,
                Icon = ResourceLoader.LoadSprite("item_latchknife"),
                OnUnlockUsesTHE = true,

            };

            notKeyblade.item._ItemTypeIDs =
            [
                ItemType_GameIDs.Knife.ToString(),
            ];

            ItemUtils.AddItemToShopStatsCategoryAndGamePool(notKeyblade.item, new ItemModdedUnlockInfo(notKeyblade.Item_ID, ResourceLoader.LoadSprite("item_latchknife_locked", null, 32, null), "SorasToybox_Karma_Abstraction_ACH"));

            //unlock this
            string achievementID = "SorasToybox_Whhvay_Abstraction_ACH";
            string unlockID = "SorasToybox_Whhvay_Abstraction_Unlock";



            BrutalAPI.BackwardsUnlockCompatibility.TryLockItemBehindAchievement(achievementID, notKeyblade.Item_ID);

            UnlockableModData unlockData = new UnlockableModData(unlockID)
            {
                hasModdedAchievementUnlock = true,
                moddedAchievementID = achievementID,
                hasItemUnlock = true,
                items = [notKeyblade.Item_ID],
            };

            FinalBossCharUnlockCheck unlockCheck = Unlocks.GetOrCreateUnlock_CustomFinalBoss("DoulaBoss", ResourceLoader.LoadSprite("DoulaPearl", null, 32, null));
            unlockCheck.AddUnlockData("Whhvay_CH", unlockData);

            ModdedAchievements unlockAchievement = new ModdedAchievements(notKeyblade.item._itemName, "Unlocked a new item.", ResourceLoader.LoadSprite("Ach_Doula_Whhvay", null, 32, null), achievementID);
            unlockAchievement.AddNewAchievementToCUSTOMCategory("AbstractionTitleLabel", "The Abstraction");

            if (SorasToybox.extradebug.Value)
            {
                Debug.Log("Added the Latchknife.");
            }
        }

    }
}
