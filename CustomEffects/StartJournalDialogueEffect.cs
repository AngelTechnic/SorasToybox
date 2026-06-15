using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomEffects
{
    public class StartJournalDialogueEffect : EffectSO
    {
        public Dictionary<string, string> _journalList = new Dictionary<string, string>();
        public Dictionary<string, Dictionary<string, string>> _altJournalList = new Dictionary<string, Dictionary<string, string>>();
        public DialogueSO _dialogue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (targets[0].HasUnit == false)
            {
                return false;
            }

            if (caster is CharacterCombat casterCH)
            {
                if (targets[0].Unit is EnemyCombat enemy)
                {
                    foreach (CharacterCombat fieldChar in stats.CharactersOnField.Values)
                    {
                        foreach (string key in _altJournalList.Keys)
                        {
                            if (key == fieldChar.Character.name)
                            {
                                var checkerList = _altJournalList[key];
                                foreach (string key2 in checkerList.Keys)
                                {
                                    if (enemy.Enemy.name == key2)
                                    {
                                        exitAmount++;
                                        checkerList.TryGetValue(key2, out string dialogueKey);
                                        Debug.Log("Journal Mode | selected node: " + _dialogue.startNode + "." + dialogueKey);
                                        CombatManager.Instance.AddUIAction(new PlayDialogueUIAction(_dialogue.m_DialogID, _dialogue.startNode + "." + dialogueKey, _dialogue.dialog));
                                        break;
                                    }
                                }
                                //if (exitAmount >= 1) { break; }
                            }
                        }
                    }
                    if (exitAmount >= 1)
                    {
                        return true;
                    }
                    foreach (string key in _journalList.Keys)
                    {
                        if (enemy.Enemy.name == key)
                        {
                            exitAmount++;
                            _journalList.TryGetValue(key, out string dialogueKey);
                            Debug.Log("Journal Mode | selected node: " + _dialogue.startNode + "." + dialogueKey);
                            CombatManager.Instance.AddUIAction(new PlayDialogueUIAction(_dialogue.m_DialogID, _dialogue.startNode + "." + dialogueKey, _dialogue.dialog));
                            break;
                        }
                    }
                    if (exitAmount == 0)
                    {
                        CombatManager.Instance.AddUIAction(new PlayDialogueUIAction(_dialogue.m_DialogID, _dialogue.startNode, _dialogue.dialog));
                        return true;
                    }
                }
                    
            }

            return true;
        }
    }
}
