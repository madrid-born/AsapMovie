using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsapMovie.Methods_and_Models;

namespace AsapMovie.Pages ;

    public partial class InitialPage : ContentPage
    {
        private readonly DbContext _dbContext;
        private List<Movie> _movies;

        public InitialPage(DbContext db)
        {
            InitializeComponent();
            _dbContext = db;
            Task.Run(async()=> _movies = await _dbContext.GetMovies());

            var selectCategoriesButton = new Button { Text = "Select Categories" };
            selectCategoriesButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new SelectCategoriesPage());
            };
    
            var categorizeMovie = new Button { Text = "Categorize Movie" };
            categorizeMovie.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new MoviesToCategorize());
            };

            Content = new ScrollView { Content = new StackLayout { Spacing = 5, Children = {  selectCategoriesButton, categorizeMovie}} };
        }
    }