using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

        private double? _markerLocationX;
        public double? MarkerLocationX
        {
            get => _markerLocationX;
            set
            {
                _markerLocationX = value;
                NotifyPropertyChanged(nameof(MarkerLocationX));
            }
        }
        private double? _markerLocationY;
        public double? MarkerLocationY
        {
            get => _markerLocationY;
            set
            {
                _markerLocationY = value;
                NotifyPropertyChanged(nameof(MarkerLocationY));
            }
        }
        private double? _markerLocationZ;
        public double? MarkerLocationZ
        {
            get => _markerLocationZ;
            set
            {
                _markerLocationZ = value;
                NotifyPropertyChanged(nameof(MarkerLocationZ));
            }
        }

        private double _markerLocationXX;
        public double MarkerLocationXX
        {
            get => _markerLocationXX;
            set
            {
                _markerLocationXX = value;
                NotifyPropertyChanged(nameof(MarkerLocationXX));
            }
        }
        private double _markerLocationYY;
        public double MarkerLocationYY
        {
            get => _markerLocationYY;
            set
            {
                _markerLocationYY = value;
                NotifyPropertyChanged(nameof(MarkerLocationYY));
            }
        }
        private double _markerLocationZZ;
        public double MarkerLocationZZ
        {
            get => _markerLocationZZ;
            set
            {
                _markerLocationZZ = value;
                NotifyPropertyChanged(nameof(MarkerLocationZZ));
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
        public IReadOnlyList<Structure> couchStructureList { get; }
        public double HSpace { get; set; }
        public double XBaseAxis { get; set; }
        public double YBaseAxis { get; set; }
        public double ZBaseAxis { get; set; }
        public double CSHU { get; set; }
        public double CIHU { get; set; }

        public MainViewModel()
        {
            MarkerPositions = new List<string>();
            MarkerNames = new List<string>();
            MarkerPositions.Add("AA");
            MarkerNames.Add("BB");
            MarkerNames.Add("CC");
        }
        //This Code is based on the Model slice thickness = 1mm and the minimun interpolate resolution is 0.5mm

        public MainViewModel(ScriptContext scriptContext)
        {
            ScriptContext = scriptContext;
            SIU = scriptContext.Image.UserOrigin;
            StructureSet = scriptContext.StructureSet;
            SliceThickness = ScriptContext.Image.ZRes.ToString() + "mm";

            var MulMarker = StructureSet.Structures.Where(s => s.DicomType == "MARKER").ToList();
            if (MulMarker.Count > 0)
            {
                MarkerNames = new List<String>();
                foreach (Structure Iso in MulMarker)
                {
                    MarkerNames.Add(Iso.Id);
                }
                PositionRenew();
            }
            else throw new Exception("There is no marker.  At least one marker DICOM type is required.");


            CrossInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CrossInterior");
            CrossSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CrossSurface");
            CouchInterior = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchInterior");
            CouchSurface = StructureSet.Structures.FirstOrDefault(e => e.Id == "CouchSurface");

            ScriptContext.Patient.BeginModifications();
            //XYZ = StructureSet.Structures.FirstOrDefault(s => s.Id == "XYZ");
            //XYZ.MeshGeometry.Positions.Clear();
            //XYZ.MeshGeometry.TriangleIndices.Clear();
            ////CreateSphere(XYZ.MeshGeometry, 10.0, 20);

            MarkerPositions = new List<String>();
            MarkerPositions.Add("H5");
            MarkerPositions.Add("H4");
            MarkerPositions.Add("H3");
            MarkerPositions.Add("H2");
            MarkerPositions.Add("H1");
            MarkerPositions.Add("0");


            //ScriptContext.Patient.BeginModifications();
            ////Below is shift 3D structure from Segshifter.cs
            //Structure struc = StructureSet.Structures.Single(s => s.Id == "XYZ");
            //SegShift segShift = new SegShift(new VVector(1, 2, 3), scriptContext.StructureSet, struc);

            ////Below is for AddCouch From ESAPI
            //PatientOrientation orientation = ScriptContext.Image.ImagingOrientation;
            //bool imageResized = true;
            //string errorCouch = "error";
            //if (StructureSet.CanAddCouchStructures(out errorCouch) == true)
            //{
            //    StructureSet.AddCouchStructures("Exact IGRT Couch, medium", orientation, RailPosition.In, RailPosition.In, null, null, null, out IReadOnlyList<Structure> couchStructureList, out imageResized, out errorCouch);
            //}
            //else
            //{
            //    ScriptContext.StructureSet.RemoveStructure(CouchInterior);
            //    ScriptContext.StructureSet.RemoveStructure(CouchSurface);
            //    StructureSet.AddCouchStructures("Exact IGRT Couch, medium", PatientOrientation.HeadFirstSupine, RailPosition.In, RailPosition.In, null, null, null, out IReadOnlyList<Structure> couchStructureList, out imageResized, out errorCouch);
            //}
        }

        public ICommand PositionRenewCommand { get => new Command(PositionRenew); }
        private void PositionRenew()
        {
            string markerId = "Marker";
            if (SelectedMarkerName != null) markerId = SelectedMarkerName;
            MarkerLocationItem = StructureSet.Structures.Where(s => s.DicomType == "MARKER").ToList().Where(a => a.Id == markerId).FirstOrDefault();

            if (MarkerLocationItem != null)
            {
                MarkerLocationX = Math.Round((MarkerLocationItem.CenterPoint.x - SIU.x) / 10, 2);
                MarkerLocationY = Math.Round((MarkerLocationItem.CenterPoint.y - SIU.y) / 10, 2);
                MarkerLocationZ = Math.Round((MarkerLocationItem.CenterPoint.z - SIU.z) / 10, 2);
                MarkerLocationXX = MarkerLocationItem.CenterPoint.x;
                MarkerLocationYY = MarkerLocationItem.CenterPoint.y;
                MarkerLocationZZ = MarkerLocationItem.CenterPoint.z;
                //this is the value of finding z locztion corresponded to z slice
                Start = new VVector(MarkerLocationItem.CenterPoint.x - 60, MarkerLocationItem.CenterPoint.y, MarkerLocationItem.CenterPoint.z);//60
                Stop = new VVector(MarkerLocationItem.CenterPoint.x + 5, MarkerLocationItem.CenterPoint.y, MarkerLocationItem.CenterPoint.z);//5
                PreallocatedBuffer = new double[1000];
                XProfile = ScriptContext.Image.GetImageProfile(Start, Stop, PreallocatedBuffer);
                SelectedMarkerPosition = PeakDetect(XProfile);
            }
            else
            {
                MarkerLocationX = null;
                MarkerLocationY = null;
                MarkerLocationZ = null;
            }
        }

        public ICommand ButtonCommand_AddCouch { get => new Command(AddCouch); }
        private void AddCouch()
        {
            ProgressVisibility = Visibility.Visible;
            //string filePathOuter = @"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\contour.csv";
            //if (!File.Exists(filePathOuter))
            //{
            //    System.Windows.MessageBox.Show($"No file exists at path {filePathOuter}");
            //    return;
            //}

            // add Mesh from here
            //Positions = "-1 -1 0  1 -1 0  -1 1 0  1 1 0";
            //Normals = "0 0 1  0 0 1  0 0 1  0 0 1";
            //TextureCoordinates = "0 1  1 1  0 0  1 0   ";
            //TriangleIndices = "0 1 2  1 3 2";


            //string[] filelines = File.ReadAllLines(FilePathCI_point);
            //try
            //{
            //    foreach (string line in filelines)
            //    {
            //        string[] splitLine = line.Split(',');
            //        double x = Double.Parse(splitLine[0].Trim());
            //        double y = Double.Parse(splitLine[1].Trim());
            //        double z = Double.Parse(splitLine[2].Trim());
            //        XYZ.MeshGeometry.Positions.Add(new Point3D(x, y, z));
            //    }
            //}
            //catch
            //{
            //    System.Windows.MessageBox.Show("There was an error when reading the file.  Please make sure that all rows are in the form: number, number, number");
            //    return;
            //}
            string[] Basiclines = File.ReadAllLines(@"\\Vmstbox161\va_data$\ProgramData\Vision\PublishedScripts\PathInformation.csv");
            string FileFolder = Basiclines[0].ToString() + SliceThickness;
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
            string UserCouchCSName = sourceAxis[6].ToString();
            string UserCouchCIName = sourceAxis[7].ToString();
            Structure UserCS = StructureSet.Structures.FirstOrDefault(e => e.Id == UserCouchCSName);
            Structure UserCI = StructureSet.Structures.FirstOrDefault(e => e.Id == UserCouchCIName);

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
            double MMX = MaxMinDetect(CSVVector)[0]; double MMY = MaxMinDetect(CSVVector)[1]; double MMZ = MaxMinDetect(CSVVector)[2];

            if (CrossSurface != null) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossSurface"));
            CrossSurface = ScriptContext.StructureSet.AddStructure("CONTROL", "CrossSurface");
            double Multiple = ScriptContext.Image.ZRes;

            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ);
                NewVVector.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
            }
            for (int i = 0; i < NewVVector.Max(p => p.z) + 1; i ++)
            {
                CrossSurface.AddContourOnImagePlane(NewVVector.Select(v => new VVector(v.x, v.y, v.z)).ToArray(), i);
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

            NewVVector.Clear();
            foreach (VVector vec in CSVVector)
            {
                VVector vv = AxisAlignment(vec, SelectedMarkerPosition, MMX, MMY, MMZ);
                NewVVector.Add(new VVector(vv.x, vv.y, Convert.ToInt32(vv.z / Multiple)));
            }
            for (int i = 0; i < NewVVector.Max(p => p.z) + 1; i ++)
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
            //double OriginZ = Math.Round(ScriptContext.Image.Origin.z, 1, MidpointRounding.AwayFromZero);
            //bool is_integer = unchecked(CheckSlice == Multiple);
            double OriginZ = NewVVector.Min(p => p.z);
            List<VVector> ForInterpolate = new List<VVector>();
            List<VVector> ForInterpolate1 = new List<VVector>();
            foreach (VVector vec in NewVVector.Where(p => ((p.z - OriginZ)/ Multiple) == (int)((p.z - OriginZ) / Multiple)))
            {
                ForInterpolate.Add(new VVector(vec.x, vec.y, Convert.ToInt32(vec.z / Multiple)));
            }
            List<double> CheckArray = new List<double>();
            CheckArray = ForInterpolate.Select(x => x.z).Distinct().ToList();
            double CheckSlice = Math.Abs(CheckArray[CheckArray.Count - 1] - CheckArray[CheckArray.Count - 2]);
            using (StreamWriter writer = new StreamWriter(@"C:\Users\aria\Downloads\Interpolation\ForInterpolate.csv"))
            {
                {
                    foreach (VVector vector in ForInterpolate)
                    {
                        writer.WriteLine(vector.x + "," + vector.y + "," + vector.z);
                    }
                }
            }
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

            UserCI.SegmentVolume = UserCI.SegmentVolume.Or(CrossInterior.SegmentVolume);
            UserCS.SegmentVolume = UserCS.SegmentVolume.Or(CrossSurface.SegmentVolume);
            if (CheckSlice == 1) { UserCS.SegmentVolume = UserCS.SegmentVolume.Sub(UserCI.SegmentVolume); }
            UserCI.SetAssignedHU(CIHU);
            UserCS.SetAssignedHU(CSHU);
            StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossSurface"));
            StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CrossInterior"));
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

        public ICommand ButtonCommand_AddCouchLine { get => new Command(AddCouchLine); }
        private void AddCouchLine()
        {
            ScriptContext.Patient.BeginModifications();
            if (StructureSet.Structures.Any(s => s.Id == "CouchLine")) StructureSet.RemoveStructure(StructureSet.Structures.First(s => s.Id == "CouchLine"));
            Structure CouchLine = StructureSet.AddStructure("CONTROL", "CouchLine");

            for (int i = 0; i < ScriptContext.Image.ZSize; i++)
            {
                CouchLine.AddContourOnImagePlane(GetpseudoLine(MarkerLocationYY), i);
            }
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
            double HalfTrend = 0;
            double Maximum = XProfile.Where(p => !Double.IsNaN(p.Value)).Max(p => p.Value);
            if (Maximum < 0)
            {
                HalfTrend = Maximum * 2;
            }
            else 
            {
                HalfTrend = Maximum / 2;
            }
            for (int i = 1; i < XProfile.Count - 1; i++)
            {

                double ii = XProfile[i].Value;
                double iadd = XProfile[i + 1].Value;
                double iminus = XProfile[i - 1].Value;
                if (!Double.IsNaN(ii) && ii > HalfTrend && (ii > iadd) && (ii > iminus))
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
        public VVector AxisAlignment(VVector Original, string LockBarType, double Xmin, double Ymax, double Zmin)
        {
            //The value of YAxis is opposite
            double X = MarkerLocationXX - XBaseAxis - Xmin;
            double Y = MarkerLocationYY + YBaseAxis - Ymax;
            double Z = MarkerLocationZZ - ZBaseAxis - ScriptContext.Image.Origin.z - Zmin;
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
        public double[] MaxMinDetect(List<VVector> VVectors)
        {
            double[] Final = { VVectors[0].x, VVectors[0].y, VVectors[0].z };
            for (int i = 1; i < VVectors.Count(); i++)
            {
                Final[0] = Math.Min(VVectors[i].x, Final[0]); //Always get the maximum value
                Final[1] = Math.Max(VVectors[i].y, Final[1]);
                Final[2] = Math.Min(VVectors[i].z, Final[2]);
            }
            return Final;
        }

    }
}