using System.Linq;
using VMS.TPS.Common.Model.API;

namespace CouchInsert
{
    public class StructureHR
    {
        public void ConvertHRType(StructureSet SS, string Type)
        {
            foreach (Structure st in SS.Structures.Where(s => s.DicomType == Type))
            {
                if (st.CanConvertToHighResolution() == true)
                {
                    st.ConvertToHighResolution();
                }
            }
        }
        public void ConvertHRId(StructureSet SS, string Id)
        {
            foreach (Structure st in SS.Structures.Where(s => s.Id == Id))
            {
                if (st.CanConvertToHighResolution() == true)
                {
                    st.ConvertToHighResolution();
                }
            }
        }
    }
}
