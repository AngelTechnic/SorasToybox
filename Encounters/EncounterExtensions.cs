using System;
using System.Collections.Generic;
using BrutalAPI;

namespace SorasToybox.Encounters
{
    // Token: 0x02000266 RID: 614
    public static class EncounterExtensions
    {
        // Token: 0x060007EF RID: 2031 RVA: 0x00039E8C File Offset: 0x0003808C
        public static void SimpleAddEncounter(this EnemyEncounter_API self, int num1 = 0, string enemy1 = "", int num2 = 0, string enemy2 = "", int num3 = 0, string enemy3 = "", int num4 = 0, string enemy4 = "", int num5 = 0, string enemy5 = "")
        {
            List<string> list = new List<string>();
            bool flag = enemy1 != "";
            if (flag)
            {
                for (int i = 0; i < num1; i++)
                {
                    list.Add(enemy1);
                }
            }
            bool flag2 = enemy2 != "";
            if (flag2)
            {
                for (int j = 0; j < num2; j++)
                {
                    list.Add(enemy2);
                }
            }
            bool flag3 = enemy3 != "";
            if (flag3)
            {
                for (int k = 0; k < num3; k++)
                {
                    list.Add(enemy3);
                }
            }
            bool flag4 = enemy4 != "";
            if (flag4)
            {
                for (int l = 0; l < num4; l++)
                {
                    list.Add(enemy4);
                }
            }
            bool flag5 = enemy5 != "";
            if (flag5)
            {
                for (int m = 0; m < num5; m++)
                {
                    list.Add(enemy5);
                }
            }
            bool valid = true;
            foreach (string eval in list)
            {
                try
                {
                    if (LoadedAssetsHandler.GetEnemy(eval) == null) { throw new NullReferenceException("no such enemy - this better get caught"); }
                }
                catch
                {
                    UnityEngine.Debug.LogWarning("Encounter Extensions | no enemy with id " + eval + " - skipping encounter!");
                    valid = false;
                    break;
                }
            }
            if (valid) { self.CreateNewEnemyEncounterData(list.ToArray(), null); }
        }

        // Token: 0x060007F0 RID: 2032 RVA: 0x00039F9C File Offset: 0x0003819C
        public static void AddRandomEncounter(this EnemyEncounter_API self, string enemy1 = "", string enemy2 = "", string enemy3 = "", string enemy4 = "", string enemy5 = "")
        {
            List<string> list = new List<string>();
            bool flag = enemy1 != "";
            if (flag)
            {
                list.Add(enemy1);
            }
            bool flag2 = enemy2 != "";
            if (flag2)
            {
                list.Add(enemy2);
            }
            bool flag3 = enemy3 != "";
            if (flag3)
            {
                list.Add(enemy3);
            }
            bool flag4 = enemy4 != "";
            if (flag4)
            {
                list.Add(enemy4);
            }
            bool flag5 = enemy5 != "";
            if (flag5)
            {
                list.Add(enemy5);
            }
            bool valid = true;
            foreach (string eval in list)
            {
                try
                {
                    if (LoadedAssetsHandler.GetEnemy(eval) == null) { throw new NullReferenceException("no such enemy - this better get caught"); }
                }
                catch
                {
                    UnityEngine.Debug.LogWarning("Encounter Extensions | no enemy with id " + eval + " - skipping encounter!");
                    valid = false;
                    break;
                }
            }
            if (valid) { self.CreateNewEnemyEncounterData(list.ToArray(), null); }
        }
    }
}
