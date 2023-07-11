using System;
using VMS.TPS.Common.Model.API;

namespace CouchInsert
{
    public class SliceConverter
    {
        public int GetSlice(StructureSet SS, double z)
        {
            var imageRes = SS.Image.ZRes;
            return Convert.ToInt32((z - SS.Image.Origin.z) / imageRes);
        }
    }
}
