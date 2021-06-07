using KompiMe.Views;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeDJ.Controls;
using WeDJ.CustomRenderers;
using WeDJ.Helpers;
using WeDJ.Models;
using WeDJ.Pages.AuthPages;
using WeDJ.Services;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace WeDJ.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationsPage : ContentPage
    {
        public List<Pin> pins = new List<Pin>();
        public List<CustomPin> cpins = new List<CustomPin>();
        private CustomEntry search = new CustomEntry();

        private CancellationTokenSource throttleCts = new CancellationTokenSource();

        public Room selectedRoom = new Room();


        private void EntryOnTextChanged(object sender, TextChangedEventArgs args)
        {
            Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel(); Task.Delay(TimeSpan.FromMilliseconds(800), this.throttleCts.Token).ContinueWith(delegate { this.HandleString(this.search.Text); }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }
        private void HandleString(string str)
        {
            if (search.Text.Length > 2)
            {
                var ignore = taskFinishLoading();

            }
        }
        public LocationsPage()
        {

            BackgroundColor = Color.FromHex("#64D9D6");

            InitializeComponent();



            NavigationPage.SetHasNavigationBar(this, false);
            //BackgroundImage = "bg.png";


            if (Xamarin.Forms.Device.Idiom != TargetIdiom.Tablet)
            {
                _whereToParty.FontSize = 16;

            }

            if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
            {
                Location1.FontSize = 14;
                Location2.FontSize = 14;
                Hours1.FontSize = 14;
                Hours2.FontSize = 14;
                Hours3.FontSize = 14;
            }

            var ignore = taskFinishLoading(); // are nevoie de asta pt ca nu poti pune await pe Main()




            _whereToParty.Focused += (s, e) =>
            {
                if (_whereToParty.Text == " ·   Where to Party?")
                {

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        _whereToParty.Text = string.Empty;

                        _whereToParty.Focus();
                    });
                }
            };

            _whereToParty.Unfocused += (s, e) =>
            {
                if (_whereToParty.Text == "")
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    {
                        _whereToParty.Text = " ·   Where to Party?";
                        _whereToParty.Focus();
                    });
                }
            };

        }


        async void OpenMenu(object sender, EventArgs args)
        {
            MessagingCenter.Send<LocationsPage>(this, "OpenMenu");
        }
    

        async Task taskFinishLoading()
        {
 
            LocationInfo.Opacity = 0;
            ErrorBackground.IsVisible = false;
            await Task.Run(() =>
            {
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                    if (status != PermissionStatus.Granted)
                    {
                        if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                        {
                           await DisplayAlert("Need location", "Gunna need that location", "OK");
                        }

                        var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                        //Best practice to always check that the key exists
                        if (results.ContainsKey(Permission.Location))
                            status = results[Permission.Location];
                    }

                    if (status != PermissionStatus.Unknown)
                    {
                        await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                    }

                    var locator = CrossGeolocator.Current;
                    if (locator.IsGeolocationEnabled && CrossConnectivity.Current.IsConnected)
                    {  
                        var position = await locator.GetPositionAsync();
                        var keyword = search.Text;
                        double roomLat = 0;
                        double roomLng = 0;
                        roomLat = position.Latitude;
                        roomLng = position.Longitude;
                        var radius = 0;
                        LocationServices ls = new LocationServices();

                        RoomGetList filters = new RoomGetList()
                        {
                            Radius = roomLat > 0 && roomLng > 0 ? radius : 0,
                            RoomLat = roomLat,
                            RoomLng = roomLng                            
                        };

                        var map = new Map()
                        {
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            MapType = MapType.Street
                        };

                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(roomLat, roomLng), Distance.FromMeters(1000)), true);
                        map.MyLocationEnabled = true;

                        var locations = await ls.LoadLocations(filters);

                        List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(locations.ToString());

                        PartyServices ps = new PartyServices();

                        foreach (Room item in rooms)
                        { 
                            if (item.Party != null)
                            {
                                CustomPin pin = new CustomPin
                                {
                                    Pin = new Pin
                                    {
                                        Type = PinType.Place,
                                        Position = new Position(item.RoomLat, item.RoomLng),
                                        Label = item.RoomName,
                                        Address = item.RoomAddress,
                                        Icon = BitmapDescriptorFactory.FromBundle("location_pin.png"),
                                        ZIndex=1,
                                        Tag = item
                                    }                                  
                                };
                                map.Pins.Add(pin.Pin);
                            }
                        }                        
                     
                        map.UiSettings.MyLocationButtonEnabled = false;
                        map.UiSettings.ZoomControlsEnabled = false;
                        map.UiSettings.CompassEnabled = false;

                        map.PinClicked += (sender, e) =>
                        {
                            e.Handled = !map.IsEnabled;
                        };

                        

                        map.SelectedPinChanged +=  (object sender, SelectedPinChangedEventArgs e) =>
                        {
                            if (e.SelectedPin != null)
                            {
                                LocationInfo.IsVisible = true;
                                Room room = (Room)e.SelectedPin.Tag;                       
                                LocationName.Text = room.RoomName;
                                Location2.Text = room.RoomAddress;
                                if (room.Party.PartyStart == room.Party.PartyEnd)
                                {

                                    Hours2.Text = room.Party.PartyStart.ToString("HH:mm");
                                    Hours3.Text = " - " + room.Party.PartyEnd?.ToString("HH:mm");
                                }
                                else {
                                    Hours2.Text = "JUKEBOX MODE";
                                }

                                LocationInfo.Opacity = 1;

                                selectedRoom = room;                                

                                var distanceToB = distance(roomLat, roomLng, room.RoomLat, room.RoomLng, 'K');
                                var userCanJoinParty = distanceToB < 1;

                                if (userCanJoinParty)
                                {
                                    selectedRoom.DisplayOrder = 1;
                                    JoinParyBtn.IsVisible = true;
                                    ViewpParyBtn.IsVisible = false;

                                }
                                else
                                {
                                    selectedRoom.DisplayOrder = 0;
                                    ViewpParyBtn.IsVisible = true;
                                    JoinParyBtn.IsVisible = false;
                                }

                                foreach (Pin x in map.Pins)
                                {
                                    x.Icon = BitmapDescriptorFactory.FromBundle("location_pin.png");
                                }

                                e.SelectedPin.Icon = BitmapDescriptorFactory.FromBundle("location_pin_selected.png");
                                e.SelectedPin.Label = null;
                                e.SelectedPin.Address = null;                             
                            }
                        };
    
                        MapPage.Children.Add(map);
                        Loader.IsVisible = false;
                        MenuIcon.IsVisible = true; 
                    }
                    else
                    {
                        Loader.IsVisible = false;

                        if (CrossConnectivity.Current.IsConnected)
                        {
                            if (locator.IsGeolocationAvailable && !locator.IsGeolocationEnabled)
                            {
                                if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                                {
                                    MainPageGrid.RowDefinitions[0].Height = 40;
                                }
                                else
                                {
                                    MainPageGrid.RowDefinitions[0].Height = 30;
                                }

                                ErrorBackground.IsVisible = true;
                                ErrorTypeMsg.Text = "Error -";
                                ErrorMsg.Text = "Turn on device location";
                            }
                            else if (!locator.IsGeolocationAvailable && !locator.IsGeolocationEnabled)
                            {
                                if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                                {
                                    MainPageGrid.RowDefinitions[0].Height = 40;
                                }
                                else
                                {
                                    MainPageGrid.RowDefinitions[0].Height = 30;
                                }

                                ErrorBackground.IsVisible = true;

                                ErrorTypeMsg.Text = "Error -";
                                ErrorMsg.Text = "Allow WeDJ to access location";
                            }
                        }
                        else
                        {
                            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                            {
                                MainPageGrid.RowDefinitions[0].Height = 40;
                            }
                            else
                            {
                                MainPageGrid.RowDefinitions[0].Height = 30;
                            }

                            ErrorBackground.IsVisible = true;
                            ErrorTypeMsg.Text = "Error -";
                            ErrorMsg.Text = "Please connect to internet and try again";
                        }
                    }
                });
            });
        }

        private void FocusWhere(object sender, EventArgs e)
        {
            if (_whereToParty.Placeholder == "Where to party?")
            {
                _whereToParty.Placeholder = string.Empty;
                _whereToParty.Focus();
            }
        }


        private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        private async void TryAgain(object sender, EventArgs e)
        {
            var ignore = taskFinishLoading();
        }

        private async void partyBtn(object sender, EventArgs e)
        {
            //if (joinParty.Text != "3. DECIDE")
            //{
            //    if (joinParty.Text == "VIEW PLAYLIST")
            //    {
            //        //await Navigation.PushAsync(new ViewLocationPlaylistMenuPage(), false);
            //    }
            //    else if (joinParty.Text == "JOIN PARTY")

            //    {
            //        //await Navigation.PushAsync(new LocationPlaylistMenuPage(), false);
            //        //await Navigation.PushAsync(new ViewLocationPlaylistPage());

            //    }
            //    else if (joinParty.Text == "WeDJ can access device location")

            //    {
            //        var locator = CrossGeolocator.Current;

            //        var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            //        if (status != PermissionStatus.Granted)lo
            //        {


            //            if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
            //            {
            //                //    await DisplayAlert("Need location", "Gunna need that location", "OK");
            //            }

            //            var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
            //            //Best practice to always check that the key exists
            //            if (results.ContainsKey(Permission.Location))
            //                status = results[Permission.Location];
            //        }

            //        if (locator.IsGeolocationAvailable && !locator.IsGeolocationEnabled)
            //        {
            //            joinParty.Text = "Location is turned on";
            //        }

            //        if (status != PermissionStatus.Unknown)
            //        {
            //            //await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
            //        }
            //    }
            //    else if (joinParty.Text == "Location is turned on")
            //    {
            //        //await Navigation.PushAsync(new LocationPlaylistMenuPage(), false);

            //    }
            //    else
            //    {
            //        Loader.IsRunning = true;
            //        var ignore = taskFinishLoading();
            //    }
            //}
        }

        private async void JoinParty(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Text == "JOIN PARTY")
            {
                await Navigation.PushModalAsync(new MenuPage(new PlaylistPage(selectedRoom)));
            }
            else
            {
                await Navigation.PushModalAsync(new MenuPage(new ViewPlaylistPage(selectedRoom)));
            }
        }
    }
}

