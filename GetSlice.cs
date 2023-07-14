using System;
using System.Linq;
using System.Windows;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace CouchInsert
{
    public class SliceConverter
    {
        public int GetSlice(StructureSet SS, double z)
        {
            var imageRes = SS.Image.ZRes;
            return Convert.ToInt32((z - SS.Image.Origin.z) / imageRes);
        }
        
        public double GetLimitValue(string Checkpoint, Image image, Structure structure, double chkOrientation, int forXYSlice)
        {
            double BBs = new double();
            if (chkOrientation == 1) { BBs = double.MinValue; }
            else if (chkOrientation == -1) { BBs = double.MaxValue; }
            if (Checkpoint == "X1")
            {
                BBs = double.MaxValue;
                foreach (VVector[] vectors in structure.GetContoursOnImagePlane(forXYSlice))
                {
                    foreach (VVector v in vectors)
                    {
                        BBs = Math.Min(BBs, v.x); 
                    }
                }
            }
            else if (Checkpoint == "X2")
            {
                BBs = double.MinValue;
                foreach (VVector[] vectors in structure.GetContoursOnImagePlane(forXYSlice))
                {
                    foreach (VVector v in vectors)
                    {
                        BBs = Math.Max(BBs, v.x);
                    }
                }
            }
            else if (Checkpoint == "Y")
            {
                foreach (VVector[] vectors in structure.GetContoursOnImagePlane(forXYSlice))
                {
                    foreach (VVector v in vectors)
                    {
                        if (chkOrientation == 1) { BBs = Math.Max(BBs, v.y); }
                        else if (chkOrientation == -1) { BBs = Math.Min(BBs, v.y); }
                    }
                }
            }
            else if (Checkpoint == "Z")
            {
                for (int i = 0; i < image.ZSize; i++)
                {
                    foreach (VVector[] vectors in structure.GetContoursOnImagePlane(i))
                    {
                        double vs = vectors.FirstOrDefault().z;
                        if (chkOrientation == 1) { BBs = Math.Max(BBs, vs); }
                        else if (chkOrientation == -1) { BBs = Math.Min(BBs, vs); }
                    }
                }
            }
            return BBs;
        }
    }
}
