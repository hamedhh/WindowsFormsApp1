using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public abstract class LatLng
    {
        public double lat { get; set; }
        public double lng { get; set; }

    }
    public class LatLngIdentiy : LatLng
    {

        public  int Id { get; set; }
        public LatLngIdentiy(double lat, double lng,int id )
        {
            Id = id;
            this.lat = lat;
            this.lng = lng;
        }
        public LatLngIdentiy(string str,int id)
        {
            var temp = str.Split(',');
            Id = id;
            this.lat = Convert.ToDouble(temp[0]);
            this.lng = Convert.ToDouble(temp[1]);
        }
        public bool isMarker { get; set; }
    }


    public class PolyganIdentity
    {
        public string Name{ get; set; }
        public int id { get; set; }
        public LatLngIdentiy Source { get; set; }
        public LatLngIdentiy Destination { get; set; }
        public bool isVisible { get; set; }

        private void set(int id, LatLngIdentiy s, LatLngIdentiy d, bool visi = true)
        {
            this.id = id;
            isVisible = visi;
            Destination = d;
            Source = s;
        }
        public PolyganIdentity(int id, LatLngIdentiy s, LatLngIdentiy d, bool visi = true)
        {
            set(id, s, d, visi);
        }
        public PolyganIdentity(List<PolyganIdentity> polies, LatLngIdentiy s, LatLngIdentiy d, bool visi = true)
        {
            set(polies.Count, s, d, visi);
        }
    }

    public class MapIdentity
    {
        public static PolyganIdentity MakeNewRouting(List<PolyganIdentity> polies, List<LatLngIdentiy> latlngs)
        {
            var sourceIDs = polies.GroupBy(p => p.Source.Id, p => p.Destination, (k, g) => new { id = k, latlngs = g.ToDictionary(q => q.Id, q => q) }).ToDictionary(p => p.id, p => p.latlngs);
            var listAllIds = latlngs.GroupBy(p => p.Id, p => p, (k, g) => new { id = k, mainLatlngs = g.First() }).ToList();
            var listAllIdsDic = listAllIds.ToDictionary(p => p.id, p => p.mainLatlngs);
            var availableSource = listAllIds.Where(p =>!sourceIDs.ContainsKey(p.id) ||( sourceIDs.ContainsKey(p.id) && sourceIDs[p.id].Count < listAllIds.Count())).ToList();
            var availableDestForEachSource = sourceIDs.Where(n => availableSource.Any(m => m.id == n.Key)).Select(p => new
            {
                key = p.Key,
                value = listAllIdsDic[p.Key],
                avDest = p.Value.Where(r => r.Key != p.Key && !listAllIds.Any(k => k.id == p.Key)).Select(t => t.Value).ToList()
            }).ToList();

            if (availableDestForEachSource.Count() > 0)
            {
                var t = new PolyganIdentity(polies, availableDestForEachSource[0].value, availableDestForEachSource[0].avDest[0]);
                
                return t;
            }

            return null;
        }
    }

}
