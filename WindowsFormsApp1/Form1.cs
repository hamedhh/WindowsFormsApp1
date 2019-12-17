using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitMap();
            InitLatLngs();
        }


        List<LatLngIdentiy> lstIden = new List<LatLngIdentiy>();
        List<PolyganIdentity> polies = new List<PolyganIdentity>();
        GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");

        public void InitLatLngs()
        {
            lstIden.Add(new LatLngIdentiy("35.696887, 51.341422",1));
            lstIden.Add(new LatLngIdentiy("35.718439, 51.353885",2));
            lstIden.Add(new LatLngIdentiy("35.721948, 51.448784", 3));
            lstIden.Add(new LatLngIdentiy("35.707518, 51.446272", 4));
            lstIden.Add(new LatLngIdentiy("35.785415, 51.397596", 5));
            lstIden.Add(new LatLngIdentiy("35.760401, 51.291507", 6));
            lstIden.Add(new LatLngIdentiy("35.735378, 51.480500", 7));
            lstIden.Add(new LatLngIdentiy("35.730254, 51.506598", 8));
            lstIden.Add(new LatLngIdentiy("35.776957, 51.600064", 9));
            lstIden.Add(new LatLngIdentiy("35.684324, 51.487177", 10));
        }

        Timer tm = new Timer();
         
        private void button1_Click(object sender, EventArgs e)
        {
            //double lat = 35.6971159;
            //double lng = 51.3418066;
            //gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            ////var tt = gMapControl1.FromLatLngToLocal(new GMap.NET.PointLatLng() { Lat = 35.6971159, Lng = 51.3418066 });
            //gMapControl1.Position = new GMap.NET.PointLatLng(lat, lng);
            //gMapControl1.ShowCenter = false;
            //gMapControl1.MinZoom = 3;
            //gMapControl1.MaxZoom = 17;
            //gMapControl1.MarkersEnabled = true;
            //gMapControl1.Overlays.Add(new GMap.NET.WindowsForms.GMapOverlay());
            //GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            //GMap.NET.WindowsForms.GMapMarker markerA = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(lat, lng), GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
            //GMap.NET.WindowsForms.GMapMarker markerB = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(35.7319066, 51.4176804), GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
            //markers.Markers.Add(markerA);
            //markers.Markers.Add(markerB);
            //var points = markers.Markers.Select(p => p.Position).ToList();
            //GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            //polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            //polygon.Stroke = new Pen(Color.Red, 1);
            //markers.Polygons.Add(polygon);
            //gMapControl1.Overlays.Add(markers);
            //gMapControl1.CanDragMap = true;
            //gMapControl1.DragButton = MouseButtons.Left;


            tm.Tick += Tm_Tick;
            tm.Interval = 10000;
            tm.Start();

        }

        public void InitMap()
        {
            double lat = 35.6971159;
            double lng = 51.3418066;
            gMapControl1.MapProvider = GMap.NET.MapProviders.BingSatelliteMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            //var tt = gMapControl1.FromLatLngToLocal(new GMap.NET.PointLatLng() { Lat = 35.6971159, Lng = 51.3418066 });
            gMapControl1.Position = new GMap.NET.PointLatLng(lat, lng);
            gMapControl1.ShowCenter = true;
            gMapControl1.MinZoom = 3;
            gMapControl1.MaxZoom = 17;
            gMapControl1.MarkersEnabled = true;
            DrawPointing(lat, lng);
            gMapControl1.Overlays.Add(markers);
            gMapControl1.CanDragMap = true;
            gMapControl1.DragButton = MouseButtons.Left;

        }

        void DrawPointing(double lat, double lng)
        {
            GMap.NET.WindowsForms.GMapMarker markerB = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(lat, lng), GMap.NET.WindowsForms.Markers.GMarkerGoogleType.black_small);
            markers.Markers.Add(markerB);
        }
        
        void DrawPointing(LatLngIdentiy dest)
        {
            if (dest.isMarker)
                return;
            dest.isMarker = true;
            GMap.NET.WindowsForms.GMapMarker markerB = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(dest.lat, dest.lng), GMap.NET.WindowsForms.Markers.GMarkerGoogleType.blue_pushpin);
            markers.Markers.Add(markerB);
        }
        void DrawPolygan(LatLngIdentiy source, LatLngIdentiy dest)
        {
            var points = new List<GMap.NET.PointLatLng>();
            points.Add(new GMap.NET.PointLatLng(source.lat, source.lng));
            points.Add(new GMap.NET.PointLatLng(dest.lat, dest.lng));
            GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            markers.Polygons.Add(polygon);
        }

        int xindex = 0;
        private void Tm_Tick(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gMapControl1.Zoom += 4;
            //gMapControl1.ZoomAndCenterMarkers
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ////var temp =  MapIdentity.MakeNewRouting(polies, lstIden);
                //if (temp != null)
                //  polies.Add(temp);
                var tmpe = polies.Count + 1;
                xindex++;
                switch (xindex)
                {
                    case 1:
                        polies.Add(new PolyganIdentity(tmpe, lstIden[2], lstIden[1]));
                        break;
                    case 2:
                        polies.Add(new PolyganIdentity(tmpe, lstIden[2], lstIden[3]));
                        break;

                    case 3:
                        polies.Add(new PolyganIdentity(tmpe, lstIden[2], lstIden[5]));
                        break;

                    case 4:
                        polies.Add(new PolyganIdentity(tmpe, lstIden[2], lstIden[7]));
                        break;

                    case 5:
                        polies.Add(new PolyganIdentity(tmpe, lstIden[2], lstIden[8]));
                        break;

                    case 6:
                        polies.Add(new PolyganIdentity(tmpe, lstIden[2], lstIden[6]));
                        break;
                }
                DrawPointing(polies[polies.Count - 1].Source);
                DrawPointing(polies[polies.Count - 1].Destination);
                DrawPolygan(polies[polies.Count - 1].Source, polies[polies.Count - 1].Destination);
            }
            catch (Exception err)
            {

                MessageBox.Show(err.ToString());
            }
        }
    }
}
