using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.CustomOther
{
    public class ReturnValueComparatorEffectorCondition : EffectorConditionSO
    {
        public int _comparator;
        public bool _lessThan = false;
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            IntegerReference intRef = args as IntegerReference;
            if (intRef == null) { return false; }
            else 
            { 
                if (_lessThan)
                {
                    return intRef.value < _comparator;
                }
                else
                {
                    return intRef.value >= _comparator;
                }
            }
        }
    }
}
