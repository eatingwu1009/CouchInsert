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
        public double? FindNeck(int Z1, int Z2, Structure structure, double Ychk)
        {
            double? finalNeck = null;
            double limit1, limit2, zz = new double();
            List<double[]> Lists = new List<double[]>(); List<double> Lists2 = new List<double>(); List<double> Lists3 = new List<double>();
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
                    zz = vectors.FirstOrDefault().z;
                }
                double[] element = new double[4];
                element[0] = zz;
                element[1] = limit1 - limit2;
                element[2] = limit1;
                element[3] = limit2;
                Lists.Add(element);
            }
            Lists.RemoveAll(item => double.IsInfinity(item[1]));
            foreach (double[] number in Lists)
            {
                Lists2.Add(number[1]);
            }
            double mode = Lists2.GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => (double)x.Key).FirstOrDefault();
            Lists.RemoveAll(item => item[1].Equals(mode)); Lists.RemoveAll(item => item[1] > mode);
            //Check Absolute Y value
            if (Lists.Count == 0) { finalNeck = null; }
            else
            {
                if (Ychk == 1)
                {
                    foreach (double[] number in Lists)
                    {
                        Lists3.Add(number[2]);
                    }
                    double LimitY = Lists3.OrderByDescending(x => x).FirstOrDefault();
                    finalNeck = Lists.OrderByDescending(x => Math.Abs(x[2] - LimitY)).LastOrDefault()[0];
                }
                else if (Ychk == -1)
                {
                    foreach (double[] number in Lists)
                    {
                        Lists3.Add(number[3]);
                    }
                    double LimitY = Lists3.OrderByDescending(x => x).LastOrDefault();
                    finalNeck = Lists.OrderByDescending(x => Math.Abs(x[3] - LimitY)).LastOrDefault()[0];
                }
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
