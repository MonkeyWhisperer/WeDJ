using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeDJ.Helpers;
using WeDJ.Models;
using WeDJ.Services;
using Xamarin.Forms;

namespace WeDJ.Views
{
    public partial class SearchSongPage : ContentPage
    {

        private int itemsPerPage = 20;
        private int pageNumber = 1;
        private bool dataLoading;
        private ObservableCollection<YoutubeItem> obsSongs;
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        private List<YoutubeItem> youtubeItems = new List<YoutubeItem>();

        private void EntryOnTextChanged(object sender, TextChangedEventArgs args)
        {
            Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel(); Task.Delay(TimeSpan.FromMilliseconds(800), this.throttleCts.Token).ContinueWith(delegate { this.HandleString(this.SearchEntry.Text); }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
        }


        private void HandleString(string str)
        {
            if (SearchEntry.Text.Length > 2)
            {
                Loader.IsVisible = true;
                SearchList.IsVisible = false;
                SearchList.Content = null;
                var ignore = Search(false);
            }
            else
            {
                SearchList.Content = null;
            }

        }


        public SearchSongPage()
        {
            InitializeComponent();
            Title = Settings.CurrentLocationName;

            if (Xamarin.Forms.Device.Idiom == TargetIdiom.Tablet)
            {
                SearchEntry.FontSize = 24;
            }
            else
            {
                SearchEntry.FontSize = 18;

            }

            var ignore = Search(true);
            this.SearchEntry.TextChanged += this.EntryOnTextChanged;
        }

        async void OnTapLike(object sender, EventArgs args)
        {
            StackLayout stack = (StackLayout)sender;
            var item = stack.BindingContext as YoutubeItem;
           Song song = new Song();

               //song.RoomID = Settings.ViewedLocationID;
               //song.PartyID = Settings.ViewedPartyID;
               song.SongUrl = item.VideoId;
               song.SongLastPlayed = DateTime.Now;
               song.SongName = item.Title;
               song.SongThumbnail = item.DefaultThumbnailUrl;
               song.UserID = Settings.UserID;
             
               SongServices songServices = new SongServices();
               await songServices.AddSongToParty(song);

        }

        private List<YoutubeItem> _youtubeItems;

        public List<YoutubeItem> YoutubeItems
        {
            get { return _youtubeItems; }
            set
            {
                _youtubeItems = value;
                OnPropertyChanged();
            }
        }

        private const string ApiKey = Settings.YoutubeAPI;


        private string apiUrlForSearch = "https://www.googleapis.com/youtube/v3/search?part=snippet&maxResults=30&type=video"
         + "&key="
         + ApiKey;





        async Task Search(bool prefill)
        {
            var httpClient = new HttpClient();
            SearchList.Opacity = 1;
            SearchList.Content = null;
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
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) });
            }
            else {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) });
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

                        var right = new Label { Text = snippet.Value<string>("title"), TextColor = Color.White, VerticalTextAlignment = TextAlignment.Start, FontSize  =24, FontFamily = "" };

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
                    else {
                        grid.RowDefinitions.Add(new RowDefinition { Height = 50 });
                        var left = new Image { Source = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("medium")?.Value<string>("url"), VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.AspectFit };

                        var right = new Label { Text = snippet.Value<string>("title"), TextColor = Color.White, VerticalTextAlignment = TextAlignment.Start };

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

                        var boxView = new BoxView { BackgroundColor = Color.White, Opacity = 0.4,  VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand};

                        grid.Children.Add(boxView, 0, row);

                        Grid.SetColumnSpan(boxView, 2);


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
            SearchList.Content = grid;

            //await Task.Delay(1500);

            Loader.IsVisible = false;

            SearchList.IsVisible = true;
        }

        private async void AddNextPageData()
        {
            //if (dataLoading)
            //    return;

            //dataLoading = true;

            //pageNumber++;

            //int skip = pageNumber * 10;
            
            //foreach (YoutubeItem yyy in youtubeItems.Skip(skip).Take(10))
            //{
            //    obsSongs.Add(yyy);
            //}

            //dataLoading = false;
        }

//        private async 
//        Task
//GetVideosDetailsAsync(List<string> videoIds)
//        {

//            var videoIdsString = "";
//            foreach (var s in videoIds)
//            {
//                videoIdsString += s + ",";
//            }

//            youtubeItems = new List<YoutubeItem>();

//            var httpClient = new HttpClient();

//            var json = await httpClient.GetStringAsync(string.Format(apiUrlForVideosDetails, videoIdsString));

//            pageNumber = 0;

//            try
//            {
//                JObject response = JsonConvert.DeserializeObject<dynamic>(json);

//                var items = response.Value<JArray>("items");

//                var counter = 0;

//                foreach (var item in items)
//                {
//                    var snippet = item.Value<JObject>("snippet");
//                    var statistics = item.Value<JObject>("statistics");

//                    var youtubeItem = new YoutubeItem
//                    {
//                        Title = snippet.Value<string>("title"),
//                        Description = snippet.Value<string>("description"),
//                        ChannelTitle = snippet.Value<string>("channelTitle"),
//                        PublishedAt = snippet.Value<DateTime>("publishedAt"),
//                        VideoId = item?.Value<string>("id"),
//                        DefaultThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("default")?.Value<string>("url"),
//                        MediumThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("medium")?.Value<string>("url"),
//                        HighThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("high")?.Value<string>("url"),
//                        StandardThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("standard")?.Value<string>("url"),
//                        MaxResThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("maxres")?.Value<string>("url"),
//                        ViewCount = statistics?.Value<int>("viewCount"),
//                        LikeCount = statistics?.Value<int>("likeCount"),
//                        DislikeCount = statistics?.Value<int>("dislikeCount"),
//                        FavoriteCount = statistics?.Value<int>("favoriteCount"),
//                        CommentCount = statistics?.Value<int>("commentCount"),

//                    };


             
//                    if (youtubeItem.Title.Length > 68)
//                    {
//                        youtubeItem.Title = youtubeItem.Title.Substring(0, Math.Min(youtubeItem.Title.Length, 68));
//                        youtubeItem.Title += "...";
//                    }

//                    //if (counter < 10)
//                    //{
//                    //    obsSongs.Add(youtubeItem);
//                    //}
//                    //counter++;
//                    //youtubeItems.Add(youtubeItem);



//                }

//                //return youtubeItems;
//            }
//            catch { }

//        }
    }
}
