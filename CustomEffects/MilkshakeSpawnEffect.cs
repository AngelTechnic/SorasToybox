using System;
using System.Collections.Generic;
using System.Text;
using Tools;

//basically ripped straight out of HIF
//but i flipped the gender because of a joke equally as stupid as neptune's crown
namespace SorasToybox.CustomEffects
{
    public class MilkshakeSpawnEffect : EffectSO
    {
        public int _rank;

        public NameAdditionLocID _nameAddition;

        public bool _permanentSpawn;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            CharacterDataBase characterDB = LoadedDBsHandler.CharacterDB;
            if (characterDB == null || characterDB.Equals(null))
            {
                return false;
            }

            List<CharacterSO> randomCharacters = GetRandomMaleCharacters(entryVariable);
            string nameAdditionData = LocUtils.GameLoc.GetNameAdditionData(_nameAddition);
            foreach (CharacterSO item in randomCharacters)
            {
                int maxHealth = item.GetMaxHealth(_rank);
                int[] usedAbilities = item.GenerateAbilities();
                CombatManager.Instance.AddSubAction(new SpawnCharacterAction(item, -1, trySpawnAnyways: false, nameAdditionData, _permanentSpawn, _rank, usedAbilities, maxHealth));
            }

            exitAmount = entryVariable;
            return true;
        }
        public List<CharacterSO> GetRandomMaleCharacters(int amount)
        {
            List<string> list = new(LoadMaleCharacterIDs());
            List<CharacterSO> list2 = [];
            while (amount > 0 && list.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, list.Count);
                string characterName = list[index];
                list.RemoveAt(index);
                CharacterSO character = LoadedAssetsHandler.GetCharacter(characterName);
                if (!(character == null) && !character.Equals(null))
                {
                    list2.Add(character);
                    amount--;
                }
            }

            return list2;
        }
        private static IEnumerable<string> LoadMaleCharacterIDs()
        {
            var processed = new List<string>();
            var characters = LoadedDBsHandler.CharacterDB;

            foreach (string i in characters._charactersList)
            {
                if (!processed.Contains(i) && LoadedAssetsHandler.GetCharacter(i).unitTypes.Contains("MaleID"))
                {
                    Debug.Log(LoadedAssetsHandler.GetCharacter(i)._characterName);
                    processed.Add(i);

                    yield return i;
                }
            }
        }
    }
}
