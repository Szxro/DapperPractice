using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Cinema
    {
        public int Id { get; set; }

        public int CinemaType { get; set; }

        public double Price { get; set; }

        public int MovieTheaterId { get; set; }

        public HashSet<Movies> Movies { get; set; } = new();
    }
}
