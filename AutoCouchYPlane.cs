using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.Types;
using VMS.TPS.Common.Model.API;
using static CouchInsert.AutoCouchYPlane;

namespace CouchInsert
{
    public class AutoCouchYPlane
    {
        public double FinalYcenter { get; set; }
        public void AutoFindYPlane(ImageProfile YProfile,Image SI,double YchkOrientation,double Xcenter, double Ycenter, double UserOriginZ, double X1, double X2, double Y1, double Y2)
        {
            //chkBrain
            bool chkBrain, chkBrain2 = new bool();
            chkBrain2 = false;
            FindLimit find = new FindLimit();
            find.Limitation(YProfile, -1000);
            double BrainBorder1 = find.min; double BrainBorder2 = find.max;
            if (Math.Abs(BrainBorder1 - BrainBorder2) <= 500) chkBrain = true; else chkBrain = false;

            //Find Y line edge near 53cm
            List<double> YHU_Diff = new List<double>();
            List<double> YLocation = new List<double>();
            double sum = new double();
            VVector Start = new VVector();
            VVector Stop = new VVector();
            int a = 1; if (chkBrain == true) a = Convert.ToInt32(BrainBorder1);
            double SIY = SI.YSize * SI.YRes / 2;
            for (int i = a; i < Convert.ToInt32(SI.YSize * SI.YRes / 2); i++)
            {
                sum = 0;
                double YYiForXprfile = YchkOrientation * (SIY - (i)) + Ycenter;
                Start = new VVector(X1, YYiForXprfile, UserOriginZ);
                Stop = new VVector(X2, YYiForXprfile, UserOriginZ);
                double[] PreallocatedBuffer = new double[1000];
                ImageProfile Profile = SI.GetImageProfile(Start, Stop, PreallocatedBuffer);
                if (chkBrain == true)
                {
                    foreach (ProfilePoint x in Profile.Where(p => !Double.IsNaN(p.Value) && (p.Value != -1000)))
                    {
                        sum += Math.Abs(x.Value - (-450));
                    }
                }
                else
                {
                    foreach (ProfilePoint x in Profile.Where(p => !Double.IsNaN(p.Value)))
                    {
                        sum += Math.Abs(x.Value - (-450));
                    }
                }
                if (sum != 0)
                {
                    YHU_Diff.Add(sum);
                    YLocation.Add(YchkOrientation * (SIY - (i)) + Ycenter);
                }
            }

            double chkHeight, Brn1, Brn2, Brn3, Brn4 = new double();
            List<double> BadChk = new List<double>();
            int index = new int();
            index = YHU_Diff.IndexOf(YHU_Diff.Min());
            FinalYcenter = YLocation.ElementAt(index);
            //Find the point with the highest slope from centerx, and check the distance near 47cm or 51cm
            VVector Couch1, Couch2, Couch3, Couch4 = new VVector();
            double limit1 = FinalYcenter + YchkOrientation * 5;
            double limit2 = FinalYcenter - YchkOrientation * 5;
            double[] _PreallocatedBuffer = new double[1000];
            double[] _PreallocatedBuffer1 = new double[100];

            for (int i = 0; i < 50; i++)
            {
                GetImageXYZProfile profile = new GetImageXYZProfile();
                profile.TakeProfile(SI, -275 + Xcenter, 0 + Xcenter, FinalYcenter, UserOriginZ);
                Couch3 = FindHighestSlope(profile.getXprofile);
                profile.TakeProfile(SI, 0 + Xcenter, 275 + Xcenter, FinalYcenter, UserOriginZ);
                Couch4 = FindHighestSlope(profile.getXprofile);
                limit1 = FinalYcenter + YchkOrientation * 5; limit2 = FinalYcenter - YchkOrientation * 5;
                if (limit1 > limit2) { limit2 = limit1; limit1 = FinalYcenter - YchkOrientation * 5; }

                if ((limit2 < Y2) && (limit1 > Y1))
                {
                    profile.TakeProfile(SI, -275 + Xcenter, 0 + Xcenter, FinalYcenter + YchkOrientation * 3, UserOriginZ);
                    Couch1 = FindHighestSlope(profile.getXprofile);
                    profile.TakeProfile(SI, 0 + Xcenter, 275 + Xcenter, FinalYcenter + YchkOrientation * 3, UserOriginZ);
                    Couch2 = FindHighestSlope(profile.getXprofile);

                    profile.TakeProfile(SI, -50 + Xcenter, 50 + Xcenter, FinalYcenter + YchkOrientation, UserOriginZ);
                    chkHeight = profile.getXprofile[500].Value;

                    profile.TakeProfile(SI, -50 + Xcenter, 50 + Xcenter, FinalYcenter + YchkOrientation * 5, UserOriginZ);
                    Brn1 = profile.getXprofile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);
                    profile.TakeProfile(SI, -250 + Xcenter, 250 + Xcenter, FinalYcenter - YchkOrientation * 5, UserOriginZ);
                    Brn2 = profile.getXprofile.Where(p => p.Value != -1024).Where(p => p.Value != -1000).Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);
                    profile.TakeProfile(SI, -50 + Xcenter, 50 + Xcenter, FinalYcenter + YchkOrientation * 4, UserOriginZ);
                    Brn3 = profile.getXprofile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);

                    profile.TakeProfile(SI, -250 + Xcenter, 250 + Xcenter, FinalYcenter - YchkOrientation * 4, UserOriginZ);
                    Brn4 = profile.getXprofile.Where(p => p.Value != -1024).Where(p => p.Value != -1000).Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);

                    if (Brn1 < -600 && Brn2 < -600 && (Brn3 <= -850 && Brn3 >= -950) && Brn4 < -600)
                    { chkBrain2 = true; }

                    double CouchBorder1 = Math.Round(VVector.Distance(Couch1, Couch2) / 10);
                    double CouchBorder2 = Math.Round(VVector.Distance(Couch3, Couch4) / 10);
                    if (CouchBorder1 < CouchBorder2)
                    {
                        CouchBorder1 = Math.Round(VVector.Distance(Couch3, Couch4) / 10);
                        CouchBorder2 = Math.Round(VVector.Distance(Couch1, Couch2) / 10);
                    }
                    if (((CouchBorder1 >= 50 && CouchBorder1 <= 54) && (CouchBorder2 >= 47 && CouchBorder2 <= 54) && (chkHeight > -650 | chkBrain2 == true)) | (chkBrain == true && chkBrain2 == true)) break;
                    YHU_Diff.RemoveAt(index);
                    YLocation.RemoveAt(index);
                }
                else
                {
                    YHU_Diff.RemoveAt(index);
                    YLocation.RemoveAt(index);
                }
                index = YHU_Diff.IndexOf(YHU_Diff.Min());
                FinalYcenter = YLocation.ElementAt(index);
                i++;
            }
        }
        public VVector FindHighestSlope(ImageProfile collection)
        {
            VVector HighestSlope = new VVector();
            var minDifference = double.MinValue;
            for (int i = 1; i < collection.Count() - 1; i++)
            {
                var difference = Math.Abs((long)collection[i + 1].Value - collection[i].Value);
                if (difference > minDifference)
                {
                    minDifference = (double)difference;
                    HighestSlope = collection[i].Position;
                }
            }
            return HighestSlope;
        }
        public class FindLimit
        {
            public double max { get; set; }
            public double min { get; set; }
            public void Limitation(ImageProfile collection, double VValue)
            {
                min = collection.Where(p => !Double.IsNaN(p.Value) && p.Value != VValue).Min(p => p.Position.y);
                max = collection.Where(p => !Double.IsNaN(p.Value) && p.Value != VValue).Max(p => p.Position.y);
            }
        }
        public class GetImageXYZProfile
        {
            public ImageProfile getXprofile { get; set; }
            public ImageProfile getYprofile { get; set; }
            public ImageProfile getZprofile { get; set; }
            public void TakeProfile(Image SI, double A1, double A2, double B, double C)
            {
                VVector StartX = new VVector(A1, B, C);
                VVector StopX = new VVector(A2, B, C);
                double[] PreallocatedBufferX = new double[1000];
                getXprofile = SI.GetImageProfile(StartX, StopX, PreallocatedBufferX);

                VVector StartY = new VVector(B, A1, C);
                VVector StopY = new VVector(B, A2, C);
                double[] PreallocatedBufferY = new double[1000];
                getYprofile = SI.GetImageProfile(StartY, StopY, PreallocatedBufferY);

                VVector StartZ = new VVector(B, C, A1);
                VVector StopZ = new VVector(B, C, A2);
                double[] PreallocatedBufferZ = new double[1000];
                getZprofile = SI.GetImageProfile(StartZ, StopZ, PreallocatedBufferZ);
            }
        }
    }
}
