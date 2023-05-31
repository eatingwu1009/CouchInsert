﻿using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using CouchInsert;
using System.IO;
using System.ComponentModel;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;

// TODO: Replace the following version attributes by creating AssemblyInfo.cs. You can do this in the properties of the Visual Studio project.
[assembly: AssemblyVersion("1.0.0.1")]
[assembly: AssemblyFileVersion("1.0.0.1")]
[assembly: AssemblyInformationalVersion("1.0")]

// TODO: Uncomment the following line if the script requires write access.
[assembly: ESAPIScript(IsWriteable = true)]
namespace VMS.TPS
{
    public class Script
    {
        public Script()
        {
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Execute(ScriptContext scriptContext, ScriptEnvironment environment)
        {
            // TODO : Add here the code that is called when the script is launched from Eclipse.
            DateTime dateTime1 = DateTime.Now;
            VVector SIU = scriptContext.Image.UserOrigin;
            Image SI = scriptContext.Image;
            double chkOrientation = new double();
            PatientOrientation orientation = scriptContext.Image.ImagingOrientation;
            if (orientation == PatientOrientation.HeadFirstSupine | orientation == PatientOrientation.FeetFirstSupine | orientation == PatientOrientation.Sitting) chkOrientation = 1;
            else if (orientation == PatientOrientation.HeadFirstProne | orientation == PatientOrientation.FeetFirstProne) chkOrientation = -1;
            else MessageBox.Show("This CT image Orientation is not supported : No Orientation or Decubitus");


            //string FileFolder = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\PhtonCouchModel";
            //string FilePathCI = System.IO.Path.Combine(new string[] { FileFolder, "CouchInterior.csv" });
            //string FilePathCS = System.IO.Path.Combine(new string[] { FileFolder, "CouchSurface.csv" });
            scriptContext.Patient.BeginModifications();
            StructureSet SS = scriptContext.StructureSet;
            if (SS is null)
            {
                SS = SI.CreateNewStructureSet();
                SS.Id = SI.Id;
            }
            Structure Marker = SS.Structures.FirstOrDefault(e => e.Id == "Marker");
            double final = Marker.CenterPoint.y;
            //NOTICE : if structureset ID is Not the date same as Image then ESAPI add new structureset but under image date+1

            ////Find the BODY and decide the lowest x,y,z first
            //Structure BODY = SS.Structures.Where(s => s.DicomType == "EXTERNAL").FirstOrDefault();
            //List<VVector> Temp = new List<VVector>();
            //for (int i = 0; i < SI.ZSize; i++)
            //{
            //    foreach (VVector[] vectors in BODY.GetContoursOnImagePlane(i))
            //    {
            //        foreach (VVector v in vectors)
            //        {
            //            Temp.Add(new VVector(v.x, v.y, v.z));
            //        }
            //    }
            //}
            //VVector Ymax = Temp.Where(s => s.y.Equals(Temp.Max(p => p.y))).FirstOrDefault();

            //Find center X
            double originX = SI.UserOrigin.x;//SI.Origin.x + (SI.XRes * SI.XSize / 2);
            double originY = SI.UserOrigin.y;//SI.Origin.y + chkOrientation*(SI.YRes * SI.YSize / 2);
            double originZ = SI.UserOrigin.z; //SI.Origin.z + (SI.ZRes * SI.ZSize / 2);
            VVector Start = new VVector(originX + 700, originY, originZ);
            VVector Stop = new VVector(originX - 700, originY, originZ);
            double[] PreallocatedBuffer = new double[1000];
            ImageProfile XProfile = SI.GetImageProfile(Start, Stop, PreallocatedBuffer);
            double X2 = XProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Position.x);
            double X1 = XProfile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Position.x);
            double Xcenter = (X2 + X1) / 2;
            double Xborder = Math.Abs(X2 - X1);

            //Find center Y
            Start = new VVector(Xcenter, 700 * chkOrientation + originY, originZ);
            Stop = new VVector(Xcenter, -700 * chkOrientation + originY, originZ);
            double[] YPreallocatedBuffer = new double[1000];
            ImageProfile YProfile = SI.GetImageProfile(Start, Stop, YPreallocatedBuffer);
            double Y2 = YProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Position.y);
            double Y1 = YProfile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Position.y);
            double Ycenter = (Y2 + Y1) / 2;
            double Yborder = Math.Abs(X2 - X1);

            //chkBrain
            bool chkBrain, chkBrain2 = new bool(); chkBrain2 = false;
            double BrainBorder1 = YProfile.Where(p => !Double.IsNaN(p.Value) && p.Value != -1000).Min(p => p.Position.y);
            double BrainBorder2 = YProfile.Where(p => !Double.IsNaN(p.Value) && p.Value != -1000).Max(p => p.Position.y);
            if (Math.Abs(BrainBorder1 - BrainBorder2) <= 500) chkBrain = true; else chkBrain = false;

            //Find Y line edge near 53cm
            List<double> YHU_Diff = new List<double>();
            List<double> YLocation = new List<double>();
            VVector __Start = new VVector();
            VVector __Stop = new VVector();
            int a = 1; if (chkBrain == true) a = Convert.ToInt32(BrainBorder1);
            double sum = new double();
            for (int i = a; i < Convert.ToInt32(SI.YSize * SI.YRes / 2); i++)
            {

                __Start = new VVector(X1, chkOrientation * ((SI.YSize * SI.YRes / 2) - (i)) + Ycenter, originZ);//(-SI.XSize * SI.XRes / 2) + Xcenter
                __Stop = new VVector(X2, chkOrientation * ((SI.YSize * SI.YRes / 2) - (i)) + Ycenter, originZ);
                double[] __PreallocatedBuffer = new double[1000];
                ImageProfile __Profile = SI.GetImageProfile(__Start, __Stop, __PreallocatedBuffer);
                sum = 0;
                if (chkBrain == true)
                {
                    foreach (ProfilePoint x in __Profile.Where(p => !Double.IsNaN(p.Value) && (p.Value != -1000)))
                    {
                        sum += Math.Abs(x.Value - (-450));
                    }
                }
                else
                {
                    foreach (ProfilePoint x in __Profile.Where(p => !Double.IsNaN(p.Value)))
                    {
                        sum += Math.Abs(x.Value - (-450));
                    }
                }

                if (sum != 0)
                {
                    YHU_Diff.Add(sum);
                    YLocation.Add(chkOrientation * ((SI.YSize * SI.YRes / 2) - (i)) + Ycenter);
                }
            }
            int index = new int();
            double FinalYcenter, chkHeight, Brn1, Brn2, Brn3, Brn4 = new double(); 
            List<double> BadChk = new List<double>();
            double[] _PreallocatedBuffer = new double[1000];
            double[] _PreallocatedBuffer1 = new double[100];

            //Find the point with the highest slope from centerx, and check the distance near 47cm or 51cm
            VVector Couch1, Couch2, Couch3, Couch4, _Start, _Stop = new VVector();
            index = YHU_Diff.IndexOf(YHU_Diff.Min());
            FinalYcenter = YLocation.ElementAt(index);
            double limit1 = FinalYcenter + chkOrientation * 5;
            double limit2 = FinalYcenter - chkOrientation * 5;

            for (int i = 0; i < 50; i++)
            {
                _Start = new VVector(-275 + Xcenter, FinalYcenter, originZ);
                _Stop = new VVector(0 + Xcenter, FinalYcenter, originZ);
                ImageProfile XProfile3 = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer);
                Couch3 = FindHighestSlope(XProfile3);
                _Start = new VVector(0 + Xcenter, FinalYcenter, originZ);
                _Stop = new VVector(275 + Xcenter, FinalYcenter, originZ);
                ImageProfile XProfile4 = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer);
                Couch4 = FindHighestSlope(XProfile4);
                limit1 = FinalYcenter + chkOrientation * 5; limit2 = FinalYcenter - chkOrientation * 5;
                if (limit1 > limit2) { limit2 = limit1; limit1 = FinalYcenter - chkOrientation * 5; }

                if ((limit2 < Y2) && (limit1 > Y1))
                {
                    _Start = new VVector(-275 + Xcenter, FinalYcenter + chkOrientation * 3, originZ);
                    _Stop = new VVector(0 + Xcenter, FinalYcenter + chkOrientation * 3, originZ);
                    ImageProfile XProfile1 = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer);
                    Couch1 = FindHighestSlope(XProfile1);
                    _Start = new VVector(0 + Xcenter, FinalYcenter + chkOrientation * 3, originZ);
                    _Stop = new VVector(275 + Xcenter, FinalYcenter + chkOrientation * 3, originZ);
                    ImageProfile XProfile2 = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer);
                    Couch2 = FindHighestSlope(XProfile2);

                    _Start = new VVector(-50 + Xcenter, FinalYcenter + chkOrientation , originZ);
                    _Stop = new VVector(50 + Xcenter, FinalYcenter + chkOrientation , originZ);
                    ImageProfile XProfilechk = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer1);
                    chkHeight = XProfilechk[50].Value;

                    _Start = new VVector(-50 + Xcenter, FinalYcenter + chkOrientation * 5, originZ);
                    _Stop = new VVector(50 + Xcenter, FinalYcenter + chkOrientation * 5, originZ);
                    ImageProfile XProfileBrn1 = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer1);
                    Brn1 = XProfileBrn1.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);

                    _Start = new VVector(-250 + Xcenter, FinalYcenter - chkOrientation * 5, originZ);
                    _Stop = new VVector(250 + Xcenter, FinalYcenter - chkOrientation * 5, originZ);
                    ImageProfile XProfileBrn2 = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer1);
                    Brn2 = XProfileBrn2.Where(p => p.Value != -1024).Where(p => p.Value != -1000).Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);

                    _Start = new VVector(-50 + Xcenter, FinalYcenter + chkOrientation * 4, originZ);
                    _Stop = new VVector(50 + Xcenter, FinalYcenter + chkOrientation * 4, originZ);
                    ImageProfile XProfileBrn3 = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer1);
                    Brn3 = XProfileBrn3.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);
  
                    _Start = new VVector(-250 + Xcenter, FinalYcenter - chkOrientation * 4, originZ);
                    _Stop = new VVector(250 + Xcenter, FinalYcenter - chkOrientation * 4, originZ);
                    ImageProfile XProfileBrn4 = SI.GetImageProfile(_Start, _Stop, _PreallocatedBuffer1);
                    Brn4 = XProfileBrn4.Where(p => p.Value != -1024).Where(p => p.Value != -1000).Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);

                    if (Brn1 < -600 &&  Brn2 < -600 && (Brn3 <= -850 && Brn3 >=-950) && Brn4 < -600)
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
            //Add Couch
            FinalYcenter = FinalYcenter - 0.4* chkOrientation;
            bool imageResized = true;
            string errorCouch = "error";
            if (SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL") is null)
            {
                var BodyPar = SS.GetDefaultSearchBodyParameters();
                BodyPar.KeepLargestParts = false;
                SS.CreateAndSearchBody(BodyPar);
            }
            if (SS.CanAddCouchStructures(out errorCouch) == true)
            {
                SS.AddCouchStructures("Exact_IGRT_Couch_Top_medium", orientation, RailPosition.In, RailPosition.In, -500, -950, null, out IReadOnlyList<Structure> couchStructureList, out imageResized, out errorCouch);
                Structure CouchSurface = SS.Structures.FirstOrDefault(e => e.Id == "CouchSurface");
                Structure CouchInterior = SS.Structures.FirstOrDefault(e => e.Id == "CouchInterior");
                StructureCode CScode = CouchSurface.StructureCode;
                StructureCode CIcode = CouchInterior.StructureCode;
                CouchSurface.SegmentVolume = CouchSurface.SegmentVolume.Or(CouchInterior.SegmentVolume);
                List<VVector> CSVVector = new List<VVector>();
                foreach (VVector[] vectors in CouchSurface.GetContoursOnImagePlane(1))
                {
                    foreach (VVector v in vectors)
                    {
                        double x = v.x;
                        double y = v.y;
                        double z = v.z;
                        CSVVector.Add(new VVector(x, y, z));
                    }
                }
                double MMX = MaxMinDetect(CSVVector, orientation)[0]; double MMY = MaxMinDetect(CSVVector, orientation)[1];
                double ShiftX = -265 - MMX;
                double ShiftY = (FinalYcenter) - MMY;

                SS.RemoveStructure(CouchSurface);
                CouchSurface = SS.AddStructure("SUPPORT", "CouchSurface");
                for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                {
                    CouchSurface.AddContourOnImagePlane(CSVVector.Select(v => new VVector(v.x + ShiftX, v.y + ShiftY, v.z)).ToArray(), i);
                }


                CSVVector.Clear();
                foreach (VVector[] vectors in CouchInterior.GetContoursOnImagePlane(1))
                {
                    foreach (VVector v in vectors)
                    {
                        double x = v.x;
                        double y = v.y;
                        double z = v.z;
                        CSVVector.Add(new VVector(x, y, z));
                    }
                }
                SS.RemoveStructure(CouchInterior);
                CouchInterior = SS.AddStructure("SUPPORT", "CouchInterior");
                for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                {
                    CouchInterior.AddContourOnImagePlane(CSVVector.Select(v => new VVector(v.x + ShiftX, v.y + ShiftY, v.z)).ToArray(), i);
                }
                CouchSurface.SegmentVolume = CouchSurface.SegmentVolume.Sub(CouchInterior.SegmentVolume);
                //CouchInterior.SegmentVolume = CouchInterior.AsymmetricMargin(new AxisAlignedMargins(StructureMarginGeometry.Outer, 0,0,0,0, 0.03, 0));
                CouchInterior.SetAssignedHU(-950);
                CouchSurface.SetAssignedHU(-550);
                CouchInterior.Comment = "NTUH_Exact IGRT Couch, medium";
                CouchSurface.Comment = "NTUH_Exact IGRT Couch, medium";
                CouchSurface.StructureCode = CScode;
                CouchInterior.StructureCode = CIcode;

                //BODY part
                Structure Temp = SS.AddStructure("CONTROL", "Temp_ForCouch");
                VVector[] TempVec = GetpseudoLine(FinalYcenter, SI.XSize, SI.YSize, chkOrientation);
                for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                {
                    Temp.AddContourOnImagePlane(TempVec, i);
                }
                Structure BODY = SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL");
                BODY.SegmentVolume = BODY.SegmentVolume.Sub(Temp.SegmentVolume);
                SS.RemoveStructure(Temp);

                using (StreamWriter writer = new StreamWriter(@"C: \Users\aria\Downloads\Interpolation\" + scriptContext.Patient.Id + ".csv"))
                //\Priscilla\API\  //C: \Users\aria\Downloads\Interpolation\
                {
                    Structure OriCS = SS.Structures.FirstOrDefault(e => e.Id == "Ori_CouchSurface");
                    Structure OriCI = SS.Structures.FirstOrDefault(e => e.Id == "Ori_CouchInterior");
                    writer.WriteLine(orientation + "," + CouchSurface.Volume + "," + OriCS.Volume);
                    Structure record = SS.AddStructure("ORGAN", "Record");
                    record.SegmentVolume = CouchSurface.SegmentVolume.And(OriCS);
                    writer.WriteLine(record.Volume + "," + 2 * record.Volume / (CouchSurface.Volume + OriCS.Volume));
                    writer.WriteLine();

                    writer.WriteLine(CouchInterior.Volume + "," + OriCI.Volume);
                    record.SegmentVolume = CouchInterior.SegmentVolume.And(OriCI);
                    writer.WriteLine(record.Volume + "," + 2 * record.Volume / (CouchInterior.Volume + OriCI.Volume));
                    SS.RemoveStructure(record);
                    writer.WriteLine();

                    writer.WriteLine(final - FinalYcenter + "," + SI.ZSize * SI.ZRes + "," + SI.ZRes + "," + SI.ZSize);
                    DateTime dateTime2 = DateTime.Now;
                    TimeSpan dateTime3 = dateTime2.Subtract(dateTime1);
                    writer.WriteLine(dateTime3.ToString() + "," + SI.Series.ImagingDeviceId.ToString());
                }
            }
            else { MessageBox.Show(errorCouch); }

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

        public List<VVector> PeakDetect(ImageProfile Profile)
        {
            List<VVector> Peakpoint = new List<VVector>();
            double OverallMaximum = Profile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value);
            double OverallMinimum = Profile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);

            double Maximum = Profile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value) - OverallMinimum;
            double HalfTrend = Maximum / 2;
            for (int i = 1; i < Profile.Count() - 1; i++)
            {

                double ii = Profile[i].Value - OverallMinimum;
                double iadd = Profile[i + 1].Value - OverallMinimum;
                double iminus = Profile[i - 1].Value - OverallMinimum;
                if (!Double.IsNaN(ii) && ii > HalfTrend && (ii > iadd) && (ii > iminus))
                {
                    Peakpoint.Add(Profile[i].Position);
                }
            }
            return Peakpoint;
        }

        public static double ClosestTo(List<double> collection, double target)
        {
            // NB Method will return int.MaxValue for a sequence containing no elements.
            // Apply any defensive coding here as necessary.
            double closest = new double();
            var minDifference = double.MaxValue;
            foreach (var element in collection)
            {
                var difference = Math.Abs((long)element - target);
                if (minDifference > difference)
                {
                    minDifference = (double)difference;
                    closest = element;
                }
            }
            return closest;
        }

        public static VVector FindHighestSlope(ImageProfile collection)
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

        public static VVector[] GetpseudoLine(double yPlane, double Xsize, double Ysize, double chkorientation)
        {
            List<VVector> vvectors = new List<VVector>();
            double reverse = 1;
            if (yPlane - (chkorientation * Ysize) < 0) {reverse = -1;}
            vvectors.Add(new VVector(-Xsize, yPlane - reverse * 30 , 0));
            vvectors.Add(new VVector(Xsize, yPlane - reverse * 30, 0));
            vvectors.Add(new VVector(Xsize, chkorientation * Ysize, 0));
            vvectors.Add(new VVector(-Xsize, chkorientation * Ysize, 0));
            return vvectors.ToArray();
        }
    }
}
