using System.Collections.Generic;
using System;
using System.Linq;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace CouchInsert
{
    public class StructureModifier
    {
        public Structure FindStructure(StructureSet SS, string name, string Type)
        {
            if (Type == "Id")
            { return SS.Structures.FirstOrDefault(e => e.Id == name); }
            else if (Type == "DicomType")
            { return SS.Structures.FirstOrDefault(e => e.DicomType == name); }
            else { return null; }
        }

        public VVector[] GetpseudoLine(double yStartPlane, double yEndPlane, double Xcenter, double Xsize)
        {
            List<VVector> vvectors = new List<VVector>();
            vvectors.Add(new VVector(Xcenter - Xsize, yStartPlane, 0));
            vvectors.Add(new VVector(Xcenter + Xsize, yStartPlane, 0));
            vvectors.Add(new VVector(Xcenter + Xsize, yEndPlane, 0));
            vvectors.Add(new VVector(Xcenter - Xsize, yEndPlane, 0));
            return vvectors.ToArray();
        }

        public VVector[] GetCircleContours(double xRes, double radius, VVector CircleOrigin)
        {
            List<VVector> vvectors = new List<VVector>();
            for (double x = -1 * radius; x < radius; x += xRes)
            {
                double y = Math.Sqrt(Math.Abs(radius * radius - x * x));
                vvectors.Add(new VVector(x + CircleOrigin.x, y + CircleOrigin.y, 0));
            }

            for (double x = radius; x > -1 * radius; x -= xRes)
            {
                double y = -1 * Math.Sqrt(Math.Abs(radius * radius - x * x));
                vvectors.Add(new VVector(x + CircleOrigin.x, y + CircleOrigin.y, 0));
            }
            return vvectors.ToArray();
        }
    }
}
