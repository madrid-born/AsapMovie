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

    }
    