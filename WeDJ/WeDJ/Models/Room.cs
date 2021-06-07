using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDJ.Models
{

    public class Room
    {
        #region Room Private Methods 
        #endregion
        [JsonProperty(PropertyName = "room_id")]
        public int RoomID { get; set; }
        [JsonProperty(PropertyName = "room_name")]
        public string RoomName { get; set; }
        [JsonProperty(PropertyName = "room_lat")]
        public double RoomLat { get; set; }
        [JsonProperty(PropertyName = "room_lng")]
        public double RoomLng { get; set; }
        [JsonProperty(PropertyName = "room_address")]
        public string RoomAddress { get; set; }
        [JsonProperty(PropertyName = "room_city")]
        public string RoomCity { get; set; }
        [JsonProperty(PropertyName = "room_state")]
        public string RoomState { get; set; }
        [JsonProperty(PropertyName = "room_country")]
        public string RoomCountry { get; set; }
        [JsonProperty(PropertyName = "room_owner_id")]
        public int RoomOwnerID { get; set; }
        [JsonProperty(PropertyName = "display_order")]
        public int DisplayOrder { get; set; }
        [JsonProperty(PropertyName = "room_active")]
        public bool RoomActive { get; set; }
        [JsonProperty(PropertyName = "radius")]
        public int RoomRadius { get; set; }
        [JsonProperty(PropertyName = "next_party")]
        public Party Party { get; set; } = new Party();

    }
}
