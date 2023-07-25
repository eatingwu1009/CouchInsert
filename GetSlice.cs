using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Documents;
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

        public double GetDoubleZ(StructureSet SS, int z)
        {
            var imageRes = SS.Image.ZRes;
            return (z * imageRes) + SS.Image.Origin.z;
        }

        public double GetLimitValue(string Checkpoint, Image image, Structure structure, double chkOrientation, int forXYSlice)
        {
            double BBs = new double();
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
                if (chkOrientation == 1) { BBs = double.MaxValue; }
                else if (chkOrientation == -1) { BBs = double.MinValue; }
                foreach (VVector[] vectors in structure.GetContoursOnImagePlane(forXYSlice))
                {
                    foreach (VVector v in vectors)
                    {
                        if (chkOrientation == 1) { BBs = Math.Min(BBs, v.y); }
                        else if (chkOrientation == -1) { BBs = Math.Max(BBs, v.y); }
                    }
                }
            }
            else if (Checkpoint == "Z")
            {
                if (chkOrientation == 1) { BBs = double.MinValue; }
                else if (chkOrientation == -1) { BBs = double.MaxValue; }
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
        public int? FindNeck(int Z1, int Z2, Structure structure)
        {
            int? finalNeck = null;
            double limit1, limit2 = new double();
            List<double[]> Lists = new List<double[]>();
            for (int i = Z1; i <= Z2; i++)
            {
                limit1 = double.MinValue; limit2 = double.MaxValue;
                foreach (VVector[] vectors in structure.GetContoursOnImagePlane(i))
                {
                    foreach (VVector vector in vectors)
                    {
                        limit1 = Math.Max(limit1, vector.y);
                        limit2 = Math.Min(limit2, vector.y);
                    }
                }
                double[] element = new double[2];
                element[0] = Convert.ToDouble(i);
                element[1] = limit1 - limit2;
                Lists.Add(element);
            }
            for (int i = 1; i < Lists.Count() - 1; i++)
            {
                if (Lists.ElementAt(i)[1]!= Lists.ElementAt(i-1)[1] && Lists.ElementAt(i)[1] > Lists.ElementAt(i + 1)[1])
                { finalNeck = Convert.ToInt32(Lists.ElementAt(i)[0]); }
            }
            return finalNeck;// else finalNeck = null
        }

        public string CTEnough(double SIZlocation, double markerZlocation, double Zchkorientation)
        {
            string Final = "";
            double distance = Zchkorientation * (SIZlocation - markerZlocation);

            if (Math.Round(distance/10) <= 11){ Final = "H2, H1 ,H0"; }
            else if (Math.Round(distance / 10) <= (11 + 14) && Math.Round(distance / 10) > 11 ) { Final = "H1, H0"; }
            else if (Math.Round(distance / 10) > (11 + 14)) { Final = "H0"; }
            else { Final = "Error"; }
            return Final;
        }
    }
}
