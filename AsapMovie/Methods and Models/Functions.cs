using System.Text.Json;

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
        
        
    }