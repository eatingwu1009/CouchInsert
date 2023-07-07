using VMS.TPS.Common.Model.Types;

namespace CouchInsert
{
    public class ChkOrientation
    {
        public double XchkOrientation { get; set; }
        public double YchkOrientation { get; set; }
        public double ZchkOrientation { get; set; }
        //Compare to HFS Orientation
        public void XYZOrientation(PatientOrientation orientation)
        {
            if (orientation == PatientOrientation.HeadFirstSupine | orientation == PatientOrientation.FeetFirstSupine | orientation == PatientOrientation.Sitting) YchkOrientation = 1;
            else if (orientation == PatientOrientation.HeadFirstProne | orientation == PatientOrientation.FeetFirstProne) YchkOrientation = -1;
            else System.Windows.MessageBox.Show("This CT image Orientation is not supported : No Orientation or Decubitus");

            if (orientation == PatientOrientation.FeetFirstSupine | orientation == PatientOrientation.HeadFirstProne) XchkOrientation = -1;
            else XchkOrientation = 1;

            if (orientation == PatientOrientation.FeetFirstSupine | orientation == PatientOrientation.FeetFirstProne) ZchkOrientation = -1;
            else ZchkOrientation = 1;
        }
    }
}
