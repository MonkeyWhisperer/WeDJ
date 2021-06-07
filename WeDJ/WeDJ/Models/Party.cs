using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDJ.Models
{
    public class Party 
    {
        #region Party Private Methods 
        #endregion
        [JsonProperty(PropertyName = "party_id")]
        public int PartyID { get; set; }
        [JsonProperty(PropertyName = "room_id")]
        public int RoomID { get; set; }
        [JsonProperty(PropertyName = "party_name")]
        public string PartyName { get; set; }
        [JsonProperty(PropertyName = "party_start")]
        public DateTime PartyStart { get; set; }
        [JsonProperty(PropertyName = "party_end")]
        public DateTime? PartyEnd { get; set; }
        [JsonProperty(PropertyName = "party_fee")]
        public double PartyFee { get; set; }
        [JsonProperty(PropertyName = "display_order")]
        public int DisplayOrder { get; set; }
        [JsonProperty(PropertyName = "party_active")]
        public bool PartyActive { get; set; }
        [JsonProperty(PropertyName = "party_djs")]
        public int PartyDJS { get; set; }
    }
}
