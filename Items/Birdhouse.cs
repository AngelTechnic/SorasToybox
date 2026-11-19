using SorasToybox.CustomEffects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Items
{
    public class Birdhouse
    {
        public static void Add()
        {
            UnitStoreData_ModIntSO desyncTracker = ScriptableObject.CreateInstance<UnitStoreData_ModIntSO>();
            desyncTracker.m_Text = "Desync: {0}";
            desyncTracker._UnitStoreDataID = "MonsterBirdhouseStoredValue";
            desyncTracker.m_TextColor = new Color32(200, 200, 200, 255);
            desyncTracker.m_CompareDataToThis = -1;
            desyncTracker.m_ShowIfDataIsOver = true;
            LoadedDBsHandler.MiscDB.AddNewUnitStoreData("MonsterBirdhouseStoredValue", desyncTracker);

            CasterStoredValueSetEffect desyncSet = ScriptableObject.CreateInstance<CasterStoredValueSetEffect>();
            desyncSet._valueName = "MonsterBirdhouseStoredValue";

            ExtraVariableForNext_SVEffect desyncGet = ScriptableObject.CreateInstance<ExtraVariableForNext_SVEffect>();
            desyncGet.m_unitStoredDataID = "MonsterBirdhouseStoredValue";

            CasterStoredValueChangeEffect desyncGoUp = ScriptableObject.CreateInstance<CasterStoredValueChangeEffect>();
            desyncGoUp.m_unitStoredDataID = "MonsterBirdhouseStoredValue";
            desyncGoUp._minimumValue = 0;
            desyncGoUp._exitValueIsChange = false;
            desyncGoUp._increase = true;
            desyncGoUp._randomBetweenPrevious = false;
            desyncGoUp._usePreviousExitValue = false;
            desyncGoUp._exitValueIsChange = false;

            StatusEffect_Apply_Effect entropyByPrevious = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            entropyByPrevious._Status = StatusField.GetCustomStatusEffect("Salt_Entropy_ID");
            entropyByPrevious._MultPreviousExitValueForEntry = true;

            StatusEffect_Apply_Effect wheresMyPigment = ScriptableObject.CreateInstance<StatusEffect_Apply_Effect>();
            wheresMyPigment._Status = StatusField.GetCustomStatusEffect("Whiplash_ID");


        }
    }
}
