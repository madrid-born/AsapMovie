using System.Text.Json;
using SQLite;

namespace AsapMovie.Methods_and_Models ;

    public class Movie
    {
        [PrimaryKey, AutoIncrement]
        [Column("id")]
        public int id { get; set; }
        
        [Column("Title")]
        public string Title { get; set; }
        
        [Column("Address")]
        public string Address { get; set; }
        
        [Column("Description")]
        public string Description { get; set; }

        [Column("Categories")]
        public string Categories { get; set; }
        
        // [Column("pic")]
        // public string pic { get; set; }

        // public Movie(string title, string address, string description, List<string> categories)
        // {
        //     Title = title;
        //     Address = address;
        //     Description = description;
        //     Categories = JsonSerializer.Serialize(categories);
        // }
        //
        // public void AddCategory(string category)
        // {
        //     var categories = string.IsNullOrEmpty(Categories) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(Categories);
        //     categories.Add(category);
        //     Categories = JsonSerializer.Serialize(categories);
        // }
        
        
    }