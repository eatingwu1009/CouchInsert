using System.Collections.Generic;
using VMS.TPS.Common.Model.Types;

namespace CouchInsert
{
    public class MarkerWPositions
    {
        public string Markers { get; set; }
        public List<VVector> Positions { get; set; }
    }
}
