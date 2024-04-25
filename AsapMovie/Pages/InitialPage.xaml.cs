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
    }