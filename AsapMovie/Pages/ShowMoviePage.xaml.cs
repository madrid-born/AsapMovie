using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsapMovie.Methods_and_Models;

namespace AsapMovie.Pages ;

    public partial class ShowMoviePage : ContentPage
    {
        private string _path;

        public ShowMoviePage(Movie movie)
        {
            InitializeComponent();
            // _path = path;
        }
        
        private async void OpenVideoButton_Clicked(object sender, EventArgs e)
        {
            string videoPath = _path;
            await OpenVideoFile(videoPath);
        }

        private async Task OpenVideoFile(string filePath)
        {
            try
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
            catch (Exception ex)
            {
                // Handle error
                Console.WriteLine($"Error opening file: {ex.Message}");
            }
        }
        
        private void OpenFolderButton_Clicked(object sender, EventArgs e)
        {
            string filePath = _path;
            // OpenFolder(filePath);
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
    }
    
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