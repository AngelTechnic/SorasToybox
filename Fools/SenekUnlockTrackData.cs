using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox
{
    public class SenekUnlockTrackData : UnlockTrack_Data
    {
        public override string GetDescription(GameInformationHolder holder)
        {
            return "This party member will only join you if they recognize someone in your party.";
        }

        public override bool IsTrackDataAvailable(GameInformationHolder holder)
        {
            return LoadedDBsHandler.InfoHolder.Game.GetBoolData("SenekTrack");
        }
    }
}
