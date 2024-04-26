using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsapMovie.Methods_and_Models;

namespace AsapMovie.Pages ;

    public partial class MoviesToCategorizePage : ContentPage
    {
        private List<Movie> _movies;
        private DbContext _dbContext;
        public MoviesToCategorizePage(DbContext dbContext, List<Movie> movies)
        {
            InitializeComponent();
            _movies = movies;
            _dbContext = dbContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }

        private void FillTheFront()
        {
            var sl = new StackLayout { Spacing = 5 };
            foreach (var address in Functions.AllMovies())
            {
                // if (_movies.Any(movie => address == movie.Address))
                // {
                //     continue;
                // }
                var button = new Button { Text = address  , BackgroundColor = Colors.Aqua};
                button.Clicked += (sender, args) =>
                {
                    Navigation.PushAsync(new CategorizeMoviePage(_dbContext, address));
                    button.BackgroundColor = Colors.Red;
                };
                sl.Children.Add(button);

            }
            Content = new ScrollView { Content = sl};
        }
    }