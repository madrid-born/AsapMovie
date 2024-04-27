using System.Text.Json;
using System.Text.RegularExpressions;
using System;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace AsapMovie.Methods_and_Models ;

    public static class Functions
    {
        public static List<string> GetCategories()
        {
            var jsonString = Preferences.Get("Categories", "");
            var categories = jsonString is "" ? new List<string>() : JsonSerializer.Deserialize<List<string>>(jsonString);
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
        
        public static void RemoveAllCategories()
        {
            Preferences.Set("Categories", "");
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
            input = input.Replace(".", " ");
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

        public static string SerializeCategories(List<string> categories )
        {
            return JsonSerializer.Serialize(categories);
        }

        public static byte[] SerializeAndResizeImage(string pictureFilePath)
        {
            using var image = Image.Load(pictureFilePath);
            image.Mutate(x => x.Resize(200, 300));
            using var memoryStream = new MemoryStream();
            image.SaveAsJpeg(memoryStream);
            return memoryStream.ToArray();
        }
    }