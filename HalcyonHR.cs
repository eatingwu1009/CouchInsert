using System.Linq;
using VMS.TPS.Common.Model.API;

namespace CouchInsert
{
    public class HalcyonHR
    {
        public void ConvertHR(StructureSet SS, string Type)
        {
            foreach (Structure st in SS.Structures.Where(s => s.DicomType == Type))
            {
                if (st.CanConvertToHighResolution() == true)
                {
                    st.ConvertToHighResolution();
                }
            }
        }
    }
}
