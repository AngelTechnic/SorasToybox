using System;
using System.Collections.Generic;
using System.Text;

namespace SorasToybox.Fools
{
    public class KarmaUnlockTrackData : UnlockTrack_Data
    {
        public override string GetDescription(GameInformationHolder holder)
        {
            return "This party member demands that you overcome your Antagonist.";
        }

        public override bool IsTrackDataAvailable(GameInformationHolder holder)
        {
            return LoadedDBsHandler.InfoHolder.Game.GetBoolData("KarmaTrack");
        }
    }
}
