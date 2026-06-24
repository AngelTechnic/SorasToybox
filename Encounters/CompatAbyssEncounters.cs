using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Encounters
{
    public class CompatAbyssEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                AddTo abyssAdd = new AddTo(Abyss.H.Kcolclock.Hard);
                abyssAdd.SimpleAddGroup(1, "Kcolclock_EN", 1, "GearYinimro_EN");

                abyssAdd = new AddTo(Abyss.H.Kookoo.Hard);
                abyssAdd.SimpleAddGroup(1, "Kookoo_EN", 2, "GearYinimro_EN");

                

            }
        }
    }
}