using System.IO;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace CouchInsert
{
    public class ModelBuilder
    {
        public void SaveModel(ScriptContext scriptContext, Structure structure, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < scriptContext.Image.ZSize; i++)
                {
                    foreach (VVector[] vectors in structure.GetContoursOnImagePlane(i))
                    {
                        foreach (VVector vector in vectors) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                    }
                }
            }
        }
    }
}