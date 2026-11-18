using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Enemies
{
    public class Freezer
    {
        public static void Add()
        {
            StatusEffect_Apply_Effect getWhiplash = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            getWhiplash._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");

            ConsumeColorManaEffect eatBlue = ScriptableObject.CreateInstance<ConsumeColorManaEffect>();
            eatBlue.mana = Pigments.Blue;
        }
    }
}
