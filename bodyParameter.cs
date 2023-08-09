using System.Windows.Forms;
using System;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Collections.Generic;

namespace CouchInsert
{
    public class bodyParameter
    {
        public double BodyVolume { get; set; }
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

        public void PreBODY(StructureSet SS)
        {
            StructureModifier getStructure = new StructureModifier();
            Structure BODY = getStructure.FindStructure(SS, "EXTERNAL", "DicomType");
            bodyParameter AddBODY = new bodyParameter();
            if (BODY == null)
            {
                AddBODY.BuildBODY(SS, -350, true, 1, true, 0.2, true, true, 0.2, true, 3);
                BodyVolume = getStructure.FindStructure(SS, "EXTERNAL", "DicomType").Volume;
                SS.RemoveStructure(getStructure.FindStructure(SS, "EXTERNAL", "DicomType"));
                AddBODY.BuildBODY(SS, -350, false, 1, false, 0.2, true, true, 0.2, true, 3);
            }
            else if (BODY.Volume == 0)
            {
                AddBODY.BuildBODY(SS, -350, true, 1, true, 0.2, true, true, 0.2, true, 3);
                BodyVolume = getStructure.FindStructure(SS, "EXTERNAL", "DicomType").Volume;
                SS.RemoveStructure(getStructure.FindStructure(SS, "EXTERNAL", "DicomType"));
                AddBODY.BuildBODY(SS, -350, false, 1, false, 0.2, true, true, 0.2, true, 3);
            }
        }

        public void PostProtonBODY(StructureSet SS, VMS.TPS.Common.Model.API.Image SI, Structure CS, Structure CI, double FinalYcenter, PatientOrientation orientation, double YchkOrientation, double Xcenter)
        {
            StructureModifier getStructure = new StructureModifier();
            DetectTool detectTool = new DetectTool();
            Structure BODY = getStructure.FindStructure(SS, "EXTERNAL", "DicomType");
            Structure Temp = SS.AddStructure("CONTROL", "Temp_ForCouch");
            List<VVector> CSVVector = new List<VVector>();
            if (CS != null)
            {
                for (int i = 0; i < SI.ZSize; i++)
                {
                    foreach (VVector[] vectors in CS.GetContoursOnImagePlane(i))
                    {
                        foreach (VVector v in vectors)
                        {
                            double x = v.x;
                            double y = v.y;
                            double z = v.z;
                            CSVVector.Add(new VVector(x, y, z));
                        }
                    }
                }
                FinalYcenter = detectTool.MaxMinDetect(CSVVector, orientation)[1];
            }
            else { System.Windows.Forms.MessageBox.Show("There is no Couch Structure.", "Couch Structure is not existed", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            VVector[] TempVec = getStructure.GetpseudoLine(FinalYcenter, SI.YSize * YchkOrientation, 0 , SI.XSize);
            VVector[] TempVec2 = getStructure.GetpseudoLine(FinalYcenter, FinalYcenter - 27.1 * YchkOrientation, Xcenter, 260);
            for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
            {
                Temp.AddContourOnImagePlane(TempVec, i);
            }
            BODY.SegmentVolume = BODY.SegmentVolume.Sub(Temp.SegmentVolume); SS.RemoveStructure(Temp); Temp = SS.AddStructure("CONTROL", "Temp_ForCouch");
            for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
            {
                Temp.AddContourOnImagePlane(TempVec2, i);
            }
            BODY.SegmentVolume = BODY.SegmentVolume.Or(Temp.SegmentVolume).Or(CS.SegmentVolume).Or(CI.SegmentVolume); SS.RemoveStructure(Temp);
            BODY.Comment = "Modified by ESAPI";
        }
    }
}
