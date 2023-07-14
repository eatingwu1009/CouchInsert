using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using System.Windows.Documents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

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
        private List<MarkerWPositions> _automarkerWPosition;
        public List<MarkerWPositions> AutoMarkerWPosition
        {
            get => _automarkerWPosition;
            set
            {
                _automarkerWPosition = value;
                NotifyPropertyChanged(nameof(AutoMarkerWPosition));
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
        private string _markerId;
        public string MarkerId
        {
            get => _markerId;
            set
            {
                _markerId = value;
                NotifyPropertyChanged(nameof(MarkerId));
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
        private string _showselectedMarkerPosition;
        public string ShowSelectedMarkerPosition
        {
            get => _showselectedMarkerPosition;
            set
            {
                _showselectedMarkerPosition = value;
                NotifyPropertyChanged(nameof(ShowSelectedMarkerPosition));
            }
        }
        private string _noDeleteID;
        public string NoDeleteID
        {
            get => _noDeleteID;
            set
            {
                _noDeleteID = value;
                NotifyPropertyChanged(nameof(NoDeleteID));
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
        private double _xcenter;
        public double Xcenter
        {
            get => _xcenter;
            set
            {
                _xcenter = value;
                NotifyPropertyChanged(nameof(Xcenter));
            }
        }
        private double _ycenter;
        public double Ycenter
        {
            get => _ycenter;
            set
            {
                _ycenter = value;
                NotifyPropertyChanged(nameof(Ycenter));
            }
        }
        private double _useroriginZ;
        public double UserOriginZ
        {
            get => _useroriginZ;
            set
            {
                _useroriginZ = value;
                NotifyPropertyChanged(nameof(UserOriginZ));
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
        private ScriptContext _sC;
        public ScriptContext SC
        {
            get => _sC;
            set
            {
                _sC = value;
                NotifyPropertyChanged(nameof(SC));
            }
        }
        private StructureSet _sS;
        public StructureSet SS
        {
            get => _sS;
            set
            {
                _sS = value;
                NotifyPropertyChanged(nameof(SS));
            }
        }
        private double _bodyVolume;
        public double BodyVolume
        {
            get => _bodyVolume;
            set
            {
                _bodyVolume = value;
                NotifyPropertyChanged(nameof(BodyVolume));
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
        private double _ychkOrientation;
        public double YchkOrientation
        {
            get => _ychkOrientation;
            set
            {
                _ychkOrientation = value;
                NotifyPropertyChanged(nameof(YchkOrientation));
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
        private bool _canStartProcessingChkBBs;
        public bool CanStartProcessingChkBBs
        {
            get => _canStartProcessingChkBBs;
            set
            {
                _canStartProcessingChkBBs = value;
                NotifyPropertyChanged(nameof(CanStartProcessingChkBBs));
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
        public double XchkOrientation { get; set; }
        public double ZchkOrientation { get; set; }
        public PatientOrientation orientation { get; set; }
        public VMS.TPS.Common.Model.API.Image SI { get; set; }

        //This Code is based on the Model slice thickness = 1mm and the minimun interpolate resolution is 0.5mm
        public MainViewModel() { }
        public MainViewModel(ScriptContext scriptContext)
        {
            SC = scriptContext;
            SS = scriptContext.StructureSet;
            SI = scriptContext.Image;
            orientation = scriptContext.Image.ImagingOrientation;
            ChkOrientation chkOrientation = new ChkOrientation();
            chkOrientation.XYZOrientation(SC.Image.ImagingOrientation);
            XchkOrientation = chkOrientation.XchkOrientation;
            YchkOrientation = chkOrientation.YchkOrientation;
            ZchkOrientation = chkOrientation.ZchkOrientation;

            scriptContext.Patient.BeginModifications();
            if (SS is null)
            {
                SS = SI.CreateNewStructureSet();
                SS.Id = SI.Id;
            }
            //Find center
            FindImageCenter findcenter = new FindImageCenter();
            findcenter.FinadImageCenter(SI, YchkOrientation);
            Xcenter = findcenter.Xcenter; double Xborder = findcenter.Xborder;
            Ycenter = findcenter.Ycenter; double Yborder = findcenter.Yborder;
            double AutoZmin = findcenter.AutoZmin;
            double OriginX = findcenter.originX;
            double originY = findcenter.originY;
            UserOriginZ = findcenter.userOriginZ;

            AutoCouchYPlane FindCouchYPlane = new AutoCouchYPlane();
            StructureModifier getStructure = new StructureModifier();
            FindCouchYPlane.AutoFindYPlane(findcenter.YProfile, SI, YchkOrientation, Xcenter, Ycenter, UserOriginZ, findcenter.X1, findcenter.X2, findcenter.Y1, findcenter.Y2);
            FinalYcenter = FindCouchYPlane.FinalYcenter - 0.4 * YchkOrientation;// = MarkerLocationYY
            SliceThickness = SC.Image.ZRes.ToString() + "mm";
            Multiple = SC.Image.ZRes;
            //OriginalProtonCouch           
            if (getStructure.FindStructure(SS, "MARKER", "DicomType") != null)
            {
                MarkerNames = new List<String>();
                foreach (Structure Iso in SS.Structures.Where(s => s.DicomType == "MARKER").ToList())
                {
                    MarkerNames.Add(Iso.Id);
                }
                PositionRenew();
            }
            CouchInterior = getStructure.FindStructure(SS, "CouchInterior", "Id");
            CouchSurface = getStructure.FindStructure(SS, "CouchSurface", "Id");

            MarkerPositions = new List<String>();
            MarkerPositions.Add("H5");
            MarkerPositions.Add("H4");
            MarkerPositions.Add("H3");
            MarkerPositions.Add("H2");
            MarkerPositions.Add("H1");
            MarkerPositions.Add("H0");

            //TBOXSite
            //string[] Basiclines = File.ReadAllLines(@"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\PathInformation.csv");
            //NTUCCSite
            string[] Basiclines = File.ReadAllLines(@"\\FILESVR\VA_DATA$\ProgramData\Vision\PublishedScripts\CouchPath\PathInformation.csv");
            FileFolder = Basiclines[0].ToString() + SliceThickness;
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

            UserCS = getStructure.FindStructure(SS, UserCouchCSName, "Id");
            UserCI = getStructure.FindStructure(SS, UserCouchCIName, "Id");
            ChkBBsStructure();
        }

        public ICommand PositionRenewCommand { get => new Command(PositionRenew); }
        private void PositionRenew()
        {
            MarkerId = "Marker";
            if (SelectedMarkerName != null) MarkerId = SelectedMarkerName;
            MarkerLocationItem = SS.Structures.Where(s => s.DicomType == "MARKER").ToList().Where(a => a.Id == MarkerId).FirstOrDefault();
            DetectTool detectTool = new DetectTool();

            List<string> MarkerNames = new List<string>();
            if (MarkerLocationItem != null)
            {
                MarkerNames.Clear();
                MarkerLocationX = MarkerLocationItem.CenterPoint.x;
                MarkerLocationY = MarkerLocationItem.CenterPoint.y;
                MarkerLocationZ = MarkerLocationItem.CenterPoint.z;
                for (double zzz = MarkerLocationZ - 0.5; zzz < MarkerLocationZ + 0.5; zzz += 0.1)
                {
                    VVector PositionStart = new VVector(MarkerLocationX + 60 * XchkOrientation, MarkerLocationY, zzz);
                    VVector PositionStop = new VVector(MarkerLocationX - 60 * XchkOrientation, MarkerLocationY, zzz);
                    double[] PositionPreallocatedBuffer = new double[100];
                    ImageProfile PositionXProfile = SC.Image.GetImageProfile(PositionStart, PositionStop, PositionPreallocatedBuffer);
                    if (detectTool.PeakDetect(detectTool.Twopoint(PositionXProfile)) != "") { MarkerNames.Add(detectTool.PeakDetect(detectTool.Twopoint(PositionXProfile))); }
                }
                SelectedMarkerPosition = MarkerNames.GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => (string)x.Key).FirstOrDefault();//Find the Mode
                ShowSelectedMarkerPosition = SelectedMarkerPosition; VVector A = new VVector(MarkerLocationX + 60 * XchkOrientation, MarkerLocationY, MarkerLocationItem.CenterPoint.z);
                if (ShowSelectedMarkerPosition != "0" | ShowSelectedMarkerPosition != "")
                {
                    VVector B = new VVector(MarkerLocationX - 60 * XchkOrientation, MarkerLocationY, MarkerLocationItem.CenterPoint.z);
                    double[] C = new double[100];
                    ImageProfile D = SC.Image.GetImageProfile(A, B, C);
                    if (detectTool.MarkerPlaced(detectTool.Twopoint(D), MarkerLocationItem.CenterPoint, XchkOrientation) == false)
                    { System.Windows.MessageBox.Show("Please Check your Marker Location is at the center one!!"); }
                }
            }
        }

        public ICommand BBPositionRenewCommand { get => new Command(BBPositionRenew); }
        private void BBPositionRenew()
        {
            if (SS.Structures.Where(s => s.Id.Contains("BBsForCouch")).Count() != 0)
            {
                double min = Double.MaxValue;
                VVector NearImageCenter = new VVector(Xcenter, Ycenter, UserOriginZ);
                VVector FinalMarkerBB = new VVector();
                string FFName = "";
                foreach (var s in SS.Structures.Where(s => s.Id.Contains("BBsForCouch")))
                {
                    if (VVector.Distance(NearImageCenter, s.CenterPoint) < min)
                    {
                        FinalMarkerBB = s.CenterPoint;
                        min = Math.Min(VVector.Distance(NearImageCenter, s.CenterPoint), min);
                        FFName = s.Id.Split('_').LastOrDefault();
                        NoDeleteID = s.Id;
                    }
                }
                MarkerLocationX = FinalMarkerBB.x;
                MarkerLocationY = FinalMarkerBB.y;
                MarkerLocationZ = FinalMarkerBB.z;
                SelectedMarkerPosition = FFName;
            }

        }
        public ICommand BBMarkerRenewCommand { get => new Command(BBMarkerRenew); }
        private void BBMarkerRenew()
        {
            BBPositionRenew();
            AddCouch();
            var RStructures = SS.Structures.Where(s => s.Id.Contains("BBsForCouch")).ToList();
            foreach (var structure in RStructures)
            {
                if (structure.Id != NoDeleteID) { SS.RemoveStructure(structure); }
            }
        }

        public ICommand ChkBBsStructureCommand { get => new Command(ChkBBsStructure); }
        private void ChkBBsStructure()
        {
            if (SS.Structures.Where(s => s.Id.Contains("BBsForCouch")).Count() > 1)
            {
                CanStartProcessingChkBBs = true;
            }
        }

        public ICommand ButtonCommand_StartChk { get => new Command(StartChk); }
        private void StartChk()
        {
            CurrentProgress = 0;
            AutoMarkerWPosition = new List<MarkerWPositions>();
            AutoMarkerWPosition.Clear();
            DetectTool detectTool = new DetectTool();
            for (double zaxis = UserOriginZ - 140; zaxis < UserOriginZ + 140; zaxis++)
            {
                VVector AutoStart = new VVector(Xcenter + 60 * XchkOrientation, FinalYcenter, zaxis);
                VVector AutoStop = new VVector(Xcenter - 60 * XchkOrientation, FinalYcenter, zaxis);
                double[] AutoPreallocatedBuffer = new double[1000];
                ImageProfile AutoXProfile = SI.GetImageProfile(AutoStart, AutoStop, AutoPreallocatedBuffer);
                if (Double.IsNaN(AutoPreallocatedBuffer[0]) == false)
                {
                    List<VVector> TP = detectTool.Twopoint(AutoXProfile);
                    string TheMarker = detectTool.PeakDetect(TP);

                    if (TheMarker != "")
                    {
                        MarkerWPositions AAA = new MarkerWPositions();
                        AAA.Markers = TheMarker;
                        AAA.Positions = TP;
                        AutoMarkerWPosition.Add(AAA);
                    }
                }
            }
            if (AutoMarkerWPosition.Count == 0) CanStartProcessing = false;
            else { CanStartProcessing = true; }
            CurrentProgress = 20;
        }

        public ICommand ButtonCommand_AddBBs { get => new Command(AddBBs); }
        private void AddBBs()
        {
            SliceConverter sliceConverter = new SliceConverter();
            StructureModifier structureModifier = new StructureModifier();
            var RStructures = SS.Structures.Where(s => s.Id.Contains("BBsForCouch")).ToList();
            foreach (var structure in RStructures)
            {
                SS.RemoveStructure(structure);
            }
            for (int i = 1; i < AutoMarkerWPosition.Count() - 1; i++)
            {
                if (AutoMarkerWPosition[i].Markers != AutoMarkerWPosition[i - 1].Markers && AutoMarkerWPosition[i].Markers != AutoMarkerWPosition[i + 1].Markers)
                {
                    AutoMarkerWPosition.RemoveAt(i);
                }
            }
            List<string> first = AutoMarkerWPosition.GroupBy(x => x.Markers).OrderByDescending(x => x.Count() >= 2 && x.Count() <= 6).ThenBy(x => x.Key).Select(x => (string)x.Key).Distinct().ToList();
            first = first.OrderByDescending(x => x.Replace("H", "")).ToList();
            int FirstMarkerInt = Convert.ToInt32(first[0].Replace("H", ""));
            List<VVector> BBVectors = new List<VVector>();
            foreach (MarkerWPositions v in AutoMarkerWPosition)
            {
                if (v.Markers.Contains(FirstMarkerInt.ToString()))
                {
                    foreach (VVector vv in v.Positions)
                    {
                        BBVectors.Add(vv);
                    }
                }
            }

            for (int i = -2; i < 3; i++)
            {
                BBVectors.Clear();
                foreach (MarkerWPositions v in AutoMarkerWPosition.Where(s => s.Markers.Contains((FirstMarkerInt + i).ToString())))
                {

                    BBVectors.AddRange(v.Positions);
                }
                string BBNameMarker = "H" + (FirstMarkerInt + i).ToString();
                if (BBVectors.Count() != 0)
                {
                    int mid = BBVectors.Count() / 2;
                    Structure BBs1 = SS.AddStructure("CONTROL", "BBsForCouch1_" + BBNameMarker);
                    Structure BBs2 = SS.AddStructure("CONTROL", "BBsForCouch2_" + BBNameMarker);
                    BBs1.AddContourOnImagePlane(structureModifier.GetCircleContours(SI.XRes, 5, BBVectors.ElementAtOrDefault(mid - 1)), sliceConverter.GetSlice(SS, BBVectors.ElementAtOrDefault(mid - 1).z));
                    BBs2.AddContourOnImagePlane(structureModifier.GetCircleContours(SI.XRes, 5, BBVectors.ElementAtOrDefault(mid)), sliceConverter.GetSlice(SS, BBVectors.ElementAtOrDefault(mid).z));
                }
            }
            ChkBBsStructure();
            CurrentProgress = 30;
        }

        public ICommand ButtonCommand_AddCouch { get => new Command(AddCouch); }
        private void AddCouch()
        {
            ProgressVisibility = Visibility.Visible;
            StructureModifier getStructure = new StructureModifier();
            DetectTool detectTool = new DetectTool();
            SliceConverter sliceConverter = new SliceConverter();
            SC.Patient.BeginModifications();
            PreBODY();

            FilePathCI = System.IO.Path.Combine(new string[] { FileFolder, "CouchInterior.csv" });
            FilePathCS = System.IO.Path.Combine(new string[] { FileFolder, "CouchSurface.csv" });

            //CouchSurface_Cross
            if (UserCS != null | UserCI != null)
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Proton Couch Structures are existed. Please delete your couch before adding new one.", "Couch-existed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                UserCS = SS.AddStructure("CONTROL", UserCouchCSName);
                if (UserCS.CanConvertToHighResolution()) UserCS.ConvertToHighResolution();
                List<VVector> CSVVector = new List<VVector>();
                List<VVector> CSVVector_Cross = new List<VVector>();
                string[] TempFilelines = File.ReadAllLines(FilePathCS);
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
                double zmin = CSVVector.Min(vec => vec.z);
                foreach (VVector v in CSVVector.Where(vec => vec.z == zmin))
                { CSVVector_Cross.Add(new VVector(v.x, v.y, v.z)); }

                double MMX = detectTool.MaxMinDetect(CSVVector_Cross, orientation)[0];
                double MMY = detectTool.MaxMinDetect(CSVVector_Cross, orientation)[1];
                double MMZ = detectTool.MaxMinDetect(CSVVector_Cross, orientation)[2];

                List<VVector> NewVVector_Cross = new List<VVector>();
                foreach (VVector vec in CSVVector_Cross)
                {
                    VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ, YchkOrientation, ZchkOrientation);
                    NewVVector_Cross.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
                }

                //CouchSurface
                CurrentProgress = 15;
                List<VVector> NewVVector = new List<VVector>();
                foreach (VVector vec in CSVVector)
                {
                    VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ, YchkOrientation, ZchkOrientation);
                    NewVVector.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
                }
                int a = Convert.ToInt32(NewVVector.Min(p => p.z));
                int b = Convert.ToInt32(NewVVector.Max(p => p.z));
                if (ZchkOrientation == -1)
                {
                    for (int i = a; i <= b; i++)
                    {
                        UserCS.AddContourOnImagePlane(NewVVector.Where(vv => vv.z.Equals(i)).ToArray(), -(i - 2 * b) - Convert.ToInt32(2 * ZBaseAxis / SI.ZRes));
                    }
                }
                else
                {
                    for (int i = a; i <= b; i++)
                    {

                        UserCS.AddContourOnImagePlane(NewVVector.Where(vv => (vv.z).Equals(i)).ToArray(), i);
                    }
                }
                //CouchSurface_Cross
                if (YchkOrientation == 1)
                { FinalYcenter = NewVVector_Cross.Min(p => p.y) - 27.1; }
                else { FinalYcenter = NewVVector_Cross.Max(p => p.y) + 27.1; }

                if (ZchkOrientation == -1)
                {
                    for (int i = Convert.ToInt32(NewVVector.Max(p => p.z)); i < Convert.ToInt32(SI.ZSize) + 1; i++)
                    {
                        UserCS.AddContourOnImagePlane(NewVVector_Cross.ToArray(), i);
                    }
                }
                else
                {
                    for (int i = 0; i < NewVVector_Cross.Max(p => p.z) + 1; i++)
                    {
                        UserCS.AddContourOnImagePlane(NewVVector_Cross.ToArray(), i);
                    }
                }

                //CouchInterior_Cross
                UserCI = SS.AddStructure("CONTROL", UserCouchCIName);
                if (UserCI.CanConvertToHighResolution()) UserCI.ConvertToHighResolution();

                Array.Clear(TempFilelines, 0, TempFilelines.Length);
                CSVVector.Clear(); CSVVector_Cross.Clear();
                TempFilelines = File.ReadAllLines(FilePathCI);
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
                zmin = CSVVector.Min(vec => vec.z);
                foreach (VVector v in CSVVector.Where(vec => vec.z == zmin))
                { CSVVector_Cross.Add(new VVector(v.x, v.y, v.z)); }

                NewVVector_Cross.Clear();
                foreach (VVector vec in CSVVector_Cross)
                {
                    VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ, YchkOrientation, ZchkOrientation);
                    NewVVector_Cross.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
                }
                if (ZchkOrientation == -1)
                {
                    for (int i = Convert.ToInt32(NewVVector.Max(p => p.z)); i < Convert.ToInt32(SI.ZSize) + 1; i++)
                    {
                        UserCI.AddContourOnImagePlane(NewVVector_Cross.ToArray(), i);
                    }
                }
                else
                {
                    for (int i = 0; i < NewVVector_Cross.Max(p => p.z) + 1; i++)
                    {
                        UserCI.AddContourOnImagePlane(NewVVector_Cross.ToArray(), i);
                    }
                }
                int subject = Convert.ToInt32(NewVVector.Max(p => p.z));

                //CouchInterior
                NewVVector.Clear();
                foreach (VVector vec in CSVVector)
                {
                    VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ, YchkOrientation, ZchkOrientation);
                    NewVVector.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
                }

                if (ZchkOrientation == -1)
                {
                    for (int i = a; i <= b; i++)
                    {
                        UserCI.AddContourOnImagePlane(NewVVector.Where(vv => (vv.z).Equals(i)).ToArray(), -(i - 2 * b) - Convert.ToInt32(2 * ZBaseAxis / SI.ZRes));
                    }
                }
                else
                {
                    for (int i = a; i <= b; i++)
                    {
                        UserCI.AddContourOnImagePlane(NewVVector.Where(vv => (vv.z).Equals(i)).ToArray(), i);
                    }
                }
                UserCS.SegmentVolume = UserCS.SegmentVolume.Sub(UserCI.SegmentVolume);
                CurrentProgress = 95;
                PostBODY();
                CurrentProgress = 100;
            }
        }

        public VVector Interpolate(VVector Input, VVector Next)
        {
            double x = (Input.x + Next.x) / 2;
            double y = (Input.y + Next.y) / 2;
            double z = (Input.z + Next.z) / 2;
            return new VVector(x, y, z);
        }

        public ICommand PreBODYCommand { get => new Command(PreBODY); }
        private void PreBODY()
        {
            StructureModifier getStructure = new StructureModifier();
            Structure BODY = getStructure.FindStructure(SS, "EXTERNAL", "DicomType");
            double BodyVolume = new double();
            bodyParameter AddBODY = new bodyParameter();
            if (BODY == null)
            {
                AddBODY.BuildBODY(SS, -350, true, 1, true, 0.2, true, true, 0.2, true, 3);
                BodyVolume = getStructure.FindStructure(SS, "EXTERNAL", "DicomType").Volume;
                SS.RemoveStructure(getStructure.FindStructure(SS, "EXTERNAL", "DicomType"));
                AddBODY.BuildBODY(SS, -350, false, 1, false, 0.2, true, true, 0.2, true, 3);
            }
            else if (BODY.Volume == 0)
            {
                AddBODY.BuildBODY(SS, -350, true, 1, true, 0.2, true, true, 0.2, true, 3);
                BodyVolume = getStructure.FindStructure(SS, "EXTERNAL", "DicomType").Volume;
                SS.RemoveStructure(getStructure.FindStructure(SS, "EXTERNAL", "DicomType"));
                AddBODY.BuildBODY(SS, -350, false, 1, false, 0.2, true, true, 0.2, true, 3);
            }
        }
        public ICommand PostBODYCommand { get => new Command(PostBODY); }
        private void PostBODY()
        {
            //BODY part
            StructureModifier getStructure = new StructureModifier();
            Structure BODY = getStructure.FindStructure(SS, "EXTERNAL", "DicomType");
            Structure Temp = SS.AddStructure("CONTROL", "Temp_ForCouch");
            VVector[] TempVec = getStructure.GetpseudoLine(FinalYcenter, SI.XSize, SI.YSize, YchkOrientation);
            for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
            {
                Temp.AddContourOnImagePlane(TempVec, i);
            }
            BODY.SegmentVolume = BODY.SegmentVolume.Sub(Temp.SegmentVolume);
            SS.RemoveStructure(Temp);
            if (BODY.Volume > BodyVolume) { System.Windows.Forms.MessageBox.Show("Please Check your BODY carefully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            BODY.Comment = "Modified by ESAPI";
        }

        public ICommand ButtonCommand_PhotonCouchBody { get => new Command(PhotonCouchBody); }
        private void PhotonCouchBody()
        {
            PreBODY();
            Halcyon = false;
            if (IsChecked == true) { Halcyon = true; }

            bool imageResized = true;
            string errorCouch = "error";
            List<VVector> CSVVector = new List<VVector>();
            bodyParameter AddBODY = new bodyParameter();
            StructureModifier getStructure = new StructureModifier();
            DetectTool detectTool = new DetectTool();
            bool AddCouch = true;
            if ((SI.XSize * SI.XRes) <= 540)
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show("Enlarging is irreversible. Are you sure you want to enlarge the image?", "External Beam Planning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) { AddCouch = false; }
            }
            switch (AddCouch)
            {
                case (false):
                    break;
                default:
                    if (SS.CanAddCouchStructures(out errorCouch) == true)
                    {
                        SS.AddCouchStructures("Exact_IGRT_Couch_Top_medium", orientation, RailPosition.In, RailPosition.In, -500, -950, null, out IReadOnlyList<Structure> couchStructureList, out imageResized, out errorCouch);
                        Structure CouchSurface = getStructure.FindStructure(SS, "CouchSurface", "Id");
                        Structure CouchInterior = getStructure.FindStructure(SS, "CouchInterior", "Id");
                        StructureCode CScode = CouchSurface.StructureCode;
                        StructureCode CIcode = CouchInterior.StructureCode;
                        CouchSurface.SegmentVolume = CouchSurface.SegmentVolume.Or(CouchInterior.SegmentVolume);
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
                        double MMX = detectTool.MaxMinDetect(CSVVector, orientation)[0]; double MMY = detectTool.MaxMinDetect(CSVVector, orientation)[1];
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
                        PostBODY();
                    }
                    else if (errorCouch.Contains("Support structures already exist in the structure set."))
                    {
                        Structure Encompass = getStructure.FindStructure(SS, "Encompass", "Id");
                        Structure EncompassBase = getStructure.FindStructure(SS, "Encompass Base", "Id");
                        if (EncompassBase == null)
                        {
                            CSVVector.Clear();
                            CouchSurface = SS.Structures.FirstOrDefault(z => z.Id.Contains("CouchSurface"));
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
                            if (YchkOrientation == 1)
                            { FinalYcenter = CSVVector.Min(p => p.y); }
                            else { FinalYcenter = CSVVector.Max(p => p.y); }
                            PostBODY();
                        }
                        else
                        {
                            CSVVector.Clear();
                            for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                            {
                                foreach (VVector[] vectors in EncompassBase.GetContoursOnImagePlane(i))
                                {
                                    foreach (VVector v in vectors)
                                    {
                                        double x = v.x;
                                        double y = v.y;
                                        double z = v.z;
                                        CSVVector.Add(new VVector(x, y, z));
                                    }
                                }
                            }
                            FinalYcenter = CSVVector.Min(p => p.y);
                            SS.RemoveStructure(getStructure.FindStructure(SS, "EXTERNAL", "DicomType"));
                            AddBODY.BuildBODY(SS, -700, false, 1, true, 0.2, true, true, 0.2, true, 1);

                            Structure Temp = SS.AddStructure("CONTROL", "Temp_ForCouch");
                            VVector[] TempVec = getStructure.GetpseudoLine(FinalYcenter, SI.XSize, SI.YSize, YchkOrientation);
                            for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                            {
                                Temp.AddContourOnImagePlane(TempVec, i);
                            }
                            Structure BODY = getStructure.FindStructure(SS, "EXTERNAL", "DicomType");
                            BODY.SegmentVolume = BODY.SegmentVolume.Sub(Temp.SegmentVolume);
                            BODY.SegmentVolume = BODY.SegmentVolume.Sub(Encompass.SegmentVolume);
                            BODY.SegmentVolume = BODY.SegmentVolume.Sub(EncompassBase.SegmentVolume);
                            SS.RemoveStructure(Temp);
                            if (BODY.Volume > BodyVolume) { System.Windows.Forms.MessageBox.Show("Please Check your BODY carefully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            BODY.Comment = "Modified by ESAPI";

                            //NTUCC
                            HalcyonHR halcyonHR = new HalcyonHR();
                            halcyonHR.ConvertHR(SS, "GTV");
                            halcyonHR.ConvertHR(SS, "CTV");
                            halcyonHR.ConvertHR(SS, "PTV");
                        }
                    }
                    else { System.Windows.MessageBox.Show(errorCouch); }
                    break;
            }
        }
        public ICommand ButtonCommand_FilePath { get => new Command(GetFilePath); }
        private void GetFilePath()
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show("Do you really want to change the File Path", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
                dialog.Description = "Please Choose the input Folder for Couch Model";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] ModelPathCI = new string[] { dialog.SelectedPath, SliceThickness, "CouchInterior.csv" }; ModelFilePathCI = System.IO.Path.Combine(ModelPathCI);
                    string[] ModelPathCS = new string[] { dialog.SelectedPath, SliceThickness, "CouchSurface.csv" }; ModelFilePathCS = System.IO.Path.Combine(ModelPathCS);
                }
            }
        }
        public ICommand ButtonCommand_BuildModel { get => new Command(BuildModel); }
        private void BuildModel()
        {
            ModelBuilder modelBuilder = new ModelBuilder();
            DialogResult result = System.Windows.Forms.MessageBox.Show("You are Biulding Model for Couch", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (CouchInterior != null) modelBuilder.SaveModel(SC, CouchInterior, ModelFilePathCI);
                if (CouchSurface != null) modelBuilder.SaveModel(SC, CouchSurface, ModelFilePathCS);
            }
        }

        public ICommand ButtonCommand_CheckingBB { get => new Command(CheckingBB); }
        private void CheckingBB()
        {
            DetectTool detectTool = new DetectTool();
            SliceConverter sliceConverter = new SliceConverter();
            double BBs = sliceConverter.GetLimitValue("Z", SI, UserCS, ZchkOrientation,0);

            Structure MsgStructure = SS.Structures.Where(s => s.Id.Contains("BBsForCouch")).FirstOrDefault();
            if (MsgStructure != null && SS.Structures.Where(s => s.DicomType == "MARKER").FirstOrDefault() != null)
            {
                string msg = "There are Both BB structure and Marker existed. Please confirm that which way do you prefer to double-check.";
                msg = msg + "\n\nYes = Use BB structure :\t" + MsgStructure.Id.ToString() + "\nNo = Use Marker :\t\t" + MarkerId;
                DialogResult DoubleChkResult = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (DoubleChkResult == DialogResult.Yes)
                {
                    BBPositionRenew();
                }
                else if (DoubleChkResult == DialogResult.No)
                {
                    PositionRenew();
                }
                else { CalculateBBLocation = ""; }
            }
            else if (MsgStructure != null)
            {
                BBPositionRenew();
            }
            else
            {
                PositionRenew();
            }
            VVector CompareOrigin = new VVector(MarkerLocationX, MarkerLocationY, MarkerLocationZ);
            double DoubleChkX1 = sliceConverter.GetLimitValue("X1", SI, UserCS, XchkOrientation, sliceConverter.GetSlice(SS, BBs));
            double DoubleChkX2 = sliceConverter.GetLimitValue("X2", SI, UserCS, XchkOrientation, sliceConverter.GetSlice(SS, BBs));
            double DoubleChkY = sliceConverter.GetLimitValue("Y", SI, UserCS, YchkOrientation, sliceConverter.GetSlice(SS, BBs));
            VVector DoubleChkPoint = new VVector(((DoubleChkX1+ DoubleChkX2)/2), DoubleChkY, BBs);
            //CalculateBBLocation = detectTool.DoubleChk(BBs - MarkerLocationZ);//mm
            CalculateBBLocation = detectTool.BBCalDetect(VVector.Distance(DoubleChkPoint, CompareOrigin), DoubleChkPoint.x, YBaseAxis, ZBaseAxis);
        }
        public VVector AxisAlignment(VVector Original, string LockBarType, double Xmin, double Ymax, double Zmin, double YchkOrientation, double ZchkOrientation)
        {
            //The value of YAxis is opposite
            double X = MarkerLocationX - XBaseAxis - Xmin;
            double Y = MarkerLocationY + YchkOrientation * (YBaseAxis - Ymax);
            if (YchkOrientation == -1) { Y = Y - 67.4; }
            double Z = MarkerLocationZ - ZBaseAxis - SC.Image.Origin.z - Zmin;
            //double SSZAdd = Convert.ToInt32((MarkerLocationZZ - ZBaseAxis - ScriptContext.Image.Origin.z) / ScriptContext.Image.ZRes);
            var mapAxis = new Dictionary<string, VVector>()
            {
                {"H5",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, (Original.z + Z  - 2 * HSpace * ZchkOrientation))},
                {"H4",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, (Original.z + Z - HSpace * ZchkOrientation))},
                {"H3",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, (Original.z + Z ))},
                {"H2",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, (Original.z + Z  + HSpace * ZchkOrientation))},
                {"H1",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, (Original.z + Z  + 2 * HSpace * ZchkOrientation))},
                {"0",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, (Original.z + Z + 3 * HSpace * ZchkOrientation))},
            };
            VVector output;
            return mapAxis.TryGetValue(LockBarType, out output) ? output : new VVector(0, 0, 0);
        }
    }
}