using System.Text.Json;
using System.IO;

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
        
        [Column("Picture")]
        public byte[] Picture { get; set; }
        

        // private ImageSource LoadImage()
        // {
        //     return ImageSource.FromStream(() => new MemoryStream(Picture));
        // }
    }