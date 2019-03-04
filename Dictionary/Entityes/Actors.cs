using System;
using System.Collections.Generic;

namespace Dictionary
{
    public partial class Actors
    {
        public Actors()
        {
            MoviesActors = new HashSet<MoviesActors>();
        }

        public int ActorId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MoviesActors> MoviesActors { get; set; }
    }
}
