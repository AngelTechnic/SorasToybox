using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomOther
{
    // borrowed and modified from Hell Island Fell (Custom Stuff/HellishDamageWearable.cs)
    public class ZCDamageWearable : BaseWearableSO
    {
        public bool _useDealt;

        public bool _useSimpleInt;

        public bool _useRange;

        public int _toAdd0 = 9;
        public int _toAdd0from = 3;

        public int _toAdd1 = 3;
        public int _toAdd1from = 1;

        public override bool IsItemImmediate => true;

        public override bool DoesItemTrigger => true;

        public override void TriggerPassive(object sender, object args)
        {
            if (args is DamageDealtValueChangeException context)
            {
                if (context.damagedUnit.UnitTypes.Contains("Zoincaillan"))
                {
                    if (_useSimpleInt)
                    {
                        if (args is IntValueChangeException ex && !ex.Equals(null))
                        {
                            if (_useRange)
                            {
                                int value = UnityEngine.Random.Range(_toAdd0from, _toAdd0 + 1);
                                ex.AddModifier(new BasicFlatValueModifier(true, value, true));
                            }
                            else { ex.AddModifier(new BasicFlatValueModifier(true, _toAdd0, true)); }
                        }
                    }
                    else if (_useDealt)
                    {
                        if (args is DamageDealtValueChangeException ex2 && !ex2.Equals(null))
                        {
                            if (_useRange)
                            {
                                int value = UnityEngine.Random.Range(_toAdd0from, _toAdd0 + 1);
                                ex2.AddModifier(new BasicFlatValueModifier(true, value, true));
                            }
                            else { ex2.AddModifier(new BasicFlatValueModifier(true, _toAdd0, true)); }
                        }
                    }
                    else if (args is DamageReceivedValueChangeException ex3 && !ex3.Equals(null))
                    {
                        if (_useRange)
                        {
                            int value = UnityEngine.Random.Range(_toAdd0from, _toAdd0 + 1);
                            ex3.AddModifier(new BasicFlatValueModifier(true, value, true));
                        }
                        else { ex3.AddModifier(new BasicFlatValueModifier(true, _toAdd0, true)); }
                    }
                }
                if (!context.damagedUnit.UnitTypes.Contains("Zoincaillan"))
                {
                    if (_useSimpleInt)
                    {
                        if (args is IntValueChangeException ex && !ex.Equals(null))
                        {
                            if (_useRange)
                            {
                                int value = UnityEngine.Random.Range(_toAdd1from, _toAdd1 + 1);
                                ex.AddModifier(new BasicFlatValueModifier(true, value, true));
                            }
                            else { ex.AddModifier(new BasicFlatValueModifier(true, _toAdd1, true)); }
                        }
                    }
                    else if (_useDealt)
                    {
                        if (args is DamageDealtValueChangeException ex2 && !ex2.Equals(null))
                        {
                            if (_useRange)
                            {
                                int value = UnityEngine.Random.Range(_toAdd1from, _toAdd1 + 1);
                                ex2.AddModifier(new BasicFlatValueModifier(true, value, true));
                            }
                            else { ex2.AddModifier(new BasicFlatValueModifier(true, _toAdd1, true)); }
                        }
                    }
                    else if (args is DamageReceivedValueChangeException ex3 && !ex3.Equals(null))
                    {
                        if (_useRange)
                        {
                            int value = UnityEngine.Random.Range(_toAdd1from, _toAdd1 + 1);
                            ex3.AddModifier(new BasicFlatValueModifier(true, value, true));
                        }
                        else { ex3.AddModifier(new BasicFlatValueModifier(true, _toAdd1, true)); }
                    }
                }
            }
        }
        public class BasicFlatValueModifier(bool dmgDealt, int amount, bool increase) : IntValueModifier(dmgDealt ? 4 : 62)
        {
            public override int Modify(int value)
            {
                if (!increase)
                {
                    return Mathf.Max(0, value + amount);
                }
                return value + amount;
            }
        }
    }
}
