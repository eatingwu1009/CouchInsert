using System;
using System.Diagnostics;
using System.Windows.Interactivity;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Xml;
using System.Reflection;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Text.RegularExpressions;
using System.Windows.Media;
using static System.Windows.Forms.LinkLabel;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;
using System.Windows.Documents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Controls;

namespace CouchInsert
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
        public List<string> MarkerNames { get; }
        public List<string> MarkerPositions { get; }

        private int _currentProgress;
        public int CurrentProgress
        {
            get => _currentProgress;
            set
            {
                _currentProgress = value;
                NotifyPropertyChanged(nameof(CurrentProgress));
            }
        }

        private Visibility _progressVisibility;
        public Visibility ProgressVisibility
        {
            get => _progressVisibility;
            set
            {
                _progressVisibility = value;
                NotifyPropertyChanged(nameof(ProgressVisibility));
            }
        }


        private string _selectedMarkerName;
        public string SelectedMarkerName
        {
            get => _selectedMarkerName;
            set
            {
                _selectedMarkerName = value;
                NotifyPropertyChanged(nameof(SelectedMarkerName));
            }
        }
        private string _selectedMarkerPosition;
        public string SelectedMarkerPosition
        {
            get => _selectedMarkerPosition;
            set
            {
                _selectedMarkerPosition = value;
                NotifyPropertyChanged(nameof(SelectedMarkerPosition));
            }
        }

        private double _markerLocationX;
        public double MarkerLocationX
        {
            get => _markerLocationX;
            set
            {
                _markerLocationX = value;
                NotifyPropertyChanged(nameof(MarkerLocationX));
            }
        }
        private double _markerLocationY;
        public double MarkerLocationY
        {
            get => _markerLocationY;
            set
            {
                _markerLocationY = value;
                NotifyPropertyChanged(nameof(MarkerLocationY));
            }
        }
        private double _markerLocationZ;
        public double MarkerLocationZ
        {
            get => _markerLocationZ;
            set
            {
                _markerLocationZ = value;
                NotifyPropertyChanged(nameof(MarkerLocationZ));
            }
        }

        private double _finalYcenter;
        public double FinalYcenter
        {
            get => _finalYcenter;
            set
            {
                _finalYcenter = value;
                NotifyPropertyChanged(nameof(FinalYcenter));
            }
        }

        private String _calculateBBLocation;
        public String CalculateBBLocation
        {
            get => _calculateBBLocation;
            set
            {
                _calculateBBLocation = value;
                NotifyPropertyChanged(nameof(CalculateBBLocation));
            }
        }


        private VVector _start;
        public VVector Start
        {
            get => _start;
            set
            {
                _start = value;
                NotifyPropertyChanged(nameof(Start));
            }
        }

        private VVector _stop;
        public VVector Stop
        {
            get => _stop;
            set
            {
                _stop = value;
                NotifyPropertyChanged(nameof(Stop));
            }
        }

        private Structure _markerLocationItem;
        public Structure MarkerLocationItem
        {
            get => _markerLocationItem;
            set
            {
                _markerLocationItem = value;
                NotifyPropertyChanged(nameof(MarkerLocationItem));
            }
        }

        private String _filePath;
        public String FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                NotifyPropertyChanged(nameof(FilePath));
            }
        }

        private String _sliceThickness;
        public String SliceThickness
        {
            get => _sliceThickness;
            set
            {
                _sliceThickness = value;
                NotifyPropertyChanged(nameof(SliceThickness));
            }
        }


        private String _filePathCI;
        public String FilePathCI
        {
            get => _filePathCI;
            set
            {
                _filePathCI = value;
                NotifyPropertyChanged(nameof(FilePathCI));
            }
        }

        private String _filePathCS;
        public String FilePathCS
        {
            get => _filePathCS;
            set
            {
                _filePathCS = value;
                NotifyPropertyChanged(nameof(FilePathCS));
            }
        }

        private String _filePathCSI;
        public String FilePathCSI
        {
            get => _filePathCSI;
            set
            {
                _filePathCSI = value;
                NotifyPropertyChanged(nameof(FilePathCSI));
            }
        }

        private String _filePathCSS;
        public String FilePathCSS
        {
            get => _filePathCSS;
            set
            {
                _filePathCSS = value;
                NotifyPropertyChanged(nameof(FilePathCSS));
            }
        }

        private String _modelfilePathCI;
        public String ModelFilePathCI
        {
            get => _modelfilePathCI;
            set
            {
                _modelfilePathCI = value;
                NotifyPropertyChanged(nameof(ModelFilePathCI));
            }
        }

        private String _modelfilePathCS;
        public String ModelFilePathCS
        {
            get => _modelfilePathCS;
            set
            {
                _modelfilePathCS = value;
                NotifyPropertyChanged(nameof(FilePathCS));
            }
        }

        private String _modelfilePathCSI;
        public String ModelFilePathCSI
        {
            get => _modelfilePathCSI;
            set
            {
                _modelfilePathCSI = value;
                NotifyPropertyChanged(nameof(ModelFilePathCSI));
            }
        }

        private String _modelfilePathCSS;
        public String ModelFilePathCSS
        {
            get => _modelfilePathCSS;
            set
            {
                _modelfilePathCSS = value;
                NotifyPropertyChanged(nameof(ModelFilePathCSS));
            }
        }

        private String _filePathAxis;
        public String ModelFilePathBasic
        {
            get => _filePathAxis;
            set
            {
                _filePathAxis = value;
                NotifyPropertyChanged(nameof(ModelFilePathBasic));
            }
        }

        private double[] _alignment;
        public double[] Alignment
        {
            get => _alignment;
            set
            {
                _alignment = value;
                NotifyPropertyChanged(nameof(Alignment));
            }
        }

        public ScriptContext ScriptContext { get; }
        public StructureSet StructureSet { get; }
        public VVector SIU { get; }

        private ImageProfile _xProfile;
        public ImageProfile XProfile
        {
            get => _xProfile;
            set
            {
                _xProfile = value;
                NotifyPropertyChanged(nameof(XProfile));
            }
        }

        private double[] _preallocatedBuffer;
        public double[] PreallocatedBuffer
        {
            get => _preallocatedBuffer;
            set
            {
                _preallocatedBuffer = value;
                NotifyPropertyChanged(nameof(PreallocatedBuffer));
            }
        }

        private double _multiple;
        public double Multiple
        {
            get => _multiple;
            set
            {
                _multiple = value;
                NotifyPropertyChanged(nameof(Multiple));
            }
        }

        private Structure _xyz;
        public Structure XYZ
        {
            get => _xyz;
            set
            {
                _xyz = value;
                NotifyPropertyChanged(nameof(XYZ));
            }
        }

        private Structure _couchInterior;
        public Structure CouchInterior
        {
            get => _couchInterior;
            set
            {
                _couchInterior = value;
                NotifyPropertyChanged(nameof(CouchInterior));
            }
        }

        private Structure _couchSurface;
        public Structure CouchSurface
        {
            get => _couchSurface;
            set
            {
                _couchSurface = value;
                NotifyPropertyChanged(nameof(CouchSurface));
            }
        }

        private Structure _crossInterior;
        public Structure CrossInterior
        {
            get => _crossInterior;
            set
            {
                _crossInterior = value;
                NotifyPropertyChanged(nameof(CrossInterior));
            }
        }

        private Structure _crossSurface;
        public Structure CrossSurface
        {
            get => _crossSurface;
            set
            {
                _crossSurface = value;
                NotifyPropertyChanged(nameof(CrossSurface));
            }
        }

        private Structure _userCI;
        public Structure UserCI
        {
            get => _userCI;
            set
            {
                _userCI = value;
                NotifyPropertyChanged(nameof(UserCI));
            }
        }

        private Structure _userCS;
        public Structure UserCS
        {
            get => _userCS;
            set
            {
                _userCS = value;
                NotifyPropertyChanged(nameof(UserCS));
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                System.Windows.MessageBox.Show($"You are using Halcyon Couch : {_isChecked}");
                //System.Windows.Forms.MessageBox.Show($"{_isChecked}", "Halcyon Couch", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool _canStartProcessing;
        public bool CanStartProcessing
        {
            get => _canStartProcessing;
            set
            {
                _canStartProcessing = value;
                NotifyPropertyChanged(nameof(CanStartProcessing));
            }
        }

        private String _filFolder;
        public String FileFolder
        {
            get => _filFolder;
            set
            {
                _filFolder = value;
                NotifyPropertyChanged(nameof(FileFolder));
            }
        }

        private String _userCouchCIName;
        public String UserCouchCIName
        {
            get => _userCouchCIName;
            set
            {
                _userCouchCIName = value;
                NotifyPropertyChanged(nameof(UserCouchCIName));
            }
        }

        private String _userCouchCSName;
        public String UserCouchCSName
        {
            get => _userCouchCSName;
            set
            {
                _userCouchCSName = value;
                NotifyPropertyChanged(nameof(UserCouchCSName));
            }
        }
        private bool _halcyon;
        public bool Halcyon
        {
            get => _halcyon;
            set
            {
                _halcyon = value;
                NotifyPropertyChanged(nameof(Halcyon));
            }
        }

        public double HSpace { get; set; }
        public double XBaseAxis { get; set; }
        public double YBaseAxis { get; set; }
        public double ZBaseAxis { get; set; }
        public double CSHU { get; set; }
        public double CIHU { get; set; }
        public double chkOrientation { get; set; }
        public double XchkOrientation { get; set; }
        public PatientOrientation orientation { get; set; }
        public VMS.TPS.Common.Model.API.Image SI { get; set; }

        //This Code is based on the Model slice thickness = 1mm and the minimun interpolate resolution is 0.5mm
        public MainViewModel() { }
        public MainViewModel(ScriptContext scriptContext)
        {
            VVector SIU = scriptContext.Image.UserOrigin;
            SI = scriptContext.Image;
            orientation = scriptContext.Image.ImagingOrientation;
            if (orientation == PatientOrientation.HeadFirstSupine | orientation == PatientOrientation.FeetFirstSupine | orientation == PatientOrientation.Sitting) chkOrientation = 1;
            else if (orientation == PatientOrientation.HeadFirstProne | orientation == PatientOrientation.FeetFirstProne) chkOrientation = -1;
            else System.Windows.MessageBox.Show("This CT image Orientation is not supported : No Orientation or Decubitus");

            if (orientation == PatientOrientation.FeetFirstSupine | orientation == PatientOrientation.FeetFirstProne) XchkOrientation = -1;
            else XchkOrientation = 1;

            Halcyon = false;
            if (IsChecked == true) { Halcyon = true; }

            scriptContext.Patient.BeginModifications();
            StructureSet SS = scriptContext.StructureSet;
            if (SS is null)
            {
                SS = SI.CreateNewStructureSet();
                SS.Id = SI.Id;
            }
            //Structure Marker = SS.Structures.FirstOrDefault(e => e.Id == "Marker");
            //double final = Marker.CenterPoint.y
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

                    _Start = new VVector(-50 + Xcenter, FinalYcenter + chkOrientation, originZ);
                    _Stop = new VVector(50 + Xcenter, FinalYcenter + chkOrientation, originZ);
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

                    if (Brn1 < -600 && Brn2 < -600 && (Brn3 <= -850 && Brn3 >= -950) && Brn4 < -600)
                    { chkBrain2 = true; }

                    double CouchBorder1 = Math.Round(VVector.Distance(Couch1, Couch2));
                    double CouchBorder2 = Math.Round(VVector.Distance(Couch3, Couch4));
                    if (CouchBorder1 < CouchBorder2)
                    {
                        CouchBorder1 = Math.Round(VVector.Distance(Couch3, Couch4));
                        CouchBorder2 = Math.Round(VVector.Distance(Couch1, Couch2));
                    }
                    if (((CouchBorder1 >= 495 && CouchBorder1 <= 515) && (CouchBorder2 >= 495 && CouchBorder2 <= 515) && (chkHeight > -650 | chkBrain2 == true)) | (chkBrain == true && chkBrain2 == true)) break;
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
            FinalYcenter = FinalYcenter - 0.4 * chkOrientation;// = MarkerLocationYY
            List<string> Zposition = new List<string>();
            for (double zaxis = originZ - 150; zaxis < originZ + 150; zaxis++)
            {
                VVector AutoStart = new VVector(Xcenter + 10 * XchkOrientation, FinalYcenter, zaxis);
                VVector AutoStop = new VVector(Xcenter - 60 * XchkOrientation, FinalYcenter, zaxis);
                double[] AutoPreallocatedBuffer = new double[1000];
                ImageProfile AutoXProfile = SI.GetImageProfile(AutoStart, AutoStop, AutoPreallocatedBuffer);
                if (PeakDetect(AutoXProfile) != "") { Zposition.Add(PeakDetect(AutoXProfile) + "," + zaxis); }
            }
            if (Zposition.Count == 0) CanStartProcessing = false;
            else CanStartProcessing = true;

            //OriginalProtonCouch           
            if (scriptContext.StructureSet.Structures.Where(s => s.DicomType == "MARKER").FirstOrDefault() != null)
            {
                ScriptContext = scriptContext;
                SIU = scriptContext.Image.UserOrigin;
                StructureSet = scriptContext.StructureSet;
                SliceThickness = ScriptContext.Image.ZRes.ToString() + "mm";
                Multiple = ScriptContext.Image.ZRes;
                MarkerNames = new List<String>();
                foreach (Structure Iso in StructureSet.Structures.Where(s => s.DicomType == "MARKER").ToList())
                {
                    MarkerNames.Add(Iso.Id);
                }
                PositionRenew();
            }
            else throw new Exception("There is no marker.  At least one marker DICOM type is required.");

            CouchInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchInterior");
            CouchSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchSurface");
            CrossInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CrossInterior");
            CrossSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CrossSurface");

            MarkerPositions = new List<String>();
            MarkerPositions.Add("H5");
            MarkerPositions.Add("H4");
            MarkerPositions.Add("H3");
            MarkerPositions.Add("H2");
            MarkerPositions.Add("H1");
            MarkerPositions.Add("0");

            string[] Basiclines = File.ReadAllLines(@"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\PathInformation.csv");
            FileFolder = Basiclines[0].ToString() + SliceThickness;
            //FilePathCI_point = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CouchInterior.csv";
            //FilePathCI_vector = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CouchInterior.csv";
            //FilePathCI_TI = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\CouchInterior.csv";
            string FilePathBasic = System.IO.Path.Combine(new string[] { FileFolder, "BasicInformation.csv" });
            Basiclines = File.ReadAllLines(FilePathBasic);
            List<string> sourceAxis = Basiclines[1].Trim().Split(',').Select(s => s.Trim()).ToList();
            HSpace = Double.Parse(sourceAxis[0]);
            XBaseAxis = Double.Parse(sourceAxis[1]);
            YBaseAxis = Double.Parse(sourceAxis[2]);
            ZBaseAxis = Double.Parse(sourceAxis[3]);
            CSHU = Double.Parse(sourceAxis[4]);
            CIHU = Double.Parse(sourceAxis[5]);
            UserCouchCSName = sourceAxis[6].ToString();
            UserCouchCIName = sourceAxis[7].ToString();
            UserCS = StructureSet.Structures.FirstOrDefault(e => e.Id == UserCouchCSName);
            UserCI = StructureSet.Structures.FirstOrDefault(e => e.Id == UserCouchCIName);
        }

        public ICommand PositionRenewCommand { get => new Command(PositionRenew); }
        private void PositionRenew()
        {
            string markerId = "Marker";
            if (SelectedMarkerName != null) markerId = SelectedMarkerName;
            MarkerLocationItem = StructureSet.Structures.Where(s => s.DicomType == "MARKER").ToList().Where(a => a.Id == markerId).FirstOrDefault();

            List<string> MarkerNames = new List<string>();
            if (MarkerLocationItem != null)
            {
                MarkerNames.Clear();
                MarkerLocationX = MarkerLocationItem.CenterPoint.x;
                MarkerLocationY = MarkerLocationItem.CenterPoint.y;
                MarkerLocationZ = MarkerLocationItem.CenterPoint.z;
                for (double zzz = MarkerLocationZ - 0.5; zzz < MarkerLocationZ + 0.5; zzz += 0.1)
                {
                    Start = new VVector(MarkerLocationX + 5 * XchkOrientation, MarkerLocationY, zzz);
                    Stop = new VVector(MarkerLocationX - 60 * XchkOrientation, MarkerLocationY, zzz);
                    PreallocatedBuffer = new double[100];
                    XProfile = ScriptContext.Image.GetImageProfile(Start, Stop, PreallocatedBuffer);
                    if (PeakDetect(XProfile) != "") { MarkerNames.Add(PeakDetect(XProfile)); }
                }
                SelectedMarkerPosition = MarkerNames.GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => (string)x.Key).FirstOrDefault();//Find the Mode
                //SelectedMarkerPosition = PeakDetect(XProfile);
            }
        }

        public ICommand ButtonCommand_AddCouch { get => new Command(AddCouch); }
        private void AddCouch()
        {
            ScriptContext.Patient.BeginModifications();
            StructureSet SS = ScriptContext.StructureSet;
            if (SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL") is null | SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL").Volume == 0)
            {
                var BodyPar = SS.GetDefaultSearchBodyParameters();
                BodyPar.KeepLargestParts = false;
                SS.CreateAndSearchBody(BodyPar);
            }
            ProgressVisibility = Visibility.Visible;
            DateTime dateTime = DateTime.Now;
            //string filePathOuter = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\contour.csv";
            //if (!File.Exists(filePathOuter))
            //{
            //    System.Windows.MessageBox.Show($"No file exists at path {filePathOuter}");
            //    return;
            //}

            FilePathCI = System.IO.Path.Combine(new string[] { FileFolder, "CouchInterior.csv" });
            FilePathCS = System.IO.Path.Combine(new string[] { FileFolder, "CouchSurface.csv" });
            FilePathCSI = System.IO.Path.Combine(new string[] { FileFolder, "CrossInterior.csv" });
            FilePathCSS = System.IO.Path.Combine(new string[] { FileFolder, "CrossSurface.csv" });

            CurrentProgress = 5;

            List<VVector> NewVVector = new List<VVector>();
            List<VVector> CSVVector = new List<VVector>();
            string[] TempFilelines = File.ReadAllLines(FilePathCSS);
            {
                foreach (string line in TempFilelines)
                {
                    string[] splitLine = line.Split(',');
                    double x = Double.Parse(splitLine[0].Trim());
                    double y = Double.Parse(splitLine[1].Trim());
                    double z = Double.Parse(splitLine[2].Trim());
                    CSVVector.Add(new VVector(x, y, z));
                }
            }
            double MMX = MaxMinDetect(CSVVector, orientation)[0]; double MMY = MaxMinDetect(CSVVector, orientation)[1]; double MMZ = MaxMinDetect(CSVVector, orientation)[2];

            if (CrossSurface != null) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossSurface"));
            CrossSurface = ScriptContext.StructureSet.AddStructure("CONTROL", "CrossSurface");
            //if (IsChecked == true) { if (CrossSurface.CanConvertToHighResolution()) CrossSurface.ConvertToHighResolution(); }
            if (CrossSurface.CanConvertToHighResolution()) CrossSurface.ConvertToHighResolution();


            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ);
                NewVVector.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
            }
            for (int i = 0; i < NewVVector.Max(p => p.z) + 1; i++)
            {
                CrossSurface.AddContourOnImagePlane(NewVVector.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);
                //Convert.ToInt32(NewVVector.FirstOrDefault().z)
            }
            CurrentProgress = 15;

            Array.Clear(TempFilelines, 0, TempFilelines.Length);
            CSVVector.Clear();
            TempFilelines = File.ReadAllLines(FilePathCSI);
            {
                foreach (string line in TempFilelines)
                {
                    string[] splitLine = line.Split(',');
                    double x = Double.Parse(splitLine[0].Trim());
                    double y = Double.Parse(splitLine[1].Trim());
                    double z = Double.Parse(splitLine[2].Trim());
                    CSVVector.Add(new VVector(x, y, z));
                }
            }

            if (CrossInterior != null) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossInterior"));
            CrossInterior = ScriptContext.StructureSet.AddStructure("CONTROL", "CrossInterior");
            //if (IsChecked == true) { if (CrossInterior.CanConvertToHighResolution()) CrossInterior.ConvertToHighResolution(); }
            if (CrossInterior.CanConvertToHighResolution()) CrossInterior.ConvertToHighResolution();

            NewVVector.Clear();
            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ);
                NewVVector.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
            }
            for (int i = 0; i < NewVVector.Max(p => p.z) + 1; i++)
            {
                CrossInterior.AddContourOnImagePlane(NewVVector.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);
            }


            CurrentProgress = 25;
            //CrossSurface.SegmentVolume = CrossSurface.SegmentVolume.Sub(CrossInterior.SegmentVolume);

            //string[] filelines3 = File.ReadAllLines(FilePathCI_TI);
            //{
            //    foreach (string line in filelines3)
            //    {
            //        Int32 x = Int32.Parse(line.Trim());
            //        XYZ.MeshGeometry.TriangleIndices.Add(x);
            //    }
            //}
            if (UserCS != null) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == UserCouchCSName));
            UserCS = ScriptContext.StructureSet.AddStructure("CONTROL", UserCouchCSName);
            //if (IsChecked  == true) { if (UserCS.CanConvertToHighResolution()) UserCS.ConvertToHighResolution(); }
            if (UserCS.CanConvertToHighResolution()) UserCS.ConvertToHighResolution();

            Array.Clear(TempFilelines, 0, TempFilelines.Length);
            CSVVector.Clear();
            TempFilelines = File.ReadAllLines(FilePathCS);
            {
                foreach (string line in TempFilelines)
                {
                    string[] splitLine = line.Split(',');
                    double x = Double.Parse(splitLine[0].Trim());
                    double y = Double.Parse(splitLine[1].Trim());
                    double z = Double.Parse(splitLine[2].Trim());
                    CSVVector.Add(new VVector(x, y, z));
                }
            }

            NewVVector.Clear();
            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ);
                NewVVector.Add(new VVector(vv.x, vv.y, vv.z));
            }
            //using (StreamWriter writer = new StreamWriter(@"C: \Users\aria\Downloads\Interpolation\NewVVector.csv"))
            //{
            //    foreach (VVector vector in NewVVector) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
            //}
            //double OriginZ = Math.Round(ScriptContext.Image.Origin.z, 1, MidpointRounding.AwayFromZero);
            //bool is_integer = unchecked(CheckSlice == Multiple);
            double OriginZ = NewVVector.Min(p => p.z);
            List<VVector> ForInterpolate = new List<VVector>();
            List<VVector> ForInterpolate1 = new List<VVector>();
            foreach (VVector vec in NewVVector.Where(p => ((p.z - OriginZ) / Multiple) == (int)((p.z - OriginZ) / Multiple)))
            {
                ForInterpolate.Add(new VVector(vec.x, vec.y, Convert.ToInt32(vec.z / Multiple)));
            }

            List<double> CheckArray = new List<double>();
            CheckArray = ForInterpolate.Select(x => x.z).Distinct().ToList();
            double CheckSlice = Math.Abs(CheckArray[CheckArray.Count - 1] - CheckArray[CheckArray.Count - 2]);

            int a = Convert.ToInt32(ForInterpolate.Min(p => p.z));
            int b = Convert.ToInt32(ForInterpolate.Max(p => p.z));
            List<VVector> Loop = new List<VVector>();
            for (int i = a; i <= b; i += Convert.ToInt32(CheckSlice))
            {
                //if (CheckSlice == 1)
                Loop.Clear();
                {
                    foreach (VVector vec in ForInterpolate.Where(vv => vv.z.Equals(i)))
                    {
                        Loop.Add(vec);
                    }
                }
                //else
                //{
                //    if ((i * Multiple + OriginZ)  % CheckSlice == 0 )
                //    {
                //        foreach (VVector vec in NewVVector.Where(vv => vv.z.Equals(i)))
                //        {
                //            Loop.Add(vec);
                //        }
                //    }
                //    else
                //    {
                //        //ForInterpolate.Clear();
                //        //ForInterpolate1.Clear();
                //        //foreach (VVector vec in NewVVector.Where(vv => vv.z.Equals(i-1)))
                //        //{
                //        //    ForInterpolate.Add(vec);
                //        //}
                //        //foreach (VVector vec in NewVVector.Where(vv => vv.z.Equals(i+1)))
                //        //{
                //        //    ForInterpolate1.Add(vec);
                //        //}
                //        //int MinInterpolateCount = Math.Min(Convert.ToInt32(ForInterpolate.Count()), Convert.ToInt32(ForInterpolate1.Count()));
                //        //{
                //        //    for (int index = 0; index < MinInterpolateCount - 1; index++)
                //        //    {
                //        //        Loop.Add(Interpolate(ForInterpolate[index], ForInterpolate1[index]));
                //        //    }
                //        //}
                //    }
                //}
                //using (StreamWriter writer = new StreamWriter(@"C: \Users\aria\Downloads\Interpolation\Loop" + Loop.Min(p => p.z)+ ".csv"))
                //{
                //    foreach (VVector vector in Loop) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                //}
                UserCS.AddContourOnImagePlane(Loop.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);
            }
            CurrentProgress = 85;

            Array.Clear(TempFilelines, 0, TempFilelines.Length);
            TempFilelines = File.ReadAllLines(FilePathCI);
            CSVVector.Clear();
            foreach (string line in TempFilelines)
            {
                string[] splitLine = line.Split(',');
                double x = Double.Parse(splitLine[0].Trim());
                double y = Double.Parse(splitLine[1].Trim());
                double z = Double.Parse(splitLine[2].Trim());
                CSVVector.Add(new VVector(x, y, z));
            }

            if (UserCI != null) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == UserCouchCIName));
            UserCI = ScriptContext.StructureSet.AddStructure("CONTROL", UserCouchCIName);
            //if (IsChecked == true) { if (UserCI.CanConvertToHighResolution()) UserCI.ConvertToHighResolution(); }
            if (UserCI.CanConvertToHighResolution()) UserCI.ConvertToHighResolution();

            NewVVector.Clear();
            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ);
                NewVVector.Add(new VVector(vv.x, vv.y, vv.z));
            }
            ForInterpolate.Clear();
            foreach (VVector vec in NewVVector.Where(p => ((p.z - OriginZ) / Multiple) == (int)((p.z - OriginZ) / Multiple)))
            {
                ForInterpolate.Add(new VVector(vec.x, vec.y, Convert.ToInt32(vec.z / Multiple)));
            }
            CheckArray.Clear();
            CheckArray = ForInterpolate.Select(x => x.z).Distinct().ToList();
            CheckSlice = Math.Abs(CheckArray[CheckArray.Count - 1] - CheckArray[CheckArray.Count - 2]);
            a = Convert.ToInt32(ForInterpolate.Min(p => p.z));
            b = Convert.ToInt32(ForInterpolate.Max(p => p.z));

            for (int i = a; i <= b; i += Convert.ToInt32(CheckSlice))
            {
                Loop.Clear();
                //if (is_integer == true)
                {
                    foreach (VVector vec in ForInterpolate.Where(vv => vv.z.Equals(i)))
                    {
                        Loop.Add(vec);
                    }
                }
                //else
                //{
                //    if ((i * Multiple + OriginZ) % CheckSlice == 0)
                //    {
                //        foreach (VVector vec in NewVVector.Where(vv => vv.z.Equals(i)))
                //        {
                //            Loop.Add(vec);
                //        }
                //    }
                //    else
                //    {
                //        //ForInterpolate.Clear();
                //        //ForInterpolate1.Clear();
                //        //foreach (VVector vec in NewVVector.Where(vv => vv.z.Equals(i-1)))
                //        //{
                //        //    ForInterpolate.Add(vec);
                //        //}
                //        //foreach (VVector vec in NewVVector.Where(vv => vv.z.Equals(i+1)))
                //        //{
                //        //    ForInterpolate1.Add(vec);
                //        //}
                //        //int MinInterpolateCount = Math.Min(Convert.ToInt32(ForInterpolate.Count()), Convert.ToInt32(ForInterpolate1.Count()));
                //        //{
                //        //    for (int index = 0; index < MinInterpolateCount - 1; index++)
                //        //    {
                //        //        Loop.Add(Interpolate(ForInterpolate[index], ForInterpolate1[index]));
                //        //    }
                //        //}
                //    }
                //}
                UserCI.AddContourOnImagePlane(Loop.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);
            }

            CurrentProgress = 99;
            using (StreamWriter writer = new StreamWriter(@"C: \Users\aria\Downloads\Interpolation\Volume.csv"))
            {
                Structure None = StructureSet.Structures.FirstOrDefault(e => e.Id == "None");
                Structure None1 = StructureSet.Structures.FirstOrDefault(e => e.Id == "None1");
                Structure temp1 = StructureSet.Structures.FirstOrDefault(e => e.Id == "CS_High");
                Structure temp2 = StructureSet.Structures.FirstOrDefault(e => e.Id == "CSS_High");
                if (IsChecked == true)
                {
                    UserCI.SegmentVolume = UserCI.SegmentVolume.Or(CrossInterior.SegmentVolume);
                    UserCS.SegmentVolume = UserCS.SegmentVolume.Or(CrossSurface.SegmentVolume);
                    UserCS.SegmentVolume = UserCS.SegmentVolume.Sub(UserCI.SegmentVolume);
                    if (CheckSlice == 1) { UserCS.SegmentVolume = UserCS.SegmentVolume.Sub(UserCI.SegmentVolume); }
                    UserCI.SetAssignedHU(CIHU);
                    UserCS.SetAssignedHU(CSHU);
                    StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossSurface"));
                    StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossInterior"));
                }
                else
                {
                    UserCI.SegmentVolume = UserCI.SegmentVolume.Or(CrossInterior.SegmentVolume);
                    UserCS.SegmentVolume = UserCS.SegmentVolume.Or(CrossSurface.SegmentVolume);
                    UserCI.SetAssignedHU(CIHU);
                    UserCS.SetAssignedHU(CSHU);
                    StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossSurface"));
                    StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossInterior"));
                }
                DateTime dateTime2 = DateTime.Now;
                TimeSpan dateTime3 = dateTime2.Subtract(dateTime);
                writer.WriteLine(dateTime3.ToString());
                //UserCI.SegmentVolume = UserCI.SegmentVolume.Or(CrossInterior.SegmentVolume);
                //UserCS.SegmentVolume = UserCS.SegmentVolume.Or(CrossSurface.SegmentVolume);
                //if (CheckSlice == 1) { UserCS.SegmentVolume = UserCS.SegmentVolume.Sub(UserCI.SegmentVolume); }
                //UserCI.SetAssignedHU(CIHU);
                //UserCS.SetAssignedHU(CSHU);
                //StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossSurface"));
                //StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossInterior"));
            }

            CurrentProgress = 100;
            //ProgressVisibility = Visibility.Hidden;
        }

        public VVector Interpolate(VVector Input, VVector Next)
        {
            double x = (Input.x + Next.x) / 2;
            double y = (Input.y + Next.y) / 2;
            double z = (Input.z + Next.z) / 2;
            return new VVector(x, y, z);
        }

        //public static void CreateSphere(MeshGeometry3D mesh, double radius, int subdivisions)
        //{
        //    if (mesh.Positions == null) mesh.Positions = new Point3DCollection();
        //    if (mesh.TriangleIndices == null) mesh.TriangleIndices = new Int32Collection();
        //    mesh.Positions.Clear();
        //    mesh.TriangleIndices.Clear();

        //    for (int i = 0; i <= subdivisions; i++)
        //    {
        //        double v = i / (double)subdivisions;
        //        double phi = v * Math.PI;

        //        for (int j = 0; j <= subdivisions; j++)
        //        {
        //            double u = j / (double)subdivisions;
        //            double theta = u * 2 * Math.PI;

        //            double x = Math.Cos(theta) * Math.Sin(phi);
        //            double y = Math.Cos(phi);
        //            double z = Math.Sin(theta) * Math.Sin(phi);

        //            mesh.Positions.Add(new Point3D(radius * x, radius * y, radius * z));
        //        }
        //    }

        //    for (int i = 0; i < subdivisions; i++)
        //    {
        //        for (int j = 0; j < subdivisions; j++)
        //        {
        //            int index1 = i * (subdivisions + 1) + j;
        //            int index2 = index1 + subdivisions + 1;

        //            mesh.TriangleIndices.Add(index1);
        //            mesh.TriangleIndices.Add(index2);
        //            mesh.TriangleIndices.Add(index1 + 1);

        //            mesh.TriangleIndices.Add(index1 + 1);
        //            mesh.TriangleIndices.Add(index2);
        //            mesh.TriangleIndices.Add(index2 + 1);
        //        }
        //    }
        //}
        public ICommand ButtonCommand_CouchBody { get => new Command(CouchBody); }
        private void CouchBody()
        {
            StructureSet SS = ScriptContext.StructureSet;
            ScriptContext SC = ScriptContext;
            bool imageResized = true;
            string erroemessage = null;
            SC.Patient.BeginModifications();
            Structure BODY = SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL");
            if (BODY is null)
            {
                var BodyPar = SS.GetDefaultSearchBodyParameters();
                SS.CreateAndSearchBody(BodyPar);
            }
            if (SS.CanAddCouchStructures(out erroemessage) == true)
            {
                SS.AddCouchStructures("Exact_IGRT_Couch_Top_medium", orientation, RailPosition.In, RailPosition.In, -550, -950, null, out IReadOnlyList<Structure> couchStructureList, out imageResized, out erroemessage);
                Structure CouchSurface = SS.Structures.FirstOrDefault(p => p.Id == "CouchSurface");
                Structure CouchInterior = SS.Structures.FirstOrDefault(p => p.Id == "CouchInterior");
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
                if (Halcyon == true)
                {
                    CouchSurface = SS.AddStructure("SUPPORT", "HalcyonCouchSurface");
                }
                else
                {
                    CouchSurface = SS.AddStructure("SUPPORT", "IGRTCouchSurface");
                }
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
                if (Halcyon == true)
                {
                    CouchInterior = SS.AddStructure("SUPPORT", "HalcyonCouchInterior");
                }
                else
                {
                    CouchInterior = SS.AddStructure("SUPPORT", "IGRTCouchInterior");
                }
                for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                {
                    CouchInterior.AddContourOnImagePlane(CSVVector.Select(v => new VVector(v.x + ShiftX, v.y + ShiftY, v.z)).ToArray(), i);
                }
                CouchSurface.SegmentVolume = CouchSurface.SegmentVolume.Sub(CouchInterior.SegmentVolume);
                CouchSurface.StructureCode = CScode;
                CouchInterior.StructureCode = CIcode;
                if (Halcyon)
                {
                    CouchInterior.SetAssignedHU(-1000);
                    CouchSurface.SetAssignedHU(-300);
                    CouchInterior.Comment = "NTUCC_Halcyon Couch";
                    CouchSurface.Comment = "NTUCC_Halcyon Couch";
                }
                else
                {
                    CouchInterior.SetAssignedHU(-950);
                    CouchSurface.SetAssignedHU(-550);
                    CouchInterior.Comment = "NTUCC_Exact IGRT Couch, medium";
                    CouchSurface.Comment = "NTUCC_Exact IGRT Couch, medium";
                }
                //BODY part
                Structure Temp = SS.AddStructure("CONTROL", "Temp_ForCouch");
                VVector[] TempVec = GetpseudoLine(FinalYcenter, SI.XSize, SI.YSize, chkOrientation);
                for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                {
                    Temp.AddContourOnImagePlane(TempVec, i);
                }
                BODY = SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL");
                BODY.SegmentVolume = BODY.SegmentVolume.Sub(Temp.SegmentVolume);
                SS.RemoveStructure(Temp);
            }
            else if (erroemessage == "Support structures already exist in the structure set.")
            {
                Structure CS = SS.Structures.FirstOrDefault(a => a.Id == "IGRTCouchSurface");
                Structure CI = SS.Structures.FirstOrDefault(a => a.Id == "IGRTCouchInterior");
                if (Halcyon == true)
                {
                    CS = SS.Structures.FirstOrDefault(a => a.Id == "HalcyonCouchSurface");
                    CI = SS.Structures.FirstOrDefault(a => a.Id == "HalcyonCouchInterior");
                }
                //BODY part
                Structure Temp_null = SS.AddStructure("CONTROL", "Temp_ForCouch");
                List<VVector> CSVVector_null = new List<VVector>();
                foreach (VVector[] vectors in CS.GetContoursOnImagePlane(1))
                {
                    foreach (VVector v in vectors)
                    {
                        double x = v.x;
                        double y = v.y;
                        double z = v.z;
                        CSVVector_null.Add(new VVector(x, y, z));
                    }
                }
                VVector[] TempVec_null = GetpseudoLine(MaxMinDetect(CSVVector_null, orientation)[1], SI.XSize, SI.YSize, chkOrientation);
                for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                {
                    Temp_null.AddContourOnImagePlane(TempVec_null, i);
                }
                BODY = SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL");
                BODY.SegmentVolume = BODY.SegmentVolume.Sub(Temp_null.SegmentVolume);
                SS.RemoveStructure(Temp_null);
            }
            else { System.Windows.MessageBox.Show(erroemessage); }
        }
        public ICommand ButtonCommand_FilePath { get => new Command(GetFilePath); }
        private void GetFilePath()
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("You are modifying the File Path", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.Description = "Please Choose the input Folder for Couch Model";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] ModelPathCI = new string[] { dialog.SelectedPath, SliceThickness, "CouchInterior.csv" };
                    string[] ModelPathCS = new string[] { dialog.SelectedPath, SliceThickness, "CouchSurface.csv" };
                    string[] ModelPathCSI = new string[] { dialog.SelectedPath, SliceThickness, "CrossInterior.csv" };
                    string[] ModelPathCSS = new string[] { dialog.SelectedPath, SliceThickness, "CrossSurface.csv" };

                    ModelFilePathCI = System.IO.Path.Combine(ModelPathCI);
                    ModelFilePathCS = System.IO.Path.Combine(ModelPathCS);
                    ModelFilePathCSI = System.IO.Path.Combine(ModelPathCSI);
                    ModelFilePathCSS = System.IO.Path.Combine(ModelPathCSS);

                }
            }
        }

        public ICommand ButtonCommand_BuildModel { get => new Command(BuildModel); }
        private void BuildModel()
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("You are Biulding Model for Couch", "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (CouchInterior != null)
                {
                    //Point3DCollection Points = CouchInterior.MeshGeometry.Positions;
                    //Vector3DCollection Normals = CouchInterior.MeshGeometry.Normals;
                    //PointCollection TextureCoordinate = CouchInterior.MeshGeometry.TextureCoordinates;
                    //System.Windows.Media.Int32Collection TriangleIndices = CouchInterior.MeshGeometry.TriangleIndices;
                    //using (StreamWriter writer = new StreamWriter(FilePathCI_point))
                    //{
                    //    foreach (Point3D v in Points) writer.WriteLine(v.X + "," + v.Y + "," + v.Z);
                    //    //writer.WriteLine();
                    //}
                    //using (StreamWriter writer = new StreamWriter(FilePathCI_TI))
                    //{
                    //    foreach (Int32 t in TriangleIndices) writer.WriteLine(t);
                    //}
                    using (StreamWriter writer = new StreamWriter(ModelFilePathCI))
                    {
                        for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                        {
                            foreach (VVector[] vectors in CouchInterior.GetContoursOnImagePlane(i))
                            {
                                foreach (VVector vector in vectors) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                            }
                        }
                    }
                }
                if (CouchSurface != null)
                {
                    //VVector contours = CouchSurface.MeshGeometry.Points.Select(e => new VVector(e.x, e.y, e.z));
                    using (StreamWriter writer = new StreamWriter(ModelFilePathCS))
                    {
                        for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                        {
                            foreach (VVector[] vectors in CouchSurface.GetContoursOnImagePlane(i))
                            {
                                foreach (VVector vector in vectors) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                            }
                        }
                    }
                }
                if (CrossInterior != null)
                {
                    using (StreamWriter writer = new StreamWriter(ModelFilePathCSI))
                    {
                        for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                        {
                            foreach (VVector[] vectors in CrossInterior.GetContoursOnImagePlane(i))
                            {
                                foreach (VVector vector in vectors) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                            }
                        }
                    }
                }
                if (CrossSurface != null)
                {
                    using (StreamWriter writer = new StreamWriter(ModelFilePathCSS))
                    {
                        for (int i = 0; i < ScriptContext.Image.ZSize; i++)
                        {
                            foreach (VVector[] vectors in CrossSurface.GetContoursOnImagePlane(i))
                            {
                                foreach (VVector vector in vectors) writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                            }
                        }
                    }
                }
            }
        }

        public ICommand ButtonCommand_CheckingBB { get => new Command(CheckingBB); }
        private void CheckingBB()
        {
            double zzz = 0;
            for (int i = 0; i < ScriptContext.Image.ZSize; i++)
            {
                foreach (VVector[] vectors in UserCS.GetContoursOnImagePlane(i))
                {
                    double vs = vectors.FirstOrDefault().z;
                    zzz = Math.Max(0, vs);
                }
            }
            double distanceBB = zzz - MarkerLocationZ;//mm
            CalculateBBLocation = BBCalDetect(distanceBB);
        }
        public VVector[] GetpseudoLine(double yPlane)
        {
            List<VVector> vvectors = new List<VVector>();
            vvectors.Add(new VVector(-ScriptContext.Image.XSize, yPlane, 0));
            vvectors.Add(new VVector(ScriptContext.Image.XSize, yPlane, 0));
            vvectors.Add(new VVector(ScriptContext.Image.XSize, ScriptContext.Image.YSize, 0));
            vvectors.Add(new VVector(-ScriptContext.Image.XSize, ScriptContext.Image.YSize, 0));
            return vvectors.ToArray();
        }

        public String PeakDetect(ImageProfile XProfile)
        {
            List<VVector> Twopoint = new List<VVector>();
            double OverallMaximum = XProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value);
            double OverallMinimum = XProfile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);
            double mean = XProfile.Where(p => !Double.IsNaN(p.Value)).Average(p => p.Value);
            double Maximum = XProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value) - OverallMinimum;
            double HalfTrend = Maximum / 2;
            for (int i = 1; i < XProfile.Count() - 1; i++)
            {
                double iiHU = XProfile[i].Value;
                double ii = XProfile[i].Value - OverallMinimum;
                double iadd = XProfile[i + 1].Value - OverallMinimum;
                double iminus = XProfile[i - 1].Value - OverallMinimum;
                if (!Double.IsNaN(ii) && ii > HalfTrend && (ii > iadd) && (ii > iminus) && iiHU > mean + 100)
                {
                    Twopoint.Add(XProfile[i].Position);
                }
            }
            if (Twopoint.Count > 1)
            {
                double distance = VVector.Distance(Twopoint[0], Twopoint[1]);
                if (Math.Round((distance / 10)) > 0.5)// length unit by mm
                {
                    var map = new Dictionary<int, string>()
                {
                    {1, "H1"},
                    {2, "H2"},
                    {3, "H3"},
                    {4, "H4"},
                    {5, "H5"},
                };
                    string output;
                    return map.TryGetValue(Convert.ToInt32(Math.Round((distance / 10))), out output) ? output : null;
                }
                else return "";
            }
            else if (Twopoint.Count == 1)
            {
                return "0";
            }
            else return "";
        }
        public String BBCalDetect(Double distance)
        {
            var mapBB = new Dictionary<double, string>()
                {
                    {42 + 4, "0"},
                    {28 + 4, "H1"},
                    {14 + 4, "H2"},
                    {4, "H3"},
                    {4 - 14, "H4"},
                    {4 - 28, "H5"},
                };
            string output;
            return mapBB.TryGetValue(Math.Round(distance / 10), out output) ? output : null;
        }
        public VVector AxisAlignment(VVector Original, string LockBarType, double Xmin, double Ymax, double Zmin)
        {
            //The value of YAxis is opposite
            double X = MarkerLocationX - XBaseAxis - Xmin;
            double Y = MarkerLocationY + YBaseAxis - Ymax;
            double Z = MarkerLocationZ - ZBaseAxis - ScriptContext.Image.Origin.z - Zmin;
            //double SSZAdd = Convert.ToInt32((MarkerLocationZZ - ZBaseAxis - ScriptContext.Image.Origin.z) / ScriptContext.Image.ZRes);
            var mapAxis = new Dictionary<string, VVector>()
            {
                {"H5",  new VVector ( Original.x + X, Original.y + Y, (Original.z + Z  - 2 * HSpace))},
                {"H4",  new VVector ( Original.x + X, Original.y + Y, (Original.z + Z - HSpace))},
                {"H3",  new VVector ( Original.x + X, Original.y + Y,(Original.z + Z ))},
                {"H2",  new VVector ( Original.x + X, Original.y + Y, (Original.z + Z  + HSpace))},
                {"H1",  new VVector ( Original.x + X, Original.y + Y, (Original.z + Z  + 2 * HSpace))},
                {"0",  new VVector ( Original.x + X, Original.y + Y, (Original.z + Z + 3 * HSpace))},
            };
            VVector output;
            return mapAxis.TryGetValue(LockBarType, out output) ? output : new VVector(0, 0, 0);
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

        public VVector FindHighestSlope(ImageProfile collection)
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
        public VVector[] GetpseudoLine(double yPlane, double Xsize, double Ysize, double chkorientation)
        {
            List<VVector> vvectors = new List<VVector>();
            double reverse = 1;
            if (yPlane - (chkorientation * Ysize) < 0) { reverse = -1; }
            vvectors.Add(new VVector(-Xsize, yPlane - reverse * 30, 0));
            vvectors.Add(new VVector(Xsize, yPlane - reverse * 30, 0));
            vvectors.Add(new VVector(Xsize, chkorientation * Ysize, 0));
            vvectors.Add(new VVector(-Xsize, chkorientation * Ysize, 0));
            return vvectors.ToArray();
        }
    }
}