using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsapMovie.Methods_and_Models;

namespace AsapMovie.Pages ;

    public partial class SelectCategoriesPage : ContentPage
    {
        private List<string> _checkedList = new ();
        private List<Movie> _movies;

        public SelectCategoriesPage(List<Movie> movies)
        {
            InitializeComponent();
            _movies = movies;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }

        private void FillTheFront()
        {
            _checkedList = new List<string>();
            var sl = new StackLayout();

            var executeButton = new Button
            {
                Text = "Execute"
            };
            executeButton.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new ShowMoviesInCategoriesPage(_movies, _checkedList));
            };
            
            sl.Children.Add(executeButton);

            var categories = Functions.GetCategories();

            var vsl = new VerticalStackLayout { Spacing = 5 };
            sl.Add(vsl);
            foreach (var item in categories)
            {
                vsl.Children.Add(CategoryCheckBox(item));

            }

            var addNewCategoryButton = new Button { Text = "Add New Category" };
            addNewCategoryButton.Clicked += async (sender, args) =>
            {
                var userInput = await Application.Current.MainPage.DisplayPromptAsync("Title", "Message", "OK", "Cancel", "Default text");
                if (userInput != null)
                {
                    Functions.SetCategory(userInput);
                    vsl.Children.Add(CategoryCheckBox(userInput));
                    await DisplayAlert("Message", "Successfully added", "OK");
                }
                else
                {
                    await DisplayAlert("Message", "Error", "OK");
                }
            }; 
            sl.Children.Add(addNewCategoryButton);
            
            Content = new ScrollView { Content = sl };
        }

        private HorizontalStackLayout CategoryCheckBox(string item)
        {
            var hsl = new HorizontalStackLayout();
                
            var checkBox = new CheckBox
            {
                IsChecked = false
            };
                
            var label = new Label
            {
                Text = item,
                VerticalOptions = LayoutOptions.Center
            };
                
            checkBox.BindingContext = label.Text;
            checkBox.CheckedChanged += (sender, args) =>
            {
                if (args.Value)
                {
                    _checkedList.Add(label.Text);
                    return;
                }
                _checkedList.Remove(label.Text);
            };
                
            hsl.Children.Add(checkBox);
            hsl.Children.Add(label);
            return hsl;
        }
    }