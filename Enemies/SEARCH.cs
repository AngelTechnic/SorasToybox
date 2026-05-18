using BrutalAPI;
using System;
using System.Collections.Generic;
using System.Text;


namespace SorasToybox.Enemies
{
    public class SEARCH
    {
        public static void Add()
        {
            Enemy search = new Enemy("SEARCH", "SEARCH_EN")
            {
                Health = 8,
                HealthColor = Pigments.Red
            };
        }
    }
}
