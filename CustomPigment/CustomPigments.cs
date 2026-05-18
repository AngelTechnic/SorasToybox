using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SorasToybox.CustomPigment
{
    public class CustomPigments
    {
        public static void Add()
        {
            if (!LoadedDBsHandler.PigmentDB._PigmentPool.ContainsKey("Broken"))
            {
                ManaColorSO BrokenPigment = ScriptableObject.CreateInstance<ManaColorSO>();
                BrokenPigment.canGenerateMana = true;
                BrokenPigment.dealsCostDamage = true;
                BrokenPigment.pigmentID = "Broken";
                BrokenPigment.manaSprite = ResourceLoader.LoadSprite("BrokenMana", null, 32, null);
                BrokenPigment.manaUsedSprite = ResourceLoader.LoadSprite("BrokenManaUsed", null, 32, null);
                BrokenPigment.manaCostSelectedSprite = ResourceLoader.LoadSprite("BrokenManaCostSelected", null, 32, null);
                BrokenPigment.manaCostSprite = ResourceLoader.LoadSprite("BrokenManaCostUnselected", null, 32, null);
                BrokenPigment.manaSoundEvent = "event:/BrokenPigmentGen";
                BrokenPigment.healthSprite = ResourceLoader.LoadSprite("BrokenManaHealth", null, 32, null);
                BrokenPigment.pigmentTypes = new List<string> { "Broken" };
                Pigments.AddNewPigment("Broken", BrokenPigment);
            }
        }
    }
}
