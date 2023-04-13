using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace CouchInsert
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl, INotifyPropertyChanged
    {
        private List<string> _markerNames;
        public List<String> MarkerNames
        {
            get => _markerNames;
            set
            {
                _markerNames = value;
                NotifyPropertyChanged(nameof(MarkerNames));
            }
        }
        private List<string> _markerPositions;
        public List<String> MarkerPositions
        {
            get => _markerPositions;
            set
            {
                _markerPositions = value;
                NotifyPropertyChanged(nameof(MarkerPositions));
            }
        }
        private VVector _markerLocation;
        public VVector MarkerLocation
        {
            get => _markerLocation;
            set
            {
                _markerLocation = value;
                NotifyPropertyChanged(nameof(MarkerLocation));
            }
        }
        public string _markerLocationY;
        public String MarkerLocationY
        {
            get => _markerLocationY;
            set
            {
                _markerLocationY = value;
                NotifyPropertyChanged(nameof(MarkerLocationY));
            }
        }
        public VVector SIU { get; set; }
        private string _result;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                NotifyPropertyChanged(nameof(Result));
            }
        }
        public Structure CollisionZone { get; set; }
        public Structure pseudoCouch { get; set; }
        public ScriptContext SC { get; set; }
        public StructureSet ss { get; set; }
        public Structure newStructure { get; set; }
        public UserControl1(ScriptContext scriptContext)
        {
            SC = scriptContext;
            VVector SIU = scriptContext.Image.UserOrigin;
            ss = scriptContext.StructureSet;
            var MulIso = ss.Structures.Where(s => s.DicomType == "MARKER").ToList();
            MarkerNames = new List<String>();
            foreach (Structure Iso in MulIso)
                {
                    MarkerNames.Add(Iso.Id);
                }
            PositionRenew();

            MarkerPositions = new List<String>();
            MarkerPositions.Add("H5");
            MarkerPositions.Add("H4");
            MarkerPositions.Add("H3");
            MarkerPositions.Add("H2");
            MarkerPositions.Add("H1");
            MarkerPositions.Add("0");
            MarkerPositions.Add("F1");
            MarkerPositions.Add("F2");


            double XMax = 0;
            double Xmin = 0;


            //for (int i = 0; i < SC.Image.ZSize; i++)
            //{
            //    foreach (VVector[] vectors in CouchPositionZ.GetContoursOnImagePlane(i))
            //    {
            //        CollisionAxisY = vectors.Select(v => v.y).Min();
            //        XMax = vectors.Select(v => v.x).Max();
            //        Xmin = vectors.Select(v => v.x).Min();
            //        CollisionAxisX = (XMax + Xmin) / 2;
            //    }
            //}



            newStructure.SegmentVolume = pseudoCouch.Or(BODY).Sub(CollisionZone);
            Result = "";


            //var CollisionZone = context.StructureSet.Structures.First(e => e.Id == "CollisionZone");

            //MessageBox.Show(CollisionAxisX.ToString());

            //string filePath = @"\\Vmstbox151\va_data$\ProgramData\Vision\PublishedScripts\contour.txt";
            //for (int i = 0; i < context.Image.ZSize; i++)
            //{
            //    foreach (VVector[] vectors in Couch.GetContoursOnImagePlane(i))
            //    {
            //        using (StreamWriter writer = new StreamWriter(filePath))
            //        {
            //            writer.WriteLine(String.Join(",", vectors.Select(v => $"{v.x}, {v.y},\n ")));
            //        }
            //    }
            //}

            InitializeComponent();
            DataContext = this;

        }
        private void PositionRenew()
        {
            string markerId;
            if (marker != null && marker.SelectedValue != null) markerId = marker.SelectedValue.ToString();
            else markerId = "marker";
            
            Structure MarkerLocationItem = ss.Structures.Where(s => s.DicomType == "MARKER").ToList().Where(a => a.Id == markerId).FirstOrDefault();
            
            if (MarkerLocationItem != null)
            {
                MarkerLocationY = Math.Round((MarkerLocationItem.CenterPoint.y) / 10, 2).ToString();

            }
            else MarkerLocationY = "";
        }


        public VVector[] GetCircleContours(double xRes, double yRes, double radius, VVector UserOrigin)
        {
            List<VVector> vvectors = new List<VVector>();
            for (double x = -1 * radius; x < radius; x += xRes)
            {
                double y = Math.Sqrt(Math.Abs(radius * radius - x * x));
                vvectors.Add(new VVector(x + UserOrigin.x, y + UserOrigin.y, 0));
            }

            for (double x = radius; x > -1 * radius; x -= xRes)
            {
                double y = -1 * Math.Sqrt(Math.Abs(radius * radius - x * x));
                vvectors.Add(new VVector(x + UserOrigin.x, y + UserOrigin.y, 0));
            }
            return vvectors.ToArray();
        }

        public VVector[] GetpseudoCouchContours(double yAxis)
        {
            List<VVector> vvectors = new List<VVector>();
            for(double y = 0;)
            vvectors.Add(new VVector(xAxis - (tableWidth / 2), yAxis, 0));
            //vvectors.Add(new VVector(xAxis - (tableWidth / 2), yAxis + 50, 0));
            vvectors.Add(new VVector(xAxis + (tableWidth / 2), yAxis + 50, 0));
            //vvectors.Add(new VVector(xAxis + (tableWidth / 2), yAxis, 0));
            return vvectors.ToArray();
        }

        private void CheckCollision(Structure structure)
        {
            if (structure == null) Result = "Structure was null!";
            else if (structure.Volume != 0) { Result = " ⚠️Collision⚠️ "; }
            else { Result = " No Collision! 😊 "; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckCollision(newStructure);
        }

        private void Button_CouchPosition(object sender, RoutedEventArgs e)
        {
            SC.Patient.BeginModifications();
            Structure CouchPositionZ = ss.Structures.FirstOrDefault(a => a.Id == "CouchPosition");
            if (CouchPositionZ == null)
            {
                CouchPositionZ = ss.AddStructure("CONTROL", "CouchPosition");
            }
            else
            {
                SC.StructureSet.RemoveStructure(CouchPositionZ);
                CouchPositionZ = ss.AddStructure("CONTROL", "CouchPosition");
            }
            for (int i = 0; i < SC.Image.ZSize; i++)
            {
                CouchPositionZ.AddContourOnImagePlane(GetpseudoCouchContours(MarkerLocationY), i);
            }
            Structure newStructure = ss.AddStructure("CONTROL", "segment");
        }


        //public static IEnumerable<int> _GetMeshBounds(Structure structure, StructureSet SS)
        //{
        //    var mesh = structure.MeshGeometry.Bounds;
        //    var meshLow = _GetSlice(mesh.Z, SS);
        //    var meshUp = _GetSlice(mesh.Z + mesh.SizeZ, SS) + 1; return Enumerable.Range(meshLow, meshUp);
        //}

    }
}
