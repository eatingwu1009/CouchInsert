using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace CouchInsert
{
    public class DetectTool
    {
        public List<VVector> Twopoint(ImageProfile InputProfile)
        {
            List<VVector> Twopoint = new List<VVector>();
            double OverallMaximum = InputProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value);
            double OverallMinimum = InputProfile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);
            double mean = InputProfile.Where(p => !Double.IsNaN(p.Value)).Average(p => p.Value);
            double Maximum = InputProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value) - OverallMinimum;
            double HalfTrend = Maximum / 2;
            for (int i = 1; i < InputProfile.Count() - 1; i++)
            {
                double iiHU = InputProfile[i].Value;
                double ii = InputProfile[i].Value - OverallMinimum;
                double iadd = InputProfile[i + 1].Value - OverallMinimum;
                double iminus = InputProfile[i - 1].Value - OverallMinimum;
                if (!Double.IsNaN(ii) && ii > HalfTrend && (ii > iadd) && (ii > iminus) && iiHU > mean + 100)
                {
                    Twopoint.Add(InputProfile[i].Position);
                }
            }
            return Twopoint;
        }

        public bool MarkerPlaced(List<VVector> VVectorPoints, VVector MarkerLocationItem, double Xchkorientation)
        {
            bool OK = false;
            double BaseNumber = 0;
            VVector final = new VVector();
            foreach (VVector v in VVectorPoints)
            {
                double d = VVector.Distance(v, MarkerLocationItem);
                if(Math.Abs(d) > BaseNumber) { BaseNumber = Math.Abs(d); final = v; }
            }
            if (Xchkorientation == 1) { if (final.x < MarkerLocationItem.x) OK = true; }
            else if (Xchkorientation == -1) { if (final.x > MarkerLocationItem.x) OK = true; }
            return OK;
        }

    public String PeakDetect(List<VVector> Twopoint)
        {
            if (Twopoint.Count > 1)
            {
                double distance = VVector.Distance(Twopoint[0], Twopoint[1]);
                if (Math.Round((distance / 10)) > 0.5)// length unit by mm
                {
                    var map = new Dictionary<int, string>()
                {
                    {1, "H1"},
                    {2, "H2"},
                    {3, "H3"},
                    {4, "H4"},
                    {5, "H5"},
                };
                    string output;
                    return map.TryGetValue(Convert.ToInt32(Math.Round((distance / 10))), out output) ? output : null;
                }
                else return "";
            }
            else if (Twopoint.Count == 1)
            {
                return "0";
            }
            else return "";
        }

        public String BBCalDetect(double distance, double Xcenter, double StandardY, double StandardZ)
        {
            var mapBB = new Dictionary<double, string>()
            {
                    {Math.Round((Math.Sqrt(Math.Pow(Xcenter, 2) +Math.Pow(StandardY, 2) + Math.Pow(420 + StandardZ, 2)))/10,0,MidpointRounding.AwayFromZero) , "H0"},
                    {Math.Round((Math.Sqrt(Math.Pow(Xcenter, 2) +Math.Pow(StandardY, 2) + Math.Pow(280 + StandardZ, 2)))/10,0,MidpointRounding.AwayFromZero) , "H1"},
                    {Math.Round((Math.Sqrt(Math.Pow(Xcenter, 2) +Math.Pow(StandardY, 2) + Math.Pow(140 + StandardZ, 2)))/10,0,MidpointRounding.AwayFromZero), "H2"},
                    {Math.Round((Math.Sqrt(Math.Pow(Xcenter, 2) +Math.Pow(StandardY, 2) + Math.Pow(StandardZ, 2)))/10,0,MidpointRounding.AwayFromZero), "H3"},
                    {Math.Round((Math.Sqrt(Math.Pow(Xcenter, 2) +Math.Pow(StandardY, 2) + Math.Pow(StandardZ - 140, 2)))/10,0,MidpointRounding.AwayFromZero) , "H4"},
                    {Math.Round((Math.Sqrt(Math.Pow(Xcenter, 2) +Math.Pow(StandardY, 2) + Math.Pow(StandardZ - 280, 2)))/10,0,MidpointRounding.AwayFromZero), "H5"},
            };
            string output;
            double temp = Math.Round((Math.Sqrt(Math.Pow(Xcenter, 2) + Math.Pow(StandardY, 2) + Math.Pow(StandardZ, 2))) / 10, 0, MidpointRounding.AwayFromZero);
            return mapBB.TryGetValue((Math.Round(distance/10, 0, MidpointRounding.AwayFromZero)), out output) ? output : "Error";
            //The distance are using absolute value cuz there is no foot side marker
        }

        public double[] MaxMinDetect(List<VVector> VVectors, PatientOrientation Ori)
        {
            double[] Final = { VVectors[0].x, VVectors[0].y, VVectors[0].z };
            for (int i = 1; i < VVectors.Count(); i++)
            {
                Final[0] = Math.Min(VVectors[i].x, Final[0]); //Always get the maximum value
                if (Ori == PatientOrientation.HeadFirstSupine | Ori == PatientOrientation.FeetFirstSupine)
                {
                    Final[1] = Math.Min(VVectors[i].y, Final[1]);
                }
                else if (Ori == PatientOrientation.HeadFirstProne | Ori == PatientOrientation.FeetFirstProne)
                {
                    Final[1] = Math.Max(VVectors[i].y, Final[1]);
                }
                Final[2] = Math.Min(VVectors[i].z, Final[2]);
            }
            return Final;
        }
    }
}
