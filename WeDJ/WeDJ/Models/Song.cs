using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDJ.Models
{
     public class Song
    {
        [JsonProperty(PropertyName = "song_id")]
        public int SongID { get; set; }
        [JsonProperty(PropertyName = "user_id")]
        public int UserID { get; set; }
        [JsonProperty(PropertyName = "party_id")]
        public int PartyID { get; set; }
        [JsonProperty(PropertyName = "room_id")]
        public int RoomID { get; set; }
        [JsonProperty(PropertyName = "song_url")]
        public string SongUrl { get; set; }
        [JsonProperty(PropertyName = "song_name")]
        public string SongName { get; set; }
        [JsonProperty(PropertyName = "song_thumbnail")]
        public string SongThumbnail { get; set; }
        [JsonProperty(PropertyName = "song_is_playing")]
        public bool SongIsPlaying { get; set; }
        [JsonProperty(PropertyName = "song_last_played")]
        public DateTime? SongLastPlayed { get; set; }
        [JsonProperty(PropertyName = "song_likes")]
        public int SongLikes { get; set; }
        [JsonProperty(PropertyName = "song_unlikes")]
        public int SongUnlikes { get; set; }
        [JsonProperty(PropertyName = "song_start")]
        public double SongStart { get; set; }
        [JsonProperty(PropertyName = "display_order")]
        public int DisplayOrder { get; set; }
        [JsonProperty(PropertyName = "song_active")]
        public bool SongActive { get; set; }
        [JsonProperty(PropertyName = "user_song_like")]
        public bool? UserSongLike { get; set; }
        [JsonProperty(PropertyName = "song_like")]
        public int? SongLike { get; set; }

    }
}
