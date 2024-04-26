using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsapMovie.Methods_and_Models;

namespace AsapMovie.Pages ;

    public partial class ShowMoviesInCategoriesPage : ContentPage
    {
        private List<string> _categories;
        private List<Movie> _movies;
        public ShowMoviesInCategoriesPage(List<Movie> movies, List<string> categories)
        {
            InitializeComponent();
            _categories = categories;
            _movies = movies;

        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }

        private void FillTheFront()
        {
            var sl = new StackLayout { Spacing = 5 };

            // get movies
            foreach (var movie in _movies)
            {
                var button = new Button { Text = movie.Title };
                button.Clicked += async (sender, args) =>
                {
                    await Navigation.PushAsync(new ShowMoviePage(movie));
                };
            }
            Content = new ScrollView { Content = sl };
        }
    }