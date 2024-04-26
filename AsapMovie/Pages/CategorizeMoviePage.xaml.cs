using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsapMovie.Methods_and_Models;

namespace AsapMovie.Pages ;

    public partial class CategorizeMoviePage : ContentPage
    {
        private string _address;
        private List<string> _checkedList = new ();
        private DbContext _dbContext;
        public CategorizeMoviePage(DbContext dbContext, string address)
        {
            InitializeComponent();
            _address = address;
            _dbContext = dbContext;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FillTheFront();
        }

        private void FillTheFront()
        {
            var sl = new StackLayout { Margin = 10, Spacing = 5};

            var titleEntry = new Entry { Text = Functions.MovieTitle(_address)};
            sl.Add(titleEntry);
            
            var textPath = Path.Combine(@"D:\Movies and Series\Movies\Project\Details",
                Functions.MovieTitle(_address) + ".txt");
            var fileContent = "";
            if (File.Exists(textPath))
            {
                using var sr = new StreamReader(textPath);
                fileContent = sr.ReadToEnd();
            }
            var descriptionEntry = new Entry { Text = fileContent };
            sl.Children.Add(descriptionEntry);

            var picturePath = "";
            var pictureButton = new Button { Text = "Select Picture"};
            pictureButton.Clicked += async (sender, e) =>
            {
                try
                {
                    var result = await FilePicker.PickAsync(new PickOptions
                    {
                        PickerTitle = "Select a file"
                    });
            
                    if (result == null) return;
                    picturePath = result.FullPath;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Error selecting file: {ex.Message}", "OK");
                }
            };
            sl.Children.Add(pictureButton);
            
            sl.Children.Add(CategoriesContent());

            var addButton = new Button { Text = "Create" };
            addButton.Clicked += async (sender, args) =>
            {
                try
                {
                    var title = titleEntry.Text;
                    var description = descriptionEntry.Text;
                    var image = Functions.SerializeImage(picturePath);
                    var categories = Functions.SerializeCategories(_checkedList);
                    var movie = new Movie { Title = title, Description = description, Address = _address, Categories = categories, Picture = image};
                    _dbContext.Create(movie);
                }
                catch (Exception e)
                {
                    await DisplayAlert("as", e.Message, "s");
                }
            };
            sl.Children.Add(addButton);
            
            Content = new ScrollView { Content = sl};
        }
        
        private StackLayout CategoriesContent()
        {
            var sl = new StackLayout { Spacing = 5 };
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
            return sl;
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
        
        // selectFileButton.Clicked += async (sender, e) =>
        // {
        //     try
        //     {
        //         var result = await FilePicker.PickAsync(new PickOptions
        //         {
        //             PickerTitle = "Select a file"
        //         });
        //
        //         if (result == null) return;
        //         var selectedFilePath = result.FullPath;
        //         var items = Reader(selectedFilePath);
        //         GoToNextPage(items);
        //     }
        //     catch (Exception ex)
        //     {
        //         await DisplayAlert("Error", $"Error selecting file: {ex.Message}", "OK");
        //     }
        // };

    }