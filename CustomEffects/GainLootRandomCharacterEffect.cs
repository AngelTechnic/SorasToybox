using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomEffects
{
    public class GainLootRandomCharacterEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            CharacterDataBase characterDB = LoadedDBsHandler.CharacterDB;
            if (characterDB == null || characterDB.Equals(null))
            {
                return false;
            }
            List<CharacterSO> randomCharacters = characterDB.GetRandomCharacters(entryVariable);

            foreach (CharacterSO item in randomCharacters)
            {
                int maxHealth = item.GetMaxHealth(0);
                int[] usedAbs = item.GenerateAbilities();
                SpawnedCharacterAddition newCharacter = new SpawnedCharacterAddition(item, NameAdditionLocID.NameAdditionNone.ToString(), 0, usedAbs, maxHealth);
                stats.GainCharacterLoot(newCharacter);
            }
            exitAmount = entryVariable;
            CombatManager.Instance.AddSubAction(new ShowAttackInformationUIAction(caster.ID, caster.IsUnitCharacter, "Check your party menu after combat!"));
            return true;
        }
    }
}
