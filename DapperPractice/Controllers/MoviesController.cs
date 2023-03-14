using DapperPractice.Repositories.MoviesRepository;
using DTOS;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperPractice.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _repository;

        public MoviesController(IMovieRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("get/TotalMovies")]

        public async Task<ActionResult<object>> TotalMovies()
        {
            return Ok(await _repository.CountMovies());
        }

        [HttpGet("get/AllMovies")]

        public async Task<ActionResult<IEnumerable<Movies>>> GetAllMovies()
        {
            return Ok(await _repository.GetAllMovies());
        }

        [HttpGet("get/SpecifyMovie")]

        public async Task<ActionResult<Movies>> GetSpecifyMovie()
        {
            return Ok(await _repository.GetSpecifyMovie());
        }

        [HttpGet("get/MovieByIdExeception/{id:int}")]

        public async Task<ActionResult<object>> GetMovieByIdExeception(int id)
        {
            return Ok(await _repository.GetMovieByIdExeception(id));
        }

        [HttpGet("get/MoviesByIdDefault/{id:int}")]

        public async Task<ActionResult<object>> GetMovieByIdDefault(int id)
        {
            return Ok(await _repository.GetMovieByIdDefault(id));
        }

        [HttpGet("get/Movies-MovieGenders/{id:int}")]

        public async Task<ActionResult<object>> GetMovieAndMovieGenders(int id)
        {
            return Ok(await _repository.GetMovieAndMovieGenders(id));
        }

        [HttpGet("get/ExecutedMovieProcedure/{id:int}")]

        public async Task<ActionResult<Movies?>> ExecuteMovieProcedure(int id)
        {
            return Ok(await _repository.ExecuteStoredProcedure(id));
        }

        [HttpGet("get/ManyToMany")]

        public async Task<ActionResult<IEnumerable<object>>> GetManyToMany()
        {
            return Ok(await _repository.GetManyToMany());
        }

        [HttpGet("get/getByUrl")]

        public async Task<ActionResult<string>> GetByUrl(string url)
        {
            return Ok(await _repository.GetByUrl(url));
        }

        [HttpPost("post/Movie")]

        public async Task<ActionResult> InsertMovie(MovieDTO movieDTO)
        {
            await _repository.InsertMovie(movieDTO);

            return Ok();
        }

        [HttpPut("update/Movie/{id:int}")]

        public async Task<ActionResult> UpdateMovie(MovieDTO movieDTO,int id)
        {
            await _repository.UpdateMovie(movieDTO,id);
            return Ok();
        }

        [HttpDelete("delete/Movies/{id:int}")]

        public async Task<ActionResult> DeleteMovie(int id)
        {
            await _repository.DeleteMovie(id);
            return Ok();
        }
    }
}
