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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }

        private void FillTheFront()
        {
            var selectCategoriesButton = new Button { Text = "Select Categories", HeightRequest = 100};
            selectCategoriesButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new SelectCategoriesPage(_movies));
            };
    
            var categorizeMovie = new Button { Text = "Categorize Movie", HeightRequest = 100};
            categorizeMovie.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new MoviesToCategorizePage(_dbContext, _movies));
            };
            
            Content = new ScrollView { Content = new StackLayout { VerticalOptions = LayoutOptions.Center, Spacing = 50, Margin = 50, Children = {  selectCategoriesButton, categorizeMovie}} };
        }
    }