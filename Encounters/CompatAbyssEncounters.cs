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
                abyssAdd.SimpleAddGroup(1, "Kookoo_EN", 1, "GearYinimro_EN", 1, "Streetlight_EN");

                abyssAdd = new AddTo(Abyss.H.Faceless.Med);
                abyssAdd.SimpleAddGroup(1, "Faceless_EN", 1, "BurningShame_EN", 2, "Streetlight_EN");
                abyssAdd.SimpleAddGroup(1, "Faceless_EN", 1, "BurningShame_EN", 1, "Wug_EN");
                abyssAdd.SimpleAddGroup(1, "Faceless_EN", 1, "GearYinimro_EN", 1, "Sycophant_EN");

                abyssAdd = new AddTo(Abyss.H.Bear.Hard);
                abyssAdd.SimpleAddGroup(2, "Bear_EN", 1, "BurningShame_EN", 1, "GearYinimro_EN");

                if (SorasToybox.extradebug.Value)
                {
                    UnityEngine.Debug.Log("Compat Encounters loaded.");
                }
            }
        }
    }
}