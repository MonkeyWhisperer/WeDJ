using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace WeDJ.Models
{
    public class CustomPin
    {
        public CustomPin(Party party = null, Room room = null)
        {
            if (room == null || party == null)
                return;

            Pin = new Pin
            {
                Type = PinType.SearchResult,
                Position = new Position(room.RoomLat, room.RoomLng),
                Label = room.RoomName,
                Address = "Tap for details"
            };
            Party = party;

            LocationName = room.RoomName;
            Lat = room.RoomLat;
            Lng = room.RoomLng;
            RoomId = party.RoomID;
            PartyId = party.RoomID;
            EntryFee = party.PartyFee.ToString();
            StartedAt = party.PartyStart.ToString("hh:mm tt");
            DJS = party.PartyDJS.ToString();
        }

        public Pin Pin { get; set; }
        public Party Party { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }
        public int RoomId { get; set; }
        public int PartyId { get; set; }
        public string LocationName { get; set; }
        public string EntryFee { get; set; }
        public string StartedAt { get; set; }
        public string DJS { get; set; }
    }
}
