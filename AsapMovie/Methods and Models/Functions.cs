using System.Text.Json;
using System.Text.RegularExpressions;

namespace AsapMovie.Methods_and_Models ;

    public static class Functions
    {
        public static List<string> GetCategories()
        {
            var jsonString = Preferences.Get("Categories", "");
            var categories = jsonString is "" ? new List<string>() : JsonSerializer.Deserialize<List<string>>(jsonString);
            categories.Sort();
            return categories;
        }
        
        public static void SetCategory(string newCategory)
        {
            var categoriesList = GetCategories();
            if (categoriesList.Contains(newCategory)) return;
            categoriesList.Add(newCategory);
            var jsonString = JsonSerializer.Serialize(categoriesList);
            Preferences.Set("Categories", jsonString);
        }
        
        public static void RemoveCategory(string category)
        {
            var categoriesList = GetCategories();
            if (!categoriesList.Contains(category)) return;
            categoriesList.Remove(category);
            var jsonString = JsonSerializer.Serialize(categoriesList);
            Preferences.Set("Categories", jsonString);
        }
        
        
        public static string MovieTitle(string input)
        {
            var lastSlashIndex = input.LastIndexOf('\\');
            return lastSlashIndex >= 0 ? input.Substring(lastSlashIndex + 1) : input;
        }
        
        public static string ExtractTitle(string input)
        {
            string pattern = @"(.+?)\.(\d{4})";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                string title = match.Groups[1].Value.Trim() + " " + match.Groups[2].Value;
                return title;
            }
            return input;
            
        }

        public static List<string> AllMovies()
        {
            var allFiles = new List<string>();
            var directoryPath = @"D:\Movies and Series\Movies\Plan To Watch";
            try
            {
                string[] directories = Directory.GetDirectories(directoryPath);
                string[] files = Directory.GetFiles(directoryPath);
                allFiles.AddRange(directories);
                allFiles.AddRange(files);
            }
            catch (Exception ex)
            {
                
            }
            return allFiles;
        } 
        
    }
    
           // private void OpenFolder(string filePath)
        // {
        //     try
        //     {
        //         Process.Start(new ProcessStartInfo
        //         {
        //             FileName = "explorer.exe",
        //             Arguments = $"/select,\"{filePath}\""
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         // Handle error
        //         Console.WriteLine($"Error opening file location: {ex.Message}");
        //     }
        // }
    
    
    // public partial class newPage : ContentPage
    // {
    //     public newPage()
    //     {
    //         InitializeComponent();
    //
    //         var sl = new StackLayout{Margin = 20 , Spacing = 5};
    //
    //         foreach (var movie in Functions.AllMovies())
    //         {
    //             var explorerButton = new Button { Text = "Open" };
    //             explorerButton.Clicked += (sender, args) =>
    //             {
    //                 OpenFolder(movie);
    //             };
    //             
    //             var label = new Label { TextColor = Colors.Black ,Text = Functions.MovieTitle(movie) };
    //             
    //             var copyButton = new Button { Text = "Copy" };
    //             copyButton.Clicked += async (sender, args) =>
    //             {
    //                 await Clipboard.SetTextAsync(Functions.ExtractTitle(Functions.MovieTitle(movie)));
    //             };
    //
    //             var detailEntry = new Entry { Placeholder = "description" };
    //
    //             var submitButton = new Button { Text = "Submit"};
    //             submitButton.Clicked += async (sender, args) =>
    //             {
    //                 try
    //                 {
    //                     AddDescription(Functions.MovieTitle(movie), detailEntry.Text);
    //                     await DisplayAlert("Congrats", "Added", "Done");
    //                 }
    //                 catch (Exception e)
    //                 {
    //                     await DisplayAlert("Congrats", e.Message, "Done");
    //                 }
    //             };
    //
    //             var hsl = new HorizontalStackLayout { HorizontalOptions = LayoutOptions.Center, Children = { explorerButton, copyButton, submitButton } };
    //             var vsl = new VerticalStackLayout  { BackgroundColor = Colors.Aqua ,Children = {label, detailEntry, hsl }};
    //             sl.Children.Add(vsl);
    //         }
    //         Content = new ScrollView { Content = sl};
    //     }
    //
    //     public void AddDescription(string title, string description)
    //     {
    //         string directoryPath = @"D:\Movies and Series\Movies\Project\Details";
    //         string filePath = Path.Combine(directoryPath, $"{title}.txt");
    //
    //         File.WriteAllText(filePath, description);
    //     }
    //     
    //     private void OpenFolder(string filePath)
    //     {
    //         try
    //         {
    //             Process.Start(new ProcessStartInfo
    //             {
    //                 FileName = "explorer.exe",
    //                 Arguments = $"/select,\"{filePath}\""
    //             });
    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine($"Error opening file location: {ex.Message}");
    //         }
    //     }
    // }