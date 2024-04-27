﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsapMovie.Methods_and_Models;

namespace AsapMovie.Pages ;

    public partial class MoviesToCategorizePage : ContentPage
    {
        private List<Movie> _movies;
        private readonly DbContext _dbContext;
        public MoviesToCategorizePage(DbContext dbContext)
        {
            InitializeComponent();
            Task.Run(async()=> _movies = await _dbContext.GetMovies());
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
                if (_movies.Any(movie => movie.Address == address)) continue;
                var button = new Button { Text = address  , BackgroundColor = Colors.Aqua};
                button.Clicked += (sender, args) =>
                {
                    Navigation.PushAsync(new CategorizeMoviePage(_dbContext, address));
                };
                sl.Children.Add(button);
            }
            Content = new ScrollView { Content = sl};
        }
    }