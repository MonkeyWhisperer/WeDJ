using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeDJ.Helpers
{
    public class ApiUrls
    {
        public static string UserLogin = "user/login";
        public static string UserRegister = "user/register";
        public static string ForgotPassword = "user/forgotpassword";

        public static string SongAddOne = "song/addone";
        public static string SongUpdate = "song/update";
        public static string SongGetListByPartyId = "song/getlistbypartyid";
        public static string SongRate = "song/rate";

        public static string RoomGetList = "room/getlistwithnextparty";

        public static string PartyGetListByRoomId = "party/getlistbyroomid";

        public static string FacebookLogin = "/user/facebooklogin";
        public static string FacebookRegister = "/user/facebookregister";
    }   
}
