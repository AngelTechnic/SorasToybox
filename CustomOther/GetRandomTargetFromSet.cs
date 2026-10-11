
using System.Collections.Generic;
using SorasToybox;
using UnityEngine;

public class GetRandomTargetFromSet : BaseCombatTargettingSO
{
	public BaseCombatTargettingSO orig;

	public bool EmptySlots;

	public override bool AreTargetAllies => orig.AreTargetAllies;

	public override bool AreTargetSlots => orig.AreTargetSlots;

	public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
	{
		List<TargetSlotInfo> list = new List<TargetSlotInfo>();
		TargetSlotInfo[] targets = orig.GetTargets(slots, casterSlotID, true);
		foreach (TargetSlotInfo val in targets)
		{
			if (val.HasUnit)
			{
				list.Add(val);
			}
		}
		List<TargetSlotInfo> list2 = new List<TargetSlotInfo>();
		if (list.Count > 0)
		{
			list2.Add(list[Random.Range(0, list.Count)]);
		}
		return list2.ToArray();
	}

	public static GetRandomTargetFromSet Create(BaseCombatTargettingSO orig, bool empties = false)
	{
		GetRandomTargetFromSet getRandomTargetFromSet = ScriptableObject.CreateInstance<GetRandomTargetFromSet>();
		getRandomTargetFromSet.orig = orig;
		getRandomTargetFromSet.EmptySlots = empties;
		return getRandomTargetFromSet;
	}
}
