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

            Halcyon = false;
            if (IsChecked == true) { Halcyon = true; }

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
            FindCouchYPlane.AutoFindYPlane(findcenter.YProfile, SI, YchkOrientation, Xcenter, Ycenter, UserOriginZ, findcenter.X1, findcenter.X2, findcenter.Y1, findcenter.Y2);
            FinalYcenter = FindCouchYPlane.FinalYcenter - 0.4 * YchkOrientation;// = MarkerLocationYY
            SliceThickness = SC.Image.ZRes.ToString() + "mm";
            Multiple = SC.Image.ZRes;
            //OriginalProtonCouch           
            if (FindStructure(SS, "MARKER", "DicomType") != null)
            {
                MarkerNames = new List<String>();
                foreach (Structure Iso in SS.Structures.Where(s => s.DicomType == "MARKER").ToList())
                {
                    MarkerNames.Add(Iso.Id);
                }
                PositionRenew();
            }
            CouchInterior = FindStructure(SS, "CouchInterior", "Id");
            CouchSurface = FindStructure(SS, "CouchSurface", "Id"); 
            CrossInterior = FindStructure(SS, "CrossInterior", "Id");
            CrossSurface = FindStructure(SS, "CrossSurface", "Id");

            MarkerPositions = new List<String>();
            MarkerPositions.Add("H5");
            MarkerPositions.Add("H4");
            MarkerPositions.Add("H3");
            MarkerPositions.Add("H2");
            MarkerPositions.Add("H1");
            MarkerPositions.Add("0");

            string[] Basiclines = File.ReadAllLines(@"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\PathInformation.csv");
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

            UserCS = FindStructure(SS, UserCouchCSName, "Id");
            UserCI = FindStructure(SS, UserCouchCIName, "Id");
            ChkBBsStructure();
        }

        public ICommand PositionRenewCommand { get => new Command(PositionRenew); }
        private void PositionRenew()
        {
            MarkerId = "Marker";
            if (SelectedMarkerName != null) MarkerId = SelectedMarkerName;
            MarkerLocationItem = SS.Structures.Where(s => s.DicomType == "MARKER").ToList().Where(a => a.Id == MarkerId).FirstOrDefault();

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
                    if (PeakDetect(Twopoint(PositionXProfile)) != "") { MarkerNames.Add(PeakDetect(Twopoint(PositionXProfile))); }
                }
                SelectedMarkerPosition = MarkerNames.GroupBy(x => x).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => (string)x.Key).FirstOrDefault();//Find the Mode
                ShowSelectedMarkerPosition = SelectedMarkerPosition;
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
                if (structure.Id != NoDeleteID)
                {
                   SS.RemoveStructure(structure);
                }
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
            AutoMarkerWPosition = new List<MarkerWPositions>();
            AutoMarkerWPosition.Clear();
            for (double zaxis = UserOriginZ - 150; zaxis < UserOriginZ + 150; zaxis++)
            {
                VVector AutoStart = new VVector(Xcenter + 60 * XchkOrientation, FinalYcenter, zaxis);
                VVector AutoStop = new VVector(Xcenter - 60 * XchkOrientation, FinalYcenter, zaxis);
                double[] AutoPreallocatedBuffer = new double[1000];
                ImageProfile AutoXProfile = SI.GetImageProfile(AutoStart, AutoStop, AutoPreallocatedBuffer);
                if (Double.IsNaN(AutoPreallocatedBuffer[0]) == false)
                {
                    List<VVector> TP = Twopoint(AutoXProfile);
                    string TheMarker = PeakDetect(TP);

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
        }

        public ICommand ButtonCommand_AddBBs { get => new Command(AddBBs); }
        private void AddBBs()
        {
            var RStructures = SS.Structures.Where(s => s.Id.Contains("BBsForCouch")).ToList();
            foreach (var structure in RStructures)
            {
               SS.RemoveStructure(structure);
            }
            int FirstMarkerInt = Convert.ToInt32(AutoMarkerWPosition.GroupBy(x => x.Markers).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => (string)x.Key).FirstOrDefault().Replace("H", ""));
            string SecondMarker = (FirstMarkerInt + 1).ToString();
            string ThirdMarker = (FirstMarkerInt - 1).ToString();
            List<VVector> BBVectors = new List<VVector>();
            string BBNameMarker = AutoMarkerWPosition.GroupBy(x => x.Markers).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => (string)x.Key).Distinct().ToList().FirstOrDefault();
            foreach (MarkerWPositions v in AutoMarkerWPosition)
            {
                if (v.Markers.Contains(FirstMarkerInt.ToString()))
                {
                    foreach (VVector vv in v.Positions) { BBVectors.Add(vv); }
                }
            }
            int mid = BBVectors.Count() / 2;
            if (BBVectors.Count() != 0)
            {
                Structure BBs1 = SS.AddStructure("CONTROL", "BBsForCouch1_" + BBNameMarker);
                Structure BBs2 = SS.AddStructure("CONTROL", "BBsForCouch2_" + BBNameMarker);
                BBs1.AddContourOnImagePlane(GetCircleContours(SI.XRes, 5, BBVectors.ElementAtOrDefault(mid - 1)), GetSlice(BBVectors.ElementAtOrDefault(mid - 1).z));
                BBs2.AddContourOnImagePlane(GetCircleContours(SI.XRes, 5, BBVectors.ElementAtOrDefault(mid)), GetSlice(BBVectors.ElementAtOrDefault(mid).z));
            }
            MarkerWPositions CCC = new MarkerWPositions();
            BBNameMarker = AutoMarkerWPosition.GroupBy(x => x.Markers).OrderByDescending(x => x.Count()).ThenBy(x => x.Key).Select(x => (string)x.Key).Distinct().ToList().ElementAt(1);
            BBVectors.Clear();
            foreach (MarkerWPositions v in AutoMarkerWPosition)
            {
                if (v.Markers.Contains(SecondMarker) | v.Markers.Contains(ThirdMarker))
                {
                    foreach (VVector vv in v.Positions) { BBVectors.Add(vv); }
                }
            }
            mid = BBVectors.Count() / 2;
            if (BBVectors.Count() != 0)
            {
                Structure BBs3 = SS.AddStructure("CONTROL", "BBsForCouch1_" + BBNameMarker);
                Structure BBs4 = SS.AddStructure("CONTROL", "BBsForCouch2_" + BBNameMarker);
                BBs3.AddContourOnImagePlane(GetCircleContours(SI.XRes, 5, BBVectors.ElementAtOrDefault(mid - 1)), GetSlice(BBVectors.ElementAtOrDefault(mid - 1).z));
                BBs4.AddContourOnImagePlane(GetCircleContours(SI.XRes, 5, BBVectors.ElementAtOrDefault(mid)), GetSlice(BBVectors.ElementAtOrDefault(mid).z));
            }
            ChkBBsStructure();
        }

        public ICommand ButtonCommand_AddCouch { get => new Command(AddCouch); }
        private void AddCouch()
        {
            SC.Patient.BeginModifications();
            PreBODY();
            ProgressVisibility = Visibility.Visible;

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
            if (CrossSurface != null) SS.RemoveStructure(FindStructure(SS, "CrossSurface", "Id"));
            CrossSurface = SS.AddStructure("CONTROL", "CrossSurface");
            if (CrossSurface.CanConvertToHighResolution()) CrossSurface.ConvertToHighResolution();

            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ, YchkOrientation, ZchkOrientation);
                NewVVector.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
            }
            if (YchkOrientation == 1)
            { FinalYcenter = CSVVector.Min(p => p.y); }
            else { FinalYcenter = CSVVector.Max(p => p.y); }
            if (ZchkOrientation == -1)
            {
                for (int i = 0; i < NewVVector.Max(p => p.z) - Convert.ToInt32(2 * ZBaseAxis / SI.ZRes) + 1; i++)
                {
                    CrossSurface.AddContourOnImagePlane(NewVVector.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);

                }
            }
            else
            {
                for (int i = 0; i < NewVVector.Max(p => p.z) + 1; i++)
                {
                    CrossSurface.AddContourOnImagePlane(NewVVector.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);

                }
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

            if (CrossInterior != null) SS.RemoveStructure(SS.Structures.First(s => s.Id == "CrossInterior"));
            CrossInterior = SS.AddStructure("CONTROL", "CrossInterior");
            //if (IsChecked == true) { if (CrossInterior.CanConvertToHighResolution()) CrossInterior.ConvertToHighResolution(); }
            if (CrossInterior.CanConvertToHighResolution()) CrossInterior.ConvertToHighResolution();

            NewVVector.Clear();
            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ, YchkOrientation, ZchkOrientation);
                NewVVector.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
            }
            if (ZchkOrientation == -1)
            {
                for (int i = 0; i < NewVVector.Max(p => p.z) - Convert.ToInt32(2 * ZBaseAxis / SI.ZRes) + 1; i++)
                {
                    CrossInterior.AddContourOnImagePlane(NewVVector.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);

                }
            }
            else
            {
                for (int i = 0; i < NewVVector.Max(p => p.z) + 1; i++)
                {
                    CrossInterior.AddContourOnImagePlane(NewVVector.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);

                }
            }
            CurrentProgress = 25;
            if (UserCS != null) SS.RemoveStructure(SS.Structures.First(s => s.Id == UserCouchCSName));
            UserCS = SS.AddStructure("CONTROL", UserCouchCSName);
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
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ, YchkOrientation, ZchkOrientation);
                NewVVector.Add(new VVector(vv.x, vv.y, vv.z));
            }

            double CouchOriginZ = NewVVector.Min(p => p.z);
            List<VVector> ForInterpolate = new List<VVector>();
            List<VVector> ForInterpolate1 = new List<VVector>();
            foreach (VVector vec in NewVVector.Where(p => ((p.z - CouchOriginZ) / Multiple) == (int)((p.z - CouchOriginZ) / Multiple)))
            {
                ForInterpolate.Add(new VVector(vec.x, vec.y, Convert.ToInt32(vec.z / Multiple)));
            }

            List<double> CheckArray = new List<double>();
            CheckArray = ForInterpolate.Select(x => x.z).Distinct().ToList();
            double CheckSlice = Math.Abs(CheckArray[CheckArray.Count - 1] - CheckArray[CheckArray.Count - 2]);

            int a = Convert.ToInt32(ForInterpolate.Min(p => p.z));
            int b = Convert.ToInt32(ForInterpolate.Max(p => p.z));
            List<VVector> Loop = new List<VVector>();
            if (ZchkOrientation == -1)
            {
                for (int i = a; i <= b; i += Convert.ToInt32(CheckSlice))
                {
                    Loop.Clear();
                    //if (is_integer == true)
                    foreach (VVector vec in ForInterpolate.Where(vv => vv.z.Equals(i)))
                    {
                        Loop.Add(vec);
                    }
                    UserCS.AddContourOnImagePlane(Loop.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), -(i - 2 * b) - Convert.ToInt32(2 * ZBaseAxis / SI.ZRes));
                }
            }
            else
            {
                for (int i = a; i <= b; i += Convert.ToInt32(CheckSlice))
                {
                    Loop.Clear();
                    //if (is_integer == true)
                    foreach (VVector vec in ForInterpolate.Where(vv => vv.z.Equals(i)))
                    {
                        Loop.Add(vec);
                    }
                    UserCS.AddContourOnImagePlane(Loop.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);
                }
            }

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

            if (UserCI != null) SS.RemoveStructure(SS.Structures.First(s => s.Id == UserCouchCIName));
            UserCI = SS.AddStructure("CONTROL", UserCouchCIName);
            if (UserCI.CanConvertToHighResolution()) UserCI.ConvertToHighResolution();

            NewVVector.Clear();
            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ, YchkOrientation, ZchkOrientation);
                NewVVector.Add(new VVector(vv.x, vv.y, vv.z));
            }
            ForInterpolate.Clear();
            foreach (VVector vec in NewVVector.Where(p => ((p.z - CouchOriginZ) / Multiple) == (int)((p.z - CouchOriginZ) / Multiple)))
            {
                ForInterpolate.Add(new VVector(vec.x, vec.y, Convert.ToInt32(vec.z / Multiple)));
            }
            CheckArray.Clear();
            CheckArray = ForInterpolate.Select(x => x.z).Distinct().ToList();
            CheckSlice = Math.Abs(CheckArray[CheckArray.Count - 1] - CheckArray[CheckArray.Count - 2]);
            a = Convert.ToInt32(ForInterpolate.Min(p => p.z));
            b = Convert.ToInt32(ForInterpolate.Max(p => p.z));

            if (ZchkOrientation == -1)
            {
                for (int i = a; i <= b; i += Convert.ToInt32(CheckSlice))
                {
                    Loop.Clear();
                    //if (is_integer == true)
                    foreach (VVector vec in ForInterpolate.Where(vv => vv.z.Equals(i)))
                    {
                        Loop.Add(vec);
                    }
                    UserCI.AddContourOnImagePlane(Loop.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), -(i - 2 * b) - Convert.ToInt32(2 * ZBaseAxis / SI.ZRes));
                }
            }
            else
            {
                for (int i = a; i <= b; i += Convert.ToInt32(CheckSlice))
                {
                    Loop.Clear();
                    //if (is_integer == true)
                    foreach (VVector vec in ForInterpolate.Where(vv => vv.z.Equals(i)))
                    {
                        Loop.Add(vec);
                    }
                    UserCI.AddContourOnImagePlane(Loop.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);
                }
            }
            UserCS.SegmentVolume = UserCS.SegmentVolume.Or(CrossSurface.SegmentVolume);
            UserCI.SegmentVolume = UserCI.SegmentVolume.Or(CrossInterior.SegmentVolume);
            SS.RemoveStructure(FindStructure(SS, "CrossSurface", "Id"));
            SS.RemoveStructure(FindStructure(SS, "CrossInterior","Id"));
            CurrentProgress = 95;
            PostBODY();
            CurrentProgress = 100;
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
            Structure BODY = FindStructure(SS, "EXTERNAL", "DicomType");
            double BodyVolume = new double();
            if (BODY == null)
            {
                bodyParameter(SS, -350, true, 1, true, 0.2, true, true, 0.2, true, 3);
                BodyVolume = FindStructure(SS, "EXTERNAL", "DicomType").Volume;
                SS.RemoveStructure(FindStructure(SS, "EXTERNAL", "DicomType"));
                bodyParameter(SS, -350, false, 1, false, 0.2, true, true, 0.2, true, 3);
            }
            else if (BODY.Volume == 0)
            {
                var BodyPar = SS.GetDefaultSearchBodyParameters();
                BodyPar.KeepLargestParts = true;
                SS.CreateAndSearchBody(BodyPar);
                BodyVolume = SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL").Volume;
                SS.RemoveStructure(SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL"));
                BodyPar = SS.GetDefaultSearchBodyParameters();
                //NTUH default setting
                BodyPar.KeepLargestParts = false;
                BodyPar.PreDisconnect = false;
                BodyPar.FillAllCavities = true;
                BodyPar.PreCloseOpenings = true;
                BodyPar.PreCloseOpeningsRadius = 0.2;
                BodyPar.Smoothing = true;
                BodyPar.SmoothingLevel = 3;
                SS.CreateAndSearchBody(BodyPar);
            }
        }
        public ICommand PostBODYCommand { get => new Command(PostBODY); }
        private void PostBODY()
        {
            //BODY part
            Structure BODY = SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL");
            Structure Temp = SS.AddStructure("CONTROL", "Temp_ForCouch");
            VVector[] TempVec = GetpseudoLine(FinalYcenter, SI.XSize, SI.YSize, YchkOrientation);
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
            bool imageResized = true;
            string errorCouch = "error";
            List<VVector> CSVVector = new List<VVector>();
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
                        Structure CouchSurface = SS.Structures.FirstOrDefault(e => e.Id == "CouchSurface");
                        Structure CouchInterior = SS.Structures.FirstOrDefault(e => e.Id == "CouchInterior");
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
                        PostBODY();
                    }
                    else if (errorCouch.Contains("Support structures already exist in the structure set."))
                    {
                        Structure Encompass = SS.Structures.FirstOrDefault(s => s.Id == "Encompass");
                        Structure EncompassBase = SS.Structures.FirstOrDefault(s => s.Id == "Encompass Base");
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
                            SS.RemoveStructure(SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL"));
                            var BodyPar = SS.GetDefaultSearchBodyParameters();
                            BodyPar.LowerHUThreshold = -700;
                            BodyPar.Smoothing = true;
                            BodyPar.SmoothingLevel = 1;
                            //NTUH default setting
                            BodyPar.KeepLargestParts = false;
                            //BodyPar.PreDisconnect = true;
                            //BodyPar.PreDisconnectRadius = 0.2;
                            //BodyPar.FillAllCavities = true;
                            //BodyPar.PreCloseOpenings = true;
                            //BodyPar.PreCloseOpeningsRadius = 0.2;
                            //BodyPar.Smoothing = true;
                            //BodyPar.SmoothingLevel = 3;
                            SS.CreateAndSearchBody(BodyPar);
                            Structure Temp = SS.AddStructure("CONTROL", "Temp_ForCouch");
                            VVector[] TempVec = GetpseudoLine(FinalYcenter, SI.XSize, SI.YSize, YchkOrientation);
                            for (int i = 0; i < Convert.ToInt32(SI.ZSize); i++)
                            {
                                Temp.AddContourOnImagePlane(TempVec, i);
                            }
                            Structure BODY = SS.Structures.FirstOrDefault(s => s.DicomType == "EXTERNAL");
                            BODY.SegmentVolume = BODY.SegmentVolume.Sub(Temp.SegmentVolume);
                            BODY.SegmentVolume = BODY.SegmentVolume.Sub(Encompass.SegmentVolume);
                            BODY.SegmentVolume = BODY.SegmentVolume.Sub(EncompassBase.SegmentVolume);
                            SS.RemoveStructure(Temp);
                            if (BODY.Volume > BodyVolume) { System.Windows.Forms.MessageBox.Show("Please Check your BODY carefully", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                            BODY.Comment = "Modified by ESAPI";

                            //NTUCC
                            foreach (Structure st in SS.Structures.Where(s => s.DicomType == "GTV"))
                            {
                                if (st.CanConvertToHighResolution() == true)
                                {
                                    st.ConvertToHighResolution();
                                }
                            }
                            foreach (Structure st in SS.Structures.Where(s => s.DicomType == "CTV"))
                            {
                                if (st.CanConvertToHighResolution() == true)
                                {
                                    st.ConvertToHighResolution();
                                }
                            }
                            foreach (Structure st in SS.Structures.Where(s => s.DicomType == "PTV"))
                            {
                                if (st.CanConvertToHighResolution() == true)
                                {
                                    st.ConvertToHighResolution();
                                }
                            }
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
            ModelBuilder modelBuilder = new ModelBuilder();
            DialogResult result = System.Windows.Forms.MessageBox.Show("You are Biulding Model for Couch", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (CouchInterior != null) modelBuilder.SaveModel(SC, CouchInterior, ModelFilePathCI);
                if (CouchSurface != null) modelBuilder.SaveModel(SC, CouchSurface, ModelFilePathCS);
                if (CrossInterior != null) modelBuilder.SaveModel(SC, CrossInterior, ModelFilePathCSI);
                if (CrossSurface != null) modelBuilder.SaveModel(SC, CrossSurface, ModelFilePathCSS);
            }
        }

        public ICommand ButtonCommand_CheckingBB { get => new Command(CheckingBB); }
        private void CheckingBB()
        {
            double BBs = 0;
            for (int i = 0; i < SC.Image.ZSize; i++)
            {
                foreach (VVector[] vectors in UserCS.GetContoursOnImagePlane(i))
                {
                    double vs = vectors.FirstOrDefault().z;
                    BBs = Math.Max(0, vs);
                }
            }
            Structure MsgStructure = SS.Structures.Where(s => s.Id.Contains("BBsForCouch")).FirstOrDefault();
            if (MsgStructure != null && SS.Structures.Where(s => s.DicomType == "MARKER").FirstOrDefault() != null)
            {
                string msg = "There are Both BB structure and Marker existed. Please confirm that which way do you prefer to double-check.";
                msg = msg + "\nYes = Use BB structure:" + MsgStructure.Id.ToString() + "\nNo = Use Marker :" + MarkerId;
                DialogResult DoubleChkResult = System.Windows.Forms.MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (DoubleChkResult == DialogResult.Yes)
                {
                    BBPositionRenew();
                    double distanceBB = BBs - MarkerLocationZ;//mm
                    CalculateBBLocation = BBCalDetect(distanceBB);
                }
                else if (DoubleChkResult == DialogResult.No)
                {
                    PositionRenew();
                    double distanceBB = BBs - MarkerLocationZ;//mm
                    CalculateBBLocation = BBCalDetect(distanceBB);
                }
                else { CalculateBBLocation = ""; }
            }
            else 
            {
                double distanceBB = BBs - MarkerLocationZ;//mm
                CalculateBBLocation = BBCalDetect(distanceBB);
            }

        }
        public List<VVector> Twopoint(ImageProfile InputProfile)
        {
            List<VVector> Twopoint = new List<VVector>();
            double OverallMaximum = InputProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value);
            double OverallMinimum = InputProfile.Where(p => !Double.IsNaN(p.Value)).Min(p => p.Value);
            double mean = InputProfile.Where(p => !Double.IsNaN(p.Value)).Average(p => p.Value);
            double Maximum = InputProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value) - OverallMinimum;
            double HalfTrend = Maximum / 2;
            for (int i = 1; i < InputProfile.Count() - 1; i++)
            {
                double iiHU = InputProfile[i].Value;
                double ii = InputProfile[i].Value - OverallMinimum;
                double iadd = InputProfile[i + 1].Value - OverallMinimum;
                double iminus = InputProfile[i - 1].Value - OverallMinimum;
                if (!Double.IsNaN(ii) && ii > HalfTrend && (ii > iadd) && (ii > iminus) && iiHU > mean + 100)
                {
                    Twopoint.Add(InputProfile[i].Position);
                }
            }
            return Twopoint;
        }
        public String PeakDetect(List<VVector> Twopoint)
        {
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
        public VVector AxisAlignment(VVector Original, string LockBarType, double Xmin, double Ymax, double Zmin, double YchkOrientation, double ZchkOrientation)
        {
            //The value of YAxis is opposite
            double X = MarkerLocationX - XBaseAxis - Xmin;
            double Y = MarkerLocationY + YchkOrientation * (YBaseAxis - Ymax);
            if (YchkOrientation == -1 && ZchkOrientation == 1) { Y = Y - 67.4; }
            double Z = MarkerLocationZ - ZBaseAxis - SC.Image.Origin.z - Zmin;
            //double SSZAdd = Convert.ToInt32((MarkerLocationZZ - ZBaseAxis - ScriptContext.Image.Origin.z) / ScriptContext.Image.ZRes);
            var mapAxis = new Dictionary<string, VVector>()
            {
                {"H5",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, ZchkOrientation * (Original.z + Z  - 2 * HSpace))},
                {"H4",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, ZchkOrientation * (Original.z + Z - HSpace))},
                {"H3",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, ZchkOrientation * (Original.z + Z ))},
                {"H2",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, ZchkOrientation * (Original.z + Z  + HSpace))},
                {"H1",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, ZchkOrientation * (Original.z + Z  + 2 * HSpace))},
                {"0",  new VVector ( Original.x + X, YchkOrientation * Original.y + Y, ZchkOrientation * (Original.z + Z + 3 * HSpace))},
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
                else if (Ori == PatientOrientation.HeadFirstProne | Ori == PatientOrientation.FeetFirstSupine)
                {
                    Final[1] = Math.Max(VVectors[i].y, Final[1]);
                }
                Final[2] = Math.Min(VVectors[i].z, Final[2]);
            }
            return Final;
        }

        public VVector[] GetpseudoLine(double yPlane, double Xsize, double Ysize, double chkorientation)
        {
            List<VVector> vvectors = new List<VVector>();
            vvectors.Add(new VVector(-Xsize, yPlane, 0));
            vvectors.Add(new VVector(Xsize, yPlane, 0));
            vvectors.Add(new VVector(Xsize, chkorientation * Ysize, 0));
            vvectors.Add(new VVector(-Xsize, chkorientation * Ysize, 0));
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
        public int GetSlice(double z)
        {
            var imageRes = SS.Image.ZRes;
            return Convert.ToInt32((z - SS.Image.Origin.z) / imageRes);
        }
        public Structure FindStructure(StructureSet SS, string name, string Type)
        {
            if (Type == "Id")
            { return SS.Structures.FirstOrDefault(e => e.Id == name); }
            else if (Type == "DicomType")
            { return SS.Structures.FirstOrDefault(e => e.DicomType == name); }
            else { return null; }

        }
        public Structure bodyParameter(StructureSet SS, int Lower, bool K, int N, bool D, double DRadius, bool F, bool Pre, double PreRadius, bool S, int SLevel)
        {
            var BodyPar = SS.GetDefaultSearchBodyParameters();
            BodyPar.FillAllCavities = F;
            BodyPar.KeepLargestParts = K;
            BodyPar.LowerHUThreshold = Lower;
            BodyPar.NumberOfLargestPartsToKeep = N;
            BodyPar.PreCloseOpenings = Pre;
            BodyPar.PreCloseOpeningsRadius = PreRadius;
            BodyPar.PreDisconnect = D;
            BodyPar.PreDisconnectRadius = DRadius;
            BodyPar.Smoothing = S;
            BodyPar.SmoothingLevel = SLevel;
            return SS.CreateAndSearchBody(BodyPar);
        }
    }
}