using VMS.TPS.Common.Model.API;

namespace CouchInsert
{
    public class bodyParameter
    {
        public Structure BuildBODY(StructureSet SS, int Lower, bool K, int N, bool D, double DRadius, bool F, bool Pre, double PreRadius, bool S, int SLevel)
        {
            var BodyPar = SS.GetDefaultSearchBodyParameters();
            BodyPar.LowerHUThreshold = Lower; 
            BodyPar.KeepLargestParts = K;
            BodyPar.NumberOfLargestPartsToKeep = N;
            BodyPar.PreDisconnect = D;
            BodyPar.PreDisconnectRadius = DRadius;
            BodyPar.FillAllCavities = F;
            BodyPar.PreCloseOpenings = Pre;
            BodyPar.PreCloseOpeningsRadius = PreRadius;
            BodyPar.Smoothing = S;
            BodyPar.SmoothingLevel = SLevel;
            return SS.CreateAndSearchBody(BodyPar);
        }
    }
}
