using Dapper;
using DapperPractice.Connection;
using DapperPractice.Exeptions;
using DapperPractice.Utilities;
using DTOS;
using Entities;
using System.Data;
using System.Data.SqlClient;

namespace DapperPractice.Repositories.MoviesRepository
{
    public class MoviesRepository : IMovieRepository
    {
        private readonly ISqlConnectionFactory _connection;
        private readonly HttpClient _client;

        public MoviesRepository(ISqlConnectionFactory connection,HttpClient client)
        {
            _connection = connection;
            _client = client;
        }

        public async Task<object> CountMovies()
        {
            await using SqlConnection sqlConnection = _connection.CreateConnection();

            //ExecuteScalar (is often use to return one row and one column)
            int count = await sqlConnection.ExecuteScalarAsync<int>(@"SELECT COUNT(*) [TOTAL TABLES] FROM MOVIES");
            //ExecuteScalar(sql,new {options...})

            if (count <= 0)
            {
                throw new ExeceptionNotItems("Not Items was found");
            }

            return new { TotalCount = count };
        }

        public async Task DeleteMovie(int id)
        {
            await using SqlConnection sqlConnection= _connection.CreateConnection();

            await sqlConnection.ExecuteAsync(@"DELETE FROM Movies Where Id = @Id", new {Id = id });
        }

        public async Task<IEnumerable<Movies>> GetAllMovies()
        {
            //creating the connection with the DB
            await using SqlConnection sqlConnection = _connection.CreateConnection();

            //Using Dapper to execute a query (QueryAsync return an IEnumerable)
            IEnumerable<Movies> movies = await sqlConnection.QueryAsync<Movies>(@"SELECT * FROM Movies");

            if (movies.Count() <= 0)
            {
                throw new ExeceptionNotItems($"Not items was found in the DB");
            }

            return movies;
        }

        public async Task<object?> GetMovieByIdDefault(int id)
        {
            await using SqlConnection sqlConnection = _connection.CreateConnection();

            //QueryFirstOrDefaultAsync (is the same as QueryFirst but dont throw an exception)
            Movies? movies = await sqlConnection.QueryFirstOrDefaultAsync<Movies>(@"SELECT * FROM Movies M Where M.Id = @Id", new {Id = id });

            if (movies is null)
            {
                //Custom Exeception
                throw new ExeceptionNotItems($"Not Items was found with the Id of {id}");
            }

            return movies;
        }

        public async Task<object> GetMovieByIdExeception(int id)
        {
            //Always have to use the sqlConnection 
            await using SqlConnection sqlConnection = _connection.CreateConnection();

            //QueryFirstAsync(return just one the first value in this case a Movie and it throw and exception if dont find anything)
            Movies movies = await sqlConnection.QueryFirstAsync<Movies>(@"SELECT * FROM Movies M  WHERE M.Id = @Id", new { Id = id });
            //QueryFirstAsync<TValue>(sql,new {options...})

            return new
            {
                movies.Id,
                movies.Name,
                IsLive = movies.isLive ? "Online" : "Offline",
                movies.ReleaseDate,
            };
        }

        public async Task InsertMovie(MovieDTO movieDTO)
        {
            await using SqlConnection sqlConnection = _connection.CreateConnection();

            //ExecuteAsync (is used to Update,Delete and Add)
            await sqlConnection
                .ExecuteAsync(@"INSERT INTO Movies (Name,IsLive,ReleaseDate) VALUES (@Name,@IsLive,@ReleaseDate) ", 
                 new { movieDTO.Name,movieDTO.isLive,movieDTO.ReleaseDate});
            //ExecuteAsync(sql,new{options..})
        }

        public async Task<object> GetMovieAndMovieGenders(int id)
        {
            string sql = @"
            SELECT * FROM Movies WHERE Id = @Id
            SELECT * FROM MovieGenders WHERE Id = @Id
            ";
            await using SqlConnection sqlConnection = _connection.CreateConnection();

            using var multi = await sqlConnection.QueryMultipleAsync(sql, new { Id = id });

            Movies movies = await multi.ReadFirstOrDefaultAsync<Movies>();

            MovieGenders movieGenders = await multi.ReadFirstOrDefaultAsync<MovieGenders>();    

            return new { Resullt = new {movies,movieGenders} };
        }

        public async Task UpdateMovie(MovieDTO movieDTO, int id)
        {
            await using SqlConnection sqlConnection = _connection.CreateConnection();

            //Updating the MovieName
            await sqlConnection
                .ExecuteAsync(@"UPDATE Movies SET Name = @Name,IsLive = @IsLive,ReleaseDate = @ReleaseDate WHERE Id = @Id ", new { Id = id, movieDTO.Name,movieDTO.isLive,movieDTO.ReleaseDate});
        }

        public async Task<Movies> GetSpecifyMovie()
        {
            string sql = @"SELECT Name,ReleaseDate FROM Movies WHERE IsLive = 0";

            await using SqlConnection sqlConnection = _connection.CreateConnection();

            Movies movies = await sqlConnection.QueryFirstAsync<Movies>(sql);

            return movies;
        }

        public async Task<Movies?> ExecuteStoredProcedure(int id)
        {
            await using SqlConnection sqlConnection = _connection.CreateConnection();

            Movies? movies = await sqlConnection.QueryFirstOrDefaultAsync<Movies>("GetMovieById", new { id }, commandType:CommandType.StoredProcedure);

            if (movies is null)
            {
                throw new ExeceptionNotItems($"Not Movie was found with id of {id}");
            }

            return movies;
        }
        public async Task<IEnumerable<object>> GetManyToMany()
        {
            string sql = @"SELECT M.Id,M.Name,M.IsLive,M.ReleaseDate,C.Price,C.CinemaType,C.Id
                           FROM Movies M
                           INNER JOIN CinemaMovies cm ON cm.MovieId = M.Id
                           INNER JOIN Cinemas C On C.Id = cm.CinemaId";

            await using SqlConnection sqlConnection = _connection.CreateConnection();

            var movies = await sqlConnection.QueryAsync<Movies, Cinema, Movies>(sql, (movies,cinemas) => 
            {
                movies.Cinemas.Add(cinemas);
                return movies;
            },splitOn:"Id,Id");

            return movies.Select(prop => new {Movies = prop.ToMovieDto(),Cinema = prop.Cinemas.Select(x => x.ToCinemaDto()) });
        }

        public async Task<string> GetByUrl(string url)
        {
            //Making the request for the desired url
            var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
