using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsapMovie.Methods_and_Models;

namespace AsapMovie.Pages ;

    public partial class ShowMoviesInCategoriesPage : ContentPage
    {
        private readonly List<string> _categories;
        private readonly List<Movie> _movies;
        public ShowMoviesInCategoriesPage(List<Movie> movies, List<string> categories)
        {
            InitializeComponent();
            _movies = movies;
            _categories = categories;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }

        private void FillTheFront()
        {
            var sl = new StackLayout { Spacing = 5 , Margin = 10};
            foreach (var movie in _movies.Where(movie => _categories.All(item => movie.GetCategories().Contains(item))))
            {
                sl.Children.Add(MovieLayout(movie));
            }
            Content = new ScrollView { Content = sl };
        }

        private Grid MovieLayout(Movie movie)
        {
            var grid = new Grid { BackgroundColor = Colors.Aqua, ColumnSpacing = 10, Margin = 10};
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                
            var image = new Image { Source = movie.LoadImage() , HeightRequest = 300, WidthRequest = 200};
            grid.Children.Add(image);
            grid.SetColumn(image, 0);

            var titleLabel = new Label { Text = movie.Title, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Start};
            var descriptionLabel = new Label { Text = movie.Description, HorizontalTextAlignment = TextAlignment.Center ,LineBreakMode = LineBreakMode.CharacterWrap};
            var vsl1 = new VerticalStackLayout { Spacing = 5 , VerticalOptions = LayoutOptions.Center ,Children = { titleLabel, descriptionLabel }};
            grid.Children.Add(vsl1);
            grid.SetColumn(vsl1, 1);

            var openButton = new Button { Text = "Open File"};
            openButton.Clicked += async (sender, args) =>
            {
                try
                {
                    Process.Start(new ProcessStartInfo { FileName = "explorer.exe",Arguments = $"/select,\"{movie.Address}\""});
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                }
            };
            var vsl2 = new VerticalStackLayout { Spacing = 5 , VerticalOptions = LayoutOptions.Center, Children = { openButton }};
            grid.Children.Add(vsl2);
            grid.SetColumn(vsl2, 2);

            return grid;
        }
    }