using SQLite;

namespace AsapMovie.Methods_and_Models ;

    public class DbContext
    {
        private const string DbName = "Database.db3";
        private readonly SQLiteAsyncConnection _connection;
        
        public DbContext()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(@"D:\Movies and Series\Movies", DbName));
            _connection.CreateTableAsync<Movie>();
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _connection.Table<Movie>().ToListAsync();
        }

        public async Task<Movie> GetById(int id)
        {
            return await _connection.Table<Movie>().Where(x => x.id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Movie user)
        {
            await _connection.InsertAsync(user);
        }
        
        public async Task Update(Movie user)
        {
            await _connection.UpdateAsync(user);
        }

        public async Task Delete(Movie user)
        {
            await _connection.DeleteAsync(user);
        }
    }