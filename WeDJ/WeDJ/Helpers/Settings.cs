// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace WeDJ.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private static readonly string SettingsDefault = string.Empty;

        public const string ServiceURL = "Your_API_URL";        
        public const string ApplicationCode = "Your_API_Application_Code";
        public const string CdnUrl = "Your_CDN_URL";
        public const string YoutubeAPI = "Youtube_API";

        private const string SToken = "sToken";
        private const string UserIDX = "UserID";
        private const string UserTypeIDX = "UserTypeID";
        private const string UserFirstNameX = "UserFirstName";
        private const string UserLastNameX = "UserLastName";
        private const string UserPictureX = "UserPicture";
        private const string UserFullnameX = "UserFullname";

        #endregion

        public static string UserFullname
        {
            get { return AppSettings.GetValueOrDefault(UserFullnameX, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(UserFullnameX, value); }
        }

        public static string SessionToken
        {
            get { return AppSettings.GetValueOrDefault(SToken, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(SToken, value); }
        }

        public static string UserFirstName
        {
            get { return AppSettings.GetValueOrDefault(UserFirstNameX, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(UserFirstNameX, value); }
        }

        public static string UserLastName
        {
            get { return AppSettings.GetValueOrDefault(UserLastNameX, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(UserLastNameX, value); }
        }

        public static string UserPicture
        {
            get { return AppSettings.GetValueOrDefault(UserPictureX, SettingsDefault); }
            set { AppSettings.AddOrUpdateValue(UserPictureX, value); }
        }

        public static int UserID
        {
            get { return AppSettings.GetValueOrDefault(UserIDX, 0); }
            set { AppSettings.AddOrUpdateValue(UserIDX, value); }
        }

        public static int UserTypeID
        {
            get { return AppSettings.GetValueOrDefault(UserTypeIDX, 0); }
            set { AppSettings.AddOrUpdateValue(UserTypeIDX, value); }
        }

        public static string CurrentLocationName { get; internal set; }
    }
}