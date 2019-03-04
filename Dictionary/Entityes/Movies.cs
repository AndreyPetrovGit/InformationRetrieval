using System;
using System.Collections.Generic;

namespace Dictionary
{
    public partial class Movies
    {
        public Movies()
        {
            MoviesActors = new HashSet<MoviesActors>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<MoviesActors> MoviesActors { get; set; }
    }
}
