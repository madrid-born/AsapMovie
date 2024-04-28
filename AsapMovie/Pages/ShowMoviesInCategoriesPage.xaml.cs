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
            var grid = new Grid { BackgroundColor = Colors.Wheat, ColumnSpacing = 10, Margin = 10};
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                
            var image = new Image { Source = movie.LoadImage() , HeightRequest = 300, WidthRequest = 200, Margin = 10};
            grid.Children.Add(image);
            grid.SetColumn(image, 0);
            
            var insideGrid = new Grid { Margin = 10};
            insideGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            insideGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            var titleLabel = new Label { Text = movie.Title, HorizontalTextAlignment = TextAlignment.Center, VerticalOptions = LayoutOptions.Start, FontSize = 60, TextColor = Colors.Black};
            insideGrid.Children.Add(titleLabel);
            insideGrid.SetRow(titleLabel, 0);
            var descriptionLabel = new Label { Text = movie.Description, HorizontalTextAlignment = TextAlignment.Center , VerticalOptions = LayoutOptions.Center, TextColor = Colors.Black ,LineBreakMode = LineBreakMode.CharacterWrap};
            insideGrid.Children.Add(descriptionLabel);
            insideGrid.SetRow(descriptionLabel, 1);
            
            grid.Children.Add(insideGrid);
            grid.SetColumn(insideGrid, 1);

            var openButton = new Button { Text = "Open File", Margin = 10};
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