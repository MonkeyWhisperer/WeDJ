using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDJ.Models
{
    public class RoomGetList
    {
        [JsonProperty(PropertyName = "radius")]
        public int Radius { get; set; }
        [JsonProperty(PropertyName = "room_lat")]
        public double RoomLat { get; set; }
        [JsonProperty(PropertyName = "room_lng")]
        public double RoomLng { get; set; }
    }
}
