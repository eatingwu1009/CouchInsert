using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace CouchInsert
{
    public class DetectTool
    {
        public string NeckInfo { get; set; }
        public List<VVector> Twopoint(ImageProfile InputProfile)
        {
            List<LineProfile> Twopoint_Temp = new List<LineProfile>();
            List<VVector> Twopoint = new List<VVector>();
            Twopoint_Temp.Clear(); Twopoint.Clear();
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
                    double slope = Math.Abs(2 * InputProfile[i].Value - InputProfile[i + 1].Value - InputProfile[i - 1].Value);
                    LineProfile cc = new LineProfile();
                    cc.position = InputProfile[i].Position; cc.slope = slope;
                    Twopoint_Temp.Add(cc);
                }
            }
            if (Twopoint_Temp.Count > 1)
            {
                Twopoint_Temp = Twopoint_Temp.OrderByDescending(x => x.slope).ToList();
                Twopoint.Add(Twopoint_Temp.ElementAtOrDefault(0).position);
                Twopoint.Add(Twopoint_Temp.ElementAtOrDefault(1).position);
            }
            else if (Twopoint_Temp.Count > 0)
            {
                Twopoint_Temp = Twopoint_Temp.OrderByDescending(x => x.slope).ToList();
                Twopoint.Add(Twopoint_Temp.ElementAtOrDefault(0).position);
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
                if (Math.Abs(d) > BaseNumber) { BaseNumber = Math.Abs(d); final = v; }
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
                return "H0";
            }
            else return "";
        }

        public String NeckDetect(double distance, double HSpace, double ZChk, double Zresult)
        {
            double Ans = double.MaxValue;
            var mapBB = new Dictionary<double, string>()
            {
                {Math.Round((Zresult - 2 *  (HSpace)) *ZChk/10,0,MidpointRounding.AwayFromZero) , "H5"},
                {Math.Round((Zresult - 1 *  (HSpace)) *ZChk/10,0,MidpointRounding.AwayFromZero) , "H4"},
                {Math.Round((Zresult - 0 *  (HSpace)) *ZChk/10,0,MidpointRounding.AwayFromZero) , "H3"},
                {Math.Round((Zresult + 1 *  (HSpace)) *ZChk/10,0,MidpointRounding.AwayFromZero) , "H2"},
                {Math.Round((Zresult + 2 *  (HSpace)) *ZChk/10,0,MidpointRounding.AwayFromZero) , "H1"},
                {Math.Round((Zresult + 3 *  (HSpace)) *ZChk/10,0,MidpointRounding.AwayFromZero) , "H0"},
            };
            string output;
            List<double> Listfor5mm = IsBetween(Convert.ToInt32(Math.Round(distance, 0, MidpointRounding.AwayFromZero)), 5);
            foreach (double i in Listfor5mm)
            {
                double temp = i / 10;
                if (mapBB.ContainsKey(Math.Round(temp, 0, MidpointRounding.AwayFromZero)) == true) 
                { Ans = Convert.ToInt32(Math.Round(temp, 0, MidpointRounding.AwayFromZero)); }
            }
            NeckInfo = "\n\nH5 : " + Math.Round((Zresult - 2 * (HSpace)) * ZChk / 10, 0, MidpointRounding.AwayFromZero) + "cm"+
            "\nH4 : " + Math.Round((Zresult - 1 * (HSpace)) * ZChk / 10, 0, MidpointRounding.AwayFromZero) + "cm" +
            "\nH3 : " + Math.Round((Zresult - 0 * (HSpace)) * ZChk / 10, 0, MidpointRounding.AwayFromZero) + "cm" +
            "\nH2 : " + Math.Round((Zresult + 1 * (HSpace)) * ZChk / 10, 0, MidpointRounding.AwayFromZero) + "cm" +
            "\nH1 : " + Math.Round((Zresult + 2 * (HSpace)) * ZChk / 10, 0, MidpointRounding.AwayFromZero) + "cm" +
            "\nH0 : " + Math.Round((Zresult + 3 * (HSpace)) * ZChk / 10, 0, MidpointRounding.AwayFromZero) + "cm" ;
            return mapBB.TryGetValue(Ans, out output) ? output : "Error";
            //The distance are using absolute value cuz there is no foot side marker
        }


        public List<double> IsBetween(double source, double range)
        {
            List<double> ints = new List<double>();
            for (double a = source - range; a <= source + range; a++)
            { ints.Add(a); }
            return ints;
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
