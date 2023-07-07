using System;
using System.Linq;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace CouchInsert
{
    public class FindImageCenter
    {
        public double originX { get; set; }
        public double originY { get; set; }
        public double Xcenter { get; set; }
        public double Ycenter { get; set; }
        public double AutoZmin { get; set; }
        public double userOriginZ { get; set; }
        public double Xborder { get; set; }
        public double Yborder { get; set; }
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }
        public ImageProfile YProfile { get; set; }
        public void FinadImageCenter(Image image, double YchkOrientation)
        {
            originX = image.UserOrigin.x;//SI.Origin.x + (SI.XRes * SI.XSize / 2);
            originY = image.UserOrigin.y;//SI.Origin.y + chkOrientation*(SI.YRes * SI.YSize / 2);
            userOriginZ = image.UserOrigin.z; //SI.Origin.z + (SI.ZRes * SI.ZSize / 2);
            VVector Start = new VVector(originX + 700, originY, userOriginZ);
            VVector Stop = new VVector(originX - 700, originY, userOriginZ);
            double[] PreallocatedBuffer = new double[1000];
            ImageProfile XProfile = image.GetImageProfile(Start, Stop, PreallocatedBuffer);
            X2 = XProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Position.x);
            X1 = XProfile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Position.x);
            Xcenter = (X2 + X1) / 2;
            Xborder = Math.Abs(X2 - X1);

            //Find center Y
            VVector YStart = new VVector(Xcenter, 700 + originY, userOriginZ);
            VVector YStop = new VVector(Xcenter, -700 + originY, userOriginZ);
            double[] YPreallocatedBuffer = new double[1000];
            YProfile = image.GetImageProfile(YStart, YStop, YPreallocatedBuffer);
            Y2 = YProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Position.y);
            Y1 = YProfile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Position.y);
            Ycenter = (Y2 + Y1) / 2;
            Yborder = Math.Abs(Y2 - Y1);

            //Find min Z
            Start = new VVector(Xcenter, Ycenter, userOriginZ - 1500);
            Stop = new VVector(Xcenter, Ycenter, userOriginZ);
            double[] ZPreallocatedBuffer = new double[1000];
            ImageProfile ZProfile = image.GetImageProfile(Start, Stop, ZPreallocatedBuffer);
            double AutoZmin = ZProfile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Position.z);
        }
    }
}
