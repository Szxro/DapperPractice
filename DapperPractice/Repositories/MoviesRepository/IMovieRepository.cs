using DTOS;
using Entities;

namespace DapperPractice.Repositories.MoviesRepository
{
    public interface IMovieRepository
    {
        //QueryAsync<TValue> (return an IEnumerable with the desired data  )
        Task<IEnumerable<Movies>> GetAllMovies();

        //QueryFirstAsync(is often use when you is only a row and column)
        Task<object> GetMovieByIdExeception(int id);

        //QueryFirstorDefaultAsync()
        Task<object> GetMovieByIdDefault(int id);

        //ExecuteAsync(is often use when you only going to execute a query dont return nothing)
        Task InsertMovie(MovieDTO movieDTO);

        Task UpdateMovie(MovieDTO movieDTO,int id);

        Task DeleteMovie(int id);

        //ExecuteScalarASync
        Task<object> CountMovies();

        //QueryMultiple(execute multiple Queries)
        Task<object> GetMovieAndMovieGenders(int id);

        //Query(is often use to query specific columns)
        Task<Movies> GetSpecifyMovie(); 
    }
}
