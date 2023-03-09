using DTOS;
using Entities;
using System.Security.Cryptography.X509Certificates;

namespace DapperPractice.Utilities
{
    public static class MapTo
    {
        public static MovieDTO ToMovieDto(this Movies movies)
        {
            return new MovieDTO
            {
                Name = movies.Name,
                ReleaseDate = movies.ReleaseDate,
                isLive = movies.isLive
            };
        }


        public static CinemaDTO ToCinemaDto(this Cinema cinema) 
        {
            return new CinemaDTO
            {
                CinemaType= cinema.CinemaType,
                Price= cinema.Price,
            };
        }
    }
}
