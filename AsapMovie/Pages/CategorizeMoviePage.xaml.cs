using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsapMovie.Pages ;

    public partial class CategorizeMoviePage : ContentPage
    {
        private string _address;
        public CategorizeMoviePage(string address)
        {
            InitializeComponent();
            _address = address;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }

        private void FillTheFront()
        {
            var sl = new StackLayout { Spacing = 5};
            
            // TODO: get information
            
            
            Content = new ScrollView { Content = sl};
        }
    }