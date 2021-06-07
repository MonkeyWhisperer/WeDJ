using WeDJ.Models;
using WeDJ.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace WeDJ.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {

        /// <summary>
        /// The _calendar view
        /// </summary>
        readonly CalendarView _calendarView;
        /// <summary>
        /// The _relative layout
        /// </summary>
        readonly RelativeLayout _relativeLayout;
        /// <summary>
        /// The _stacker
        /// </summary>
        readonly ScrollView _stacker;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarPage"/> class.
        /// </summary>
        public EventsPage()
        {
            InitializeComponent();

            Title = "Calendar"; 
            this.BackgroundColor = Color.White;
            _relativeLayout = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Content = _relativeLayout;

            _calendarView = new CalendarView()
            {
                //BackgroundColor = Color.Blue
                MinDate = CalendarView.FirstDayOfMonth(DateTime.Now),
                MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddMonths(3)),
                HighlightedDateBackgroundColor = Color.FromRgb(227, 227, 227),
                ShouldHighlightDaysOfWeekLabels = false,
                SelectionBackgroundStyle = CalendarView.BackgroundStyle.CircleFill,
                TodayBackgroundStyle = CalendarView.BackgroundStyle.CircleOutline,
                HighlightedDaysOfWeek = new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Sunday },
                ShowNavigationArrows = true,
                MonthTitleFont = Font.OfSize("Open 24 Display St", NamedSize.Medium)
            };

            _relativeLayout.Children.Add(_calendarView,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height * 2 / 3));

            _stacker = new ScrollView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand
            };
            _relativeLayout.Children.Add(_stacker,
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Height * 2 / 3),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height * 1 / 3)
            );
            //_calendarView.DateSelected += async (object sender, DateTime e) =>
            //{
            //    EventService us = new EventService();
            //    var xxx = await us.GetList(e);

            //    List<Event> events = JsonConvert.DeserializeObject<List<Event>>(xxx);


            //    Grid grid = new Grid();
            //    Grid grid2 = new Grid();

            //    int row = 0;
            //    int row2 = 0;

            //    var i = 0;
            //    var j = 0;

            //    foreach (Event ev in events)
            //    {
            //        if (ev.EventStartDate < DateTime.Now)
            //        {
            //            i++;
            //            grid.RowSpacing = 0;
            //            grid.ColumnSpacing = 0;
                        
            //            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Absolute) });
            //            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Absolute) });
            //            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(180, GridUnitType.Absolute) });
            //            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Absolute) });
            //            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });

            //            if (i > 1)
            //            {
            //                var boxView3 = new BoxView { BackgroundColor = Color.FromHex("#F07F76") };
            //                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) });
            //                var boxView2 = new BoxView { BackgroundColor = Color.FromHex("#F3F3F3") };
            //                grid.Children.Add(boxView3, 0, row);
            //                grid.Children.Add(boxView2, 1, row);
            //                Grid.SetColumnSpan(boxView2, 4);
            //                row++;
            //            }
            //            else if (i == 1)
            //            {
            //                var boxView3 = new BoxView { BackgroundColor = Color.FromHex("#F07F76") };
            //                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) });
            //                var boxView2 = new Label { Text = "Past Events",  VerticalTextAlignment = TextAlignment.Center };
            //                grid.Children.Add(boxView3, 0, row);
            //                grid.Children.Add(boxView2, 2, row);
            //                Grid.SetColumnSpan(boxView2, 4);
            //                row++;
            //            }

            //            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            //            var boxView = new BoxView { BackgroundColor = Color.FromHex("#F07F76") };
            //            var label1 = new Label { Text = ev.EventName, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalTextAlignment = TextAlignment.Center, ClassId= ev.EventID.ToString(), FontAttributes = FontAttributes.Bold };
            //            var label2 = new Label { Text = "", HorizontalOptions = LayoutOptions.FillAndExpand, VerticalTextAlignment = TextAlignment.Center, ClassId = ev.EventID.ToString() }; // notification
            //            var label3 = new Label { Text = ev.EventStartDate.ToString("HH:mm tt"), HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.FromHex("#F07F76"), FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, ClassId = ev.EventID.ToString() };

            //            grid.Children.Add(boxView, 0, row);
            //            grid.Children.Add(label1, 2, row);
            //            grid.Children.Add(label2, 3, row);
            //            grid.Children.Add(label3, 4, row);


            //            TapGestureRecognizer EventTap = new TapGestureRecognizer();
            //            EventTap.Tapped += async (sender2, e2) =>
            //            {
            //                Label lbl = (Label)sender2;
            //                await Navigation.PushAsync(new ViewEventPage(int.Parse(lbl.ClassId)));
            //            };


            //            label1.GestureRecognizers.Add(EventTap);
            //            label2.GestureRecognizers.Add(EventTap);
            //            label3.GestureRecognizers.Add(EventTap);


            //            row++;

                      
            //        }
            //        else
            //        {
            //            j++;
            //            grid2.RowSpacing = 0;
            //            grid2.ColumnSpacing = 0;

            //            grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(8, GridUnitType.Absolute) });
            //            grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20, GridUnitType.Absolute) });
            //            grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(180, GridUnitType.Absolute) });
            //            grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Absolute) });
            //            grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) });

            //            if (j > 1)
            //            {
            //                var boxView3 = new BoxView { BackgroundColor = Color.FromHex("#743f31") };
            //                grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) });
            //                var boxView2 = new BoxView { BackgroundColor = Color.FromHex("#F3F3F3") };
            //                var boxView21 = new BoxView { BackgroundColor = Color.FromHex("#F3F3F3") };
            //                var boxView22 = new BoxView { BackgroundColor = Color.FromHex("#F3F3F3") };
            //                var boxView23 = new BoxView { BackgroundColor = Color.FromHex("#F3F3F3") };
            //                grid2.Children.Add(boxView3, 0, row2);
            //                grid2.Children.Add(boxView2, 1, row2);
            //                grid2.Children.Add(boxView21, 2, row2);
            //                grid2.Children.Add(boxView22, 3, row2);
            //                grid2.Children.Add(boxView23, 4, row2);
            //                //grid2.SetColumnSpan(boxView2, 4);
            //                row2++;
            //            }
            //            else if (j == 1)
            //            {
            //                var boxView3 = new BoxView { BackgroundColor = Color.FromHex("#743f31") };
            //                grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) });
            //                var boxView2 = new Label { Text = "Upcoming Events", VerticalTextAlignment = TextAlignment.Center };
            //                grid2.Children.Add(boxView3, 0, row2);
            //                grid2.Children.Add(boxView2, 2, row2);
            //                Grid.SetColumnSpan(boxView2, 4);
            //                row2++;
            //            }

            //            grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            //            var boxView = new BoxView { BackgroundColor = Color.FromHex("#743f31") };
            //            var label1 = new Label { Text = ev.EventName, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalTextAlignment = TextAlignment.Center, ClassId = ev.EventID.ToString(), FontAttributes = FontAttributes.Bold };
            //            var label2 = new Label { Text = "", HorizontalOptions = LayoutOptions.FillAndExpand, VerticalTextAlignment = TextAlignment.Center, ClassId = ev.EventID.ToString() }; // notification
            //            var label3 = new Label { Text = ev.EventStartDate.ToString("HH:mm tt"), HorizontalOptions = LayoutOptions.StartAndExpand, TextColor = Color.FromHex("#743f31"), FontAttributes = FontAttributes.Bold, VerticalTextAlignment = TextAlignment.Center, ClassId = ev.EventID.ToString() };

            //            grid2.Children.Add(boxView, 0, row2);
            //            grid2.Children.Add(label1, 2, row2);
            //            grid2.Children.Add(label2, 3, row2);
            //            grid2.Children.Add(label3, 4, row2);


            //            TapGestureRecognizer EventTap = new TapGestureRecognizer();
            //            EventTap.Tapped += async (sender2, e2) =>
            //            {
            //                Label lbl = (Label)sender2;
            //                await Navigation.PushAsync(new ViewEventPage(int.Parse(lbl.ClassId)));
            //            };


            //            label1.GestureRecognizers.Add(EventTap);
            //            label2.GestureRecognizers.Add(EventTap);
            //            label3.GestureRecognizers.Add(EventTap);


            //            row2++;

            //        }

            //    }

            //    var framePassed = new Frame();
            //    var frameUpcomming = new Frame();

            //    StackLayout stackAll = new StackLayout()
            //    {
            //        Padding = new Thickness(20, 0, 20, 10)
            
            //    };

            //    if (grid != null)
            //    {
            //        framePassed = new Frame
            //        {

            //            Content = grid,
            //            Padding = 0,
            //            Margin = new Thickness(0, 1, 0, 1), // bug
            //            HasShadow = true,
            //            VerticalOptions = LayoutOptions.CenterAndExpand,
            //            HorizontalOptions = LayoutOptions.Center,
            //            CornerRadius = 6
            //        };

            //        stackAll.Children.Add(framePassed);

            //    }


            //    if (grid2 != null)
            //    {
            //        frameUpcomming = new Frame
            //        {

            //            Content = grid2,
            //            Padding = 0,
            //            Margin = new Thickness(0, 1, 0, 1), // bug
            //            HasShadow = true,
            //            VerticalOptions = LayoutOptions.CenterAndExpand,
            //            HorizontalOptions = LayoutOptions.Center,
            //            CornerRadius = 6
            //        };

            //        stackAll.Children.Add(frameUpcomming);

            //    }


            //    _stacker.Content = stackAll;
            //};
        }
    }
}

