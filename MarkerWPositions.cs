using System.Collections.Generic;
using VMS.TPS.Common.Model.Types;

namespace CouchInsert
{
    public class MarkerWPositions
    {
        public string Markers { get; set; }
        public List<VVector> Positions { get; set; }
    }
    public class LineProfile
    {
        public VVector position { get; set; }
        public double slope { get; set; }
    }
}
