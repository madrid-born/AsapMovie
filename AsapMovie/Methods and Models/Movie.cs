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
        // [Column("pic")]
        // public string pic { get; set; }
    }