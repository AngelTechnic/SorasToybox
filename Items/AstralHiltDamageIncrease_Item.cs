using SorasToybox.CustomOther;
using BrutalAPI.Items;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace SorasToybox.Items
{
    public class AstralHiltDamageIncrease_Item : BaseItem
    {
        public ZCDamageWearable item;

        public override BaseWearableSO Item => item;

        public int NormalAddition
        {
            set => item._toAdd1 = value;
        }

        public int ZCAddition
        {
            set => item._toAdd0 = value;
        }

        public int NormalAddition2
        {
            set => item._toAdd1from = value;
        }

        public int ZCAddition2
        {
            set => item._toAdd0from = value;
        }

        public bool AffectDamageDealtInsteadOfReceived
        {
            set => item._useDealt = value;
        }

        public bool UseSimpleIntegerInsteadOfDamage
        {
            set => item._useSimpleInt = value;
        }

        public bool UseRangeFromTo
        {
            set => item._useRange = value;
        }

        public AstralHiltDamageIncrease_Item(string itemID = "DefaultID_Item", int additionNormal = 1, int additionZC = 1, int additionNormal2 = 1, int additionZC2 = 1, bool useDealt = false, bool useInt = false, bool useRange = false)
        {
            item = ScriptableObject.CreateInstance<ZCDamageWearable>();
            item._toAdd0 = additionZC;
            item._toAdd1 = additionNormal;
            item._toAdd0from = additionZC2;
            item._toAdd1from = additionNormal2;
            item._useDealt = useDealt;
            item._useSimpleInt = useInt;
            item._useRange = useRange;
            InitializeItemData(itemID);
        }
    }
}
