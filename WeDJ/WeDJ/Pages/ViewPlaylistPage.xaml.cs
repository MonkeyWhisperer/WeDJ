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
    public partial class ViewPlaylistPage : ContentPage
    {
        private CancellationTokenSource throttleCts = new CancellationTokenSource();
        public Room room = new Room();

        public ViewPlaylistPage(Room room2)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            Title = "View Playlist";
            room = room2;
            var ignore = taskFinishLoading();         
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
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                    }
                    PartyListScrollView.Content = grid;
                });
            });

        }

        async void OpenMenu2(object sender, EventArgs args)
        {
            MessagingCenter.Send<ViewPlaylistPage>(this, "OpenMenu3");
        }

        async void ClosePage(object sender, EventArgs args)
        {
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }    
    }
}