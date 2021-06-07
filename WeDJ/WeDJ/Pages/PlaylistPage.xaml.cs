using FormsPlugin.Iconize;
using KompiMe.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Iconize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeDJ.CustomRenderers;
using WeDJ.Helpers;
using WeDJ.Models;
using WeDJ.Services;
using WeDJ.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeDJ.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistPage : ContentPage
    {
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        public Room room = new Room();

        private void EntryOnTextChanged(object sender, TextChangedEventArgs args)
        {
            Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel(); Task.Delay(TimeSpan.FromMilliseconds(800), this.throttleCts.Token).ContinueWith(delegate { this.HandleString(this.SearchEntry.Text); }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void HandleString(string str)
        {
            if (SearchEntry.Text.Length > 2)
            {
                SearchList.IsVisible = false;
                PartyListScrollView.HeightRequest = 0;
                var ignore = Search(false);
            }
            else
            {
                SearchList.IsVisible = false;
                CurrentSongImage.IsVisible = true;
                NoSongImage.IsVisible = true;
                ClosePageImg.IsVisible = true;
                ClosePageBox.IsVisible = true;
                PartyListScrollView.IsVisible = true;
                SongTitle.IsVisible = true;
                HorizontalLine.IsVisible = false;
                CloseSearchImg.IsVisible = false;                
            }




        }


        public PlaylistPage(Room room2)
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            Title = "Playlist";

            room = room2;

            var ignore = taskFinishLoading(); // are nevoie de asta pt ca nu poti pune await pe Main()
            //var ignore1 = Search(true);
            this.SearchEntry.TextChanged += this.EntryOnTextChanged;

        }

        async Task taskFinishLoading()
        {

            await Task.Run(async () =>
            {

                SongServices ss = new SongServices();
                Party party = new Party();
                party.PartyID = room.Party.PartyID;
                var songs = await ss.LoadPartySongs(party);

                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
               {


                   var shouldTimerContinueWork = true;








                   List<Song> songxx = JsonConvert.DeserializeObject<List<Song>>(songs.ToString());


                   var grid = new Grid { ColumnSpacing = 20, RowSpacing = 0 };

                   if (Xamarin.Forms.Device.Idiom == TargetIdiom.Phone)
                   {
                       grid.ColumnSpacing = 0;
                       grid.RowSpacing = 0;
                   }


                   if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                   {
                       grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                       grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) });
                   }
                   else
                   {
                       grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                       grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
                       grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                       grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                   }

                   try
                   {

                       grid.Padding = new Thickness(0, 5, 0, 0);

                       int row = 0;

                       if (songxx.Count == 0)
                       {
                           NoSongImage.Text = "Add song & start good vibes";
                       }

                       foreach (var item in songxx)
                       {
                           HorizontalLine.IsVisible = true;
                           if (!item.SongIsPlaying)
                           {

                               if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                               {
                                   grid.RowDefinitions.Add(new RowDefinition { Height = 100 });

                                   var left = new Image { Source = item.SongThumbnail, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Start };

                                   var right = new Label { Text = item.SongName, TextColor = Color.White, VerticalTextAlignment = TextAlignment.Start, FontSize = 24, FontFamily = "" };

                                   if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
                                   {
                                       right.FontFamily = "CircularStd-Book";
                                   }
                                   else

                                   {
                                       right.FontFamily = "CircularStd-Book.otf#CircularStd-Book";
                                   }

                                   grid.Children.Add(left, 0, row);

                                   grid.Children.Add(right, 1, row);
                                   row++;


                                   grid.RowDefinitions.Add(new RowDefinition { Height = 1 });

                                   var boxView = new BoxView { BackgroundColor = Color.White, Opacity = 0.4, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

                                   grid.Children.Add(boxView, 0, row);

                                   Grid.SetColumnSpan(boxView, 2);


                                   row++;
                               }
                               else
                               {
                                   grid.RowDefinitions.Add(new RowDefinition { Height = 10 });

                                   grid.RowDefinitions.Add(new RowDefinition { Height = 25 });
                                   grid.RowDefinitions.Add(new RowDefinition { Height = 25 });

                                   grid.RowDefinitions.Add(new RowDefinition { Height = 10 });

                                   row++;

                                   var left = new Image { Source = item.SongThumbnail, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.AspectFit };

                                   var right = new Label { Text = item.SongName, TextColor = Color.White, VerticalTextAlignment = TextAlignment.Start, Margin = new Thickness(10, 0, 0, 0) };

                                   if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
                                   {
                                       right.FontFamily = "CircularStd-Book";
                                   }
                                   else
                                   {
                                       right.FontFamily = "CircularStd-Book.otf#CircularStd-Book";
                                   }

                                   bool? like = item.UserSongLike;

                                   var likeColor = Color.White;
                                   var dislikeColor = Color.White;


                                   if (like != null)
                                   {
                                       if ((bool)like)
                                       {
                                           likeColor = Color.FromHex("#37A000");
                                       }
                                       else if (!(bool)like)
                                       {
                                           dislikeColor = Color.FromHex("#CD4141");
                                       }
                                   }
                                    //IconLabel ftu = new IconLabel { FontSize = 20, Text = "fa-thumbs-up", TextColor = Color.White, VerticalOptions = LayoutOptions.End, HorizontalOptions = LayoutOptions.Center };
                                    IconLabel ftu = new IconLabel { FontSize = 20, Text = "fa-thumbs-up", TextColor = likeColor, VerticalOptions = LayoutOptions.End, HorizontalOptions = LayoutOptions.Center };
                                   IconLabel ftd = new IconLabel { FontSize = 20, Text = "fa-thumbs-down", TextColor = dislikeColor, VerticalOptions = LayoutOptions.End, HorizontalOptions = LayoutOptions.Center };

                                   Label ftul = new Label { Text = item.SongLikes.ToString(), VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Center, TextColor = likeColor };
                                   Label ftdl = new Label { Text = item.SongUnlikes.ToString(), VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Center, TextColor = dislikeColor };

                                   grid.Children.Add(left, 0, row);

                                   grid.Children.Add(right, 1, row);

                                   Grid.SetRowSpan(left, 2);
                                   Grid.SetRowSpan(right, 2);


                                   grid.Children.Add(ftu, 3, row);
                                   grid.Children.Add(ftd, 2, row);

                                   row++;

                                   grid.Children.Add(ftul, 3, row);
                                   grid.Children.Add(ftdl, 2, row);

                                   row++;

                                   BoxView likeBox = new BoxView { StyleId = item.SongID.ToString(), ClassId = item.UserSongLike.ToString() };
                                   BoxView dislikeBox = new BoxView { StyleId = item.SongID.ToString(), ClassId = item.UserSongLike.ToString() };

                                   grid.Children.Add(likeBox, 3, row - 2);
                                   grid.Children.Add(dislikeBox, 2, row - 2);

                                   TapGestureRecognizer likeTap = new TapGestureRecognizer();
                                   likeTap.Tapped += async (sender, e) =>
                                   {

                                       BoxView song = (BoxView)sender;

                                       Song songTap = new Song();

                                       var songLike = song.ClassId;

                                       if (songLike == "" || songLike == "False")
                                       {
                                           songTap.SongLike = 1;
                                       }
                                       else
                                       {
                                           songTap.SongLike = null;
                                       }

                                       songTap.SongID = int.Parse(song.StyleId);

                                       await ss.RateSong(songTap);
                                       await taskFinishLoading();

                                   };

                                   likeBox.GestureRecognizers.Add(likeTap);

                                   TapGestureRecognizer dislikeTap = new TapGestureRecognizer();
                                   dislikeTap.Tapped += async (sender, e) =>
                                   {

                                       BoxView song = (BoxView)sender;

                                       Song songTap = new Song();

                                       var songLike = song.ClassId;

                                       if (songLike == "" || songLike == "True")
                                       {
                                           songTap.SongLike = 0;
                                       }
                                       else
                                       {
                                           songTap.SongLike = null;
                                       }

                                       songTap.SongID = int.Parse(song.StyleId);

                                       await ss.RateSong(songTap);
                                       await taskFinishLoading();

                                   };

                                   dislikeBox.GestureRecognizers.Add(dislikeTap);

                                   Grid.SetRowSpan(likeBox, 2);
                                   Grid.SetRowSpan(dislikeBox, 2);






                                   row++;

                                   grid.RowDefinitions.Add(new RowDefinition { Height = 1 });


                                   BoxView bv = new BoxView { BackgroundColor = Color.White, Opacity = 0.4 };

                                   grid.Children.Add(bv, 0, row);

                                   Grid.SetColumnSpan(bv, 4);


                                   row++;



                               }
                           }
                           else
                           {
                               SongTitle.Text = item.SongName;
                               CurrentSongImage.Source = item.SongThumbnail.Replace("default", "hqdefault").Replace("medium", "hqdefault").Replace("standard", "hqdefault");
                               //SongProgres.IsVisible = true;
                               //SongProgresBox.IsVisible = true;
                           }




                            //videoIds.Add(item.Value<JObject>("id")?.Value<string>("videoId"));
                        }
                        //obsSongs = new ObservableCollection<YoutubeItem>();

                        //SearchList.ItemsSource = obsSongs;
                        //Xamarin.Forms.Device.BeginInvokeOnMainThread(async() =>
                        //{
                        //    await GetVideosDetailsAsync(videoIds);
                        //});

                        //SearchList.ItemAppearing += (object sender, ItemVisibilityEventArgs e) =>
                        //{
                        //    var item = e.Item as YoutubeItem;
                        //    int index = obsSongs.IndexOf(item);
                        //    if (obsSongs.Count - 3 <= index && index < 40)
                        //        AddNextPageData();
                        //};





                    }
                   catch (Exception exception)
                   {
                   }
                   PartyListScrollView.Content = grid;




                   //Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                   //{
                   //    SongProgres.Value += 1;
                   //    SongProgres.MinColor = Color.FromHex("#D73333");
                   //    return shouldTimerContinueWork;
                   //});
               });
            });

        }

        async void OpenMenu2(object sender, EventArgs args)
        {
            MessagingCenter.Send<PlaylistPage>(this, "OpenMenu2");
        }

        async void ClosePage(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Xamarin.Forms.Device.StartTimer(TimeSpan.FromSeconds(20),  () =>
            {
                var shouldTimerContinueWork = true;
                Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                {
                    await taskFinishLoading();
                });
                return shouldTimerContinueWork;
            });
        }

            async void AddSong(object sender, EventArgs args, JToken item)
        {
            Song song = new Song();

            var snippet = item.Value<JObject>("snippet");


            song.RoomID = room.RoomID;
            song.PartyID = room.Party.PartyID;
            song.SongUrl = item?.Value<JObject>("id")?.Value<string>("videoId");
            song.SongLastPlayed = DateTime.Now;
            song.SongName = snippet.Value<string>("title");
            song.SongThumbnail = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("medium")?.Value<string>("url");
            song.UserID = Settings.UserID;

            SongServices songServices = new SongServices();
            await songServices.AddSongToParty(song);
            ResetSearch(sender, args);

        }


        async void ResetSearch(object sender, EventArgs args)
        {
            SearchEntry.Text = "";

        }





        protected override bool OnBackButtonPressed()
        {


            return base.OnBackButtonPressed();
        }

        private const string ApiKey = Settings.YoutubeAPI;


        private string apiUrlForSearch = "https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=30&type=video"
         + "&key="
         + ApiKey;

        async Task Search(bool prefill)
        {
            PartyListScrollView.HeightRequest = 0;

            var httpClient = new HttpClient();
            SearchList.Opacity = 1;
            //SearchList.Content = null;
            if (prefill)
            {

                SearchList.Opacity = 0;
            }

            string json = await httpClient.GetStringAsync(apiUrlForSearch + "&q=" + SearchEntry.Text);

            //var videos = JsonConvert.DeserializeObject<YoutubeItem>(json);

            //var videoIds = new List<string>();



            var grid = new Grid { ColumnSpacing = 20, RowSpacing = 20 };

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Phone)
            {
                grid.ColumnSpacing = 10;
                grid.RowSpacing = 10;
            }


            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });

            }
            else
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            }

            try
            {
                JObject response = JsonConvert.DeserializeObject<dynamic>(json);

                var items = response.Value<JArray>("items");
                int row = 0;



                foreach (var item in items)
                {
                    var snippet = item.Value<JObject>("snippet");

                    if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = 100 });

                        var left = new Image { Source = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("medium")?.Value<string>("url"), VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.Start };

                        var right = new Label { Text = snippet.Value<string>("title"), TextColor = Color.White, VerticalTextAlignment = TextAlignment.Start, FontSize = 24, FontFamily = "" };

                        if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
                        {
                            right.FontFamily = "CircularStd-Book";
                        }
                        else
                        {
                            right.FontFamily = "CircularStd-Book.otf#CircularStd-Book";
                        }

                        grid.Children.Add(left, 0, row);

                        grid.Children.Add(right, 1, row);
                        row++;


                        grid.RowDefinitions.Add(new RowDefinition { Height = 1 });

                        var boxView = new BoxView { BackgroundColor = Color.White, Opacity = 0.4, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

                        grid.Children.Add(boxView, 0, row);

                        Grid.SetColumnSpan(boxView, 2);


                        row++;
                    }
                    else
                    {
                        grid.RowDefinitions.Add(new RowDefinition { Height = 50 });
                        var left = new Image { Source = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("default")?.Value<string>("url"), VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.AspectFill };

                        var right = new Label { Text = snippet.Value<string>("title"), TextColor = Color.White, VerticalTextAlignment = TextAlignment.Start };
                        var button = new CustomButton { Text = "Add Song", HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center, TextColor = Color.White, Margin = new Thickness(0, 10, 0, 10) };

                        button.Clicked += (s, ev) => { AddSong(s, ev, item); };



                        if (Xamarin.Forms.Device.OS == TargetPlatform.iOS)
                        {
                            right.FontFamily = "CircularStd-Book";
                        }
                        else
                        {
                            right.FontFamily = "CircularStd-Book.otf#CircularStd-Book";
                        }


                        grid.Children.Add(left, 0, row);

                        grid.Children.Add(right, 1, row);
                        grid.Children.Add(button, 2, row);
                        row++;

                        grid.RowDefinitions.Add(new RowDefinition { Height = 1 });

                        var boxView = new BoxView { BackgroundColor = Color.White, Opacity = 0.4, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

                        grid.Children.Add(boxView, 0, row);

                        Grid.SetColumnSpan(boxView, 3);


                        row++;


                    }





                    //videoIds.Add(item.Value<JObject>("id")?.Value<string>("videoId"));
                }
                //obsSongs = new ObservableCollection<YoutubeItem>();

                //SearchList.ItemsSource = obsSongs;
                //Xamarin.Forms.Device.BeginInvokeOnMainThread(async() =>
                //{
                //    await GetVideosDetailsAsync(videoIds);
                //});

                //SearchList.ItemAppearing += (object sender, ItemVisibilityEventArgs e) =>
                //{
                //    var item = e.Item as YoutubeItem;
                //    int index = obsSongs.IndexOf(item);
                //    if (obsSongs.Count - 3 <= index && index < 40)
                //        AddNextPageData();
                //};





            }
            catch (Exception exception)
            {
            }
            SearchListScrollView.Content = grid;
            PartyListScrollView.HeightRequest = 0;
            //await Task.Delay(1500);

            //Loader.IsVisible = false;
            if (!prefill)
            {
                CurrentSongImage.IsVisible = false;
                NoSongImage.IsVisible = false;
                ClosePageImg.IsVisible = false;
                ClosePageBox.IsVisible = false;
                SongProgres.IsVisible = false;
                SongProgresBox.IsVisible = false;
                SongTitle.IsVisible = false;
                PartyListScrollView.IsVisible = false;
                HorizontalLine.IsVisible = false;
                //PlaylistBox.IsVisible = false;
                CloseSearchImg.IsVisible = true;
                //SearchList.IsVisible = true;
            }
        }


    }
}